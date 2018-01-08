using System;
using System.Data;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Globalization;

namespace Operationen.Wizards.InstallLicense
{
    public class LicenseInstaller : ImporterExporter
    {
        private const string FormName = "Wizards_InstallLicense_LicenseInstaller";

        public LicenseInstaller(BusinessLayer b, ProgressBar progressBar)
            : base(b, progressBar)
        {
        }

        public bool Install()
        {
            bool success = true;
            string dst = StartupPath + Path.DirectorySeparatorChar + BusinessLayer.LicenseFileName;

            TheProgressBar.Visible = true;

            Progress();

            try
            {
                File.Copy(_fileName, dst, true);
            }
            catch
            {
                string msg = string.Format(CultureInfo.InvariantCulture, GetText(FormName, "error1"), _fileName, dst);

                _businessLayer.MessageBox(msg);
                success = false;
                goto _exit;
            }

            _exit:

            TheProgressBar.Value = TheProgressBar.Maximum;

            return success;
        }
    }
}
