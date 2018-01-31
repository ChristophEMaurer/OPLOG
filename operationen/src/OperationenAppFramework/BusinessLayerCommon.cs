using System;
using System.Data;
using System.Resources;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using AppFramework;
using Utility;
using Security;
using Security.Cryptography;

namespace CMaurer.Operationen.AppFramework
{
    /// <summary>
    /// Common class that contains all code used on both Windows forms and web pages.
    /// This class must NOT have any user interface such as message boxes 
    /// because it is used by web pages.
    /// </summary>
    public abstract class BusinessLayerCommon : BusinessLayerBase, ISecurityManager
    {
        public const string ProgramTitle = "OP-LOG";
        public const int AbortLoopCount = 50;
        public const string BasisKenntnis = "BK";

        private DatabaseLayerCommon _databaseLayerCommon;
        protected static string RICHTZAHL = "Richtzahl";

        /// <summary>
        /// VersionMajor/VersionMinor müssen mit der Datenbankversion übereinstimmen
        /// ReleaseMajor ändert sich, wenn sich nur das Programm, nicht aber 
        /// die Datenbankstruktur ändert.
        /// </summary>
        /// 1.7.7
        public const int VersionMajor = 1;      // CHANGE_FOR_NEW_VERSION
        public const int VersionMinor = 29;     // CHANGE_FOR_NEW_VERSION
        public const int ReleaseMajor = 19;     // CHANGE_FOR_NEW_VERSION

        public const string VersionDate = "(04.02.2018)";   // CHANGE_FOR_NEW_VERSION

        public const int OperationQuelleIntern = 0;
        public const int OperationQuelleExtern = 1;
        public const int OperationQuelleAlle = 2;

        public const int ID_Alle = -100;

        protected const string Insert = "insert";
        protected const string Update = "update";
        protected const string Delete = "delete";

        protected DataRow _currentUser;
        protected ArrayList _currentUserRights;

        protected DataView _chirurgenFunktionen;

        private ProgressEventArgs _progressEventArgs = new ProgressEventArgs();

        public event ProgressCallback Progress;

        protected bool _write2Log = true;

        //
        // operationen123
        //
        protected const string _passwordCypher = "gEdMGrmfaOBd96L4ypdFBA==";

        protected string _password;

        public BusinessLayerCommon(ResourceManager resourceManager)
            : base(resourceManager, VersionMajor, VersionMinor, VersionDate, ProgramTitle)
        {
            _password = Decrypt(_passwordCypher);

            //RICHTZAHL = GetText("OperationenForm", "richtzahl");
        }

        public static string VersionString
        {
            get { return VersionMajor + "." + VersionMinor + "." + ReleaseMajor + " " + VersionDate; }
        }

        public static string VersionOnlyString
        {
            get { return VersionMajor + "." + VersionMinor + "." + ReleaseMajor; }
        }

        public System.Web.Configuration.AuthenticationMode AuthenticationMode
        { 
            get; set; 
        }

        protected void FireProgressEvent(ProgressEventArgs e)
        {
            if (Progress != null)
            {
                Progress(e);
            }
        }

        public string Password
        {
            get { return _password; }
        }

        public ArrayList CurrentUserRights
        {
            get { return _currentUserRights; }
        }

        public DataRow CurrentUser
        {
            get { return _currentUser; }
        }
        public int CurrentUser_ID_Chirurgen
        {
            get { return ConvertToInt32(_currentUser["ID_Chirurgen"]); }
        }
        public string CurrentUser_Nachname
        {
            get { return (string)_currentUser["Nachname"]; }
        }
        public string CurrentUser_Vorname
        {
            get { return (string)_currentUser["Vorname"]; }
        }
        public string CurrentUser_UserID
        {
            get { return (string)_currentUser["UserID"]; }
        }
        public bool CurrentUser_IstWeiterbilder
        {
            get { return 1 == ConvertToInt32(_currentUser["IstWeiterbilder"]); }
        }

        public bool UserIstWeiterbilder(int ID_Chirurgen)
        {
            bool istWeiterbilder = false;

            if (ID_Chirurgen == CurrentUser_ID_Chirurgen)
            {
                istWeiterbilder = (1 == ConvertToInt32(_currentUser["IstWeiterbilder"])); 
            }
            else
            {
                istWeiterbilder =  _databaseLayerCommon.UserIstWeiterbilder(ID_Chirurgen);
            }

            return istWeiterbilder;
        }
 
        public DatabaseLayerCommon DatabaseLayerCommon
        {
            get { return _databaseLayerCommon; }
        }

        public override void SetDatabaseLayer(DatabaseLayerBase databaseLayer)
        {
            base.SetDatabaseLayer(databaseLayer);
            _databaseLayerCommon = (DatabaseLayerCommon)databaseLayer;
        }

