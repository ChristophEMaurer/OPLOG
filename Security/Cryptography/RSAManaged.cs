//
// RSAManaged.cs - Implements the RSA algorithm.
//
// Authors:
//	Sebastien Pouliot (sebastien@ximian.com)
//	Ben Maurer (bmaurer@users.sf.net)
//
// (C) 2002, 2003 Motus Technologies Inc. (http://www.motus.com)
// Portions (C) 2003 Ben Maurer
// Copyright (C) 2004,2006 Novell, Inc (http://www.novell.com)
//
// Key generation translated from Bouncy Castle JCE (http://www.bouncycastle.org/)
// See bouncycastle.txt for license.
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

using System;
using System.Security.Cryptography;
using System.Text;

using Security;

// Big chunks of code are coming from the original RSACryptoServiceProvider class.
// The class was refactored to :
// a.	ease integration of new hash algorithm (like MD2, RIPEMD160, ...);
// b.	provide better support for the coming SSL implementation (requires 
//	EncryptValue/DecryptValue) with, or without, Mono runtime/corlib;
// c.	provide an alternative RSA implementation for all Windows (like using 
//	OAEP without Windows XP).

namespace Security.Cryptography
{

    public sealed class RSAManaged : RSA
    {
        /*
         http://www.w3.org/TR/xmldsig-core/#sec-SignatureValue
          
         6.4.2 PKCS1 (RSA-SHA1)

        Identifier:
            http://www.w3.org/2000/09/xmldsig#rsa-sha1

        The expression "RSA algorithm" as used in this specification refers to the RSASSA-PKCS1-v1_5 
         * algorithm described in RFC 2437 [PKCS1]. The RSA algorithm takes no explicit parameters. 
         * An example of an RSA SignatureMethod element is:

           <SignatureMethod Algorithm="http://www.w3.org/2000/09/xmldsig#rsa-sha1"/>

        The SignatureValue content for an RSA signature is the base64 [MIME] encoding of the octet 
         * string computed as per RFC 2437 [PKCS1, section 8.1.1: Signature generation for the 
         * RSASSA-PKCS1-v1_5 signature scheme]. As specified in the EMSA-PKCS1-V1_5-ENCODE function 
         * RFC 2437 [PKCS1, section 9.2.1], the value input to the signature function MUST contain 
         * a pre-pended algorithm object identifier for the hash function, but the availability of 
         * an ASN.1 parser and recognition of OIDs is not required of a signature verifier. 
         * The PKCS#1 v1.5 representation appears as:

           CRYPT (PAD (ASN.1 (OID, DIGEST (data))))

        Note that the padded ASN.1 will be of the following form:

           01 | FF* | 00 | prefix | hash

        where "|" is concatenation, "01", "FF", and "00" are fixed octets of the corresponding 
         * hexadecimal value, "hash" is the SHA1 digest of the data, and "prefix" is the ASN.1 BER SHA1 
         * algorithm designator prefix required in PKCS1 [RFC 2437], that is,

           hex 30 21 30 09 06 05 2B 0E 03 02 1A 05 00 04 14

        This prefix is included to make it easier to use standard cryptographic libraries. 
         * The FF octet MUST be repeated the maximum number of times such that the value of 
         * the quantity being CRYPTed is one octet shorter than the RSA modulus.

        The resulting base64 [MIME] string is the value of the child text node of the SignatureValue element, e.g.

        <SignatureValue>
        IWijxQjUrcXBYoCei4QxjWo9Kg8D3p9tlWoT4t0/gyTE96639In0FZFY2/rvP+/bMJ01EArmKZsR5VW3rwoPxw=
        </SignatureValue>

 
         */

        public enum HASH_ALGORITHM { RSA_RAW = 0, RSA_SHA1 = 1, RSA_MD2 = 2, RSA_MD4 = 4, RSA_MD5 = 5 };
        const int RSA_SIGN = 0x01;
        const int RSA_CRYPT = 0x02;

