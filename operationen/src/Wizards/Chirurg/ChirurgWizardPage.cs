using System;
using System.Reflection;
using System.IO;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Windows.Forms.Wizard;

namespace Operationen.Wizards.Chirurg
{
    public partial class ChirurgWizardPage : OperationenWizardPage
    {
        public const string Chirurg = "Arzt";

        public ChirurgWizardPage()
        {
        }

        public ChirurgWizardPage(BusinessLayer businessLayer)
            : base(businessLayer)
        {
            InitializeComponent();
        }
    }
}
