//
// XmlCryptography.cs - Sample usage of RSAManaged class
//
// Authors:
//	Christoph Maurer <ch.maurer@gmx.de>

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

using System;
using System.Security;
using System.Security.Permissions;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Xml;
using System.Text;
using System.IO;

using Security.Cryptography;

namespace Security
{
    /// <summary>
    /// A class with some useful functions. It was created so that XML documents can be signed
    /// without requiring Full Trust so that this code can run on a shared host.
    /// 
    /// The two function that require Full Trust are
    /// 
    /// RSACryptoServiceProvider.FromXmlString();
    /// and
    /// SignedXml.ComputeSignature()
    /// 
    /// </summary>
    public sealed class XmlCryptography
    {
        public const string RsaKey = "rsaKey";

        private XmlCryptography()
        {
            // FxCop: StaticHolderTypesShouldNotHaveConstructors
        }

#if false
        /// <summary>
        /// Does NOT require Full Trust
        /// </summary>
        /// <param name="xmlDocumentFileName"></param>
        /// <param name="privateKeyFileName"></param>
        /// <param name="writer"></param>
        public void XmlSignManaged(string xmlDocumentFileName, string privateKeyFileName, XmlTextWriter writer)
        {
            /*
             * This does not work yet because xmlns="" is inserted into various nodes. See NamespaceProblem.
             */


            SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();
            XmlDsigC14NTransform xmlTransform = new XmlDsigC14NTransform();

            XmlDocument xmldoc = new XmlDocument();
            XmlTextReader reader = new XmlTextReader(xmlDocumentFileName);
            xmldoc.Load(reader);

            XmlDocument signedXmlDoc = new XmlDocument();
            reader = new XmlTextReader(xmlDocumentFileName);
            signedXmlDoc.Load(reader);
            reader.Close();

            string xmlKey = new StreamReader(privateKeyFileName).ReadToEnd();


            XmlElement signature = signedXmlDoc.CreateElement("Signature", "http://www.w3.org/2000/09/xmldsig#");
            XmlElement signedInfo = signedXmlDoc.CreateElement("SignedInfo", "http://www.w3.org/2000/09/xmldsig#");
            XmlElement canonicalizationMethod = signedXmlDoc.CreateElement("CanonicalizationMethod");
            canonicalizationMethod.SetAttribute("Algorithm", "http://www.w3.org/TR/2001/REC-xml-c14n-20010315");
            XmlElement signatureMethod = signedXmlDoc.CreateElement("SignatureMethod");
            signatureMethod.SetAttribute("Algorithm", "http://www.w3.org/2000/09/xmldsig#rsa-sha1");
            XmlElement reference = signedXmlDoc.CreateElement("Reference");
            reference.SetAttribute("URI", "");
            XmlElement transforms = signedXmlDoc.CreateElement("Transforms");
            XmlElement transform = signedXmlDoc.CreateElement("Transform");
            transform.SetAttribute("Algorithm", "http://www.w3.org/2000/09/xmldsig#enveloped-signature");
            XmlElement digestMethod = signedXmlDoc.CreateElement("DigestMethod");
            digestMethod.SetAttribute("Algorithm", "http://www.w3.org/2000/09/xmldsig#sha1");
            XmlElement digestValue = signedXmlDoc.CreateElement("DigestValue");
            XmlElement signatureValue = signedXmlDoc.CreateElement("SignatureValue");

            signedXmlDoc.DocumentElement.AppendChild(signature);
            signature.AppendChild(signedInfo);
            signature.AppendChild(signatureValue);
            signedInfo.AppendChild(canonicalizationMethod);
            signedInfo.AppendChild(signatureMethod);
            signedInfo.AppendChild(reference);
            reference.AppendChild(transforms);
            reference.AppendChild(digestMethod);
            reference.AppendChild(digestValue);
            transforms.AppendChild(transform);


            //
            // Create digest value. Message M : the XML root element OuterXml as defined by the Reference element.
            // This is the complete original document, in this case "<root>...</root>"
            // The digest value is: BASE64(SHA1(M))
            //
            digestValue.InnerText = ComputeDigest(xmldoc, xmlTransform, sha1);

            //
            // Create the signatureValue.
            // Message data: The OuterXml of the canonicalized SignedInfo element
            // This is all of "<SignedInfo>...</SignedInfo>"
            // The signatureValue is: CRYPT (PAD (ASN.1 (OID, DIGEST (data))))
            // as specified in 6.4.2 PKCS1 (RSA-SHA1) of http://www.w3.org/TR/xmldsig-corec
            //

            string data = signedInfo.OuterXml;//.Replace(@"xmlns=""""", "");
            signatureValue.InnerText = ComputeSignatureValue(data, xmlTransform, xmlKey);

            signedXmlDoc.WriteTo(writer);
        }
#endif

#if false
        /// <summary>
        /// First try. Do NOT use
        /// </summary>
        /// <param name="xmlPrivateKey"></param>
        /// <param name="xmlText"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        private string SignDataManaged(string xmlPrivateKey, string xmlText, string node)
        {
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.PreserveWhitespace = true;
            xmldoc.LoadXml(xmlText);

            XmlElement elementToSign = xmldoc.GetElementsByTagName(node)[0] as XmlElement;

            // Throw an XmlException if the element was not found.
            if (elementToSign == null)
            {
                throw new XmlException("The specified element was not found");
            }

            // ok RSAManaged rsa = new RSAManaged(512);
            //RSAManaged rsa = new RSAManaged(512);
            RSAManaged rsa = new RSAManaged();
            rsa.FromXmlString(xmlPrivateKey);

            string plainText = elementToSign.OuterXml;
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            SHA1CryptoServiceProvider hasher = new SHA1CryptoServiceProvider();
            byte[] signatureBytes = rsa.SignData(plainTextBytes, hasher);
            string signature = Convert.ToBase64String(signatureBytes);

            XmlNode xmlSignature = xmldoc.CreateNode(XmlNodeType.Element, "signature", null);
            xmlSignature.InnerText = signature;

            xmldoc.DocumentElement.AppendChild(xmlSignature);

            string signedXml = xmldoc.OuterXml;

            return signedXml;

        }

