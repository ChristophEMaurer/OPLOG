using System;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

using Utility;

namespace Operationen.Wizards.ImportRichtlinienZuordnung
{
    public partial class RichtlinienValidateView : OperationenForm
    {
        new private const string FormName = "Wizards_ImportRichtlinienZuordnung_RichtlinienValidateView";
        private int _ID_Gebiete;
        private string _fileName;

        Dictionary<int, string> _vorhandeneRichtlinien = new Dictionary<int, string>();
        Dictionary<int, string> _neueRichtlinien = new Dictionary<int, string>();

        public RichtlinienValidateView(BusinessLayer businessLayer, int ID_Gebiete, string fileName)
            : base(businessLayer)
        {
            _ID_Gebiete = ID_Gebiete;
            _fileName = fileName;

            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            this.Text = AppTitle(GetText(FormName, "title"));

            SetInfoText(lblInfo, GetText(FormName, "info"));

            lblInfo2.ForeColor = Color.Red;
            lblInfo2.Text = GetText(FormName, "info2");

            chkAccept.Checked = false;
            chkAccept.Text = GetText(FormName, "info3");

            if (BusinessLayer.CheckTextFileSignature(_fileName, BusinessLayer.FileSignatureOPSKodesRichtlinien, RichtlinienZuordnungImporter.Version))
            {
                InitRichtlinien(lvVorhandeneRichtlinien);
                PopulateVorhandeneRichtlinien();

                InitRichtlinien(lvNeueRichtlinien);
                PopulateNeueRichtlinien();
            }
            else
            {
                Close();
            }
        }

        private void PopulateVorhandeneRichtlinien()
        {
            DataRow gebiet = BusinessLayer.GetGebiet(_ID_Gebiete);
            lblVorhandenesGebiet2.Text = (string)gebiet["Gebiet"];

            DataView dataview = BusinessLayer.GetRichtlinien(_ID_Gebiete);

            _vorhandeneRichtlinien.Clear();
            lvVorhandeneRichtlinien.Items.Clear();
            lvVorhandeneRichtlinien.BeginUpdate();

            foreach (DataRow dataRow in dataview.Table.Rows)
            {
                int lfdNummer = ConvertToInt32(dataRow["LfdNummer"]);
                string untBehMethode = (string)dataRow["UntBehMethode"];

                if (!_vorhandeneRichtlinien.ContainsKey(lfdNummer))
                {
                    _vorhandeneRichtlinien.Add(lfdNummer, untBehMethode);
                }

                ListViewItem lvi = new ListViewItem(dataRow["LfdNummer"].ToString());
                lvi.Tag = ConvertToInt32(dataRow["ID_Richtlinien"]);

                AddRichtzahl(lvi, dataRow["Richtzahl"].ToString());
                AddRichtlinie(lvi, (string)dataRow["UntBehMethode"], false);

                lvVorhandeneRichtlinien.Items.Add(lvi);
            }
            lvVorhandeneRichtlinien.EndUpdate();

            SetGroupBoxText(lvVorhandeneRichtlinien, grpVorhandeneRichtlinien, GetText(FormName, "msg1") + " " + (string)gebiet["Gebiet"]);
        }

        private bool CheckRichtlinie(int lfdNummerNeu, string untBehMethodeNeu)
        {
            bool success = false;

            if (_vorhandeneRichtlinien.ContainsKey(lfdNummerNeu))
            {
                string untBehMethodeVorhanden = _vorhandeneRichtlinien[lfdNummerNeu];

                if (untBehMethodeNeu == untBehMethodeVorhanden)
                {
                    // Die Richtlinie aus der Datei, die importiert wird, muss schon vorhanden
                    // sein, und der Text muss identisch sein.
                    success = true;
                }
            }
            return success;
        }

        public bool PopulateNeueRichtlinienFromFile()
        {
            bool success = true;

            StreamReader reader = null;

            try
            {
                lvNeueRichtlinien.Items.Clear();
                lvNeueRichtlinien.BeginUpdate();

                reader = new StreamReader(_fileName, Encoding.Unicode);
                string line;

                // Erste Zeile der Daten enthält die Signatur
                if (BusinessLayer.CheckTextFileSignature(reader, BusinessLayer.FileSignatureOPSKodesRichtlinien, RichtlinienZuordnungImporter.Version))
                {
                    // Zweite Zeile der Daten enthält das Gebiet
                    line = reader.ReadLine();
                    string[] arLine = line.Split('|');
                    if (arLine.Length == 2)
                    {
                        lblNeuesGebiet2.Text = arLine[1];
                    }

                    if (lblNeuesGebiet2.Text != lblVorhandenesGebiet2.Text)
                    {
                        lblNeuesGebiet2.ForeColor = Color.Red;
                    }

                    while (line != null)
                    {
                        line = reader.ReadLine();
                        if (line != null)
                        {
                            arLine = line.Split('|');

                            if (arLine.Length == 4)
                            {
                                string strLfdNummer = arLine[1];
                                string strRichtzahl = arLine[2];
                                string untBehMethode = arLine[3];
                                int nRichtzahl;
                                int nLfdNummer;

                                if (Int32.TryParse(strLfdNummer, out nLfdNummer))
                                {
                                    if (Int32.TryParse(strRichtzahl, out nRichtzahl))
                                    {
                                        if (!_neueRichtlinien.ContainsKey(nLfdNummer))
                                        {
                                            _neueRichtlinien.Add(nLfdNummer, untBehMethode);

                                            ListViewItem lvi = new ListViewItem(nLfdNummer.ToString());

                                            AddRichtzahl(lvi, nRichtzahl.ToString());
                                            AddRichtlinie(lvi, untBehMethode, false);

                                            if (!CheckRichtlinie(nLfdNummer, untBehMethode))
                                            {
                                                lvi.ForeColor = Color.Red;
                                            }

                                            lvNeueRichtlinien.Items.Add(lvi);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
                MessageBox(string.Format(GetText(FormName, "error1"), _fileName));
                success = false;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }

                lvNeueRichtlinien.EndUpdate();
                SetGroupBoxText(lvNeueRichtlinien, grpNeueRichtlinien, GetText(FormName, "grpNeueRichtlinien"));
            }

            return success;
        }


        private void PopulateNeueRichtlinien()
        {
            PopulateNeueRichtlinienFromFile();
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            if (chkAccept.Checked)
            {
                DialogResult = DialogResult.OK;
            }
            else
            {
                string msg = string.Format(CultureInfo.InvariantCulture, GetText(FormName, "msg2"),
                    chkAccept.Text, cmdCancel.Text);

                MessageBox(msg);
            }
        }

        private static void SelectOtherIdenticalNr(ListView lv, ListView lvOther)
        {
            if (lv.SelectedItems.Count > 0)
            {
                string nr = lv.SelectedItems[0].SubItems[0].Text;

                for (int i = 0; i < lvOther.Items.Count; i++)
                {
                    if (nr == lvOther.Items[i].SubItems[0].Text)
                    {
                        int otherIndex = lvOther.Items[i].Index;

                        lvOther.SelectedIndices.Clear();
                        lvOther.SelectedIndices.Add(otherIndex);
                        lvOther.EnsureVisible(otherIndex);
                        break;
                    }
                }
            }
        }

        private void lvVorhandeneRichtlinien_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            SelectOtherIdenticalNr(lvVorhandeneRichtlinien, lvNeueRichtlinien);
        }

        private void lvNeueRichtlinien_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            SelectOtherIdenticalNr(lvNeueRichtlinien, lvVorhandeneRichtlinien);
        }
    }
}