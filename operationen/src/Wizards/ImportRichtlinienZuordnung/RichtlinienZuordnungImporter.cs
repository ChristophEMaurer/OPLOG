using System;
using System.Data;
using System.Text;
using System.IO;
using System.Windows.Forms;

using Utility;

namespace Operationen.Wizards.ImportRichtlinienZuordnung
{
    /// <summary>
    /// Format der Datei ist:
    /// 1. Zeile:               ID_Gebiete | Gebiet
    /// alle anderen Zeilen:    OPS-Kode | LfdNummer | Richtzahl | UntBehMethode
    /// </summary>
    public class RichtlinienZuordnungImporter : ImporterExporter
    {
        private const string FormName = "Wizards_ImportRichtlinien_RichtlinienZuordnungImporter";
        public const string Version = "1";
        private int _ID_Gebiete;

        public RichtlinienZuordnungImporter(BusinessLayer b, ProgressBar progressBar)
            : base(b, progressBar)
        {
        }

        public void Initialize(int ID_Gebiete, string fileName)
        {
            _ID_Gebiete = ID_Gebiete;
            _fileName = fileName;
        }

        private bool Check()
        {
            bool success = true;

            DataView view = _businessLayer.GetRichtlinienOpsKodes(_ID_Gebiete, true);
            if (view.Table.Rows.Count > 0)
            {
                if (!_businessLayer.Confirm(GetText(FormName, "msg1")))
                {
                    success = false;
                }
            }

            return success;
        }

        public bool Import()
        {
            bool success = true;

            if (!Check())
            {
                success = false;
                goto _exit;
            }

            TheProgressBar.Maximum = Tools.CountLines(Encoding.Unicode, _fileName);
            TheProgressBar.Visible = true;

            StreamReader reader = null;

            try
            {
                reader = new StreamReader(_fileName, Encoding.Unicode);

                // Erste Zeile der Daten enthält die Signatur
                if (_businessLayer.CheckTextFileSignature(reader, BusinessLayer.FileSignatureOPSKodesRichtlinien, Version))
                {
                    // Zweite Zeile der Daten enthält das Gebiet
                    string line = reader.ReadLine();

                    try
                    {
                        _businessLayer.OpenDatabaseForImport();

                        while (line != null)
                        {
                            Progress();

                            line = reader.ReadLine();
                            if (line != null)
                            {
                                ImportLine(line);
                            }
                        }
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
            catch (Exception ex)
            {
                _businessLayer.MessageBox(string.Format(GetText(FormName, "msg2"), _fileName, ex.Message));
                success = false;
                goto _exit;
            }
            finally
            {
                TheProgressBar.Value = TheProgressBar.Maximum;

                if (reader != null)
                {
                    reader.Close();
                }
            }

            _exit:

            return success;
        }

        private void ImportLine(string line)
        {
            string[] arLine = line.Split('|');

            if (arLine.Length == 4)
            {
                string opsKode = arLine[0];
                string strLfdNummer = arLine[1];
                string strRichtzahl = arLine[2];
                //string untBehMethode = arLine[3];
                int nRichtzahl;
                int nLfdNummer;
                int ID_Richtlinien;

                if (Int32.TryParse(strLfdNummer, out nLfdNummer))
                {
                    if (Int32.TryParse(strRichtzahl, out nRichtzahl))
                    {
                        DataRow richtlinie = _businessLayer.GetRichtlinieForLfdNummerGebiet(_ID_Gebiete, nLfdNummer, true);

                        if (richtlinie != null)
                        {
                            ID_Richtlinien = ConvertToInt32(richtlinie["ID_Richtlinien"]);

                            int count = _businessLayer.GetRichtlinienOpsKodesCount(ID_Richtlinien, opsKode);
                            if (count == 0)
                            {
                                //
                                // Nur einfügen, wenn es diesen Eintrag noch nicht gibt
                                //
                                DataRow row = _businessLayer.CreateDataRowRichtlinienOpsKodes();
                                row["ID_Richtlinien"] = ID_Richtlinien;
                                row["OPS-Kode"] = opsKode;

                                _businessLayer.InsertRichtlinienOpsKodes(row);
                            }
                        }
                    }
                }
            }
        }
    }
}
