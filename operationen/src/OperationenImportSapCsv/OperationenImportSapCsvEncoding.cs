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
        /// <summary>
        /// Return the unique PluginId for the SAP CSV format
        /// </summary>
        public override OpLogPluginId PluginId { get { return OpLogPluginId.PluginIdSapCsv; } }

        private Encoding GetEncoding()
        {
            return Encoding.GetEncoding(1250);
        }
        private string FormatDescription()
        {
            return "ANSI Latin-2 (1250)";
        }
    }
}