        public DataView GetRichtlinienOPSummen(int nID_Gebiete, int nID_Chirurgen, int quelle, DateTime? dtFrom, DateTime? dtTo)
        {
            // Sortiert nach LfdNummer
            DataView dv = _databaseLayerCommon.GetRichtlinienOPSummen(nID_Gebiete, nID_Chirurgen, quelle, dtFrom, dtTo);

            //
            // Wenn man manuell dieselbe Richtlinie verschiedenen Operationen fest zuordnet, kommt diese Richtlinien mehrfach heraus.
            // Diese Anzahlen müssen jetzt noch addiert werden
            // Man kann von keiner Sortierung ausgehen, also wird jede mit jeder verglichen
            //

            for (int i = 0; i < dv.Table.Rows.Count; i++)
            {
                DataRow row1 = dv.Table.Rows[i];

                int j = i + 1;
                while (j < dv.Table.Rows.Count)
                {
                    DataRow row2 = dv.Table.Rows[j];

                    //
                    // alle mit der gleichen Richtlinien LfdNummer gehören zusammen
                    // da die Richtlinien alle zu einem Gebiet gehören, kommt eine LfdNummer logisch einmal vor,
                    // und nicht zweimal mit verschiedenem Text
                    //
                    if (ConvertToInt32(row1["LfdNummer"]) == ConvertToInt32((row2["LfdNummer"])))
                    {
                        row1["Anzahl"] = ConvertToInt32(row1["Anzahl"]) + ConvertToInt32(row2["Anzahl"]);
                        dv.Table.Rows.RemoveAt(j);
                    }
                    else
                    {
                        j++;
                    }
                }
            }

            return dv;
        }

        public DataView GetGebiete()
        {
            return _databaseLayerCommon.GetGebiete();
        }
        public DataView GetGebiete(bool includeAll)
        {
            return _databaseLayerCommon.GetGebiete(includeAll);
        }

        public void PopulateGebiete(ComboBox cb)
        {
            DataView dv = GetGebiete();

            cb.ValueMember = "ID_Gebiete";
            cb.DisplayMember = "Gebiet";
            cb.DataSource = dv;
        }
        public void PopulateGebiete(System.Web.UI.WebControls.DropDownList cb)
        {
            DataView dv = GetGebiete();

            cb.DataSource = dv;
            cb.DataValueField = "ID_Gebiete";
            cb.DataTextField = "Gebiet";
            cb.DataBind();
        }

        public void PopulateFunktionen(ComboBox comboBox)
        {
            DataView dv = GetChirurgenFunktionen();

            comboBox.ValueMember = "ID_ChirurgenFunktionen";
            comboBox.DisplayMember = "Funktion";
            comboBox.DataSource = dv;
        }
        public void PopulateFunktionen(System.Web.UI.WebControls.DropDownList comboBox)
        {
            DataView dv = GetChirurgenFunktionen();

            comboBox.DataSource = dv;
            comboBox.DataValueField = "ID_ChirurgenFunktionen";
            comboBox.DataTextField = "Funktion";
            comboBox.DataBind();
        }

        public ArrayList GetUserRights(int ID_Chirurgen)
        {
            return _databaseLayerCommon.GetUserRights(ID_Chirurgen);
        }
        #region UserSettings
        public DataView GetUserSettings(int ID_Chirurgen, string section)
        {
            return _databaseLayerCommon.GetUserSettings(ID_Chirurgen, section);
        }

        public DataRow GetUserSettings(int ID_Chirurgen, string section, string key)
        {
            return _databaseLayerCommon.GetUserSettings(ID_Chirurgen, section, key);
        }

        public string GetUserSettingsString(string section, string key)
        {
            string value = "";

            DataRow dataRow = _databaseLayerCommon.GetUserSettings(CurrentUser_ID_Chirurgen, section, key);
            if (dataRow != null)
            {
                value = (string) dataRow["Value"];
            }

            return value;
        }

        public string GetUserSettingsString(int ID_Chirurgen, string section, string key)
        {
            string value = "";

            DataRow dataRow = _databaseLayerCommon.GetUserSettings(ID_Chirurgen, section, key);
            if (dataRow != null)
            {
                value = (string)dataRow["Value"];
            }

            return value;
        }

        public string GetUserSettingsString(string section, string key, string defaultValue)
        {
            string value = defaultValue;

            DataRow dataRow = _databaseLayerCommon.GetUserSettings(CurrentUser_ID_Chirurgen, section, key);
            if (dataRow != null)
            {
                value = (string)dataRow["Value"];
            }

            return value;
        }

