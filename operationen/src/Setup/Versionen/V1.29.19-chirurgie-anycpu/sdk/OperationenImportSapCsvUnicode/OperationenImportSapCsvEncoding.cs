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
        public override OpLogPluginId PluginId { get { return OpLogPluginId.PluginIdSapCsvUnicode; } }

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
