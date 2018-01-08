using System;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

using Windows.Forms.Wizard;

namespace Operationen.Wizards.CreateCustomerData
{
    public partial class Summary : CreateCustomerDataWizardPage
    {
        public Summary(BusinessLayer businessLayer)
            : base(businessLayer, "Wizards_CreateCustomerData_Summary")
        {
            InitializeComponent();
        }
        protected override string PageName
        {
            get
            {
                return GetText("pagename");
            }
        }

        protected override void OnActivate()
        {
            lblInfo.Text = string.Format(CultureInfo.InvariantCulture, GetText("info"),
                (string)Data[CreateCustomerDataWizardPage.KEY_CD_KRANKENHAUS],
                (string)Data[CreateCustomerDataWizardPage.KEY_CD_PLZ],
                (string)Data[CreateCustomerDataWizardPage.KEY_CD_STRASSE],
                GetNextText());

        }

        protected override bool OnFinish()
        {
            DataTable table = new DataTable();

            table.Rows.Add(_businessLayer.CreateDataRowConfig(
                table,
                KEY_CD_KRANKENHAUS,
                (string)Data[CreateCustomerDataWizardPage.KEY_CD_KRANKENHAUS]
                ));
            table.Rows.Add(_businessLayer.CreateDataRowConfig(
                table,
                KEY_CD_PLZ,
                (string)Data[CreateCustomerDataWizardPage.KEY_CD_PLZ]
                ));
            table.Rows.Add(_businessLayer.CreateDataRowConfig(
                table,
                KEY_CD_STRASSE,
                (string)Data[CreateCustomerDataWizardPage.KEY_CD_STRASSE]
                ));
            
            bool success = CreateCustomerData(table);

            // Egal ob geklappt oder nicht, anschlieﬂend kann man nur noch 
            // 'Schlieﬂen' klicken
            return true;
        }

        private bool CreateCustomerData(DataTable table)
        {
            CustomerDataCreator installer = new CustomerDataCreator(_businessLayer);
            installer.Initialize(table);

            bool success = installer.Create();

            SetSuccess(success);

            return success;
        }
    }
}
