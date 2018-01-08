using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Windows.Forms.Wizard
{
    public partial class WizardPage : UserControl
    {
        private WizardMaster _oWizard;
        private int _nPageIndex;

        public WizardPage()
        {
            InitializeComponent();
        }

        protected internal int PageIndex
        {
            get { return _nPageIndex; }
            set { _nPageIndex = value;  }
        }

        protected internal WizardMaster Wizard
        {
            get { return _oWizard; }
            set { _oWizard = value; }
        }

        protected internal virtual string Header1 { get { return PageName; } }
        protected internal virtual string Header2 { get { return ""; } }
        protected internal virtual Image Image { get { return null; } }
        protected internal virtual string PageName { get { return ""; } }

        protected internal virtual bool OnFinish() { return true; }
        protected internal virtual bool OnPreNext() { return true; }
        protected internal virtual bool OnPreBack() { return true; }
        protected internal virtual void OnDeactivate() {}
        protected internal virtual void OnActivate() {}
        protected internal virtual void SetInitialFocus() {}
    }
}

