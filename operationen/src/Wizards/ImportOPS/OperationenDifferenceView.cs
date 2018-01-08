using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace Operationen.Wizards.ImportOPS
{
    public partial class OperationenDifferenceView : OperationenForm
    {
        private new const string FormName = "Wizards_ImportOPS_OperationenDifferenceView";

        private Color colorDifferent = Color.Blue;
        private string colorDifferentText;

        private Color colorMissingInImport = Color.Red;
        private string colorMissingInImportText;

        private Color colorMissingInDatabase = Color.Green;
        private string colorMissingInDatabaseText;

        private enum ImportState
        {
            New = 1,
            Different = 2,
            Missing = 3
        }

        OPSImporter _importer;

        Dictionary<string, string> _database = new Dictionary<string, string>();
        Dictionary<string, string> _import = new Dictionary<string, string>();

        string _fileName;
        string _format;

        bool _mayClose = false;

        public OperationenDifferenceView(BusinessLayer businessLayer, string fileName, string format)
            : base(businessLayer)
        {
            colorDifferentText = GetText(FormName, "blau");
            colorMissingInImportText = GetText(FormName, "rot");
            colorMissingInDatabaseText = GetText(FormName, "gruen");

            InitializeComponent();

            EnableControls(false);

            SetInfoText(lblInfo, GetText(FormName, "info"));

            SetInfoText(lblInfo2, string.Format(CultureInfo.InvariantCulture, GetText(FormName, "info2"), cmdUpdateAll.Text));

            this.Text = AppTitle(GetText(FormName, "title"));

            cmdUpdateDifferent.ForeColor = colorDifferent;
            cmdUpdateNew.ForeColor = colorMissingInDatabase;

            lblExists.Text = GetText(FormName, "exists");

            lblDifferent.ForeColor = colorDifferent;
            lblDifferent.Text = string.Format(CultureInfo.InvariantCulture, GetText(FormName, "diff"), colorDifferentText);

            lblMissingInDatabase.ForeColor = colorMissingInDatabase;
            lblMissingInDatabase.Text =  string.Format(CultureInfo.InvariantCulture, GetText(FormName, "new"), colorMissingInDatabaseText);

            lblMissingInImport.ForeColor = colorMissingInImport;
            lblMissingInImport.Text = string.Format(CultureInfo.InvariantCulture, GetText(FormName, "gone"), colorMissingInImportText);

            _fileName = fileName;
            _format = format;
            _importer = new OPSImporter(businessLayer, progressBar);
        }

        private void EnableControls(bool enabled)
        {
            cmdCancel.Enabled = enabled;
            cmdUpdateDifferent.Enabled = enabled;
            cmdUpdateNew.Enabled = enabled;
            cmdUpdateAll.Enabled = enabled;
        }

        private void OperationenDifferenceView_Load(object sender, EventArgs e)
        {
            DefaultListViewProperties(lvDatabase);
            lvDatabase.Sorting = SortOrder.Ascending;
            DefaultListViewProperties(lvImport);
            lvImport.Sorting = SortOrder.Ascending;

            lvDatabase.Columns.Add(GetText(FormName, "opskode"), 200, HorizontalAlignment.Left);
            lvDatabase.Columns.Add(GetText(FormName, "opstext"), -2, HorizontalAlignment.Left);

            lvImport.Columns.Add(GetText(FormName, "opskode"), 100, HorizontalAlignment.Left);
            lvImport.Columns.Add(GetText(FormName, "status"), 100, HorizontalAlignment.Left);
            lvImport.Columns.Add(GetText(FormName, "opstext"), -2, HorizontalAlignment.Left);

            Application.DoEvents();
        }

        private void ReadData()
        {
            _importer.Initialize(_fileName, _format);

            lblProgress.Text = GetText(FormName, "step1");
            _importer.ReadFile(_import);

            // OPS-Text ist nur 255 Zeichen lang, länger geht in Access nicht
            lblProgress.Text = GetText(FormName, "step2");
            _importer.ReadDatabase(_database);
        }

        private void Populate()
        {
            ReadData();

            lvDatabase.Items.Clear();
            lvDatabase.BeginUpdate();

            lvImport.Items.Clear();
            lvImport.BeginUpdate();

            progressBar.Maximum = _database.Keys.Count + _import.Keys.Count;
            progressBar.Value = 0;
            lblProgress.Text = GetText(FormName, "step3");
            
            foreach (string key in _database.Keys)
            {
                _importer.Progress();

                string valueDatabase = _database[key];

                if (_import.ContainsKey(key))
                {
                    // ist in beiden 
                    string valueImport = _import[key];

                    string valueDatebaseStripped = valueDatabase.Replace(" ", "");
                    valueDatebaseStripped = valueDatebaseStripped.Replace("-", "");

                    string valueImportStripped = valueImport.Replace(" ", "");
                    valueImportStripped = valueImportStripped.Replace("-", "");

                    if (!valueDatebaseStripped.Equals(valueDatebaseStripped))
                    {
                        // aber der Text ist anders, nicht nur aufgrund der Leerzeichen
                        ListViewItem lvi = new ListViewItem(key);
                        lvi.SubItems.Add(valueDatabase);
                        lvi.ForeColor = colorDifferent;
                        lvDatabase.Items.Add(lvi);

                        lvi = new ListViewItem(key);
                        lvi.Tag = ImportState.Different;
                        lvi.SubItems.Add(GetText(FormName, "anders"));
                        lvi.SubItems.Add(valueImport);
                        lvi.ForeColor = colorDifferent;
                        lvImport.Items.Add(lvi);
                    }
                }
                else
                {
                    // Ist in der Datenbank, fehlt aber in der neuen Import Datei
                    ListViewItem lvi = new ListViewItem(key);
                    lvi.SubItems.Add(valueDatabase);
                    lvDatabase.Items.Add(lvi);

                    lvi = new ListViewItem(key);
                    lvi.Tag = ImportState.Missing;
                    lvi.SubItems.Add(GetText(FormName, "fehlt"));
                    lvi.SubItems.Add(valueDatabase);
                    lvi.ForeColor = colorMissingInImport;
                    lvImport.Items.Add(lvi);
                }
            }

            foreach (string key in _import.Keys)
            {
                _importer.Progress();

                if (!_database.ContainsKey(key))
                {
                    // Ist in der neuen Import Datei, fehlt aber in der Datenbank
                    // diese werden automatisch hinzugefügt
                    string valueImport = _import[key];
                    ListViewItem lvi = new ListViewItem(key);
                    lvi.Tag = ImportState.New;
                    lvi.SubItems.Add(GetText(FormName, "neu"));
                    lvi.SubItems.Add(valueImport);
                    lvi.ForeColor = colorMissingInDatabase;
                    lvImport.Items.Add(lvi);
                }
            }

            SetGroupBoxTextDatabase();
            SetGroupBoxTextImport();

            lvDatabase.EndUpdate();
            lvImport.EndUpdate();

            lblProgress.Text = GetText(FormName, "fertig");
        }

        private void SetGroupBoxTextDatabase()
        {
            SetGroupBoxText(lvDatabase, grpDatabase, GetText(FormName, "grpDatabase"));
            Application.DoEvents();
        }
        private void SetGroupBoxTextImport()
        {
            SetGroupBoxText(lvImport, grpImport, GetText(FormName, "grpImport"));
            Application.DoEvents();
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void OperationenDifferenceView_Shown(object sender, EventArgs e)
        {
            Application.DoEvents();

            Populate();

            _mayClose = true;
            EnableControls(true);
        }

        private void SelectOther(ListView thisone, ListView other)
        {
            ListViewItem lviThisOne = GetFirstSelectedLVI(thisone);

            ListViewItem lviOther = other.FindItemWithText(lviThisOne.Text);

            if (lviOther != null)
            {
                other.SelectedIndices.Clear();
                other.SelectedIndices.Add(lviOther.Index);
                other.EnsureVisible(lviOther.Index);
            }
        }

        private void lvDatabase_DoubleClick(object sender, EventArgs e)
        {
            SelectOther(lvDatabase, lvImport);
        }

        private void lvImport_DoubleClick(object sender, EventArgs e)
        {
            SelectOther(lvImport, lvDatabase);
        }

        private void InsertAllNew()
        {
            progressBar.Maximum = lvImport.Items.Count;
            progressBar.Value = 0;
            lblProgress.Text = GetText(FormName, "progress1");
            lvImport.BeginUpdate();
            Application.DoEvents();

            int i = 0;
            while (i < lvImport.Items.Count)
            {
                ListViewItem lvi = lvImport.Items[i];
                ImportState tag = (ImportState)lvi.Tag;

                if (lvi.Selected && tag == ImportState.New)
                {
                    string sql =
                        @"
                            INSERT INTO Operationen
                                (
                                [OPS-Kode],
                                [OPS-Text]
                                )
                                VALUES
                                (
                                 @OPSKode,
                                 @OPSText
                                )
                            ";

                    ArrayList sqlParameters = new ArrayList();
                    sqlParameters.Add(SqlParameter("@OPSKode", lvi.Text));
                    sqlParameters.Add(SqlParameter("@OPSText", lvi.SubItems[2].Text));

                    if (0 != BusinessLayer.DatabaseLayer.InsertRecord(sql, sqlParameters, "Operationen"))
                    {
                        lvImport.Items.Remove(lvi);
                        SetGroupBoxTextImport();
                    }
                    else
                    {
                        // hier darf man nie hinkommen, sonst gab es ja einen Datenbank-Fehler!
                        i++;
                    }
                }
                else
                {
                    i++;
                }
                _importer.Progress();
            }

            lvImport.EndUpdate();
            lblProgress.Text = "";
            Application.DoEvents();
        }

        private void InsertAll()
        {
            progressBar.Maximum = lvImport.Items.Count;
            progressBar.Value = 0;
            lblProgress.Text = GetText(FormName, "progress2");
            Application.DoEvents();

            foreach (ListViewItem lvi in lvImport.Items)
            {
                string sql =
                    @"
                        INSERT INTO Operationen
                            (
                            [OPS-Kode],
                            [OPS-Text]
                            )
                            VALUES
                            (
                             @OPSKode,
                             @OPSText
                            )
                        ";

                ArrayList sqlParameters = new ArrayList();
                sqlParameters.Add(SqlParameter("@OPSKode", lvi.Text));
                sqlParameters.Add(SqlParameter("@OPSText", lvi.SubItems[2].Text));

                // Don't ignore insert error or we might be prompted several thousand times!
                if (0 == BusinessLayer.DatabaseLayer.InsertRecord(sql, sqlParameters, "Operationen"))
                {
                    break;
                }

                _importer.Progress();
            }

            lvImport.Items.Clear();
            lblProgress.Text = "";
            Application.DoEvents();
        }

        public IDbDataParameter SqlParameter(string sParameterName, string sValue)
        {
            return BusinessLayer.DatabaseLayer.SqlParameter(sParameterName, sValue);
        }

        private void OperationenDifferenceView_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_mayClose)
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// Alle Einträge aus der Import-Datei, die schon vorhanden sind, sich aber 
        /// im Tetxt unterscheiden, übernehmen.
        /// </summary>
        private void UpdateDifferent()
        {
            EnableControls(false);
            _mayClose = false;

            lvDatabase.BeginUpdate();
            lvImport.BeginUpdate();

            progressBar.Maximum = lvImport.Items.Count;
            progressBar.Value = 0;
            lblProgress.Text = GetText(FormName, "msg1");
            Application.DoEvents();

            int i = 0;
            while (i < lvImport.Items.Count)
            {
                ListViewItem lvi = lvImport.Items[i];
                ImportState tag = (ImportState)lvi.Tag;

                if (lvi.Selected && tag == ImportState.Different)
                {
                    string sql =
                        @"
                            UPDATE Operationen SET
                                [OPS-Text] = @OPSText
                            WHERE 
                                [OPS-Kode] = @OPSKode
                            ";

                    ArrayList sqlParameters = new ArrayList();
                    sqlParameters.Add(SqlParameter("@OPSText", lvi.SubItems[2].Text));
                    sqlParameters.Add(SqlParameter("@OPSKode", lvi.Text));

                    if (0 != BusinessLayer.DatabaseLayer.ExecuteNonQuery(sql, sqlParameters))
                    {
                        ListViewItem lviDatabase = lvDatabase.FindItemWithText(lvi.Text);
                        if (lviDatabase != null)
                        {
                            lvDatabase.Items.Remove(lviDatabase);
                        }

                        lvImport.Items.Remove(lvi);
                    }
                    else
                    {
                        // hier darf man nie hinkommen, sonst gab es ja einen Datenbank-Fehler!
                        i++;
                    }
                }
                else
                {
                    i++;
                }
                _importer.Progress();
            }

            lvDatabase.EndUpdate();
            lvImport.EndUpdate();

            lblProgress.Text = "";
            SetGroupBoxTextDatabase();
            SetGroupBoxTextImport();
            Application.DoEvents();

            EnableControls(true);
            _mayClose = true;

        }
        private void cmdUpdateDifferent_Click(object sender, EventArgs e)
        {
            if (Confirm(GetText(FormName, "confirm1")))
            {
                UpdateDifferent();
            }
        }

        private void cmdUpdateNew_Click(object sender, EventArgs e)
        {
            if (Confirm(GetText(FormName, "confirm2")))
            {
                _mayClose = false;
                this.Cursor = Cursors.WaitCursor;
                EnableControls(false);

                try
                {
                    BusinessLayer.OpenDatabaseForImport();
                    InsertAllNew();
                }
                finally
                {
                    BusinessLayer.CloseDatabaseForImport();
                }

                EnableControls(true);
                this.Cursor = Cursors.Default;
                _mayClose = true;

            }
        }

        private void cmdUpdateAll_Click(object sender, EventArgs e)
        {
            if (Confirm(GetText(FormName, "confirm3")))
            {
                _mayClose = false;
                this.Cursor = Cursors.WaitCursor;
                EnableControls(false);

                try
                {
                    BusinessLayer.OpenDatabaseForImport();
                    InsertAll();
                }
                finally
                {
                    BusinessLayer.CloseDatabaseForImport();
                }

                EnableControls(true);
                this.Cursor = Cursors.Default;
                _mayClose = true;

            }
        }
    }
}