        /// <summary>
        /// First try. Do NOT use
        /// </summary>
        /// <param name="xmlPrivateKey"></param>
        /// <param name="xmlText"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        private bool VerifySignedDataManaged(string xmlPublicKey, string xmlText, string node)
        {

            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(xmlText);

            RSAManaged rsa = new RSAManaged();
            rsa.FromXmlString(xmlPublicKey);

            XmlElement elementToSign = xmldoc.GetElementsByTagName(node)[0] as XmlElement;
            XmlElement elementSignature = xmldoc.GetElementsByTagName("signature")[0] as XmlElement;

            string plainText = elementToSign.OuterXml;
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            string signature = elementSignature.InnerText;
            byte[] signatureBytes = Convert.FromBase64String(signature);
            SHA1CryptoServiceProvider hasher = new SHA1CryptoServiceProvider();
            return rsa.VerifyData(plainTextBytes, hasher, signatureBytes);
        }
#endif

        /// <summary>
        /// Encrypt an Xml document
        /// </summary>
        /// <param name="data">32 bytes</param>
        /// <param name="elementName">The XML element to encrypt</param>
        /// <param name="plainText"></param>
        /// <param name="cypherText"></param>
        /// <returns></returns>
        public static string SymmetricEncryptXmlDocument(byte[] data, string elementName, string plaintext)
        {
            string cypherText = string.Empty;

            // Create an XmlDocument object.
            XmlDocument xmlDoc = new XmlDocument();

            // Load the text into the XmlDocument object.
            xmlDoc.PreserveWhitespace = true;
            xmlDoc.LoadXml(plaintext);

            // 32 random bytes
            RijndaelManaged key = new RijndaelManaged();
            key.Key = data;

            // Encrypt the elementName element.
            SymmetricEncrypt(xmlDoc, elementName, key);

            cypherText = xmlDoc.OuterXml;

            return cypherText;
        }

