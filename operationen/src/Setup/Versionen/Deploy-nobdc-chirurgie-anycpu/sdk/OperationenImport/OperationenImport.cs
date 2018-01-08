using System;

namespace Operationen
{
    /// <summary>
    /// If an instance of this class is passed to function <see cref="Operationen.OperationenImport.OPImportInit"/>,
    /// then the import will not be interactive and not require ANY user interaction.
    /// This class defines a data source, from which the data will be imported.
    /// If a string is not good enough for you, you can alway derive from this class.
    /// </summary>
    public class OperationenImportPluginCustomData
    {
        /// <summary>
        /// A general way of specifying a data source. Can be a filename, a database connection and table etc. 
        /// </summary>
        public string DataSource;
    }

    /// <summary>
    /// When an import event is passed to the application, this indicates the
    /// type of data.
    /// </summary>
    public enum EVENT_STATE
    {
        /// <summary>
        /// The event contains data: an operation and a surgeon.
        /// The fields SurgeonLastName, SurgeonFirstName, OPCode,
        /// OPDescription, OPDateAndTime and OPFunction contain valid data.
        /// </summary>
        STATE_DATA,

        /// <summary>
        /// The event carries information text in StateText which can be displayed.
        /// </summary>
        STATE_INFO,

        /// <summary>
        /// There was an error. The data import continues all the same.
        /// </summary>
        STATE_ERROR
    }

    /// <summary>
    /// Defines whether the surgeon operated as the main surgeon or as
    /// an assistant only.
    /// </summary>
    public enum OP_FUNCTION : int
    {
        /// <summary>
        /// Main operator
        /// </summary>
        OP_FUNCTION_OP = 1,

        /// <summary>
        /// 1. Assistent
        /// </summary>
        OP_FUNCTION_ASS = 2,

        /// <summary>
        /// 2. Assistent
        /// </summary>
        OP_FUNCTION_ASS2 = 3,

        /// <summary>
        /// 3. Assistent
        /// </summary>
        OP_FUNCTION_ASS3 = 4
    }

    /// <summary>
    /// The plugin creates an instance of this class, fills it with data,
    /// and fires this event.
    /// <br>An operation is uniquely identified by OPCode and OPDateAndTime</br>
    /// </summary>
    public class OperationenImportEvent
    {
        /// <summary>
        /// The last name.
        /// Database: Chirugen.Nachname
        /// </summary>
        public string SurgeonLastName;
        
        /// <summary>
        /// The first name.
        /// Database: Chirurgen.Vorname
        /// </summary>
        public string SurgeonFirstName;

        /// <summary>
        /// A unique Identifier that uniqueliy identifies one operation, e.g. 
        /// the year followed by a sequential number: 2001010306
        /// Database: ChirurgenOperationen.Fallzahl
        /// </summary>
        public string Identifier;

        /// <summary>
        /// The unique code that identifies an operation, such as 1-440.8
        /// Database: ChirurgenOperationen.OPS-Kode
        /// </summary>
        public string OPCode;
        
        /// <summary>
        /// The text describing the OPCode.
        /// Database: ChirurgenOperationen.OPS-Text
        /// </summary>
        public string OPDescription;
        
        /// <summary>
        /// The date and begin time of the operation.
        /// Database: ChirurgenOperationen.Datum, ChirurgenOperationen.Zeit
        /// </summary>
        public DateTime OPDateAndTime;

        /// <summary>
        /// The end time of the operation. Only the time part of the DateTime will be used, 
        /// the date part will be ignored.
        /// Database: ChirurgenOperationen.ZeitBis
        /// </summary>
        public DateTime OPTimeEnd;

        /// <summary>
        /// The function of the surgeon in this operation. If more than one surgeon 
        /// operated, the same event will be raised with identical operation data but different 
        /// surgeon names and functions.
        /// Database: OPFunktionen
        /// </summary>
        public OP_FUNCTION OPFunction;

        /// <summary>
        /// Set by plugin, read by caller 
        /// </summary>
        public EVENT_STATE State;

        /// <summary>
        /// When State is STATE_INFO or STATE_ERROR, it contains a text with a description.
        /// </summary>
        public string StateText;

        /// <summary>
        /// This is set if the user clicks 'Stop import', for example. This is read by the plugin. Notifies the plugin to stop the import. 
        /// </summary>
        public bool Abort;

        /// <summary>
        /// Resets all data in the event. You can create one instance of this class and reset and
        /// fill the data, reusing the instance instead of creating a new instance for every record.
        /// </summary>
        public void ClearData()
        {
            SurgeonLastName = "";
            SurgeonFirstName = "";
            Identifier = "";
            OPCode = "";
            OPDescription = "";
            OPDateAndTime = new DateTime(1753, 1, 1, 23, 59, 59); //TODO  00:00:00
            OPTimeEnd = new DateTime(1753, 1, 1, 23, 59, 59);
            StateText = "";
        }
    }

