using System;
using System.Collections.Generic;
using System.Text;

namespace Operationen
{
    /// <summary>
    /// I prepared this but it was never requested.
    /// </summary>
    public partial class OperationenImportImedOne : OperationenImport
    {
        /// <summary>
        /// One instance only. Clear and set data instead of
        /// creating a new instance over and over again.
        /// </summary>
        private OperationenImportEvent _oEvent;

        /// <summary>
        /// Here you must return a text that sufficiently describes your plugin.
        /// This text will be displayed by the main program.
        /// </summary>
        /// <returns>The text to display</returns>
        public override string OPImportDescription()
        {
            string s = string.Format("Dieses Import-Plugin importiert Operationen aus einer Text-Datei mit Codierung '{0}'."
                + "\r\nÜberprüfen Sie nach dem Datenimport die Operationen und die Umlaute."
                + "\r\n\r\nOperationenImportImedOne"
                    , FormatDescription())
            ;

            return s;
        }

        /// <summary>
        /// This function is called before OPImportInit() once at the beginning of the import.
        /// </summary>
        public override void OPImportInit(OperationenImportPluginCustomData customData)
        {
            _oEvent = new OperationenImportEvent();
        }

        /// <summary>
        /// This does the actual import.
        /// </summary>
        public override void OPImportRun()
        {
        }

        /// <summary>
        /// This function is called after OPImportRun().
        /// We do nothing here.
        /// </summary>
        public override void OPImportFinalize()
        {
        }
    }
}
