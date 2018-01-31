using System;
using System.Data.SqlClient;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Security;
using System.Security.Permissions;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Xml;

using Operationen;

namespace Operationen
{
    /// <summary>
    /// Dieses ist ein Demo-Plugin, das veranschaulichen soll, wie man auf eine Datenbank zugreifen könnte.
    /// Wenn der connection string nicht festverdrahtet sein soll, kann dieses Plugin ein Fenster öffnen,
    /// das den Benutzer nach user und password fragt.
    /// </summary>
	public class OperationenImportPlugin : OperationenImport
	{
        public override OpLogPluginId PluginId { get { return OpLogPluginId.PluginIdSqlServerDemo; } }

        OperationenImportEvent _event;
        Byte[] _key = { 1,3,5,7,9,11,13,76,45,67,89,24,48,96,75,53,42,87,63,42,46,97,57,42,35,65,76,34,46,87,3,5};

        public override void OPImportInit(OperationenImportPluginCustomData customData)
        {
            _event = new OperationenImportEvent();
        }

        public override void OPImportRun()
        {
            DoOperationenImport();
            //OperationenDemo();
        }

        public override void OPImportFinalize()
        {
        }

        private string ConnectionString()
        {
            string path = Application.StartupPath + Path.DirectorySeparatorChar + "plugins" + Path.DirectorySeparatorChar + "connectionstring.txt";

            StreamReader sr = new StreamReader(path);

            string xmlCypherText = sr.ReadToEnd().Trim();
            string xmlPlainText = Decrypt(xmlCypherText);
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xmlPlainText);
            XmlElement element = xmlDocument.GetElementsByTagName("data")[0] as XmlElement;
            string connectionString = element.InnerText;

            return connectionString;
        }

        public string EncryptSimple(string plainText)
        {
            Byte[] b = System.Text.ASCIIEncoding.ASCII.GetBytes(plainText);
            string cypherText = Convert.ToBase64String(b);
            return cypherText;
        }

        public string DecryptSimple(string cypherText)
        {
            Byte[] b = Convert.FromBase64String(cypherText);
            string plainText = System.Text.ASCIIEncoding.ASCII.GetString(b);
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

        public string Encrypt(string plainText)
        {
            string cypherText = string.Empty;

            // Create an XmlDocument object.
            XmlDocument xmlDoc = new XmlDocument();

            // Load the text into the XmlDocument object.
            xmlDoc.PreserveWhitespace = true;
            xmlDoc.LoadXml(plainText);

            // 32 random bytes
            RijndaelManaged key = new RijndaelManaged();
            key.Key = _key;

            // Encrypt the elementName element.
            SymmetricEncrypt(xmlDoc, "data", key);

            cypherText = xmlDoc.OuterXml;

            return cypherText;
        }

        private string Decrypt(string plaintext)
        {
            string cypherText = string.Empty;

            // Create an XmlDocument object.
            XmlDocument xmlDoc = new XmlDocument();

            // Load the text into the XmlDocument object.
            xmlDoc.PreserveWhitespace = true;
            xmlDoc.LoadXml(plaintext);

            // 32 random bytes
            RijndaelManaged key = new RijndaelManaged();
            key.Key = _key;

            // Encrypt the elementName element.
            SymmetricDecrypt(xmlDoc, key);

            cypherText = xmlDoc.OuterXml;

            return cypherText;
        }


        private void DoOperationenImport()
        {
            SqlConnection connection = null;
            SqlCommand command = null;
            SqlDataReader reader = null;

            try
            {
                string connectionString = ConnectionString();
                connection = new SqlConnection();
                connection.ConnectionString = "Trusted_Connection=Yes;Data Source=CMAURER\\SQLExpress;Initial catalog=Operationen;";
                connection.Open();

                string strSQL = "select 'Nachname', 'Vorname', 'opscode', 'Operation Beschreibung', convert(DateTime, '01.01.2000', 104)";
                command = new SqlCommand(strSQL, connection);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    _event.ClearData();
                    _event.State = EVENT_STATE.STATE_DATA;
                    _event.SurgeonLastName = reader.GetString(0);
                    _event.SurgeonFirstName = reader.GetString(1);
                    _event.OPCode = reader.GetString(2);
                    _event.OPDescription = reader.GetString(3);
                    _event.OPDateAndTime = reader.GetDateTime(4);

                    FireImportOPEvent(_event);
                }
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                if (connection != null && connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
        private void OperationenDemo()
        {
            _event.SurgeonLastName = "Maurer";
            _event.SurgeonFirstName = "Christoph";
            _event.OPCode = "OPSKode";
            _event.OPDescription = "Demo OP";
            _event.OPDateAndTime = DateTime.Now;

            FireImportOPEvent(_event);
		}

        public override string OPImportDescription()
        {
            return "Dieses Demo-Import-Plugin importiert Operationen von einem SQLServer.";
        }
    }
}
