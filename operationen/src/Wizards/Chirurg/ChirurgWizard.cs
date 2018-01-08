using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using Windows.Forms.Wizard;

namespace Operationen.Wizards.Chirurg
{
    public partial class ChirurgWizard : OperationenWizardMaster
    {
        public ChirurgWizard(BusinessLayer businessLayer)
            : base(businessLayer)
        {
            InitializeComponent();

            DataRow row = businessLayer.CreateDataRowChirurg();

            _htData[ChirurgWizardPage.Chirurg] = row;

            AddPage(new Welcome(businessLayer));
            AddPage(new BasicData(businessLayer));
            AddPage(new Summary(businessLayer));
            AddPage(new Completed(businessLayer));
        }

        public override string Title
        {
            get
            {
                return "Arzt anlegen Assistent";
            }
        }
    }
}