        public static string SymmetricDecryptXmlDocument(byte[] data, string encryptedText)
        {
            string plainText = string.Empty;

            // Create an XmlDocument object.
            XmlDocument xmlDoc = new XmlDocument();

            // Load the text into the XmlDocument object.
            xmlDoc.PreserveWhitespace = true;
            xmlDoc.LoadXml(encryptedText);

            // Encrypt the "data" element.
            RijndaelManaged key = new RijndaelManaged();
            key.Key = data;

            // Encrypt the "data" element.
            SymmetricDecrypt(xmlDoc, key);

            plainText = xmlDoc.OuterXml;

            return plainText;
        }

        private static void SymmetricEncrypt(XmlDocument xmlDocument, string elementName, SymmetricAlgorithm key)
        {
            // Check the arguments.  
            if (xmlDocument == null)
            {
                throw new ArgumentNullException("xmlDocument");
            }

            if (elementName == null)
            {
                throw new ArgumentNullException("elementName");
            }

            if (key == null)
            {
                throw new ArgumentNullException("key");
            }

            ////////////////////////////////////////////////
            // Find the specified element in the XmlDocument
            // object and create a new XmlElemnt object.
            ////////////////////////////////////////////////
            XmlElement elementToEncrypt = xmlDocument.GetElementsByTagName(elementName)[0] as XmlElement;
            // Throw an XmlException if the element was not found.
            if (elementToEncrypt == null)
            {
                throw new XmlException("The specified element was not found");
            }

            //////////////////////////////////////////////////
            // Create a new instance of the EncryptedXml class 
            // and use it to encrypt the XmlElement with the 
            // symmetric key.
            //////////////////////////////////////////////////

            EncryptedXml eXml = new EncryptedXml();

            byte[] encryptedElement = eXml.EncryptData(elementToEncrypt, key, false);
            ////////////////////////////////////////////////
            // Construct an EncryptedData object and populate
            // it with the desired encryption information.
            ////////////////////////////////////////////////

            EncryptedData edElement = new EncryptedData();
            edElement.Type = EncryptedXml.XmlEncElementUrl;

            // Create an EncryptionMethod element so that the 
            // receiver knows which algorithm to use for decryption.
            // Determine what kind of algorithm is being used and
            // supply the appropriate URL to the EncryptionMethod element.

            string encryptionMethod = null;

            if (key is TripleDES)
            {
                encryptionMethod = EncryptedXml.XmlEncTripleDESUrl;
            }
            else if (key is DES)
            {
                encryptionMethod = EncryptedXml.XmlEncDESUrl;
            }
            if (key is Rijndael)
            {
                switch (key.KeySize)
                {
                    case 128:
                        encryptionMethod = EncryptedXml.XmlEncAES128Url;
                        break;

                    case 192:
                        encryptionMethod = EncryptedXml.XmlEncAES192Url;
                        break;

                    case 256:
                        encryptionMethod = EncryptedXml.XmlEncAES256Url;
                        break;
                }
            }
            else
            {
                // Throw an exception if the transform is not in the previous categories
                throw new CryptographicException("The specified algorithm is not supported for XML Encryption.");
            }

            edElement.EncryptionMethod = new EncryptionMethod(encryptionMethod);

            // Add the encrypted element data to the 
            // EncryptedData object.
            edElement.CipherData.CipherValue = encryptedElement;

            ////////////////////////////////////////////////////
            // Replace the element from the original XmlDocument
            // object with the EncryptedData element.
            ////////////////////////////////////////////////////
            EncryptedXml.ReplaceElement(elementToEncrypt, edElement, false);
        }

