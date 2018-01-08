using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

using Utility;
using Windows.Forms;

namespace Operationen
{
    public partial class OperationenSummaryView : OperationenForm
    {
        public OperationenSummaryView(BusinessLayer businessLayer) :
            base(businessLayer)
        {
            InitializeComponent();

            this.Text = AppTitle(GetText("title"));
            SetInfoText(lblInfo);

            InitListViews();
        }

        private void InitListViews()
        {
            DefaultListViewProperties(lvChirurgen);

            lvChirurgen.Columns.Add(GetText("s_nachname"), 100, HorizontalAlignment.Left);
            lvChirurgen.Columns.Add(GetText("s_vorname"), 100, HorizontalAlignment.Left);
            lvChirurgen.Columns.Add(GetText("s_opfirst"), 100, HorizontalAlignment.Left);
            lvChirurgen.Columns.Add(GetText("s_oplast"), 100, HorizontalAlignment.Left);
            lvChirurgen.Columns.Add(GetText("s_opcount"), -2, HorizontalAlignment.Left);

            DefaultListViewProperties(lvData);

            lvData.Columns.Add(GetText("d_name"), 250, HorizontalAlignment.Left);
            lvData.Columns.Add(GetText("d_value"), 100, HorizontalAlignment.Left);
            lvData.Columns.Add(GetText("d_comment"), -2, HorizontalAlignment.Left);
        }

        private void AddRow(string name, string value, string comment)
        {
            ListViewItem lvi = new ListViewItem(name);
            lvi.SubItems.Add(value);
            lvi.SubItems.Add(comment);

            lvData.Items.Add(lvi);
        }

        private void PopulateListViews()
        {
            DataRow rowDatum;

            //
            // ListView Data
            //
            rowDatum = BusinessLayer.GetChirurgenOperationenFirst();
            AddRow(GetText("opfirst"), Tools.DBNullableDateTime2DateString(rowDatum["Datum"]), "");

            rowDatum = BusinessLayer.GetChirurgenOperationenLast();
            AddRow(GetText("oplast"), Tools.DBNullableDateTime2DateString(rowDatum["Datum"]), "");

            long count = BusinessLayer.GetChirurgenOperationenCount();
            AddRow(GetText("opcount"), count.ToString(), "");

            count = BusinessLayer.GetLogTableCount();

            string name = string.Format(CultureInfo.InvariantCulture, GetText("logtable_name"), 
                OperationenLogbuchView.TheMainWindow.cmdLogView.Text);
            string comment = string.Format(CultureInfo.InvariantCulture, GetText("logtable_warn"), Command_LogView);
            AddRow(name, count.ToString(), comment);

            //
            // Data import logfiles
            //
            long numFiles;
            string totalSize;
            BusinessLayer.GetFilesinDirectoryInfo(BusinessLayer.PathLogfiles, out numFiles, out totalSize);
            if (numFiles >= 0)
            {
                name = GetText("fileInfoLogfilesCount");
                comment = string.Format(CultureInfo.InvariantCulture, GetText("fileInfoLogfilesComment"), BusinessLayer.PathLogfiles);
                AddRow(name, Convert.ToString(numFiles, CultureInfo.InvariantCulture), comment);

                name = GetText("fileInfoLogfilesTotalSize");
                AddRow(name, totalSize, "");
            }

            //
            // Number of Plugins
            //
            BusinessLayer.GetFilesinDirectoryInfo(BusinessLayer.PathPlugins, out numFiles, out totalSize);
            if (numFiles >= 0)
            {
                name = GetText("fileInfoPluginsCount");
                AddRow(name, Convert.ToString(numFiles, CultureInfo.InvariantCulture), "");
            }

            //
            // License data
            //
            name = GetText("infoFreeSerials");
            int noSerialNumbers = BusinessLayer.GetCountSerialNumbers();
            AddRow(name, Convert.ToString(noSerialNumbers, CultureInfo.InvariantCulture), "");

            name = GetText("infoValidLicense");
            Dictionary<string, string> licenseData = new Dictionary<string, string>();
            bool validLicense = BusinessLayer.VerifyLicense(licenseData, true);
            if (validLicense)
            {
                AddRow(name, GetText("infoYes"), "");
            }
            else
            {
                AddRow(name, GetText("infoNo"), "");
            }

            //
            // Chirurgen
            //
            DataView dv = BusinessLayer.GetChirurgenAlle();
            foreach (DataRow row in dv.Table.Rows)
            {
                Application.DoEvents();

                ListViewItem lvi = new ListViewItem((string)row["Nachname"]);
                lvi.SubItems.Add((string)row["Vorname"]);

                DataRow row2 = BusinessLayer.GetChirurgenOperationenFirst((int)row["ID_Chirurgen"]);
                lvi.SubItems.Add(Tools.DBNullableDateTime2DateString(row2["Datum"]));

                row2 = BusinessLayer.GetChirurgenOperationenLast((int)row["ID_Chirurgen"]);
                lvi.SubItems.Add(Tools.DBNullableDateTime2DateString(row2["Datum"]));

                count = BusinessLayer.GetChirurgenOperationenCount((int)row["ID_Chirurgen"]);
                lvi.SubItems.Add(count.ToString());

                lvChirurgen.Items.Add(lvi);
            }
        }

        private void OperationenSummaryView_Load(object sender, EventArgs e)
        {
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void OperationenSummaryView_Shown(object sender, EventArgs e)
        {
            MayFormClose = false;
            PopulateListViews();
            MayFormClose = true;
        }
    }
}
