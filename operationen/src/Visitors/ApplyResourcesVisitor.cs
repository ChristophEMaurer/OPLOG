using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;

namespace Operationen.Visitors
{
    /// <summary>
    /// This visitor switches the language by applying a resource manager and a specific culture to a control.
    /// </summary>
    public class ApplyResourcesVisitor : IControlsVisitor
    {
        ComponentResourceManager _componentResourceManager;
        CultureInfo _cultureInfo;

        public ApplyResourcesVisitor(ComponentResourceManager resources, CultureInfo cultureInfo)
        {
            _componentResourceManager = resources;
            _cultureInfo = cultureInfo;
        }

        private void ApplyResources(object value, string objectName)
        {
            _componentResourceManager.ApplyResources(value, objectName, _cultureInfo);
        }

        public void VisitControl(Control item)
        {
            ApplyResources(item, item.Name);
        }

        public void VisitControl(Elegant.Ui.Control item)
        {
            ApplyResources(item, item.Name);
        }

        public void VisitElegantUiButton(Elegant.Ui.Button item)
        {
            ApplyResources(item, item.Name);
        }

        public void VisitButton(Button item)
        {
            ApplyResources(item, item.Name);
        }

        public void VisitMenuStrip(MenuStrip item)
        {
            ApplyResources(item, item.Name);
        }

        public void VisitToolStripItem(ToolStripItem item)
        {
            ApplyResources(item, item.Name);
        }

        public void VisitToolStripMenuItem(ToolStripMenuItem item)
        {
            ApplyResources(item, item.Name);
        }

        public void VisitRibbonGroup(Elegant.Ui.RibbonGroup item)
        {
            ApplyResources(item, item.Name);
        }

        public void VisitRibbonTabPage(Elegant.Ui.RibbonTabPage item)
        {
            ApplyResources(item, item.Name);
        }
    }
}

