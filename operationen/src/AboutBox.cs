// Translation: done

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;
using System.Threading;
using System.Data;
using System.Timers; 
using System.Drawing.Drawing2D;
using System.IO;
using System.Globalization;
using System.Text;

using Utility;
using Operationen.Wizards.CreateCustomerData;
using CMaurer.Operationen.AppFramework;

namespace Operationen
{
    partial class AboutBox : OperationenForm
    {
        private const int Offset = 350;
        private bool _showingDetails = false;

        public AboutBox(BusinessLayer businessLayer)
            : base(businessLayer)
        {
            InitializeComponent();

#if !DEBUG
            cmdDetails.Visible = false;
#endif

        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            if (_timer != null)
            {
                _timer.Enabled = false;
                _timer.Stop();
                _timer.Dispose();
                _timer = null;
            }

            Close();
        }

        private void AboutBox_Load(object sender, EventArgs e)
        {
            string tmp = "";

            Text = GetText("about") + AppTitle();

#if urologie
            tmp = AppTitle() + " Urologie\r" + "Version " + BusinessLayer.VersionString;
#elif gynaekologie
            tmp = AppTitle() + " Gynäkologie\r" + "Version " + BusinessLayer.VersionString;
#else
            tmp = BusinessLayer.UrlHomepage + "\r" + AppTitle() + " Chirurgie\r" + "Version " + BusinessLayer.VersionString;
#endif
#if targetplatform_x86
            tmp = tmp + " (x86)";
#endif
            lblProduct.Text = tmp;

            lblCopyright.Text = "Copyright © Christoph Maurer"
                + "\rElisabethenstraße 38"
                + "\r61184 Karben"
                ;
            pnlCredits.Visible = false;

            PopulateInfos();
        }

        private void AddRow(string key, string value)
        {
            AddRow(key, value, false);
        }

        private void AddRow(string key, string value, bool bold)
        {
            ListViewItem lvi = new ListViewItem(key);
            if (bold)
            {
                lvi.Font = new Font(lvInfos.Font, FontStyle.Bold);
            }
            lvi.SubItems.Add(value);
            lvInfos.Items.Add(lvi);
        }

        private void AddLicenseNode(string left, Dictionary<string, string> ht, string key)
        {
            string value;
            if (ht.TryGetValue(key, out value))
            {
                AddRow(left, value);
            }
        }