        public string GetUserSettingsString(int ID_Chirurgen, string section, string key, string defaultValue)
        {
            string value = defaultValue;

            DataRow dataRow = _databaseLayerCommon.GetUserSettings(ID_Chirurgen, section, key);
            if (dataRow != null)
            {
                value = (string)dataRow["Value"];
            }

            return value;
        }


/*        public int DeleteUserSettings(int ID_Chirurgen)
        {
            return _databaseLayerCommon.DeleteUserSettings(ID_Chirurgen);
        }
 */

        public int DeleteUserSettings(string section)
        {
            return _databaseLayerCommon.DeleteUserSettings(CurrentUser_ID_Chirurgen, section);
        }

        public int DeleteUserSettings(int ID_Chirurgen, string section)
        {
            return _databaseLayerCommon.DeleteUserSettings(ID_Chirurgen, section);
        }
        public int InsertUserSettings(int ID_Chirurgen, string section, string key, string value)
        {
            return _databaseLayerCommon.InsertUserSettings(ID_Chirurgen, section, key, value);
        }
        public void SaveUserSettings(string section, string key, string value)
        {
            _databaseLayerCommon.SaveUserSettings(CurrentUser_ID_Chirurgen, section, key, value);
        }
        public void SaveUserSettings(int ID_Chirurgen, string section, string key, string value)
        {
            _databaseLayerCommon.SaveUserSettings(ID_Chirurgen, section, key, value);
        }
        public bool SetUserSettings(int ID_Chirurgen, string section, string key, string value, byte[] blob)
        {
            return _databaseLayerCommon.SetUserSettings(ID_Chirurgen, section, key, value, blob);
        }
        public DataRow CreateDataRowUserSettings(DataTable dt, int ID_Chirurgen, string section, string key, string value)
        {
            return _databaseLayerCommon.CreateDataRowUserSettings(dt, ID_Chirurgen, section, key, value);
        }
        public DataView GetUserSettings(int ID_Chirurgen)
        {
            return _databaseLayerCommon.GetUserSettings(ID_Chirurgen);
        }
        #endregion

        public void PopulateChirurgen(ComboBox cb)
        {
            DataView dv = GetChirurgen();

            cb.ValueMember = "ID_Chirurgen";
            cb.DisplayMember = "DisplayText";
            cb.DataSource = dv;

            int index = -1;

            // Der angemeldete Chirurg muss genau bestimmt werden!
            // wegen aufruf aus Fenster Einzelübersicht.
            for (int i = 0; i < cb.Items.Count; i++)
            {
                DataRow row = ((DataRowView)cb.Items[i]).Row;
                if (ConvertToInt32(row["ID_Chirurgen"]) == CurrentUser_ID_Chirurgen)
                {
                    index = i;
                    break;
                }
            }
            if (index != -1)
            {
                cb.SelectedIndex = index;
            }
        }

        public void PopulateChirurgen(System.Web.UI.WebControls.DropDownList comboBox)
        {
            DataView dv = GetChirurgen();

            comboBox.DataSource = dv;
            comboBox.DataValueField = "ID_Chirurgen";
            comboBox.DataTextField = "DisplayText";
            comboBox.DataBind();
        }


        /// <summary>
        /// Holt alle Chirurgen. Wenn der angemeldete Benutzer Admin ist,
        /// alle Chirurgen, ansonsten nur sich selber.
        /// </summary>
        /// <returns></returns>
        public DataView GetChirurgen()
        {
            return GetAktiveChirurgen();
        }


        /// <summary>
        /// Holt alle Chirurgen. Wenn der angemeldete Benutzer Admin ist,
        /// alle Chirurgen, ansonsten nur sich selber.
        /// </summary>
        /// <returns></returns>
        public DataView GetAktiveChirurgen()
        {
            return GetChirurgen("Aktiv <> 0");
        }

        /// <summary>
        /// Holt alle Chirurgen. Wenn der angemeldete Benutzer Admin ist,
        /// alle Chirurgen, ansonsten nur sich selber.
        /// </summary>
        /// <returns></returns>
        public DataView GetInaktiveChirurgen()
        {
            return GetChirurgen("Aktiv = 0");
        }

        /// <summary>
        /// Holt alle Chirurgen mit einer bestimmten WHERE Bedingung.
        /// Mit Recht select.surgeons.all bekommt man ALLE
        /// Mit Recht select.surgeons.abteilung bekommt man alle von allen Abteilungen, zu denen ich gehöre
        /// Wenn ich Weiterbilder bin, bekomme ich alle, die mir zugeordnet sind.
        /// Wenn ich Weiterbilder bin und das Recht select.surgeons.abteilung habe, bekommen ich alle,
        /// die mir zugeordnet sind und alle von allen Abteilungen, zu denen ich gehöre
        /// Nicht aktive Chirurgen werden immer weggelassen
        /// </summary>
        /// <returns></returns>
        private DataView GetChirurgen(string where)
        {
            return _databaseLayerCommon.GetChirurgen(ConvertToInt32(_currentUser["ID_Chirurgen"]), where);
        }

