using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace CMaurer
{
    public class Program
    {
        static void MD5(string fileName)
        {
            System.IO.FileStream reader = System.IO.File.OpenRead(fileName);
            // MD5-Hash aus dem Byte-Array berechnen
            System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] hashBytes = md5.ComputeHash(reader);
            reader.Close();

            //in string wandeln
            string hash = BitConverter.ToString(hashBytes).Replace("-", "").ToUpper();
            Console.Write(hash);
        }

        static void SHA1(string fileName)
        {
            System.IO.FileStream reader = System.IO.File.OpenRead(fileName);
            // MD5-Hash aus dem Byte-Array berechnen
            System.Security.Cryptography.SHA1 sha1 = new System.Security.Cryptography.SHA1CryptoServiceProvider();
            byte[] hashBytes = sha1.ComputeHash(reader);
            reader.Close();

            //in string wandeln
            string hash = BitConverter.ToString(hashBytes).Replace("-", "").ToUpper();
            Console.Write(hash);
        }

        static void FileSizeKB(string filename)
        {
            long sizeKB = 0;

            try
            {
                FileInfo fileInfo = new FileInfo(filename);
                long size = fileInfo.Length;
                sizeKB = size / 1024;
            }
            catch { }

            Console.Write(sizeKB);
        }

        static public void EncodeTo64(string toEncode)
        {
            byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(toEncode);

            string returnValue = System.Convert.ToBase64String(toEncodeAsBytes);

            Console.Write(returnValue);
        }

        /*
         
%OP_TOOL% /cmd echo /text "%OP_VERSION_NEW%|"
%OP_TOOL% /cmd fileSizeKB /fileName %OP_SETUP_FOLDER%\operationen-update.exe
%OP_TOOL% /newline yes /cmd echo /text "|%OP_WWW_UPDATE%"

rem
rem ACHTUNG: Kein NEWLINE am Ende der Zeile!!!
rem

%OP_TOOL% /cmd echo /text %OP_VERSION_NEW%^| > %OP_WWW_VERSION_FILE%
%OP_TOOL% /cmd fileSizeKB /fileName %OP_SETUP_FOLDER%\operationen-update.exe >> %OP_WWW_VERSION_FILE%
%OP_TOOL% /cmd echo /text ^|%OP_WWW_UPDATE% >> %OP_WWW_VERSION_FILE%

         */
        static int Main(string[] args)
        {
            int ret = 1;
            bool paramOk = false;
            string command = null;
            string param = null;
            bool newline = false;

            int i = 0;
            while (i + 1 < args.Length)
            {
                if (args[i] == "/cmd")
                {
                    command = args[i + 1];
                }
                else if (args[i] == "/fileName")
                {
                    param = args[i + 1];
                }
                else if (args[i] == "/text")
                {
                    param = args[i + 1];
                }
                else if (args[i] == "/newline")
                {
                    newline = true;
                }

                i += 2;
            }

            if (command == "fileSizeKB")
            {
                if (!string.IsNullOrEmpty(param))
                {
                    paramOk = true;
                    FileSizeKB(param);
                }
            }
            else if (command == "echo")
            {
                if (!string.IsNullOrEmpty(param))
                {
                    paramOk = true;
                    System.Console.Write(param);
                }
            }
            else if (command == "md5")
            {
                if (!string.IsNullOrEmpty(param))
                {
                    if (File.Exists(param))
                    {
                        paramOk = true;
                        MD5(param);
                    }
                }
            }
            else if (command == "sha1")
            {
                if (!string.IsNullOrEmpty(param))
                {
                    if (File.Exists(param))
                    {
                        paramOk = true;
                        SHA1(param);
                    }
                }
            }
            else if (command == "base64")
            {
                if (!string.IsNullOrEmpty(param))
                {
                        paramOk = true;
                        EncodeTo64(param);
                }
            }

            if (paramOk)
            {
                ret = 0;
                if (newline)
                {
                    Console.WriteLine();
                }
            }

            return ret;
        }
    }
}