        static byte[] ASN1_HASH_MDX =
        { 0x30, 0x20, 0x30, 0x0C, 0x06, 0x08, 0x2A, 0x86, 0x48,
          0x86, 0xF7, 0x0D, 0x02, 0x00, 0x05, 0x00, 0x04, 0x10 };
        static byte[] ASN1_HASH_SHA1 =
        { 0x30, 0x21, 0x30, 0x09, 0x06, 0x05, 0x2B, 0x0E, 0x03,
          0x02, 0x1A, 0x05, 0x00, 0x04, 0x14 };

        private int keylen;             // in bytes

        private const int defaultKeySize = 4096;

        private bool isCRTpossible = false;
        private bool keyBlinding = true;
        private bool keypairGenerated = false;
        private bool m_disposed = false;

        private BigInteger d;
        private BigInteger p;
        private BigInteger q;
        private BigInteger dp;
        private BigInteger dq;
        private BigInteger qInv;
        private BigInteger n;		// modulus
        private BigInteger e;

        public RSAManaged()
            : this(defaultKeySize, false)
        {
        }

        public RSAManaged(int keySize)
            : this(keySize, false)
        {
        }

        private RSAManaged(int keySize, bool dummy)
        {
            LegalKeySizesValue = new KeySizes[1];
            LegalKeySizesValue[0] = new KeySizes(384, 16384, 8);

            CreateKeySizes(keySize);
        }
         
        ~RSAManaged()
        {
            // Zeroize private key
            Dispose(false);
        }

        private void CreateKeySizes(int keySize)
        {
            base.KeySize = keySize;

            this.keylen = keySize / 8;
            if (this.keylen * 8 != keySize)
                throw new ArgumentException("keySize must be a multiple of 8");
        }

        private void EnsureKeylen(int keySize)
        {
            if (keylen * 8 != keySize)
            {
                CreateKeySizes(keySize);
            }
        }

        public void GenerateKeyPair()
        {
            // p and q values should have a length of half the strength in bits
            int pbitlength = ((KeySize + 1) >> 1);
            int qbitlength = (KeySize - pbitlength);
            const uint uint_e = 17;
            e = uint_e; // fixed

            // generate p, prime and (p-1) relatively prime to e
            for (; ; )
            {
                p = BigInteger.GeneratePseudoPrime(pbitlength);
                if (p % uint_e != 1)
                    break;
            }
            // generate a modulus of the required length
            for (; ; )
            {
                // generate q, prime and (q-1) relatively prime to e,
                // and not equal to p
                for (; ; )
                {
                    q = BigInteger.GeneratePseudoPrime(qbitlength);
                    if ((q % uint_e != 1) && (p != q))
                        break;
                }

                // calculate the modulus
                n = p * q;
                if (n.bitCount() == KeySize)
                    break;

                // if we get here our primes aren't big enough, make the largest
                // of the two p and try again
                if (p < q)
                    p = q;
            }

            BigInteger pSub1 = (p - 1);
            BigInteger qSub1 = (q - 1);
            BigInteger phi = pSub1 * qSub1;

            // calculate the private exponent
            d = e.modInverse(phi);

            // calculate the CRT factors
            dp = d % pSub1;
            dq = d % qSub1;
            qInv = q.modInverse(p);

            keypairGenerated = true;
            isCRTpossible = true;

            if (KeyGenerated != null)
                KeyGenerated(this, null);
        }

        // overrides from RSA class

        public override int KeySize
        {
            get
            {
                // in case keypair hasn't been (yet) generated
                if (keypairGenerated)
                {
                    int ks = n.bitCount();
                    if ((ks & 7) != 0)
                        ks = ks + (8 - (ks & 7));
                    return ks;
                }
                else
                    return base.KeySize;
            }
        }

        public override string KeyExchangeAlgorithm
        {
            get { return "RSA-PKCS1-KeyEx"; }
        }

        // note: when (if) we generate a keypair then it will have both
        // the public and private keys
        public bool PublicOnly
        {
            get { return (keypairGenerated && ((d == null) || (n == null))); }
        }

        public override string SignatureAlgorithm
        {
            get { return "http://www.w3.org/2000/09/xmldsig#rsa-sha1"; }
        }

