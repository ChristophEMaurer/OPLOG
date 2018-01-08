using System;

/*
 * 21.03.2011 
 *      Removed class OperationenImportEx because class OperationenImport
 *      was changed to version 3.2.0.0 and includes OperationenImportEx's stuff
 */
namespace Operationen
{
    /// <summary> 
    /// This class extends the default event as we need more data. 
    /// Previously, we only had the last name and first name, and if 
    /// we wanted to identify a surgeon by his importid, we had to pass 
    /// the importid in the lastname-field. 
    /// </summary> 
    public class OperationenImportEventEx : OperationenImportEvent
    {
        /// <summary> 
        /// The ImportId. 
        /// Database: Chirurgen.ImportID 
        /// </summary> 
        public string SurgeonImportId;

        /// <summary> 
        /// The UserId. You log on into the application with this user id and a password 
        /// Database: Chirurgen.UserID 
        /// </summary> 
        public string SurgeonUserId;

        /// <summary> 
        /// Reserve data for future use 
        /// </summary> 
        public object Reserved1;
        public object Reserved2;
        public object Reserved3;
        public object Reserved4;
        public object Reserved5;

        /// <summary> 
        /// Resets all data in the event. You can create one instance of this class and reset and 
        /// fill the data, reusing the instance instead of creating a new instance for every record. 
        /// </summary> 
        public virtual new void ClearData()
        {
            base.ClearData();

            SurgeonImportId = "";
            SurgeonUserId = "";
        }
    }
}