        public DataRow GetChirurg(int iID)
        {
            return _databaseLayerCommon.GetChirurg(iID);
        }

        public DataView GetChirurgenFunktionen()
        {
            if (_chirurgenFunktionen == null)
            {
                _chirurgenFunktionen = _databaseLayerCommon.GetChirurgenFunktionen();
            }

            return _chirurgenFunktionen;
        }

        #region ISecurityManager

        /// <summary>
        /// Hat der angemeldete Benutzer das übergeben Recht?
        /// </summary>
        /// <param name="right">Das Recht</param>
        /// <returns></returns>
        public bool UserHasRight(string right)
        {
            return _currentUserRights.Contains(right.ToLower());
        }

        public Label FindNearestLabel(Control master)
        {
            Form form = master.FindForm();
            Label nearestLabel = null;

            foreach (Control control in form.Controls)
            {
                if (control != master)
                {
                    nearestLabel = FindNearestLabel(master, nearestLabel, control);
                }
            }

            return nearestLabel;
        }

        #endregion

        /// <summary>
        /// Calculate the distance between two controls. Note that Control.Top etc. refer to its container.
        /// We must calculate the distance in respect to the form.
        /// </summary>
        /// <param name="control1"></param>
        /// <param name="control2"></param>
        /// <returns></returns>
        private double ControlDistance(Control control1, Control control2)
        {
            double distance = 0;

            if (control2 == null)
            {
                distance = Double.MaxValue;
            }
            else
            {
                Point pt1 = control1.PointToScreen(new Point(control1.Left, control1.Top));
                Point pt2 = control2.PointToScreen(new Point(control2.Left, control2.Top));

                int dx = Math.Abs(pt1.X - pt2.X);
                int dy = Math.Abs(pt1.Y - pt2.Y);

                distance = Math.Sqrt(dy * dy + dx * dx);
            }

            return distance;
        }

        private bool ControlIsLabel(Control c)
        {
            bool isLabel = false;

            Type type = c.GetType();

            while (type != null)
            {
                if (type == typeof(System.Windows.Forms.Label))
                {
                    isLabel = true;
                    break;
                }
                type = type.BaseType;
            }

            return isLabel;
        }

        private Label FindNearestLabel(Control master, Label nearestLabel, Control control)
        {
            if (control != nearestLabel)
            {
                if (ControlIsLabel(control))
                {
                    if (ControlDistance(master, control) < ControlDistance(master, nearestLabel))
                    {
                        nearestLabel = (Label)control;
                    }
                }
            }

            foreach (Control child in control.Controls)
            {
                nearestLabel = FindNearestLabel(master, nearestLabel, child);
            }

            return nearestLabel;
        }

        /// <summary>
        /// Hat der übergebene Benutzer das angegebene Recht?
        /// </summary>
        /// <param name="ID_Chirurgen">Der Benutzer</param>
        /// <param name="right">Das Recht</param>
        /// <returns></returns>
        public bool UserHasRight(int ID_Chirurgen, string right)
        {
            bool hasRight = false;

            if (ID_Chirurgen == CurrentUser_ID_Chirurgen)
            {
                hasRight =  _currentUserRights.Contains(right.ToLower());
            }
            else
            {
                hasRight = _databaseLayerCommon.UserHasRight(ID_Chirurgen, right);
            }

            return hasRight;
        }

        public List<DateTime> GetWeiterbildungsZeitraeume(int ID_Chirurgen)
        {
            List<DateTime> wbzr = new List<DateTime>();

            string strCount = GetUserSettingsString(CurrentUser_ID_Chirurgen, GlobalConstants.SectionWbzr, GlobalConstants.KeyWbzrCount, "0");
            int count = Convert.ToInt32(strCount);

            for (int i = 1; i <= count; i++)
            {
                string strI = i.ToString();
                string strDate = GetUserSettingsString(CurrentUser_ID_Chirurgen, GlobalConstants.SectionWbzr, strI);
                if (Tools.DateIsValidGermanDate(strDate))
                {
                    DateTime date = Tools.InputTextDate2DateTime(strDate);
                    wbzr.Add(date);
                }
                else
                {
                    // any bad date resets all dates
                    wbzr.Clear();
                    DeleteUserSettings(CurrentUser_ID_Chirurgen, GlobalConstants.SectionWbzr);
                    break;
                }
            }

            wbzr.Sort();

            return wbzr;
        }


