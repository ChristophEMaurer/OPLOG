using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Windows.Forms.Wizard;

namespace Operationen.Wizards
{
    public partial class OperationenWizardMaster : WizardMaster
    {
        private const string _Success = "success";
        protected BusinessLayer _oBusinessLayer;
        protected internal Hashtable _htData = new Hashtable();

        public OperationenWizardMaster() :
            this(null)
        {
        }

        public OperationenWizardMaster(BusinessLayer b)
        {
            _htData[_Success] = false;
            _oBusinessLayer = b;

            InitializeComponent();
        }

        public bool GetSuccess()
        {
            return (bool) _htData[_Success];
        }
        public void SetSuccess(bool success)
        {
            _htData[_Success] = success;
        }

        protected override string GetText(string form, string id)
        {
            return _oBusinessLayer.GetText(form, id);
        }
    }
}
