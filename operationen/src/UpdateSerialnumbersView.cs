using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

using Operationen.Wizards.InstallLicense;

namespace Operationen
{
    public partial class UpdateSerialnumbersView : OperationenForm
    {
        public UpdateSerialnumbersView(BusinessLayer businessLayer)
            : base(businessLayer)
        {
            InitializeComponent();
        }


        private void UpdateSerialnumbersView_Load(object sender, EventArgs e)
        {
            this.Text = AppTitle(GetText("title"));

            Populate();
        }

        private void Populate()
        {
            long countNeeded = 0;
            long countUnused = 0;

            countNeeded = BusinessLayer.GetCountChirurgenWithInvalidSerialNumbers() - BusinessLayer.NumberOfFreeUsers;
            countUnused = BusinessLayer.GetCountUnusedSerialNumbers();

            if (countNeeded < 0)
            {
                countNeeded = 0;
            }

            txtSerialExisting.Text = countUnused.ToString();
            txtSerialNeeded.Text = countNeeded.ToString();

            cmdAuto.Enabled = (countNeeded > 0) && (countUnused > 0);

            if (countNeeded > 0)
            {
                SetInfoText(lblInfo, string.Format(CultureInfo.InvariantCulture, GetText("info_error"), BusinessLayer.NumberOfFreeUsers));
            }
            else
            {
                SetInfoText(lblInfo, string.Format(CultureInfo.InvariantCulture, GetText("info_ok"), BusinessLayer.NumberOfFreeUsers));
            }
        }

        private void cmdSerialNumberView_Click(object sender, EventArgs e)
        {
            SerialNumbersView dlg = new SerialNumbersView(BusinessLayer, true);
            dlg.ShowDialog();

            Populate();
        }

        private void cmdInstallLicense_Click(object sender, EventArgs e)
        {
            InstallLicenseWizard dlg = new InstallLicenseWizard(BusinessLayer);
            dlg.ShowDialog();
            Populate();
        }

        /// <summary>
        /// Alle Seriennummern verteilen bis auf zwei Benutzer, die sind ja umsonst
        /// </summary>
        private void AutoUpdate()
        {
            long countNeeded = BusinessLayer.GetCountChirurgenWithInvalidSerialNumbers() - BusinessLayer.NumberOfFreeUsers;

            DataView chirurgen = BusinessLayer.GetChirurgenAlle();

            foreach (DataRow chirurg in chirurgen.Table.Rows)
            {
                if (!BusinessLayer.ValidateSerialFormat((string)chirurg["Lizenzdaten"]))
                {
                    string serial = BusinessLayer.GetFirstUnusedSerialNumber();
                    if (string.IsNullOrEmpty(serial))
                    {
                        break;
                    }
                    else
                    {
                        chirurg["Lizenzdaten"] = serial;
                        if (BusinessLayer.UpdateChirurgenSerialNumber(chirurg))
                        {
                            BusinessLayer.DeleteSerialNumber(serial);
                        }
                        countNeeded--;
                        if (countNeeded <= 0)
                        {
                            break;
                        }
                    }
                }
            }

        }
        private void cmdSerialExisting_Click(object sender, EventArgs e)
        {
            if (Confirm(GetText("confirm_autoupdate")))
            {
                MayFormClose = false;

                AutoUpdate();
                Populate();

                MayFormClose = true;

            }
        }
    }
}