        public void AuswertungenGruppiereAnzahl(DataView dvManuell)
        {
            int count = 0;
            // Bei den manuell eingegebenen kann es mehrfache Einträge für dieselbe Richtlinie geben,
            // etwas mit unterschiedlichem Ort/Datum,
            // diese müssen erstmal zusammengefasst werden
            for (int i = 0; i < dvManuell.Table.Rows.Count; i++)
            {
                DataRow row1 = dvManuell.Table.Rows[i];

                int j = i + 1;
                while (j < dvManuell.Table.Rows.Count)
                {
                    count++;
                    if (count % AbortLoopCount == 0)
                    {
                        FireProgressEvent(_progressEventArgs);
                        if (_progressEventArgs.Cancel)
                        {
                            break;
                        }
                    }

                    DataRow row2 = dvManuell.Table.Rows[j];

                    if (ConvertToInt32(row1["LfdNummer"]) == ConvertToInt32(row2["LfdNummer"]))
                    {
                        // Wenn etwas addiert wird, müssen Ort und Datum wegfallen
                        // mySQL: count() ist long ,sonst int
                        row1["Anzahl"] = ConvertToInt32(ConvertToInt64(row1["Anzahl"]) + ConvertToInt64(row2["Anzahl"]));
                        
                        //
                        // bei row["Datum"] funktioniert == nicht aber .Equals(), bei string weiß ich nicht
                        //
                        if (((string)row1["Ort"] != (string)row2["Ort"]) || (!row1["Datum"].Equals(row2["Datum"])))
                        {
                            row1["Ort"] = "";
                            row1["Datum"] = DBNull.Value;
                        }
                        dvManuell.Table.Rows.RemoveAt(j);
                    }
                    else
                    {
                        j++;
                    }
                }
            }
        }

        public void AuswertungenMergeRichtzahlen(DataView dvAuto, DataView dvManuell, bool bAll)
        {
            int count = 0;
            for (int i = 0; i < dvManuell.Table.Rows.Count; i++)
            {
                count++;
                if (count % AbortLoopCount == 0)
                {
                    FireProgressEvent(_progressEventArgs);
                    if (_progressEventArgs.Cancel)
                    {
                        break;
                    }
                }

                DataRow rowManuell = dvManuell.Table.Rows[i];
                DataRow newRowAuto = dvAuto.Table.NewRow();

                newRowAuto["LfdNummer"] = rowManuell["LfdNummer"];
                newRowAuto["Richtzahl"] = rowManuell["Richtzahl"];
                newRowAuto["RichtlinieOPSKode"] = GetText("RichtlinienVergleichView", "zuordnung_extern");
                newRowAuto["UntBehMethode"] = rowManuell["UntBehMethode"];
                if (bAll)
                {
                    newRowAuto["Datum"] = rowManuell["Datum"];
                }
                newRowAuto["OPSKode"] = rowManuell["Anzahl"];
                newRowAuto["OPSText"] = rowManuell["Ort"];

                //
                // Die extern erfasste Richtzahlen stehen am Anfang der jeweiligen Weiterbildungsrichtlinie
                // Sobald man die erste gleiche Richtlinie gefunden hat, wird der Eintrag dort am Anfang eingefügt
                // >=: Wenn es die Richtlinie nicht gibt, aber eine größere, wird der Eintrag dort eingefügt
                //
                bool inserted = false;
                for (int j = 0; j < dvAuto.Table.Rows.Count; j++)
                {
                    DataRow rowAuto = dvAuto.Table.Rows[j];
                    if ((int)rowAuto["LfdNummer"] >= (int)newRowAuto["LfdNummer"])
                    {
                        dvAuto.Table.Rows.InsertAt(newRowAuto, j);
                        inserted = true;
                        break;
                    }
                }
                if (!inserted)
                {
                    //
                    // Der Eintrag is größer als jede vorhandene Richtlinie
                    // Ans Ende anfügen
                    dvAuto.Table.Rows.Add(newRowAuto);
                    inserted = true;
                }
            }

            dvManuell.Table.Rows.Clear();

        }


