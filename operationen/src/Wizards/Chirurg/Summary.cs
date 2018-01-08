using System;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Windows.Forms.Wizard;

namespace Operationen.Wizards.Chirurg
{
    public partial class Summary : ChirurgWizardPage
    {
        public Summary(BusinessLayer businessLayer)
            : base(businessLayer)
        {
            InitializeComponent();
        }
        protected override string PageName
        {
            get
            {
                return "Zusammenfassung";
            }
        }

        protected override void OnActivate()
        {
            DataRow row = (DataRow)Data[ChirurgWizardPage.Chirurg];
            int ID_ChirurgenFunktionen = ConvertToInt32(row["ID_ChirurgenFunktionen"]);
            DataRow titel = _businessLayer.GetChirurgenFunktion(ID_ChirurgenFunktionen);

            txtInfo.Text = "Zusammenfassung:"
                + Environment.NewLine
                + Environment.NewLine
                + "Anrede:"
                + Environment.NewLine
                + "    " + (string)row["Anrede"]
                + Environment.NewLine
                + Environment.NewLine
                + "Dienststellung:"
                + Environment.NewLine
                + "    " + (string)titel["Funktion"]
                + Environment.NewLine
                + Environment.NewLine
                + "Nachname:"
                + Environment.NewLine
                + "    " + (string)row["Nachname"]
                + Environment.NewLine
                + Environment.NewLine
                + "Vorname:"
                + Environment.NewLine
                + "    " + (string)row["Vorname"]
                + Environment.NewLine
                + Environment.NewLine
                + "Anmeldename:"
                + Environment.NewLine
                + "    " + (string)row["UserID"]
                + Environment.NewLine
                + Environment.NewLine
                + "Klicken Sie auf '"
                + Wizard.FinishText
                + "', um den Vorgang abzuschlieﬂen."
            ;
        }

        protected override bool OnFinish()
        {
            DataRow chirurg = (DataRow)Data[ChirurgWizardPage.Chirurg];

            bool success = CreateChirurg(chirurg);

            // Egal ob geklappt oder nicht, anschlieﬂend kann man nur noch 
            // 'Schlieﬂen' klicken
            return true;
        }

        private bool CreateChirurg(DataRow chirurg)
        {
            ChirurgCreator installer = new ChirurgCreator(_businessLayer);
            installer.Initialize(chirurg);

            bool success = installer.Create();

            SetSuccess(success);

            return success;
        }
    }
}