        public override byte[] DecryptValue(byte[] rgb)
        {
            if (m_disposed)
                throw new ObjectDisposedException("private key");

            // decrypt operation is used for signature
            if (!keypairGenerated)
                GenerateKeyPair();

            BigInteger input = new BigInteger(rgb);
            BigInteger r = null;

            // we use key blinding (by default) against timing attacks
            if (keyBlinding)
            {
                // x = (r^e * g) mod n 
                // *new* random number (so it's timing is also random)
                r = BigInteger.GenerateRandom(n.bitCount());
                input = r.modPow(e, n) * input % n;
            }

            BigInteger output;
            // decrypt (which uses the private key) can be 
            // optimized by using CRT (Chinese Remainder Theorem)
            if (isCRTpossible)
            {
                // m1 = c^dp mod p
                BigInteger m1 = input.modPow(dp, p);
                // m2 = c^dq mod q
                BigInteger m2 = input.modPow(dq, q);
                BigInteger h;
                if (m2 > m1)
                {
                    // thanks to benm!
                    h = p - ((m2 - m1) * qInv % p);
                    output = m2 + q * h;
                }
                else
                {
                    // h = (m1 - m2) * qInv mod p
                    h = (m1 - m2) * qInv % p;
                    // m = m2 + q * h;
                    output = m2 + q * h;
                }
            }
            else if (!PublicOnly)
            {
                // m = c^d mod n
                output = input.modPow(d, n);
            }
            else
            {
                throw new CryptographicException("Missing private key to decrypt value.");
            }

            if (keyBlinding)
            {
                // Complete blinding
                // x^e / r mod n
                output = output * r.modInverse(n) % n;
                r.Clear();
            }

            // it's sometimes possible for the results to be a byte short
            // and this can break some software (see #79502) so we 0x00 pad the result
            byte[] result = GetPaddedValue(output);
            // zeroize values
            input.Clear();
            output.Clear();
            return result;
        }

        public override byte[] EncryptValue(byte[] rgb)
        {
            if (m_disposed)
                throw new ObjectDisposedException("public key");

            if (!keypairGenerated)
                GenerateKeyPair();

            BigInteger input = new BigInteger(rgb);
            BigInteger output = input.modPow(e, n);
            // it's sometimes possible for the results to be a byte short
            // and this can break some software (see #79502) so we 0x00 pad the result
            byte[] result = GetPaddedValue(output);
            // zeroize value
            input.Clear();
            output.Clear();
            return result;
        }

        public override RSAParameters ExportParameters(bool includePrivateParameters)
        {
            if (m_disposed)
                throw new ObjectDisposedException("");

            if (!keypairGenerated)
                GenerateKeyPair();

            RSAParameters param = new RSAParameters();
            param.Exponent = e.GetBytes();
            param.Modulus = n.GetBytes();
            if (includePrivateParameters)
            {
                // some parameters are required for exporting the private key
                if (d == null)
                    throw new CryptographicException("Missing private key");
                param.D = d.GetBytes();
                // hack for bugzilla #57941 where D wasn't provided
                if (param.D.Length != param.Modulus.Length)
                {
                    byte[] normalizedD = new byte[param.Modulus.Length];
                    Buffer.BlockCopy(param.D, 0, normalizedD, (normalizedD.Length - param.D.Length), param.D.Length);
                    param.D = normalizedD;
                }
                // but CRT parameters are optionals
                if ((p != null) && (q != null) && (dp != null) && (dq != null) && (qInv != null))
                {
                    // and we include them only if we have them all
                    param.P = p.GetBytes();
                    param.Q = q.GetBytes();
                    param.DP = dp.GetBytes();
                    param.DQ = dq.GetBytes();
                    param.InverseQ = qInv.GetBytes();
                }
            }
            return param;
        }

        public override void ImportParameters(RSAParameters parameters)
        {
            if (m_disposed)
                throw new ObjectDisposedException("");

            // if missing "mandatory" parameters
            if (parameters.Exponent == null)
                throw new CryptographicException("Missing Exponent");
            if (parameters.Modulus == null)
                throw new CryptographicException("Missing Modulus");

            e = new BigInteger(parameters.Exponent);
            n = new BigInteger(parameters.Modulus);
            // only if the private key is present
            if (parameters.D != null)
                d = new BigInteger(parameters.D);
            if (parameters.DP != null)
                dp = new BigInteger(parameters.DP);
            if (parameters.DQ != null)
                dq = new BigInteger(parameters.DQ);
            if (parameters.InverseQ != null)
                qInv = new BigInteger(parameters.InverseQ);
            if (parameters.P != null)
                p = new BigInteger(parameters.P);
            if (parameters.Q != null)
                q = new BigInteger(parameters.Q);

            // we now have a keypair
            keypairGenerated = true;
            isCRTpossible = ((p != null) && (q != null) && (dp != null) && (dq != null) && (qInv != null));
        }