        /// <summary>
        /// Merged die automatisch zugeordneten und die extern erfassten Richtzahlen
        /// </summary>
        /// <param name="dvAuto">Die automatisch errechneten Ist-Zahlen </param>
        /// <param name="dvManuell">Die manuell eingegebenen extern erfassten Zahlen</param>
        public void AuswertungenMergeIstZahlen(DataView dvAuto, DataView dvManuell)
        {
            int count = 0;

            AuswertungenGruppiereAnzahl(dvManuell);

            // gleiche Addieren
            foreach (DataRow rowAuto in dvAuto.Table.Rows)
            {
                for (int i = 0; i < dvManuell.Table.Rows.Count; i++)
                {
                    count++;
                    if (count % AbortLoopCount == 0)
                    {
                        FireProgressEvent(_progressEventArgs);
                        if (_progressEventArgs.Cancel)
                        {
                            break;
                        }
                    }

                    DataRow rowManuell = dvManuell.Table.Rows[i];

                    if (ConvertToInt32(rowAuto["LfdNummer"]) == ConvertToInt32(rowManuell["LfdNummer"]))
                    {
                        // Bullshit: bei mySQL ist count() long, by Access nur int
                        rowAuto["Anzahl"] = ConvertToInt32(ConvertToInt64(rowAuto["Anzahl"]) + ConvertToInt64(rowManuell["Anzahl"]));
                        dvManuell.Table.Rows.RemoveAt(i);
                    }
                }
            }

            // Die jetzt noch übrig sind, wurden nicht addiert und kommen ans Ende
            for (int i = 0; i < dvManuell.Table.Rows.Count; i++)
            {
                //
                // rowManuell muss nicht aus dvManuell.Table.Rows entfernt werden, weil dvManuell danach nicht mehr angefasst wird
                //
                DataRow rowManuell = dvManuell.Table.Rows[i];
                DataRow newRowAuto = dvAuto.Table.NewRow();
                newRowAuto["Anzahl"] = rowManuell["Anzahl"];
                newRowAuto["LfdNummer"] = rowManuell["LfdNummer"];
                newRowAuto["Richtzahl"] = rowManuell["Richtzahl"];
                newRowAuto["UntBehMethode"] = rowManuell["UntBehMethode"];

                bool inserted = false;
                for (int j = 0; j < dvAuto.Table.Rows.Count; j++)
                {
                    DataRow rowAuto = dvAuto.Table.Rows[j];
                    if (ConvertToInt32(newRowAuto["LfdNummer"]) <= ConvertToInt32(rowAuto["LfdNummer"]))
                    {
                        dvAuto.Table.Rows.InsertAt(newRowAuto, j);
                        inserted = true;
                        break;
                    }
                }

                if (!inserted)
                {
                    dvAuto.Table.Rows.Add(newRowAuto);
                }
            }
        }

        public int GetInternExternFlag(bool intern, bool ext)
        {
            int flag = OperationQuelleAlle;

            if (intern && ext)
            {
                flag = OperationQuelleAlle;
            }
            else if (intern)
            {
                flag = OperationQuelleIntern;
            }
            else if (ext)
            {
                flag = OperationQuelleExtern;
            }

            return flag;
        }

        #region Richtlinien
        public DataView GetRichtlinien(int nGebiete)
        {
            return _databaseLayerCommon.GetRichtlinien(nGebiete);
        }
        #endregion

        #region ChirurgenRichtlinien
        public DataView GetChirurgenRichtlinien(int ID_Chirurgen, int ID_Gebiete)
        {
            return _databaseLayerCommon.GetChirurgenRichtlinien(ID_Chirurgen, ID_Gebiete);
        }
        public DataView GetChirurgenRichtlinien(int ID_Chirurgen, int ID_Gebiete, DateTime? von, DateTime? bis)
        {
            return _databaseLayerCommon.GetChirurgenRichtlinien(ID_Chirurgen, ID_Gebiete, von, bis);
        }
        public DataView GetChirurgenRichtlinienSummen(int ID_Chirurgen, int ID_Gebiete, DateTime? von, DateTime? bis)
        {
            return _databaseLayerCommon.GetChirurgenRichtlinienSummen(ID_Chirurgen, ID_Gebiete, von, bis);
        }
        public DataView GetChirurgenRichtlinienRichtlinie(int ID_Chirurgen, int ID_Richtlinien, DateTime? von, DateTime? bis)
        {
            return _databaseLayerCommon.GetChirurgenRichtlinienRichtlinie(ID_Chirurgen, ID_Richtlinien, von, bis);
        }
        public bool DeleteChirurgenRichtlinien(int ID_ChirurgenRichtlinien)
        {
            return _databaseLayerCommon.DeleteChirurgenRichtlinien(ID_ChirurgenRichtlinien);
        }
        public DataRow GetChirurgenRichtlinien(int ID_ChirurgenRichtlinien)
        {
            return _databaseLayerCommon.GetChirurgenRichtlinien(ID_ChirurgenRichtlinien);
        }
        public long GetChirurgenRichtlinienCount(int ID_Chirurgen, int ID_Richtlinien)
        {
            return _databaseLayerCommon.GetChirurgenRichtlinienCount(ID_Chirurgen, ID_Richtlinien);
        }
        public int InsertChirurgenRichtlinien(DataRow dataRow)
        {
            return _databaseLayerCommon.InsertChirurgenRichtlinien(dataRow);
        }
        public bool UpdateChirurgenRichtlinien(DataRow dataRow)
        {
            return _databaseLayerCommon.UpdateChirurgenRichtlinien(dataRow);
        }
        #endregion

        #region OPFunktionen

