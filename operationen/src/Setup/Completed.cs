using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using Windows.Forms.Wizard;

namespace Operationen.Setup
{
    public partial class Completed : SetupWizardPage
    {
        public Completed()
            : base()
        {
            InitializeComponent();
        }

        protected override string PageName
        {
            get
            {
                return "Fertigstellung";
            }
        }

        protected override void OnActivate()
        {
            if ((bool)Data[SetupWizardPage.Success])
            {
                if (Wizard.UpdateMode)
                {
                    lblInfo.Text = string.Format("Das Programm wurde erfolgreich aktualisiert."
                        + Environment.NewLine
                        + Environment.NewLine
                        + "Sie können auf 'Start > Programme > {0}'"
                        + " oder auf das Icon '{0}' auf Ihrem Desktop klicken,"
                        + " um das Programm später zu starten."
                        + "\r\rSobald Sie auf '" + Wizard.CloseText + "' klicken wird das Programm automatisch gestartet."
                        , SetupData.ProgramName)
                        ;
                }
                else
                {
                    lblInfo.Text = string.Format("Das Programm wurde erfolgreich installiert."
                        + Environment.NewLine
                        + Environment.NewLine
                        + "Sie können auf 'Start > Programme > {0}'"
                        + " oder auf das Icon '{0}' auf Ihrem Desktop klicken,"
                        + " um das Programm später zu starten."
                        + "\r\rSobald Sie auf '" + Wizard.CloseText + "' klicken wird es automatisch gestartet.",
                        SetupData.ProgramName
                        );
                }
            }
            else
            {
                if (Wizard.UpdateMode)
                {
                    lblInfo.Text = "Das Programm konnte nicht oder nicht vollständig aktualisiert werden.";
                }
                else
                {
                    lblInfo.Text = "Das Programm konnte nicht oder nicht vollständig installiert werden.";
                }
            }
        }
    }
}