        private void PopulateInfos()
        {
            lvInfos.BeginUpdate();
            lvInfos.Clear();

            DefaultListViewProperties(lvInfos);
            lvInfos.Sorting = SortOrder.None;

            lvInfos.Columns.Add("", 160, HorizontalAlignment.Left);
            lvInfos.Columns.Add("", -2, HorizontalAlignment.Left);

            System.OperatingSystem os = System.Environment.OSVersion;

            AddRow(GetText("group_logbook"), "", true);
            AddRow(GetText("Benutzer"), BusinessLayer.CurrentUser_UserID);
            AddRow(GetText("version"), BusinessLayer.VersionString);
            string installedVersion = BusinessLayer.GetConfigValue("InstalledVersion");
            if (!string.IsNullOrEmpty(installedVersion))
            {
                AddRow(GetText("installedVersion"), installedVersion);
            }
            AddRow(GetText("apppath"), BusinessLayer.AppPath);
            AddRow(GetText("data"), BusinessLayer.PathData);
            AddRow(GetText("path_plugins"), BusinessLayer.PathPlugins);
            AddRow(GetText("Datenbank"), BusinessLayer.DatabasePath);

            AddRow("", "");
            AddRow(GetText("Kundendaten"), "", true);
            AddRow(GetText("Klinik"), BusinessLayer.GetConfigValue(CreateCustomerDataWizardPage.KEY_CD_KRANKENHAUS));
            AddRow(GetText("PLZ"), BusinessLayer.GetConfigValue(CreateCustomerDataWizardPage.KEY_CD_PLZ));
            AddRow(GetText("Straße"), BusinessLayer.GetConfigValue(CreateCustomerDataWizardPage.KEY_CD_STRASSE));

            AddRow("", "");
            AddRow(GetText("license_licenceData"), "", true);

            StringBuilder displayError = new StringBuilder();
            Dictionary<string, string> ht = new Dictionary<string, string>();
            if (BusinessLayer.VerifyLicense(ht, true, displayError))
            {
                //
                // Erzeugt werden die Werte auf der Webseite mit www/App_Code/UserData.
                // In A boutBox werden die Werte angezeigt.
                // Der Dictionary wird gefüllt in BusinessLeyer.
                // suchen nach __$xmllicense$__
                //
                AddLicenseNode(GetText("license_userLastName"), ht, "lastName");
                AddLicenseNode(GetText("license_userFirstName"), ht, "firstName");
                AddLicenseNode(GetText("license_company"), ht, "company");
                AddLicenseNode(GetText("license_street"), ht, "street");
                AddLicenseNode(GetText("license_city"), ht, "city");
                AddLicenseNode(GetText("license_licenseCreateDate"), ht, "licenseCreateDate");
                AddLicenseNode(GetText("license_licenseExpireDate"), ht, "licenseExpireDate");
            }
            else
            {
                AddRow(GetText("licenseError"), displayError.ToString());
            }

            AddRow("", "");
            AddRow(GetText("group_computer"), "", true);
            AddRow(GetText("machinename"), System.Environment.MachineName);
            AddRow(GetText("user"), System.Environment.MachineName + System.IO.Path.DirectorySeparatorChar + System.Environment.UserName);
            string text =  os.VersionString + " (" +  os.Platform.ToString() + ", ";
            if (IntPtr.Size == 4)
            {
                text = text + " 32Bit";
            }
            else
            {
                text = text + " 64Bit";
            }
            text = text + ")";
            AddRow(GetText("Betriebssystem"), text);
            AddRow(GetText("framework"), System.Environment.Version.ToString());

            AddRow("", "");
            AddRow(GetText("group_software"), "", true);
            // Ab 3.4 geht das: AddRow("Elegant UI Version", Elegant.Ui.AssemblyInfo.Version);
            AddRow("Elegant UI", typeof(Elegant.Ui.Ribbon).Assembly.GetName().Version.ToString());
            AddRow("System.Data.OleDb", typeof(System.Data.OleDb.OleDbFactory).Assembly.GetName().Version.ToString());
            AddRow("System.Data.SqlClient", typeof(System.Data.SqlClient.SqlClientFactory).Assembly.GetName().Version.ToString());
            AddRow("MySql.Data.MySqlClient", typeof(MySql.Data.MySqlClient.MySqlClientFactory).Assembly.GetName().Version.ToString());
            //AddRow("System.Data.OracleClient", typeof(System.Data.OracleClient.OracleClientFactory).Assembly.GetName().Version.ToString());
        }

        private void llblWWW_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LaunchInternetBrowser(BusinessLayer.UrlHomepage);
        }

        private void pbIcon_Click(object sender, EventArgs e)
        {
            LaunchInternetBrowser(BusinessLayer.UrlHomepage);
        }