        public void PopulateOPFunktionen(System.Web.UI.WebControls.DropDownList comboBox, bool includeAll)
        {
            DataView dv = GetOPFunktionen(includeAll);

            comboBox.DataSource = dv;
            comboBox.DataValueField = "ID_OPFunktionen";
            comboBox.DataTextField = "Beschreibung";
            //comboBox.SelectedValue = OP_FUNCTION.OP_FUNCTION_OP;
            comboBox.DataBind();
        }

        public DataView GetOPFunktionen()
        {
            return GetOPFunktionen(false);
        }

        public DataView GetOPFunktionen(bool includeAll)
        {
            return _databaseLayerCommon.GetOPFunktionen(includeAll);
        }

        public DataRow GetOPFunktion(int ID_OPFunktionen)
        {
            return _databaseLayerCommon.GetOPFunktion(ID_OPFunktionen);
        }
        #endregion

        #region Operationen
        public DataView GetIstOperationen(DateTime? von, DateTime? bis, string ops, int quelle, int ID_OPFunktionen)
        {
            return _databaseLayerCommon.GetIstOperationen(ConvertToInt32(_currentUser["ID_Chirurgen"]), von, bis, ops, quelle, ID_OPFunktionen);
        }

        public long GetIstOperationenCount(DateTime? von, DateTime? bis, string ops, int quelle, int opFunktionen)
        {
            int ID_ChirurgenLogin = ConvertToInt32(_currentUser["ID_Chirurgen"]);

            return _databaseLayerCommon.GetIstOperationenCount(ID_ChirurgenLogin, von, bis, ops, quelle, opFunktionen);
        }
        public long GetIstOperationenCount(DateTime? von, DateTime? bis, string ops, int quelle, int opFunktionen, int ID_Chirurgen)
        {
            return _databaseLayerCommon.GetIstOperationenCount(ID_Chirurgen, von, bis, ops, quelle, opFunktionen);
        }

        public long GetOperationenCount(string filterOpsKode, string filterOpsText)
        {
            return _databaseLayerCommon.GetOperationenCount(filterOpsKode, filterOpsText);
        }

        public long GetOperationenCount(string filterOps)
        {
            return _databaseLayerCommon.GetOperationenCount(filterOps);
        }

        #endregion

        /// <summary>
        /// Menge aller Richtlinien mit zugeordneten OPs.
        /// Es kommen nur Richtlinien, die auch OPs zugeordnet haben, 
        /// die von Chirurg nID_Chirurgen sind und im angegebenen Zeitraum liegen.
        /// </summary>
        /// <param name="nID_Chirurgen"></param>
        /// <param name="strFrom"></param>
        /// <param name="strTo"></param>
        /// <returns></returns>
        public DataView GetRichtlinienOPs(int nID_Gebiete, int nID_Chirurgen, int quelle, DateTime? dtFrom, DateTime? dtTo, bool bAll)
        {
            DataView dv;

            if (bAll)
            {
                dv = _databaseLayerCommon.GetRichtlinienOPsAll(nID_Gebiete, nID_Chirurgen, quelle, dtFrom, dtTo);
            }
            else
            {
                dv = _databaseLayerCommon.GetRichtlinienOPs(nID_Gebiete, nID_Chirurgen, quelle, dtFrom, dtTo);
            }

            return dv;
        }


        public string FormatRichtzahl(string strRichtzahl)
        {
            string strFormat = "{0," + RICHTZAHL.Length.ToString() + "}";

            if (strRichtzahl == "-1")
            {
                strRichtzahl = BusinessLayerCommon.BasisKenntnis;
            }
            else if (strRichtzahl == "0")
            {
                strRichtzahl = "";
            }
            string s = String.Format(strFormat, strRichtzahl);
            return s;
        }
        #region GetIstOperationen

        public DataView GetIstOperationenChirurgMitRichtlinien(int nID_Chirurgen, string ops, int nID_OPFunktionen, int quelle, bool sortOrderDescending, DateTime? von, DateTime? bis)
        {
            return _databaseLayerCommon.GetIstOperationenChirurgMitRichtlinien(nID_Chirurgen, ops, nID_OPFunktionen, quelle, sortOrderDescending, von, bis);
        }

        public DataView GetIstOperationenChirurg(int nID_Chirurgen, int ID_OPFunktionen, int quelle, DateTime? dtVon, DateTime? dtBis, bool sortOrderDescending)
        {
            return _databaseLayerCommon.GetIstOperationenChirurg(nID_Chirurgen, ID_OPFunktionen, quelle, dtVon, dtBis, sortOrderDescending);
        }
        #endregion

        public int GetIntegerFromConfig(string section, string key, int minValue, int maxValue, int defaultValue)
        {
            int ret = defaultValue;
            string s = GetConfigValue(section + "." + key);

            int n;
            if (Int32.TryParse(s, out n) && (n >= minValue) && (n <= maxValue))
            {
                ret = n;
            }

            return ret;
        }

