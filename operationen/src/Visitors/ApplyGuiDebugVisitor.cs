using System;
using System.Text;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;

namespace Operationen.Visitors
{
    /// <summary>
    /// Add an event to each control so that we can log the commands connected to a control
    /// </summary>
    public class ApplyGuiDebugVisitor : IControlsVisitor
    {
        private AppFramework.Debugging.IDebuggee _debuggee;
        private Elegant.Ui.Command _command = new Elegant.Ui.Command("GuiEvent");
        private bool _add = true;

        private ApplyGuiDebugVisitor()
        {
        }

        public ApplyGuiDebugVisitor(AppFramework.Debugging.IDebuggee debuggee, bool add)
        {
            _debuggee = debuggee;
            _add = add;

            if (add)
            {
                _command.Executed += new Elegant.Ui.CommandExecutedEventHandler(command_Executed);
            }
        }

        void command_Executed(object sender, Elegant.Ui.CommandExecutedEventArgs e)
        {
            //
            // Do NOT translate, debug output
            //
            string msg = "Executed command: " + OperationenForm.BuildFullControlName(e.Invoker);
            DebugPrintGui(msg);
        }

        private void DebugPrintGui(string msg)
        {
            _debuggee.DebugPrint(AppFramework.Debugging.DebugLogging.DebugFlagGuiCommand, msg);
        }

        private void EnsureCommand(Elegant.Ui.Command command)
        {
            if (_add)
            {
                //
                // If a command is on the menu and on the quick access toolbar,
                // it would be added twice or even multiple times.
                //

                // Add this to see how one command is called multiple times
                //if (command.Name == "DateiTypenView")
                //{
                //    int i = 5;
                //}
                command.Executed -= new Elegant.Ui.CommandExecutedEventHandler(command_Executed);
                command.Executed += new Elegant.Ui.CommandExecutedEventHandler(command_Executed);
            }
            else
            {
                command.Executed -= new Elegant.Ui.CommandExecutedEventHandler(command_Executed);
            }
        }

        public void VisitElegantUiButton(Elegant.Ui.Button item)
        {
            if (_add && (item.Command == null))
            {
                item.Command = _command;
            }
            else
            {
                EnsureCommand(item.Command);
            }
        }

        public void VisitButton(Button item)
        {
        }

        public void VisitControl(Elegant.Ui.Control item)
        {
            if (_add && (item.Command == null))
            {
                item.Command = _command;
            }
            else
            {
                EnsureCommand(item.Command);
            }
        }

        public void VisitRibbonGroup(Elegant.Ui.RibbonGroup item)
        {
        }

        public void VisitRibbonTabPage(Elegant.Ui.RibbonTabPage item)
        {
        }

        public void VisitControl(Control item)
        {
        }

        public void VisitMenuStrip(MenuStrip item)
        {
        }

        public void VisitToolStripItem(ToolStripItem item)
        {
        }

        public void VisitToolStripMenuItem(ToolStripMenuItem item)
        {
        }
    }
}