        private static void SymmetricDecrypt(XmlDocument Doc, SymmetricAlgorithm Alg)
        {
            // Check the arguments.  
            if (Doc == null)
                throw new ArgumentNullException("Doc");

            if (Alg == null)
                throw new ArgumentNullException("Alg");

            // Find the EncryptedData element in the XmlDocument.
            XmlElement encryptedElement = Doc.GetElementsByTagName("EncryptedData")[0] as XmlElement;

            // If the EncryptedData element was not found, throw an exception.
            if (encryptedElement == null)
            {
                throw new XmlException("The EncryptedData element was not found.");
            }

            // Create an EncryptedData object and populate it.
            EncryptedData edElement = new EncryptedData();
            edElement.LoadXml(encryptedElement);

            // Create a new EncryptedXml object.
            EncryptedXml exml = new EncryptedXml();

            // Decrypt the element using the symmetric key.
            byte[] rgbOutput = exml.DecryptData(edElement, Alg);

            // Replace the encryptedData element with the plaintext XML element.
            exml.ReplaceData(encryptedElement, rgbOutput);
        }

        public static string AsymmetricEncryptXmlDocument(string xmlKey, bool pureManaged, string plaintext, string elementName)
        {
            string cypherText = string.Empty;

            // Create an XmlDocument object.
            XmlDocument xmlDoc = new XmlDocument();

            // Load the text into the XmlDocument object.
            xmlDoc.PreserveWhitespace = true;
            xmlDoc.LoadXml(plaintext);

            // Create a new RSA key and save it in the container.  This key will encrypt
            // a symmetric key, which will then be encryped in the XML document.
            RSA rsaKey;
            if (pureManaged)
            {
                rsaKey = new RSAManaged();
            }
            else
            {
                rsaKey = new RSACryptoServiceProvider();
            }
            rsaKey.FromXmlString(xmlKey);

            try
            {
                // Encrypt the "contents" element.
                AsymmetricEncrypt(xmlDoc, elementName, "EncryptedElement1", rsaKey, RsaKey);

                // Save the XML document.
                cypherText = xmlDoc.OuterXml;
            }
            finally
            {
                // Clear the RSA key.
                rsaKey.Clear();
            }

            return cypherText;
        }

        public static string AsymmetricDecryptXmlDocument(string xmlKey, bool pureManaged, string encryptedText)
        {
            string plainText = string.Empty;

            // Create an XmlDocument object.
            XmlDocument xmlDoc = new XmlDocument();

            // Load an XML file into the XmlDocument object.
            xmlDoc.PreserveWhitespace = true;
            xmlDoc.LoadXml(encryptedText);

            // Get the RSA key from the key container.  This key will decrypt
            // a symmetric key that was imbedded in the XML document.
            RSA rsaKey;
            if (pureManaged)
            {
                rsaKey = new RSAManaged();
            }
            else
            {
                rsaKey = new RSACryptoServiceProvider();
            }
            rsaKey.FromXmlString(xmlKey);

            try
            {
                // Decrypt the elements.
                AsymmetricDecrypt(xmlDoc, rsaKey, RsaKey);
                plainText = xmlDoc.OuterXml;
            }
            finally
            {
                // Clear the RSA key.
                rsaKey.Clear();
            }

            return plainText;
        }

