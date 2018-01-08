using System;
using System.Data;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace Operationen.Wizards.ExportRichtlinienZuordnung
{
    public class RichtlinienExporterZuordnung : ImporterExporter
    {
        private const string Version = "1";
        private int _ID_Gebiete;

        public RichtlinienExporterZuordnung(BusinessLayer b, ProgressBar progressBar)
            : base(b, progressBar)
        {
        }

        public void Initialize(int ID_Gebiete, string fileName)
        {
            _ID_Gebiete = ID_Gebiete;
            _fileName = fileName;
        }

        public bool Export()
        {
            bool success = true;

            StreamWriter writer = null;

            try
            {
                writer = new StreamWriter(_fileName, false, Encoding.Unicode);

                string signature = BusinessLayer.FileSignatureOPSKodesRichtlinien + "|" + Version;
                writer.WriteLine(signature);


                DataRow gebiet = _businessLayer.GetGebiet(_ID_Gebiete);
                writer.WriteLine(_ID_Gebiete.ToString() + "|" + (string)gebiet["Gebiet"]);

                DataView view = _businessLayer.GetRichtlinienOpsKodes(_ID_Gebiete, true);
                foreach (DataRow row in view.Table.Rows)
                {
                    string line = (string)row["OPS-Kode"] + "|" + row["LfdNummer"].ToString() + "|"+ row["Richtzahl"].ToString() + "|" + (string)row["UntBehMethode"];
                    writer.WriteLine(line);
                }
            }
            catch
            {
                _businessLayer.MessageBox(string.Format("Die Datei {0} konnte nicht geschrieben werden.", _fileName));
                success = false;
                goto _exit;
            }
            finally
            {
                if (writer != null)
                {
                    writer.Flush();
                    writer.Close();
                }
            }

            _exit:

            return success;
        }
    }
}