        protected override void Dispose(bool disposing)
        {
            if (!m_disposed)
            {
                // Always zeroize private key
                if (d != null)
                {
                    d.Clear();
                    d = null;
                }
                if (p != null)
                {
                    p.Clear();
                    p = null;
                }
                if (q != null)
                {
                    q.Clear();
                    q = null;
                }
                if (dp != null)
                {
                    dp.Clear();
                    dp = null;
                }
                if (dq != null)
                {
                    dq.Clear();
                    dq = null;
                }
                if (qInv != null)
                {
                    qInv.Clear();
                    qInv = null;
                }

                if (disposing)
                {
                    // clear public key
                    if (e != null)
                    {
                        e.Clear();
                        e = null;
                    }
                    if (n != null)
                    {
                        n.Clear();
                        n = null;
                    }
                }
            }
            // call base class 
            // no need as they all are abstract before us
            m_disposed = true;
        }

        public delegate void KeyGeneratedEventHandler(object sender, EventArgs e);

        public event KeyGeneratedEventHandler KeyGenerated;

        public override string ToXmlString(bool includePrivateParameters)
        {
            StringBuilder sb = new StringBuilder();
            RSAParameters rsaParams = ExportParameters(includePrivateParameters);
            try
            {
                sb.Append("<RSAKeyValue>");

                sb.Append("<Modulus>");
                sb.Append(Convert.ToBase64String(rsaParams.Modulus));
                sb.Append("</Modulus>");

                sb.Append("<Exponent>");
                sb.Append(Convert.ToBase64String(rsaParams.Exponent));
                sb.Append("</Exponent>");

                if (includePrivateParameters)
                {
                    if (rsaParams.P != null)
                    {
                        sb.Append("<P>");
                        sb.Append(Convert.ToBase64String(rsaParams.P));
                        sb.Append("</P>");
                    }
                    if (rsaParams.Q != null)
                    {
                        sb.Append("<Q>");
                        sb.Append(Convert.ToBase64String(rsaParams.Q));
                        sb.Append("</Q>");
                    }
                    if (rsaParams.DP != null)
                    {
                        sb.Append("<DP>");
                        sb.Append(Convert.ToBase64String(rsaParams.DP));
                        sb.Append("</DP>");
                    }
                    if (rsaParams.DQ != null)
                    {
                        sb.Append("<DQ>");
                        sb.Append(Convert.ToBase64String(rsaParams.DQ));
                        sb.Append("</DQ>");
                    }
                    if (rsaParams.InverseQ != null)
                    {
                        sb.Append("<InverseQ>");
                        sb.Append(Convert.ToBase64String(rsaParams.InverseQ));
                        sb.Append("</InverseQ>");
                    }
                    sb.Append("<D>");
                    sb.Append(Convert.ToBase64String(rsaParams.D));
                    sb.Append("</D>");
                }

                sb.Append("</RSAKeyValue>");
            }
            catch
            {
                if (rsaParams.P != null)
                    Array.Clear(rsaParams.P, 0, rsaParams.P.Length);
                if (rsaParams.Q != null)
                    Array.Clear(rsaParams.Q, 0, rsaParams.Q.Length);
                if (rsaParams.DP != null)
                    Array.Clear(rsaParams.DP, 0, rsaParams.DP.Length);
                if (rsaParams.DQ != null)
                    Array.Clear(rsaParams.DQ, 0, rsaParams.DQ.Length);
                if (rsaParams.InverseQ != null)
                    Array.Clear(rsaParams.InverseQ, 0, rsaParams.InverseQ.Length);
                if (rsaParams.D != null)
                    Array.Clear(rsaParams.D, 0, rsaParams.D.Length);
                throw;
            }

            return sb.ToString();
        }