        private static void AsymmetricDecrypt(XmlDocument Doc, RSA Alg, string KeyName)
        {
            // Check the arguments.
            if (Doc == null)
                throw new ArgumentNullException("Doc");
            if (Alg == null)
                throw new ArgumentNullException("Alg");
            if (KeyName == null)
                throw new ArgumentNullException("KeyName");
            // Create a new EncryptedXml object.
            EncryptedXml exml = new EncryptedXml(Doc);

            // Add a key-name mapping.
            // This method can only decrypt documents
            // that present the specified key name.
            exml.AddKeyNameMapping(KeyName, Alg);

            // Decrypt the element.
            exml.DecryptDocument();

        }
        private static void AsymmetricEncrypt(XmlDocument Doc, string ElementToEncrypt, string EncryptionElementID, RSA Alg, string KeyName)
        {
            // Check the arguments.
            if (Doc == null)
                throw new ArgumentNullException("Doc");
            if (ElementToEncrypt == null)
                throw new ArgumentNullException("ElementToEncrypt");
            if (EncryptionElementID == null)
                throw new ArgumentNullException("EncryptionElementID");
            if (Alg == null)
                throw new ArgumentNullException("Alg");
            if (KeyName == null)
                throw new ArgumentNullException("KeyName");

            ////////////////////////////////////////////////
            // Find the specified element in the XmlDocument
            // object and create a new XmlElemnt object.
            ////////////////////////////////////////////////
            XmlElement elementToEncrypt = Doc.GetElementsByTagName(ElementToEncrypt)[0] as XmlElement;

            // Throw an XmlException if the element was not found.
            if (elementToEncrypt == null)
            {
                throw new XmlException("The specified element was not found");

            }
            RijndaelManaged sessionKey = null;

            try
            {
                //////////////////////////////////////////////////
                // Create a new instance of the EncryptedXml class
                // and use it to encrypt the XmlElement with the
                // a new random symmetric key.
                //////////////////////////////////////////////////

                // Create a 256 bit Rijndael key.
                sessionKey = new RijndaelManaged();
                sessionKey.KeySize = 256;

                EncryptedXml eXml = new EncryptedXml();

                byte[] encryptedElement = eXml.EncryptData(elementToEncrypt, sessionKey, false);
                ////////////////////////////////////////////////
                // Construct an EncryptedData object and populate
                // it with the desired encryption information.
                ////////////////////////////////////////////////

                EncryptedData edElement = new EncryptedData();
                edElement.Type = EncryptedXml.XmlEncElementUrl;
                edElement.Id = EncryptionElementID;
                // Create an EncryptionMethod element so that the
                // receiver knows which algorithm to use for decryption.

                edElement.EncryptionMethod = new EncryptionMethod(EncryptedXml.XmlEncAES256Url);
                // Encrypt the session key and add it to an EncryptedKey element.
                EncryptedKey ek = new EncryptedKey();

                byte[] encryptedKey = EncryptedXml.EncryptKey(sessionKey.Key, Alg, false);

                ek.CipherData = new CipherData(encryptedKey);

                ek.EncryptionMethod = new EncryptionMethod(EncryptedXml.XmlEncRSA15Url);

                // Create a new DataReference element
                // for the KeyInfo element.  This optional
                // element specifies which EncryptedData
                // uses this key.  An XML document can have
                // multiple EncryptedData elements that use
                // different keys.
                DataReference dRef = new DataReference();

                // Specify the EncryptedData URI.
                dRef.Uri = "#" + EncryptionElementID;

                // Add the DataReference to the EncryptedKey.
                ek.AddReference(dRef);


                // Set the KeyInfo element to specify the
                // name of the RSA key.

                // Create a new KeyInfo element.
                edElement.KeyInfo = new KeyInfo();

                // Create a new KeyInfoName element.
                KeyInfoName kin = new KeyInfoName();

                // Specify a name for the key.
                kin.Value = KeyName;

                // Add the KeyInfoName element to the
                // EncryptedKey object.
                ek.KeyInfo.AddClause(kin);

                // Add the encrypted key to the
                // EncryptedData object.
                edElement.KeyInfo.AddClause(new KeyInfoEncryptedKey(ek));

                // Add the encrypted element data to the
                // EncryptedData object.
                edElement.CipherData.CipherValue = encryptedElement;
                ////////////////////////////////////////////////////
                // Replace the element from the original XmlDocument
                // object with the EncryptedData element.
                ////////////////////////////////////////////////////
                EncryptedXml.ReplaceElement(elementToEncrypt, edElement, false);
            }
            catch
            {
                // re-throw the exception.
                throw;
            }
            finally
            {
                if (sessionKey != null)
                {
                    sessionKey.Clear();
                }
            }
        }


        static public void Sign(string xmlFileName, string xmlKeyFileName, XmlTextWriter writer)
        {
            string xmlKey = new StreamReader(xmlKeyFileName).ReadToEnd();

            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(xmlFileName);

            SignFullTrust(xmldoc.OuterXml, xmlKey, writer);
        }