        public DataView GetChirurgOperationenOverview(int nID_Chirurgen, int nID_OPFunktionen, int quelle,
            DateTime? von, DateTime? bis)
        {
            int nRelevantPositions = GetIntegerFromConfig(
                    GlobalConstants.SectionOther,
                    GlobalConstants.KeyOtherRelevantPositionsOPSCode,
                    GlobalConstants.OtherRelevantPositionsMin,
                    GlobalConstants.OtherRelevantPositionsMax,
                    GlobalConstants.OtherRelevantPositionsDefault
                    );

            return _databaseLayerCommon.GetChirurgOperationenOverview(nID_Chirurgen, nID_OPFunktionen, quelle, von, bis, nRelevantPositions);
        }

        #region Config
        public DataView GetConfig()
        {
            return DatabaseLayerCommon.GetConfig();
        }
        public DataRow GetConfig(string key)
        {
            return DatabaseLayerCommon.GetConfig(key);
        }
        public string GetConfigValue(string key)
        {
            return DatabaseLayerCommon.GetConfigValue(key);
        }
        public DataRow CreateDataRowConfig(DataTable dt, string key, string value)
        {
            return _databaseLayerCommon.CreateDataRowConfig(dt, key, value);
        }
        public bool SaveCustomerData(DataTable table)
        {
            return _databaseLayerCommon.SaveCustomerData(table);
        }
        public void SaveConfig(string key, string value)
        {
            _databaseLayerCommon.SaveConfig(key, value);
        }

        #endregion

        public void GetOperationen(System.Web.UI.WebControls.DataGrid dg, string filterOpsKode, string filterOpsText)
        {
            _databaseLayerCommon.GetOperationen(dg, filterOpsKode, filterOpsText);
        }

        public DataView GetOperationen()
        {
            return _databaseLayerCommon.GetOperationen();
        }

        public int GetCountSerialNumbers()
        {
            return _databaseLayerCommon.GetCountSerialNumbers();
        }

        #region Zeitraeume
        public DataRow CreateDataRowZeitraeume()
        {
            return _databaseLayerCommon.CreateDataRowZeitraeume();
        }

        public DataView GetZeitraeume()
        {
            return _databaseLayerCommon.GetZeitraeume();
        }

        public bool DeleteZeitraum(int ID_Zeitraeume)
        {
            this.Write2Log(Delete, "Zeitraeume", ID_Zeitraeume);
            return _databaseLayerCommon.DeleteZeitraum(ID_Zeitraeume);
        }

        public bool UpdateZeitraum(DataRow row)
        {
            return _databaseLayerCommon.UpdateZeitraum(row);
        }

        public int InsertZeitraum(DataRow dataRow)
        {
            this.Write2Log(Insert, "Zeitraeume " + dataRow["Von"].ToString() + dataRow["Bis"].ToString());

            return _databaseLayerCommon.InsertZeitraum(dataRow);
        }

        public DataRow GetZeitraum(int ID_Zeitraeume)
        {
            return _databaseLayerCommon.GetZeitraum(ID_Zeitraeume);
        }

        #region Log

        public void Write2Log(string sAction, string sMessage, int iID1, int iID2)
		{
            this.Write2Log(sAction, sMessage + " (" + iID1.ToString() + "," + iID2.ToString() + ")");
		}

        public void Write2Log(string sAction, string sMessage, int iID)
		{
            this.Write2Log(sAction, sMessage + " (" + iID.ToString() + ")");
		}
        public void Write2Log(string sAction, string sMessage)
		{
            if (_write2Log)
			{
                string strUser;

                if (_currentUser == null)
                {
                    strUser = "*";
                }
                else
                {
                    strUser = (string)_currentUser["UserID"];
                }

                if (strUser.Length > 50)
                {
                    strUser = strUser.Substring(0, 50);
                }
                if (sAction.Length > 20)
                {
                    sAction = sAction.Substring(0, 20);
                }
                if (sMessage.Length > 250)
                {
                    sMessage = sMessage.Substring(0, 250);
                }

                _databaseLayerCommon.Write2Log(strUser, sAction, sMessage);
			}
		}

        public DataView GetLogTable(string strNumRecords, string strUser, string strVon, string strBis, string strAktion, string strMessage)
        {
            return _databaseLayerCommon.GetLogTable(strNumRecords, strUser, strVon, strBis, strAktion, strMessage);
        }
        public long GetLogTableCount()
        {
            return _databaseLayerCommon.GetLogTableCount();
        }
        public void DeleteLogTable()
        {
            _databaseLayerCommon.DeleteLogTable();
        }
        public bool DeleteLogTable(int ID_LogTable)
        {
            return _databaseLayerCommon.DeleteLogTable(ID_LogTable);
        }

		#endregion

        #endregion
    }
}