        private void llLicence_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                string filename = Application.StartupPath + System.IO.Path.DirectorySeparatorChar + "license.txt";
                if (File.Exists(filename))
                {
                    System.Diagnostics.Process.Start("notepad.exe", filename);
                }
                else
                {
                    MessageBox(GetTextControlMissingFile(filename));
                }
            }
            catch
            {
            }
        }

        private void llChanges_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            BusinessLayer.ShowChangeLog();
        }

        private const int _initialYOffset = 50;
        System.Timers.Timer _timer = null;
        int _dy = 0;

        string[] text = new string[] { 
            "Software development:", 
            "Christoph Maurer", 
            "",
            "Für Teresa, Charlotte",
            "und Johannes",
            "",
            BusinessLayer.UrlHomepage
        };
        static int i = 0;

        private void ShowCredits()
        {
            lvInfos.Visible = false;
            pnlCredits.Visible = true;

            pnlCredits.Location = lvInfos.Location;
            pnlCredits.Size = lvInfos.Size;
            pnlCredits.BackColor = Color.Black;
            pnlCredits.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;

            pnlCredits.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlCredits_Paint);
            pnlCredits.MouseEnter += delegate(System.Object o, System.EventArgs e) { _timer.Enabled = false; };
            pnlCredits.MouseLeave += delegate(System.Object o, System.EventArgs e) { _timer.Enabled = true; };

            _dy = pnlCredits.Height - _initialYOffset;

            _timer = new System.Timers.Timer();
            _timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            _timer.Interval = 100;
            _timer.Enabled = true;
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            _dy -= 1;
            if (_dy < text.Length * -30)
            {
                _dy = pnlCredits.Height - _initialYOffset;
            }
            pnlCredits.Invalidate();
        }

        private void pnlCredits_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            using (GraphicsPath pth = new GraphicsPath())
            {
                FontFamily ff = new FontFamily("Verdana");

                Font stringFont = new Font(ff, 16);

                for (int y = 0; y < text.Length; y++)
                {
                    string s = text[y];

                    int offset = (pnlCredits.ClientSize.Width - (s.Length * 12)) / 2;
                    if (offset < 0)
                    {
                        offset = 0;
                    }
                    pth.AddString(s, ff, 0, 20, new Point(offset, _dy + 30 * y), StringFormat.GenericTypographic);
                }

                PointF[] points = new PointF[]{ 
                  new PointF(pnlCredits.ClientSize.Width / 4, 0), 
                  new PointF(pnlCredits.ClientSize.Width - pnlCredits.ClientSize.Width / 4, 0), 
                  new PointF(0,pnlCredits.ClientSize.Height), 
                  new PointF(pnlCredits.ClientSize.Width, pnlCredits.ClientSize.Height) 
                    };

                pth.Warp(points, new RectangleF(0, 0, pnlCredits.ClientSize.Width,pnlCredits.ClientSize.Height));

                e.Graphics.FillPath(Brushes.White, pth);
            }
        }

        /// <summary>
        /// double click left, double click right, double click left: show credits.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AboutBox_DoubleClick(object sender, EventArgs e)
        {
            MouseEventArgs me = e as MouseEventArgs;

             if (me != null)
            {
                if (i == 0 && me.Button == MouseButtons.Left)
                {
                    i = 1;
                }
                else if (i == 1 && me.Button == MouseButtons.Right)
                {
                    i = 2;
                }
                else if (i == 2 && me.Button == MouseButtons.Left)
                {
                    i = 0;
                    pnlCredits.Visible = true;
                    ShowCredits();
                }
                else
                {
                    i = 0;
                }
            }
        }

        private void ShowDetails()
        {
            lvInfos.BeginUpdate();
            lvInfos.Clear();

            DefaultListViewProperties(lvInfos);
            lvInfos.Sorting = SortOrder.None;
            lvInfos.Scrollable = true;
            lvInfos.Columns.Add("", -2, HorizontalAlignment.Left);

            SetDetailsText();
        }

        private void cmdDetails_Click(object sender, EventArgs e)
        {
            _showingDetails = !_showingDetails;

            if (_showingDetails)
            {
                cmdDetails.Text = "<<";
                ShowDetails();
            }
            else
            {
                cmdDetails.Text = ">>";
                PopulateInfos();
            }
        }

        private void AddDetail(string text, bool bold)
        {
            ListViewItem lvi = new ListViewItem(text);
            if (bold)
            {
                lvi.Font = new Font(lvInfos.Font, FontStyle.Bold);
            }
            lvInfos.Items.Add(lvi);
        }

        private void AddDetail(string key)
        {
            AddDetail(key, false);
        }

        private void SetDetailsText()
        {
            AddDetail(BusinessLayer.UrlHomepageNoHttp);
            AddDetail("'" + BusinessLayerCommon.ProgramTitle + "' Windows version is programmed in C# using the .NET Framework 2.0");
            AddDetail("Web pages are ASPX pages programmed in C# using the .NET Framework 2.0");
            AddDetail("The super-sophisticated framework supports databases 'MS Access', 'MS SQLServer', 'SQLAzure' and 'MySQL'");
            AddDetail("Source code is maintained using 'Subversion' and is checked using 'Microsoft FxCop'");
            AddDetail("Texts are translated with the help of 'LEO' using 'ResEx'");
            AddDetail(".PDF files are created with 'CIB pdf brewer' and 'FreePDF'");
            AddDetail(".CHM help files are created from .HTML files with 'HTML Help Workshop'");
            AddDetail(".CHM source code documentation is generated with 'Sandcastle Help File Builder'");
            AddDetail("Color codes are examined using 'pkColorPicker'");
            AddDetail("Changes are identified using 'windiff', .NET code is inspected with '.NET Reflector' and ILSpy");
            AddDetail("Build scripts use 'msbuild.exe' and 'cmd.exe'");
            AddDetail("Files are packed using '7zip'");
            AddDetail("Setup is created with 'WinZip Self-Extractor'");
            AddDetail("Files are uploaded to the web using 'FileZilla'");
            AddDetail("Backups are created using 'Robocopy'");

            AddDetail("'" + BusinessLayerCommon.ProgramTitle + "' version for iPhone/iPad/iPod Touch");
            AddDetail("    was first programmed in Objective-C on MacOS 10.6.5 (Snowleopard) with XCode 3.2.5");

            AddDetail("The Windows version is tested on Windows 2000/XP/Vista/7/8");
            AddDetail("    The first 64Bit OS used was Windows 8.1");

            AddDetail("The version for Apple devices is tested using various simulators in XCode");
            AddDetail("    and on selected live cell phones");
        }
    }
}
