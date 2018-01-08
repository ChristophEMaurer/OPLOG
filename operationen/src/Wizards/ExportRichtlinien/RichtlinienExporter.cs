using System;
using System.Data;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Globalization;

namespace Operationen.Wizards.ExportRichtlinien
{
    public class RichtlinienExporter : ImporterExporter
    {
        private const string FormName = "Wizards_ExportRichtlinien_Exporter";

        private int _ID_Gebiete;
        private const string Version = "2";

        public RichtlinienExporter(BusinessLayer b, ProgressBar progressBar)
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

                string signature = BusinessLayer.FileSignatureRichtlinien + "|" + Version;
                writer.WriteLine(signature);

                DataView view = _businessLayer.GetRichtlinien(_ID_Gebiete);
                foreach (DataRow row in view.Table.Rows)
                {
                    string line = row["LfdNummer"].ToString() + "|" + row["Richtzahl"].ToString() + "|" + (string)row["UntBehMethode"];
                    writer.WriteLine(line);
                }
            }
            catch
            {
                _businessLayer.MessageBox(string.Format(CultureInfo.InvariantCulture, GetText(FormName, "error"), _fileName));
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