    /// <summary>
    /// Base class for one operation-surgeon item. A plugin assembly must contain one class derived from this base class.
    /// If you read from a text file, make sure you use the correct encoding. If unsure, use Unicode. After importing data,
    /// check the imported text and all other data.
    /// <br/>If you read form a text file, create two versions: one for ASCII* and one for Unicode. Study the sample plugins
    /// to see how you use partial classes to share the code that processes the data.
    /// <br/>
    /// This is how the main program uses a plugin.
    /// One instance <code>plugin</code> of the derived class will be instantiated and functions will be called in this order:
    /// <br>
    /// <code>
    /// void ImportDataFromPlugin()
    /// {
    ///     plugin.ImportOP += new OperationenImport.OperationenImportHandler(o_ImportOP);
    ///     
    ///     Import
    ///     o.OPImportInit(null);
    ///     o.OPImportRun();
    ///     o.OPImportFinalize();
    /// }
    /// void o_ImportOP(object sender, OperationenImportEvent e)
    /// {
    ///     if (...)
    ///     {
    ///         e.Abort = true;
    ///         Protokoll("Info: Import aborted.");
    ///     }
    ///     else
    ///     {
    ///         switch (e.State)
    ///         {
    ///             case EVENT_STATE.STATE_ERROR:
    ///                 Protokoll("Error:" + e.StateText);
    ///                 break;
    ///
    ///             case EVENT_STATE.STATE_DATA:
    ///                 ImportLine(e);
    ///                 break;
    ///
    ///             case EVENT_STATE.STATE_INFO:
    ///                 Protokoll("Info: " + e.StateText);
    ///                 break;
    ///
    ///             default:
    ///                 break;
    ///         }
    ///     }
    /// }
    /// </code>
    /// </br>
    /// </summary>
    public abstract class OperationenImport
    {
        public enum OpLogPluginId
        {
            PluginIdUnknown = 1,

            PluginIdCsv = 2,
            PluginIdCsvUnicode = 3,

            PluginIdIcpm3Op3Csv = 4,
            PluginIdIcpm3Op3CsvUnicode = 5,

            PluginIdIcpm5Op3Csv = 6,
            PluginIdIcpm5Op3CsvUnicode = 7,

            PluginIdIkpm10 = 8,
            PluginIdIkpm10Unicode = 9,

            PluginIdImedOne = 10,
            // PluginIdImedOneUnicode = 11,

            PluginIdMccIsop = 12,
            PluginIdMccIsopUnicode = 13,

            PluginIdOrbis = 14,
            PluginIdOrbisUnicode = 15,
            PluginIdOrbisText = 16,
            PluginIdOrbisTextUnicode = 17,

            PluginIdSqlCcopm = 18,

            PluginIdSapCsv = 20,
            PluginIdSapCsvUnicode = 21,

            PluginIdSqlServerDemo = 100,
        }

        /// <summary>
        /// Private data. Use the PluginId to identify the data.
        /// </summary>
        private object _data;


        /// <summary>
        /// The delegate.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event containing an operation-surgeon item.</param>
        public delegate void OperationenImportHandler(object sender, OperationenImportEvent e);
        
        /// <summary>
        /// This event is fired for every new record.
        /// </summary>
        public event OperationenImportHandler ImportOP;

        /// <summary>
        /// A unique ID that identifies the Plugin
        /// These numbers are defined by OP-LOG. If you need to return a used-specific value,
        /// use a number > 100000, but beware, someone else might be using the same value you just used.
        /// So send your value to info@op-log.de and we will add it to this official list.
        /// </summary>
        public abstract OpLogPluginId PluginId { get; }// { return OperationenImport.OpLogPluginId.PluginIdUnknown; } }

        /// <summary>
        /// Extra data of this class.
        /// </summary>
        public object Data
        {
            get { return _data; }
            set { _data = value; }
        }

        /// <summary>
        /// Fires the event.
        /// </summary>
        /// <param name="e">Contains the data of one record</param>
        protected void FireImportOPEvent(OperationenImportEvent e)
        {
            if (ImportOP != null)
            {
                ImportOP(this, e);
            }
        }

        /// <summary>
        /// This function is called once at the beginning of the import. Use this to perform initialization.
        /// You can pass some very basic data in this parameter. It is so far used to pass the name of 
        /// a file that should be imported automatically without user intervention.
        /// Beginning with version 3.2.0.0, any data can be passed to the plugin by setting the data with
        /// @see 
        /// </summary>
        /// <param name="customData">If this is not null, then a quiet import from the specified data will be done.
        /// Otherwise, the user will be prompted for a file or some other data source etc.</param>
        public abstract void OPImportInit(OperationenImportPluginCustomData customData);

        /// <summary>
        /// Called once after <see cref="OPImportInit"/>. This function performs the actual data import and
        /// fires the <see cref="ImportOP"/> event once for every operation-surgeon item.
        /// </summary>
        public abstract void OPImportRun();

        /// <summary>
        /// Is called once after <see cref="OPImportRun"/>. Use this function to perform any finalzation operations.
        /// </summary>
        public abstract void OPImportFinalize();

        /// <summary>
        /// Supplies a description of the plugin which is shown to the user.
        /// </summary>
        /// <returns>The description.</returns>
        public abstract string OPImportDescription();
    }
}

