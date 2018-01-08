using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

using AppFramework;
using AppFramework.Exceptions;

namespace CMaurer.Operationen.AppFramework
{
    public class UserConfiguration
    {
        private Dictionary<string, Dictionary<string, string>> _sections;

        private BusinessLayerCommon _businessLayer;
        private int _ID_Chirurgen = -1;

        public UserConfiguration(BusinessLayerCommon b)
        {
            _ID_Chirurgen = -1;
            _businessLayer = b;
            _sections = new Dictionary<string, Dictionary<string, string>>();
        }

        public string GetValue(string section, string key)
        {
            return GetValue(section, key, null);
        }

        public string GetValue(string section, string key, string defaultValue)
        {
            string value = "";

            if (defaultValue != null)
            {
                value = defaultValue;
            }

            if (!_sections.ContainsKey(section))
            {
                _sections.Add(section, new Dictionary<string, string>());
            }

            Dictionary<string, string> dicSection = _sections[section];

            if (dicSection.ContainsKey(key))
            {
                value = dicSection[key];
            }

            return value;
        }

        bool ValueExists(string section, string key)
        {
            bool exists = false;
            string value;
            Dictionary<string, string> dictSection;

            if (_sections.TryGetValue(section, out dictSection))
            {
                if (dictSection.TryGetValue(key, out value))
                {
                    exists = true;
                }
            }

            return exists;
        }

        public void SetValue(string section, string key, string value)
        {
            if (!_sections.ContainsKey(section))
            {
                _sections.Add(section, new Dictionary<string, string>() );
            }

            Dictionary<string, string> dictSections = _sections[section];
            dictSections.Remove(key);
            dictSections[key] = value;
        }

        public void RemoveSectionKey(string section, string key)
        {
            Dictionary<string, string> htSection;

            if (_sections.TryGetValue(section, out htSection))
            {
                htSection.Remove(key);
            }
        }

        public void RemoveSection(string section)
        {
            if (_sections.ContainsKey(section))
            {
                _sections.Remove(section);
            }
        }

        public void Save(int ID_Chirurgen, string section)
        {
            if (ID_Chirurgen != -1)
            {
                _businessLayer.DeleteUserSettings(ID_Chirurgen, section);

                if (_sections.ContainsKey(section))
                {
                    Dictionary<string, string> htSection = _sections[section];
                    foreach (string key in htSection.Keys)
                    {
                        _businessLayer.InsertUserSettings(ID_Chirurgen, section, key, htSection[key]);
                    }
                }
            }
        }

        /// <summary>
        /// Save by deleting all user settings and then insert-ing them again
        /// </summary>
        public void Save()
        {
            if (_ID_Chirurgen != -1)
            {
                _businessLayer.DeleteUserSettings(_ID_Chirurgen);

                foreach (KeyValuePair<string, Dictionary<string, string>> kvp in _sections)
                {
                    string section = kvp.Key;
                    Dictionary<string, string> kvp2 = kvp.Value;

                    foreach (string key in kvp2.Keys)
                    {
                        string value = kvp2[key];
                        _businessLayer.InsertUserSettings(_ID_Chirurgen, section, key, value);
                    }
                }
            }
        }

        public void Read(int ID_Chirurgen)
        {
            _ID_Chirurgen = ID_Chirurgen;

            DataView dv = _businessLayer.GetUserSettings(ID_Chirurgen);

            foreach (DataRow row in dv.Table.Rows)
            {
                int ID_Chirurgen2 = _businessLayer.ConvertToInt32(row["ID_Chirurgen"]);
                string section = (string)row["Section"];
                string key = (string)row["Key"];
                string value = (string)row["Value"];

                SetValue(section, key, value);
            }
        }
    }
}