        // internal for Mono 1.0.x in order to preserve public contract
        // they are public for Mono 1.1.x (for 1.2) as the API isn't froze ATM

#if NET_2_0
		public
#else
        internal
#endif
 bool UseKeyBlinding
        {
            get { return keyBlinding; }
            // you REALLY shoudn't touch this (true is fine ;-)
            set { keyBlinding = value; }
        }

#if NET_2_0
		public
#else
        internal
#endif
 bool IsCrtPossible
        {
            // either the key pair isn't generated (and will be 
            // generated with CRT parameters) or CRT is (or isn't)
            // possible (in case the key was imported)
            get { return (!keypairGenerated || isCRTpossible); }
        }

        private byte[] GetPaddedValue(BigInteger value)
        {
            byte[] result = value.GetBytes();
            int length = (KeySize >> 3);
            if (result.Length >= length)
                return result;

            // left-pad 0x00 value on the result (same integer, correct length)
            byte[] padded = new byte[length];
            Buffer.BlockCopy(result, 0, padded, (length - result.Length), result.Length);
            // temporary result may contain decrypted (plaintext) data, clear it
            Array.Clear(result, 0, result.Length);
            return padded;
        }

        // Check if our private key is valid
        public bool CheckPrivateKey()
        {
            BigInteger TN = this.p * this.q;
            if (TN != this.n)
                return false;

            BigInteger P1 = this.p - 1;
            BigInteger Q1 = this.q - 1;
            BigInteger H = P1 * Q1;
            BigInteger GCD = this.e.gcd(H);
            if (GCD != 1)
                return false;

            return true;
        }

        // SignData - plug compatible with RSACryptoServiceProvider.SignData,
        // but only this one override provided
        public byte[] SignData(byte[] data, HashAlgorithm hasher)
        {
            HASH_ALGORITHM ha = map_hash_algorithm(hasher);
            byte[] hash = hasher.ComputeHash(data);
            byte[] signed_hash = SignHash(hash, ha);
            return signed_hash;
        }


        // VerifyData - plug compatible with RSACryptoServiceProvider.VerifyData
        public bool VerifyData(byte[] data, HashAlgorithm hasher, byte[] signature)
        {
            HASH_ALGORITHM ha = map_hash_algorithm(hasher);
            byte[] hash = hasher.ComputeHash(data);
            return VerifyHash(hash, signature, ha);
        }

        // Map a HashAlgorithm object to our HASH_ALGORITHM enumeration
        HASH_ALGORITHM map_hash_algorithm(HashAlgorithm hasher)
        {
            Type t = hasher.GetType();

            if (Object.ReferenceEquals(t, typeof(MD5CryptoServiceProvider)))
                return HASH_ALGORITHM.RSA_MD5;
            if (Object.ReferenceEquals(t, typeof(SHA1CryptoServiceProvider)))
                return HASH_ALGORITHM.RSA_SHA1;
            throw new ArgumentException("unknown HashAlgorithm");
        }


        // SignHash - plug compatible with RSACryptoServiceProvider.SignHash
        public byte[] SignHash(byte[] sign_me, string hash_algorithm_oid)
        {
            HASH_ALGORITHM ha = MapHashAlgorithmOID(hash_algorithm_oid);
            return SignHash(sign_me, ha);
        }


