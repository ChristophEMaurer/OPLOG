using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;


namespace Security.Cryptography
{
    public class SymmetricCryptography
    {
        private string _key = string.Empty;
        private string _salt = string.Empty;
        private ServiceProviderEnum _algorithm;
        private SymmetricAlgorithm _cryptoService;

        private void SetLegalIV()
        {
            // Set symmetric algorithm
            switch (_algorithm)
            {
                case ServiceProviderEnum.Rijndael:
                    _cryptoService.IV = new byte[] { 0xf, 0x6f, 0x13, 0x2e, 0x35, 0xc2, 0xcd, 0xf9, 0x5, 0x46, 0x9c, 0xea, 0xa8, 0x4b, 0x73, 0xcc };
                    break;
                default:
                    _cryptoService.IV = new byte[] { 0xf, 0x6f, 0x13, 0x2e, 0x35, 0xc2, 0xcd, 0xf9 };
                    break;
            }
        }


        public enum ServiceProviderEnum : int
        {
            // Supported service providers
            Rijndael,
            RC2,
            DES,
            TripleDES
        }

        public SymmetricCryptography()
        {
            // Default symmetric algorithm
            _cryptoService = new RijndaelManaged();
            _cryptoService.Mode = CipherMode.CBC;
            _algorithm = ServiceProviderEnum.Rijndael;
        }

        public SymmetricCryptography(ServiceProviderEnum serviceProvider)
        {
            // Select symmetric algorithm
            switch (serviceProvider)
            {
                case ServiceProviderEnum.Rijndael:
                    _cryptoService = new RijndaelManaged();
                    _algorithm = ServiceProviderEnum.Rijndael;
                    break;
                case ServiceProviderEnum.RC2:
                    _cryptoService = new RC2CryptoServiceProvider();
                    _algorithm = ServiceProviderEnum.RC2;
                    break;
                case ServiceProviderEnum.DES:
                    _cryptoService = new DESCryptoServiceProvider();
                    _algorithm = ServiceProviderEnum.DES;
                    break;
                case ServiceProviderEnum.TripleDES:
                    _cryptoService = new TripleDESCryptoServiceProvider();
                    _algorithm = ServiceProviderEnum.TripleDES;
                    break;
            }
            _cryptoService.Mode = CipherMode.CBC;
        }

        public SymmetricCryptography(string serviceProviderName)
        {
            try
            {
                // Select symmetric algorithm
                switch (serviceProviderName.ToLower())
                {
                    case "rijndael":
                        serviceProviderName = "Rijndael";
                        _algorithm = ServiceProviderEnum.Rijndael;
                        break;
                    case "rc2":
                        serviceProviderName = "RC2";
                        _algorithm = ServiceProviderEnum.RC2;
                        break;
                    case "des":
                        serviceProviderName = "DES";
                        _algorithm = ServiceProviderEnum.DES;
                        break;
                    case "tripledes":
                        serviceProviderName = "TripleDES";
                        _algorithm = ServiceProviderEnum.TripleDES;
                        break;
                }

                // Set symmetric algorithm
                _cryptoService = (SymmetricAlgorithm)CryptoConfig.CreateFromName(serviceProviderName);
                _cryptoService.Mode = CipherMode.CBC;
            }
            catch
            {
                throw;
            }
        }

        public virtual byte[] GetLegalKey()
        {
            // Adjust key if necessary, and return a valid key
            if (_cryptoService.LegalKeySizes.Length > 0)
            {
                // Key sizes in bits
                int keySize = _key.Length * 8;
                int minSize = _cryptoService.LegalKeySizes[0].MinSize;
                int maxSize = _cryptoService.LegalKeySizes[0].MaxSize;
                int skipSize = _cryptoService.LegalKeySizes[0].SkipSize;

                if (keySize > maxSize)
                {
                    // Extract maximum size allowed
                    _key = _key.Substring(0, maxSize / 8);
                }
                else if (keySize < maxSize)
                {
                    // Set valid size
                    int validSize = (keySize <= minSize) ? minSize : (keySize - keySize % skipSize) + skipSize;
                    if (keySize < validSize)
                    {
                        // Pad the key with asterisk to make up the size
                        _key = _key.PadRight(validSize / 8, '*');
                    }
                }
            }
            PasswordDeriveBytes key = new PasswordDeriveBytes(_key, ASCIIEncoding.ASCII.GetBytes(_salt));
            return key.GetBytes(_key.Length);
        }

        public virtual string Encrypt(string plainText)
        {
            byte[] plainByte = ASCIIEncoding.ASCII.GetBytes(plainText);
            byte[] keyByte = GetLegalKey();

            // Set private key
            _cryptoService.Key = keyByte;
            SetLegalIV();

            // Encryptor object
            ICryptoTransform cryptoTransform = _cryptoService.CreateEncryptor();

            // Memory stream object
            MemoryStream ms = new MemoryStream();

            // Crpto stream object
            CryptoStream cs = new CryptoStream(ms, cryptoTransform, CryptoStreamMode.Write);

            // Write encrypted byte to memory stream
            cs.Write(plainByte, 0, plainByte.Length);
            cs.FlushFinalBlock();

            // Get the encrypted byte length
            byte[] cryptoByte = ms.ToArray();

            // Convert into base 64 to enable result to be used in Xml
            return Convert.ToBase64String(cryptoByte, 0, cryptoByte.GetLength(0));
        }

        public virtual string Decrypt(string cryptoText)
        {
            // Convert from base 64 string to bytes
            byte[] cryptoByte = Convert.FromBase64String(cryptoText);
            byte[] keyByte = GetLegalKey();

            // Set private key
            _cryptoService.Key = keyByte;
            SetLegalIV();

            // Decryptor object
            ICryptoTransform cryptoTransform = _cryptoService.CreateDecryptor();
            try
            {
                // Memory stream object
                MemoryStream ms = new MemoryStream(cryptoByte, 0, cryptoByte.Length);

                // Crpto stream object
                CryptoStream cs = new CryptoStream(ms, cryptoTransform, CryptoStreamMode.Read);

                // Get the result from the Crypto stream
                StreamReader sr = new StreamReader(cs);
                return sr.ReadToEnd();
            }
            catch
            {
                return null;
            }
        }

        public string Key
        {
            get
            {
                return _key;
            }
            set
            {
                _key = value;
            }
        }

        public string Salt
        {
            // Salt value
            get
            {
                return _salt;
            }
            set
            {
                _salt = value;
            }
        }
    }	
}