        /// <summary>
        /// This function requires Full Trust
        /// </summary>
        /// <param name="xmlKey"></param>
        /// <param name="xmlText"></param>
        /// <param name="writer"></param>
        static public void SignFullTrust(string xmlText, string xmlKey, XmlTextWriter writer)
        {
            // Load the license request file.
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(xmlText);

            RSA csp = new RSACryptoServiceProvider();
            csp.FromXmlString(xmlKey);

            // Creating the XML signing object.
            SignedXml sxml = new SignedXml(xmldoc);
            sxml.SigningKey = csp;

            // Set the canonicalization method for the document.
            sxml.SignedInfo.CanonicalizationMethod = SignedXml.XmlDsigC14NTransformUrl;

            // Create an empty reference (not enveloped) for the XPath
            // transformation. Empty reference: sign entire document.
            Reference reference = new Reference("");

            // Create the XPath transform and add it to the reference list.
            reference.AddTransform(new XmlDsigEnvelopedSignatureTransform(false));

            sxml.AddReference(reference);
            sxml.ComputeSignature();

            XmlElement signature = sxml.GetXml();
            xmldoc.DocumentElement.AppendChild(signature);

            try
            {
                xmldoc.WriteTo(writer);
            }
            finally
            {
                writer.Flush();
            }

            if (csp != null)
            {
                csp = null;
            }
        }

        /// <summary>
        /// Transform XML string data to a canonical form
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="transform">If </param>
        /// <returns></returns>
        static private XmlDocument CanonicalizeXml(string xml, Transform transform)
        {
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(xml);

            return CanonicalizeXml(xmldoc, transform);
        }

        /// <summary>
        /// Transform an XML document to a canonical form
        /// </summary>
        /// <param name="xmldoc"></param>
        /// <param name="transform"></param>
        /// <returns></returns>
        static private XmlDocument CanonicalizeXml(XmlDocument xmldoc, Transform transform)
        {
            transform.LoadInput(xmldoc);
            Stream stream = (Stream)transform.GetOutput(typeof(Stream));
            XmlTextReader reader = new XmlTextReader(stream);

            XmlDocument xml = new XmlDocument();
            xml.Load(reader);

            return xml;
        }

        /// <summary>
        /// Take the entire XML document, canonicalize it and calculate a hash value from the Base64 
        /// representation of the hash value. We use UTF8 Encoding.
        /// </summary>
        /// <param name="xmldoc"></param>
        /// <param name="transform">If null, not transfomation is done.</param>
        /// <param name="hasher"></param>
        /// <returns></returns>
        static private string ComputeDigest(XmlDocument xmldoc, Transform transform, HashAlgorithm hasher)
        {
            XmlDocument transformedXml = xmldoc;

            if (transform != null)
            {
                transformedXml = CanonicalizeXml(xmldoc, transform);
            }

            string data = transformedXml.OuterXml;

            UTF8Encoding byteConverter = new UTF8Encoding();
            byte[] p = byteConverter.GetBytes(data);
            byte[] c = hasher.ComputeHash(p);
            string digest = Convert.ToBase64String(c);

            return digest;
        }

        static private string ComputeDigest(string xml, Transform transform, HashAlgorithm hasher)
        {
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(xml);

            return ComputeDigest(xmldoc, transform, hasher);
        }

        static private string ComputeSignatureValue(string signedInfo, Transform transform, string privateKey)
        {
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(signedInfo);

            return ComputeSignatureValue(xmldoc, transform, privateKey);
        }

        /// <summary>
        /// Given a &lt;signedInfo&gt; XML document, canonicalize it, then take the 
        /// OuterXML of the entire document and calculate a hash value on that string,
        /// then encrypt the hash value bytes with the private key and 
        /// return the encrypted bytes as a Base64 string.
        /// 
        /// </summary>
        /// <param name="signedInfo"></param>
        /// <param name="transform"></param>
        /// <param name="privateKey"></param>
        /// <returns></returns>
        static private string ComputeSignatureValue(XmlDocument signedInfo, Transform transform, string privateKey)
        {
            XmlDocument xmldocCanonicalized = signedInfo;

            if (transform != null)
            {
                xmldocCanonicalized  = CanonicalizeXml(signedInfo, transform);
            }

            XmlNode signedInfoCanonicalized = xmldocCanonicalized.FirstChild;

            string data = signedInfoCanonicalized.OuterXml;

            UTF8Encoding byteConverter = new UTF8Encoding();
            byte[] dataBytes = byteConverter.GetBytes(data);

            RSAManaged rsa = new RSAManaged();
            rsa.FromXmlString(privateKey);
            byte[] digestBytes = rsa.SignData(dataBytes, new SHA1CryptoServiceProvider());
            string signatureValue = Convert.ToBase64String(digestBytes);

            return signatureValue;
        }

