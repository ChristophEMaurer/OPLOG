/*
 * OperationenImportPlugin - import operations/surgeons from a plain text file
 * Source code from OP-LOG
 * 
 * Copyright Christoph Maurer, D-61184 Karben
 * http://www.op-log.de
 * 
 */


using System;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace Operationen
{
	public partial class OperationenImportPlugin : OperationenImport
	{
        public override OpLogPluginId PluginId { get { return OpLogPluginId.PluginIdOrbisTextUnicode; } }

        private Encoding GetEncoding()
        {
            return Encoding.Unicode;
        }
        private string FormatDescription()
        {
            return "Unicode";
        }
    }
}
