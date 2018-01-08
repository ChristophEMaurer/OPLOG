using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;


namespace AppFramework
{
    public class ConfigurationFile
	{
        System.Collections.Hashtable _sections;
		System.Collections.Hashtable _values;

        public ConfigurationFile()
		{
			_sections = new System.Collections.Hashtable();
			_values = new System.Collections.Hashtable();
		}

		public void Read(string fileName)
		{
            System.IO.StreamReader reader = null;

            try
            {
                reader = new StreamReader(fileName);
                Parse(reader);
            }
            catch
            {
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
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
                _sections.Add(section, new Hashtable());
            }

			System.Collections.Hashtable KeyTable = (System.Collections.Hashtable)_sections[section];

			if (KeyTable.ContainsKey(key))
			{
				value = (string)KeyTable[key];
			}

            return value;
		}

		public string GetValue(string key)
		{
            string value = "";

			if (_values.ContainsKey(key))
			{
				value =  (string)_values[key];
			}
            return value;
		}

		public void SetValue(string key, string valueName, string value)
		{
            if (!_sections.Contains(key))
            {
                _sections.Add(key, new System.Collections.Hashtable());
            }

			System.Collections.Hashtable KeyTable = (System.Collections.Hashtable)_sections[key];
			if (KeyTable != null)
			{
				KeyTable[valueName] = value;
			}
		}

		public void SetValue(string valueName, string value)
		{
			_values[valueName] = value;
		}


		public void AddKey(string newKey)
		{
			System.Collections.Hashtable New = new System.Collections.Hashtable();
			_sections[newKey] = New;
		}

        public void RemoveSectionKey(string section, string key)
        {
            if (_sections.ContainsKey(section))
            {
                System.Collections.Hashtable htSection = (System.Collections.Hashtable)_sections[section];
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

		public void Save(string fileName)
		{
            System.IO.StreamWriter sw = null;

            try
            {
                sw = new System.IO.StreamWriter(fileName);
                System.Collections.IDictionaryEnumerator Enumerator = _values.GetEnumerator();
                //Print values
                while (Enumerator.MoveNext())
                {
                    sw.WriteLine("{0}={1}", Enumerator.Key, Enumerator.Value);
                }
                Enumerator = _sections.GetEnumerator();
                while (Enumerator.MoveNext())
                {
                    System.Collections.IDictionaryEnumerator Enumerator2nd = ((System.Collections.Hashtable)Enumerator.Value).GetEnumerator();
                    sw.WriteLine("[{0}]", Enumerator.Key);
                    while (Enumerator2nd.MoveNext())
                    {
                        sw.WriteLine("{0}={1}", Enumerator2nd.Key, Enumerator2nd.Value);
                    }
                }
            }
            catch
            {
            }
            finally
            {
                if (sw != null)
                {
                    sw.Close();
                }
            }
		}

		private void Parse(System.IO.TextReader reader)
		{
			System.Collections.Hashtable currentKey = null;
			string line;
            string valueName;
            string value;

			while (null != (line = reader.ReadLine()))
			{
				int j;
                int i = 0;

				while (line.Length > i && Char.IsWhiteSpace(line, i)) i++;//skip white space in beginning of line

				if (line.Length <= i)
					continue;
				if( line[i] == ';')//Comment
					continue;
				if (line[i] == '[')//Start new Key
				{
					string keyName;
					j = line.IndexOf(']',i);
					if (j == -1)//last ']' not found
						throw new Exceptions.InvalidInputException();

					keyName = line.Substring(i + 1, j - i - 1).Trim();

					if(!_sections.ContainsKey(keyName))
					{
						this.AddKey(keyName);
					}
					currentKey = (System.Collections.Hashtable)_sections[keyName];
					while (line.Length > ++j && Char.IsWhiteSpace(line, j));//skip white space in beginning of line
					if (line.Length > j)
					{
						if (line[j] != ';')//Anything but a comment is unacceptable after a key name
							throw new Exceptions.InvalidInputException();
					}
					continue;
				}
				//Start of a value name, ends with a '='
				j = line.IndexOf('=', i);
				if (j == -1)
					throw new Exceptions.InvalidInputException();
                valueName = line.Substring(i, j - i).Trim();
				if ((i = line.IndexOf(';', j + 1)) != -1)//remove comments from end of line
					value = line.Substring(j + 1, i - (j + 1)).Trim();
				else 
					value = line.Substring(j + 1).Trim();
				if (currentKey != null)
				{
					currentKey[valueName] = value;
				}
				else
				{
					_values[valueName] = value;
				}
			}
		}
	}
}