        /// <summary>
        /// MI: "It's complicated."
        ///     We cannot use a transform, or RSACryptoServiceProvider.FromXmlString, or 
        ///     SignedXml.ComputeSignature() on a shared host.
        /// /
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="privateKey"></param>
        /// <param name="doTransform"></param>
        /// <param name="writer"></param>
        static public void SignNoFullTrust(string xml, string privateKey, XmlTextWriter writer)
        {
            SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();

            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(xml);

            XmlDocument signedXmlDoc = new XmlDocument();
            signedXmlDoc.LoadXml(xml);

            //
            // signedInfo must be in canonicalized form because this node is canonicalized when verifying the XML document.
            // It must be exactly like this or the verification will fail.
            //
            string signedInfo = "<SignedInfo xmlns=\"http://www.w3.org/2000/09/xmldsig#\">"
                + "<CanonicalizationMethod Algorithm=\"http://www.w3.org/TR/2001/REC-xml-c14n-20010315\"></CanonicalizationMethod>"
                + "<SignatureMethod Algorithm=\"http://www.w3.org/2000/09/xmldsig#rsa-sha1\"></SignatureMethod>"
                + "<Reference URI=\"\">"
                    + "<Transforms>"
                        + "<Transform Algorithm=\"http://www.w3.org/2000/09/xmldsig#enveloped-signature\"></Transform>"
                    + "</Transforms>"
                    + "<DigestMethod Algorithm=\"http://www.w3.org/2000/09/xmldsig#sha1\"></DigestMethod>"
                    + "<DigestValue>{0}</DigestValue>"
                + "</Reference></SignedInfo>";

            string signature = @"<root><Signature xmlns=""http://www.w3.org/2000/09/xmldsig#"">{0}<SignatureValue>{1}</SignatureValue></Signature></root>";

            RSAManaged rsa = new RSAManaged();

            rsa.FromXmlString(privateKey);
            string digestValue = ComputeDigest(xmldoc, null, sha1);
            signedInfo = string.Format(signedInfo, digestValue);
            xmldoc = new XmlDocument();
            xmldoc.LoadXml(signedInfo);
            string data = xmldoc.OuterXml;

            byte[] b = Encoding.UTF8.GetBytes(data);
            byte[] sv = rsa.SignData(b, sha1);
            string signatureValue = Convert.ToBase64String(sv);

            signature = string.Format(signature, signedInfo, signatureValue);

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(signature);
            XmlNode nodeOrig = doc.DocumentElement.ChildNodes[0];

            XmlNode nodeNew = signedXmlDoc.ImportNode(nodeOrig, true);
            signedXmlDoc.DocumentElement.AppendChild(nodeNew);

            signedXmlDoc.WriteTo(writer);
        }

        /// <summary>
        /// Requires Full Trust because of RSACryptoServiceProvider.FromXmlString();
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="publicKeyFileName"></param>
        /// <returns></returns>
        static public bool CheckSignature(string fileName, string publicKeyFileName)
        {
            string xmlKey = new StreamReader(publicKeyFileName).ReadToEnd();

            RSACryptoServiceProvider csp = new RSACryptoServiceProvider();
            csp.FromXmlString(xmlKey);

            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(fileName);

            SignedXml sxml = new SignedXml(xmldoc);

            XmlNode dsig = xmldoc.GetElementsByTagName("Signature", SignedXml.XmlDsigNamespaceUrl)[0];

            sxml.LoadXml((XmlElement)dsig);

            // Verify the signature.
            return sxml.CheckSignature(csp);
        }
    }
}
