using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Windows.Forms.Wizard;

namespace Operationen.Setup
{
    public partial class Welcome : SetupWizardPage
    {
        public Welcome()
        {
            InitializeComponent();
        }

        private void Welcome_Load(object sender, EventArgs e)
        {
            string buildInfo = "";

#if urologie
            buildInfo= "(Urologie";
#elif gynaekologie
            buildInfo= "(Gynaekologie";
#else
            buildInfo= "(Chirurgie";
#endif

#if targetplatform_x86
            buildInfo = buildInfo + ", x86)";
#else
            buildInfo = buildInfo + ", anycpu)";
#endif

            if (Wizard.UpdateMode)
            {

                lblWelcome.Text = "Willkommen beim 'Update' Assistenten."
                    + Environment.NewLine
                    + Environment.NewLine
                    + "Dieser Assistent hilft Ihnen, " + ProgramName + " " + buildInfo + " auf Ihrem Computer zu akualisieren."
                    + Environment.NewLine
                    + Environment.NewLine
                    + "Sie müssen Administrator auf diesem Computer sein, damit Sie das Update ausführen können."
                    + " Wenn Sie nicht Administrator sind, brechen Sie die Installation ab und wenden Sie sich an Ihren Systemadministrator."
                    + Environment.NewLine
                    + Environment.NewLine
                    + "Klicken Sie 'Weiter', um mit dem Vorgang zu beginnen."
                    ;
            }
            else
            {
                lblWelcome.Text = "Willkommen beim 'Setup' Assistenten."
                    + Environment.NewLine
                    + Environment.NewLine
                    + "Dieser Assistent hilft Ihnen, " + ProgramName + " " + buildInfo + " auf Ihrem Computer zu installieren."
                    + Environment.NewLine
                    + Environment.NewLine
                    + "Sie müssen Administrator auf diesem Computer sein, damit Sie das Setup ausführen können."
                    + " Wenn Sie nicht Administrator sind, brechen Sie die Installation ab und wenden Sie sich an Ihren Systemadministrator."
                    + Environment.NewLine
                    + Environment.NewLine
                    + "Klicken Sie 'Weiter', um mit dem Vorgang zu beginnen."
                    ;
            }
        }

        protected override string PageName
        {
            get {return "Willkommen";}
        }
    }
}
