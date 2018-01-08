/*
 * OperationenImport Plugin - import operations/surgeons from a plain text file
 * Source code from OP-LOG
 * 
 * Copyright Christoph Maurer, D-61184 Karben
 * http://www.op-log.de
 * 
 */


using System;
using System.Text;

namespace Operationen
{
    public partial class OperationenImportImedOne : OperationenImport
    {
        public override OpLogPluginId PluginId { get { return OperationenImport.OpLogPluginId.PluginIdImedOne; } }

        private Encoding GetEncoding()
        {
            return Encoding.GetEncoding(1250);
        }
        private string FormatDescription()
        {
            return GetEncoding().ToString();
        }
    }
}
