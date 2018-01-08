using System;
using System.IO;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using Windows.Forms.Wizard;

namespace Operationen.Wizards.ImportOPS
{
    public partial class SelectVersion : ImportOPSWizardPage
    {
        public SelectVersion(BusinessLayer b)
            : base(b, "Wizards_ImportOPS_SelectVersion")
        {
            InitializeComponent();
        }

        protected override string PageName
        {
            get { return GetText("pagename"); }
        }

        protected override string Header1
        {
            get { return GetText("header1"); }
        }

        protected override string Header2
        {
            get { return GetText("header2"); }
        }

        private bool LeavePage(bool validateInput)
        {
            bool success = true;

            Hashtable data = Data;

            if (radVersion2009.Checked)
            {
                data[Format] = Format2009;
            }
            else if (radVersion2010.Checked)
            {
                data[Format] = Format2010;
            }
            else
            {
                data[Format] = Format2013Xml;
            }

            return success;
        }

        protected override bool OnPreNext()
        {
            return LeavePage(true);
        }

        protected override bool OnPreBack()
        {
            return LeavePage(false);
        }

        protected override void OnActivate()
        {
            Hashtable data = Data;

            if (data[Format].Equals(Format2009))
            {
                radVersion2009.Checked = true;
            }
            else if (data[Format].Equals(Format2010))
            {
                radVersion2010.Checked = true;
            }
            else
            {
                radVersion2013Xml.Checked = true;
            }

            lblInfo.Text = GetText("info");
        }
    }
}
