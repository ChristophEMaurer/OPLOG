using System;
using System.Data;
using System.Text;
using System.IO;
using System.Windows.Forms;

using Utility;

namespace Operationen.Wizards.ImportRichtlinien
{
    public class RichtlinienImporter: ImporterExporter
    {
        private const string FormName = "Wizards_ImportRichtlinien_RichtlinienImporter";
        public const string Version = "2";
        private int _ID_Gebiete;

        public RichtlinienImporter(BusinessLayer b, ProgressBar progressBar)
            : base(b, progressBar)
        {
        }

        public void Initialize(int ID_Gebiete, string fileName)
        {
            _ID_Gebiete = ID_Gebiete;
            _fileName = fileName;
        }

        public bool Import()
        {
            bool success = true;

            TheProgressBar.Maximum = Tools.CountLines(Encoding.Unicode, _fileName);
            TheProgressBar.Visible = true;

            StreamReader reader = null;

            try
            {
                reader = new StreamReader(_fileName, Encoding.Unicode);

                // Signatur überspringen
                if (_businessLayer.CheckTextFileSignature(reader, BusinessLayer.FileSignatureRichtlinien, Version))
                {
                    string line;

                    try
                    {
                        _businessLayer.OpenDatabaseForImport();

                        do
                        {
                            Progress();

                            line = reader.ReadLine();
                            if (line != null)
                            {
                                if (!ImportLine(line))
                                {
                                    break;
                                }
                            }
                        } while (line != null);
                    }
                    finally
                    {
                        _businessLayer.CloseDatabaseForImport();
                    }
                }
                else
                {
                    success = false;
                }
            }
            catch (Exception e)
            {
                _businessLayer.MessageBox(string.Format(GetText(FormName, "error1"), _fileName, e.Message));
                success = false;
                goto _exit;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                TheProgressBar.Value = TheProgressBar.Maximum;
            }

            _exit:

            return success;
        }

        private bool ImportLine(string line)
        {
            bool success = true;

            string[] arLine = line.Split('|');

            if (arLine.Length != 3)
            {
                _businessLayer.MessageBox(GetText(FormName, "error2"));
                success = false;
                goto exit;
            }

            string strLfdNummer = arLine[0];
            string strRichtzahl = arLine[1];
            string untBehMethode = arLine[2];
            int nLfdNummer;
            int nRichtzahl;

            if (!Int32.TryParse(strLfdNummer, out nLfdNummer))
            {
                _businessLayer.MessageBox(GetText(FormName, "error3"));
                success = false;
                goto exit;
            }
            if (!Int32.TryParse(strRichtzahl, out nRichtzahl))
            {
                _businessLayer.MessageBox(GetText(FormName, "error4"));
                success = false;
                goto exit;
            }
                
            //
            // Vorhandene Richtlinien nicht mehrfach einfügen
            //
            DataRow row = _businessLayer.GetRichtlinie(_ID_Gebiete, nLfdNummer, nRichtzahl, untBehMethode, true);
            if (row == null)
            {
                //
                // Richtlinie gibt es noch nicht
                //
                row = _businessLayer.CreateDataRowRichtlinie();

                //
                // Die LfdNummer wird immer automatisch erzeugt
                //
                row["ID_Gebiete"] = _ID_Gebiete;
                row["LfdNummer"] = nLfdNummer;
                row["Richtzahl"] = nRichtzahl;
                row["UntBehMethode"] = untBehMethode;

                _businessLayer.InsertRichtlinie(row, true);
            }

            exit:
            return success;
        }
    }
}
