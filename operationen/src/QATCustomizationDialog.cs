using System;
using System.Collections.Generic;
using System.Text;

namespace Operationen
{
    public class QATCustomizationDialog : Elegant.Ui.QuickAccessToolbarControlsEditorForm
    {
        private BusinessLayer _businessLayer;

        public QATCustomizationDialog(BusinessLayer businessLayer, Elegant.Ui.Ribbon ribbon)
            : base(ribbon)
        {
            _businessLayer = businessLayer;
        }

        protected override string GetControlTextLabel(Elegant.Ui.IControl control)
        {
            string text;

            if (control.GetType().FullName.Equals("Elegant.Ui.RibbonApplicationButton"))
            {
                text = _businessLayer.GetText("QATCustomizationDialog", "ribbonApplicationButtonText");
            }
            else
            {
                text = control.Control.Text;

            }

            return text;
        }
    }
}
