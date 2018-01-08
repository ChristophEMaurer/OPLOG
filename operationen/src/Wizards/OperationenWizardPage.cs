using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using System.Globalization;
using System.Threading;

using Windows.Forms.Wizard;

namespace Operationen.Wizards
{
    public partial class OperationenWizardPage : WizardPage
    {
        private const string FormName = "OperationenWizardPage";
        public static Image _image;
        private string _formNameForResourceTexts;
        protected string EingabeFehler;

        protected BusinessLayer _businessLayer;

        public OperationenWizardPage()
        {
        }

        public OperationenWizardPage(BusinessLayer b)
            : this(b, null)
        {
        }

        public OperationenWizardPage(BusinessLayer businessLayer, string formNameForResourceTexts)
        {
            _businessLayer = businessLayer;
            _formNameForResourceTexts = formNameForResourceTexts;

            InitializeComponent();

            EingabeFehler = GetText("eingabeFehler");

        }

        public int ConvertToInt32(object o)
        {
            return _businessLayer.ConvertToInt32(o);
        }

        protected void DefaultListViewProperties(ListView lv)
        {
            lv.View = View.Details;
            lv.FullRowSelect = true;
            lv.ShowItemToolTips = true;
            lv.GridLines = false;
            lv.HideSelection = false;
        }

        protected Hashtable Data
        {
            get
            {
                return ((OperationenWizardMaster)Wizard)._htData;
            }
        }

        public bool GetSuccess()
        {
            return ((OperationenWizardMaster)Wizard).GetSuccess();
        }
        public void SetSuccess(bool success)
        {
            ((OperationenWizardMaster)Wizard).SetSuccess(success);
        }

        protected override Image Image
        {
            get
            {
                if (_image == null)
                {
                    try
                    {
                        // Diese Datei ist im Verzeichnis Operationen/Images/Surgeon.png
                        // Wenn man bei ihren Eigenschaften nachsieht, muss man 
                        // "embedded resource" einstellen, dann landet sie in der .exe und
                        // man kann sie so wie hier auslesen.
                        Assembly assembly = Assembly.GetExecutingAssembly();
                        Stream stream = assembly.GetManifestResourceStream(
                            "Operationen.Images.Surgeon.png");

                        _image = new Bitmap(stream);
                    }
                    catch
                    {
                    }
                }
                return _image;
            }
        }

        protected string GetFormNameForResourceTexts()
        {
            string form;

            if (string.IsNullOrEmpty(_formNameForResourceTexts))
            {
                form = this.Name;
            }
            else
            {
                form = _formNameForResourceTexts;
            }

            return form;
        }

        protected string GetTextControlMissingText(Label c)
        {
            return string.Format(CultureInfo.InvariantCulture, GetText(FormName, "missingText"), c.Text);
        }
        protected string GetTextNotAllowed(string label, string badText)
        {
            return string.Format(CultureInfo.InvariantCulture, GetText(FormName, "errTextNotAllowed"), label, badText);
        }
        protected string GetText(string id)
        {
            return _businessLayer.GetText(GetFormNameForResourceTexts(), id);
        }
        protected string GetText(string formName, string id)
        {
            return _businessLayer.GetText(formName, id);
        }
        protected string GetNextText()
        {
            return _businessLayer.GetText(FormName, "next");
        }
    }
}
