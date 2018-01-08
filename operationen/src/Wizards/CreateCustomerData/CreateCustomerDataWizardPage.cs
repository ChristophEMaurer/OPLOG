using System;
using System.Reflection;
using System.IO;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Windows.Forms.Wizard;

namespace Operationen.Wizards.CreateCustomerData
{
    public partial class CreateCustomerDataWizardPage : OperationenWizardPage
    {
        public const string KEY_CD_KRANKENHAUS = "cd_krankenhaus";
        public const string KEY_CD_PLZ = "cd_plz";
        public const string KEY_CD_STRASSE = "cd_strasse";

        public CreateCustomerDataWizardPage()
        {
        }

        public CreateCustomerDataWizardPage(BusinessLayer businessLayer, string formNameForResourceTexts)
            : base(businessLayer, formNameForResourceTexts)
        {
            InitializeComponent();
        }
    }
}
