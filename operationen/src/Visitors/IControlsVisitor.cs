using System;
using System.Windows.Forms;

namespace Operationen.Visitors
{
    /// <summary>
    /// Base class for all visitors that must do something to each control of a form
    /// </summary>
    public interface IControlsVisitor
    {
        void VisitControl(Control item);
        void VisitMenuStrip(MenuStrip item);
        void VisitToolStripItem(ToolStripItem item);
        void VisitToolStripMenuItem(ToolStripMenuItem item);
        void VisitControl(Elegant.Ui.Control item);
        void VisitButton(System.Windows.Forms.Button item);
        void VisitElegantUiButton(Elegant.Ui.Button item);
        void VisitRibbonGroup(Elegant.Ui.RibbonGroup item);
        void VisitRibbonTabPage(Elegant.Ui.RibbonTabPage item);
    }
}