        // Sign a message digest and pack it up into PKCS#1 format
        public byte[] SignHash(byte[] sign_me, HASH_ALGORITHM hash_algorithm)
        {
            int input_len = sign_me.Length;

            EnsureKeylen(this.KeySize);

            int n_pad = 0;
            switch (hash_algorithm)
            {
                case HASH_ALGORITHM.RSA_RAW:
                    n_pad = this.keylen - 3 - input_len;
                    break;

                case HASH_ALGORITHM.RSA_MD2:
                case HASH_ALGORITHM.RSA_MD4:
                case HASH_ALGORITHM.RSA_MD5:
                    if (input_len != 16)
                        throw new ArgumentException("MDx hashes must be 16 bytes long");
                    n_pad = this.keylen - 3 - 34;
                    break;

                case HASH_ALGORITHM.RSA_SHA1:
                    if (input_len != 20)
                        throw new ArgumentException("SHA1 hashes must be 20 bytes long");
                    n_pad = this.keylen - 3 - 35;
                    break;
            }

            if (n_pad < 8)
                throw new ArgumentException("input too long");

            byte[] encrypt_me = new byte[this.keylen];
            encrypt_me[0] = 0;
            encrypt_me[1] = RSA_SIGN;

            for (int i = 0; i < n_pad; ++i)
                encrypt_me[i + 2] = 0xFF;

            encrypt_me[n_pad + 2] = 0;

            switch (hash_algorithm)
            {
                case HASH_ALGORITHM.RSA_RAW:
                    Array.Copy(sign_me, 0, encrypt_me, n_pad + 3, input_len);
                    break;

                case HASH_ALGORITHM.RSA_MD2:
                case HASH_ALGORITHM.RSA_MD4:
                case HASH_ALGORITHM.RSA_MD5:
                    Array.Copy(ASN1_HASH_MDX, 0, encrypt_me, n_pad + 3, 18);
                    encrypt_me[n_pad + 3 + 13] = (byte)hash_algorithm;
                    Array.Copy(sign_me, 0, encrypt_me, n_pad + 3 + 18, input_len);
                    break;

                case HASH_ALGORITHM.RSA_SHA1:
                    Array.Copy(ASN1_HASH_SHA1, 0, encrypt_me, n_pad + 3, 15);
                    Array.Copy(sign_me, 0, encrypt_me, n_pad + 3 + 15, input_len);
                    break;
            }

            return DoPrivate(encrypt_me);
        }


        // VerifyHash - plug compatible with RSACryptoServiceProvider.VerifyHash
        public bool VerifyHash(byte[] hash, string hash_algorithm_oid, byte[] signature)
        {
            HASH_ALGORITHM ha = MapHashAlgorithmOID(hash_algorithm_oid);
            return VerifyHash(hash, signature, ha);
        }


        // Verify a signed PKCS#1 message digest
        public bool VerifyHash(byte[] hash, byte[] signature, HASH_ALGORITHM hash_algorithm)
        {
            int sig_len = signature.Length;
            
            EnsureKeylen(sig_len * 8);

            if (sig_len != this.keylen)
                return false;

            byte[] decrypted = DoPublic(signature);
            if (decrypted[0] != 0 || decrypted[1] != RSA_SIGN)
                return false;

            int decrypted_len = decrypted.Length;           // = keylen

            for (int i = 2; i < decrypted_len - 1; ++i)
            {
                byte b = decrypted[i];
                if (b == 0)                                 // end of padding
                {
                    ++i;
                    int bytes_left = decrypted_len - i;

                    if (bytes_left == 34)                   // MDx
                    {
                        if (decrypted[i + 13] != (byte)hash_algorithm)
                            return false;
                        decrypted[i + 13] = 0;
                        if (!compare_bytes(decrypted, i, ASN1_HASH_MDX, 0, 18))
                            return false;
                        return compare_bytes(decrypted, i + 18, hash, 0, 16);
                    }

                    if (bytes_left == 35 && hash_algorithm == HASH_ALGORITHM.RSA_SHA1)
                    {
                        if (!compare_bytes(decrypted, i, ASN1_HASH_SHA1, 0, 15))
                            return false;
                        return compare_bytes(decrypted, i + 15, hash, 0, 20);
                    }

                    if (bytes_left == hash.Length && hash_algorithm == HASH_ALGORITHM.RSA_RAW)
                        return compare_bytes(decrypted, i, hash, 0, bytes_left);

                    return false;
                }

                if (b != 0xFF)
                    break;
            }

            return false;
        }



        // Map a hash algorithm OID to a HASH_ALGORITHM
        // HASH_ALGORITHM knows about types of hash that CryptoConfig.MapNameToOID doesn't (and vice-versa)
        public HASH_ALGORITHM MapHashAlgorithmOID(string hash_algorithm_oid)
        {
            HASH_ALGORITHM ha;
            if (String.Compare(hash_algorithm_oid, CryptoConfig.MapNameToOID("MD5"), true) == 0)
                ha = HASH_ALGORITHM.RSA_MD5;
            else if (String.Compare(hash_algorithm_oid, CryptoConfig.MapNameToOID("SHA1"), true) == 0)
                ha = HASH_ALGORITHM.RSA_SHA1;
            else
                throw new ArgumentException("unknown hash_algorithm_oid");
            return ha;
        }


