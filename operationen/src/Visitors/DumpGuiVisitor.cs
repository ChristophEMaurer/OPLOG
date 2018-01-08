using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;
using System.IO;
using System.Text;

namespace Operationen.Visitors
{
    /// <summary>
    /// This visitor prints the contents of the GUI to a file.
    /// </summary>
    public class DumpGuiVisitor : IControlsVisitor
    {
        StreamWriter _streamWriter;

        public DumpGuiVisitor(StreamWriter writer)
        {
            _streamWriter = writer;
        }

        private bool ControlDumpable(Control control)
        {
            bool dumpable = false;

            if (control is CheckBox)
            {
                dumpable = true;
            }
            else if (control is TextBox)
            {
                dumpable = true;
            }
            else if (control is RadioButton)
            {
                dumpable = true;
            }

            return dumpable;
        }

        private void DumpControl(Control control)
        {
            StringBuilder sb = null;
            bool dumpable = false;

            dumpable = ControlDumpable(control);

            if (dumpable)
            {
                sb = new StringBuilder();

                if (control is CheckBox)
                {
                    sb.Append("CheckBox.");
                    sb.Append(control.Name);
                    sb.Append(".");

                    CheckBox checkBox = (CheckBox)control;
                    if (checkBox.Checked)
                    {
                        sb.Append("checked");
                    }
                    else
                    {
                        sb.Append("not checked");
                    }
                }
                else if (control is TextBox)
                {
                    TextBox textBox = (TextBox)control;
                    sb.Append("TextBox.");
                    sb.Append(control.Name);
                }
                else if (control is RadioButton)
                {
                    RadioButton radioButton = (RadioButton)control;

                    sb.Append("RadioButton.");
                    sb.Append(control.Name);
                    sb.Append(".");

                    if (radioButton.Checked)
                    {
                        sb.Append("checked");
                    }
                    else
                    {
                        sb.Append("not checked");
                    } 
                }

                sb.Append(".");
                if (control.Enabled)
                {
                    sb.Append("enabled");
                }
                else
                {
                    sb.Append("disabled");
                }

                sb.Append(":'");
                sb.Append(control.Text);
                sb.Append("'");

                _streamWriter.WriteLine(sb.ToString());
                _streamWriter.Flush();
            }
        }

        public void VisitControl(Control item)
        {
            DumpControl(item);
        }

        public void VisitControl(Elegant.Ui.Control item)
        {
        }

        public void VisitElegantUiButton(Elegant.Ui.Button item)
        {
        }

        public void VisitButton(Button item)
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

        public void VisitRibbonGroup(Elegant.Ui.RibbonGroup item)
        {
        }

        public void VisitRibbonTabPage(Elegant.Ui.RibbonTabPage item)
        {
        }
    }
}

