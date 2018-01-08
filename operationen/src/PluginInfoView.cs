using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace Operationen
{
    public partial class PluginInfoView : OperationenForm
    {
        private string _filename;


        public PluginInfoView(BusinessLayer businessLayer, string strFilename)
            : base(businessLayer)
        {
            _filename = strFilename;
            InitializeComponent();
        }

        private void PluginInfoView_Load(object sender, EventArgs e)
        {
            try
            {
                txtAsmFilename.Text = _filename;

                OperationenImport o = null;
                this.Text = GetText("title");

                Assembly plugin = Assembly.LoadFile(_filename);

                Type[] types = plugin.GetTypes();
                // Iterate and find types derived from Form Instantiate them
                foreach (Type t in types)
                {
                    if (BusinessLayer.IsValidPlugin(t))
                    {
                        o = (OperationenImport)Activator.CreateInstance(t);
                        string strAssemblyDescription =
                            ((AssemblyDescriptionAttribute)
                            plugin.GetCustomAttributes(
                            typeof(AssemblyDescriptionAttribute), false)[0]).Description;

                        txtAsmDescription.Text = strAssemblyDescription;
                        txtInfo.Text = o.OPImportDescription();
                        int pluginId = Convert.ToInt32(o.PluginId);
                        txtPluginId.Text = string.Format(CultureInfo.InvariantCulture, "{0} [{1}]", o.PluginId.ToString(), pluginId);
                    }
                }
            }
            catch (TargetInvocationException)
            {
                //
                // Wenn man von Plugins versucht von '\\cmaurer\oplog\plugins' zu laden und hatte addTrust.bat nicht ausgeführt
                //
                txtInfo.Text = GetTextForTextBox("TargetInvocationException");
            }
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}