        // Perform an RSA public key operation on input
        public byte[] DoPublic(byte[] input)
        {
            if (input.Length != this.keylen)
                throw new ArgumentException("input.Length does not match keylen");

            if (ReferenceEquals(this.n, null))
                throw new ArgumentException("no key set!");

            BigInteger T = new BigInteger(input);
            if (T >= this.n)
                throw new ArgumentException("input exceeds modulus");

            T = T.modPow(this.e, this.n);

            byte[] b = T.GetBytes();
            return pad_bytes(b, this.keylen);
        }


        // Perform an RSA private key operation on input
        public byte[] DoPrivate(byte[] input)
        {
            if (input.Length != this.keylen)
                throw new ArgumentException("input.Length does not match keylen");

            if (ReferenceEquals(this.d, null))
                throw new ArgumentException("no private key set!");

            BigInteger T = new BigInteger(input);
            if (T >= this.n)
                throw new ArgumentException("input exceeds modulus");

            T = T.modPow(this.d, this.n);

            byte[] b = T.GetBytes();
            return pad_bytes(b, this.keylen);
        }

        // Compare byte arrays
        private bool compare_bytes(byte[] b1, int i1, byte[] b2, int i2, int n)
        {
            for (int i = 0; i < n; ++i)
            {
                if (b1[i + i1] != b2[i + i2])
                    return false;
            }

            return true;
        }

        // pad b with leading 0's to make it n bytes long
        private byte[] pad_bytes(byte[] b, int n)
        {
            int len = b.Length;
            if (len >= n)
                return b;

            byte[] result = new byte[n];
            int padding = n - len;
            for (int i = 0; i < padding; ++i)
                result[i] = 0;
            Array.Copy(b, 0, result, padding, len);
            return result;
        }


        // Encrypt a message and pack it up into PKCS#1 v1.5 format
        // Plug compatible with RSACryptoServiceProvider.Encrypt
        public byte[] Encrypt(byte[] input, bool fOAEP)
        {
            if (fOAEP)
                throw new ArgumentException("OAEP padding not supported, sorry");

            int input_len = input.Length;
            int n_pad = this.keylen - 3 - input_len;
            if (n_pad < 8)
                throw new ArgumentException("input too long");

            byte[] encrypt_me = new byte[this.keylen];
            encrypt_me[0] = 0;
            encrypt_me[1] = RSA_CRYPT;

            byte[] padding = new byte[n_pad];
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(padding);

            for (int i = 0; i < n_pad; ++i)             // padding bytes must not be zero
            {
                if (padding[i] == 0)
                    padding[i] = (byte)i;
                if ((byte)i == 0)
                    padding[i] = (byte)1;
            }

            Array.Copy(padding, 0, encrypt_me, 2, n_pad);
            encrypt_me[n_pad + 2] = 0;
            Array.Copy(input, 0, encrypt_me, n_pad + 3, input_len);

            return DoPublic(encrypt_me);
        }


        // Decrypt a message in PKCS#1 v1.5 format
        // Plug compatible with RSACryptoServiceProvider.Decrypt
        public byte[] Decrypt(byte[] input, bool fOAEP)
        {
            if (fOAEP)
                throw new ArgumentException("OAEP padding not supported, sorry");

            byte[] decrypted = DoPrivate(input);

            if (decrypted[0] != 0 || decrypted[1] != RSA_CRYPT)
                throw new ArgumentException("invalid signature bytes");

            int decrypted_len = decrypted.Length;               // = keylen
            for (int i = 2; i < decrypted_len - 1; ++i)
            {
                if (decrypted[i] == 0)
                {
                    ++i;
                    int output_len = decrypted_len - i;
                    byte[] output = new byte[output_len];
                    Array.Copy(decrypted, i, output, 0, output_len);
                    return output;
                }
            }

            throw new ArgumentException("invalid padding");
        }
    }
}

