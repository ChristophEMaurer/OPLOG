using System;
using System.Xml;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Globalization;

using Utility;

namespace Operationen.Wizards.ImportOPS
{
    /// <summary>
    /// In der Debug-Version werden alle eingelesenen Operationen in die ausgewählt Datei
    /// +  "-list.txt" geschrieben
    /// </summary>
    public class OPSImporter : ImporterExporter
    {
        private const string FormName = "Wizards_ImportOPS_OPSImporter";

        private Dictionary<string, string> _import;
        private string _lastCode;
        private string _lastText;
        private string _format;
        StreamReader _reader;
        
        //
        // when counting the 'lines' in the xml, this contains all entries of type "//Class[@kind='category' or @kind='chapter']"
        //
        private Dictionary<string, string> _codes = new Dictionary<string, string>();
        private Dictionary<string, Dictionary<string, string>> _modifiers = new Dictionary<string, Dictionary<string, string>>();
        private XmlDocument _document = new XmlDocument();

#if DEBUG
        StreamWriter _writer;
        StreamWriter _logWriter;
        string _maxCode = "";
        string _maxText = "";
        int _lenMaxText;
#endif

        public OPSImporter(BusinessLayer b, ProgressBar progressBar)
            : base(b, progressBar)
        {
        }

        public void Initialize(string fileName, string format)
        {
            base.Initialize(fileName);
            _format = format;
        }

        public void ReadDatabase(Dictionary<string, string> database)
        {
            string sql = "select [OPS-Kode] as OPSKode, [OPS-Text] as OPSText from Operationen";
            DataView view = _businessLayer.DatabaseLayer.GetDataView(sql, "Operationen");

            TheProgressBar.Maximum = view.Table.Rows.Count;
            TheProgressBar.Value = 0;

            foreach (DataRow row in view.Table.Rows)
            {
                string key = ((string)row["OPSKode"]).Trim();

                if (!database.ContainsKey(key))
                {
                    database.Add(key, ((string)row["OPSText"]).Trim());
                    Progress();
                }
            }
        }

        /// <summary>
        /// 
        /// Counts all opsCodes without modifiers and reads them into a dictionary
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private long CountLinesXml(string fileName)
        {
            StreamReader streamReader = null;
            XmlTextReader xmlTextReader = null;

            using (streamReader = new StreamReader(fileName))
            {
                using (xmlTextReader = new XmlTextReader(streamReader))
                {
                    try
                    {
                        //
                        // 08.11.2015
                        // importing the 2016 xml catalog fails because the ClaML.dtd_ file is not found...
                        // This must have worked for 2015 because the generated debug file ops2015syst_claml_20141017.xml-list.txt exists.
                        //
                        FileInfo fileInfo = new FileInfo(fileName);
                        string folder = fileInfo.DirectoryName;

                        //
                        // Windows 10 is new, maybe this changed something? 
                        // We set the current directory, now the .dtd file is found.
                        //
                        Directory.SetCurrentDirectory(folder);
                        _document.Load(xmlTextReader);

                    }
                    catch (Exception exception)
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append(string.Format(CultureInfo.InvariantCulture, GetText(FormName, "error_xml"), fileName));
                        sb.AppendLine();
                        sb.Append(exception.Message);
                        string msg = sb.ToString();

                        _businessLayer.MessageBox(msg);
                    }

                    foreach (XmlNode node in _document.SelectNodes("//Class[@kind='category' or @kind='chapter']"))
                    {
                        string opsCode = node.Attributes["code"].Value;
                        string opsText = "";

                        //
                        // get the label 
                        //
                        foreach (XmlNode subnode in node.ChildNodes)
                        {
                            if (subnode.Name == "Rubric" && (subnode.Attributes["kind"].Value == "preferred"))
                            {
                                foreach (XmlNode subsubnode in subnode.ChildNodes)
                                {
                                    if (subsubnode.Name == "Label")
                                    {
                                        opsText = subsubnode.InnerText;
                                        if (!_codes.ContainsKey(opsCode))
                                        {
                                            _codes.Add(opsCode, opsText);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return _codes.Count;
        }

         /*
	        <ModifierClass code=".0" modifier="ST538">
         */
        private void ReadModifiers(XmlDocument xmlDocument)
        {
            foreach (Dictionary<string, string> entry in _modifiers.Values)
            {
                entry.Clear();
            }
            _modifiers.Clear();

            foreach (XmlNode node in xmlDocument.SelectNodes("//ModifierClass"))
            {
                string code = node.Attributes["code"].Value;
                string modifier = node.Attributes["modifier"].Value;

                //
                // get the label 
                //
                foreach (XmlNode subnode in node.ChildNodes)
                {
                    if (subnode.Name == "Rubric" && (subnode.Attributes["kind"].Value == "preferred"))
                    {
                        foreach (XmlNode subsubnode in subnode.ChildNodes)
                        {
                            if (subsubnode.Name == "Label")
                            {
                                string text = subsubnode.InnerText;

                                Dictionary<string, string> dictionary;
                                if (!_modifiers.TryGetValue(modifier, out dictionary))
                                {
                                    dictionary = new Dictionary<string, string>();
                                    _modifiers.Add(modifier, dictionary);
                                }
                                if (!dictionary.ContainsKey(code))
                                {
                                    dictionary.Add(code, text);
                                }
                            }
                        }
                    }
                }
            }
        }

#if false
        /// <summary>
        /// Return the text that is prefixed to the current entry
        /// This reads the xml file and parses all entries. This is very very slow
        /// </summary>
        /// <param name="superClass"></param>
        /// <returns></returns>
        private string GetSuperOpsTextXml(XmlDocument document, string code)
        {
            StringBuilder sb = new StringBuilder();
            string path;

            do
            {
                path = string.Format("//Class[@code='{0}']/SuperClass", code);
                foreach (XmlNode node in document.SelectNodes(path))
                {
                    code = node.Attributes["code"].Value;
                    break;
                }

                path = string.Format("//Class[@code='{0}']/Rubric/Label", code);
                foreach (XmlNode node in document.SelectNodes(path))
                {
                    string opsText = node.InnerText;
                    sb.Insert(0, ": ");
                    sb.Insert(0, opsText);
                    break;
                }
            } while (code.IndexOf('.') != -1);

            return sb.ToString();
        }
#endif

        /// <summary>
        /// Return the text that is prefixed to the current entry.
        /// This removes the last letter and addes the text for that code until there is no '.' left in the code:
        /// 5-123.xy
        /// 5-123.xy    text1
        /// 5-123.x     text2
        /// 5-123       text3
        /// -> "text3: text2: text1"
        /// </summary>
        /// <param name="superClass"></param>
        /// <returns></returns>
        private string GetSuperOpsText(string code)
        {
            StringBuilder sb = new StringBuilder();
            string opsText;

            while (code.IndexOf('.') != -1)
            {
                code = code.Substring(0, code.Length - 1);
                if (code[code.Length - 1] == '.')
                {
                    code = code.Substring(0, code.Length - 1);
                }

                if (_codes.TryGetValue(code, out opsText))
                {
                    sb.Insert(0, ": ");
                    sb.Insert(0, opsText);
                }
            } 

            return sb.ToString();
        }

        private void ParseXml()
        {
            ReadModifiers(_document);

            /*
             * 	<Class code="1" kind="chapter">
             
                <Class code="5-870.2" kind="category">
                    <Meta name="P17b-d" value="2"/>
                    <Meta name="DRG" value="J"/>
                    <SuperClass code="5-870"/>
                    <ModifiedBy all="true" code="ST5870">
                        <ValidModifierClass code="0"/>
                        <ValidModifierClass code="1"/>
                    </ModifiedBy>
                    <Rubric kind="preferred">
                        <Label xml:lang="de" xml:space="default">Duktektomie</Label>
                    </Rubric>
                </Class>
             
                <ModifiedBy all="false" code="ST5484">

             ####################################################################
             
             	<Class code="5-433.3" kind="category">
		            <Meta name="ZusatzK" value="N"/>
		            <Meta name="EinmalK" value="N"/>
		            <Meta name="P17b-d" value="2"/>
		            <Meta name="DRG" value="J"/>
		            <SuperClass code="5-433"/>
		            <ModifiedBy code="ST5433"/>
		            <Rubric kind="preferred">
			            <Label xml:lang="de" xml:space="default">Destruktion, offen chirurgisch</Label>
		            </Rubric>
	            </Class>
             
                <Modifier code="ST5433">
                    <SubClass code="0"/>
                    <SubClass code="1"/>
                    <SubClass code="2"/>
                    <SubClass code="3"/>
                    <SubClass code="4"/>
                    <SubClass code="5"/>
                    <SubClass code="x"/>
                    <Rubric kind="note">
                        <Label xml:lang="de" xml:space="default">Die Art der Destruktion ist für die mit ** gekennzeichneten Kodes in der 6. Stelle nach folgender Liste zu kodieren:</Label>
                    </Rubric>
                </Modifier>
             
	            <ModifierClass code="0" modifier="ST5433">
		            <SuperClass code="ST5433"/>
		            <Rubric kind="preferred">
			            <Label xml:lang="de" xml:space="default">Elektrokoagulation</Label>
		            </Rubric>
	            </ModifierClass>             
             
            */
            foreach (XmlNode node in _document.SelectNodes("//Class[@kind='category' or @kind='chapter']"))
            {
                string opsCode = node.Attributes["code"].Value;
                string opsText = "";

                //
                // get the label 
                //
                foreach (XmlNode subnode in node.ChildNodes)
                {
                    if (subnode.Name == "Rubric" && (subnode.Attributes["kind"].Value == "preferred"))
                    {
                        foreach (XmlNode subsubnode in subnode.ChildNodes)
                        {
                            if (subsubnode.Name == "Label")
                            {
                                opsText = subsubnode.InnerText;
                                HandleXmlEntryMain(_document, opsCode, opsText);
                            }
                        }
                    }
                }

                //
                // check if there are modifiers
                //
                // 2013: 
                //    - nearly always all='true'
                //    - all='false' only 4 times
                //    - if no 'all', then with ValidModifierClass
                //
                // 2014
                //    - no all='true'
                //    - iff all='false' then ValidModifierClass
                //
                foreach (XmlNode subnode in node.ChildNodes)
                {
                    //
		            // <ModifiedBy all="true" code="ST5070"/>
                    //
                    if (subnode.Name == "ModifiedBy")
                    {
                        //
                        // Yes, there are modifiers
                        //
                        string modifier = subnode.Attributes["code"].Value;
                        bool hasAll = false;
                        string allValue = "";

                        if (subnode.Attributes["all"] != null)
                        {
                            //
                            // Save the value of the 'all' attribute: 'false' or 'true'
                            //
                            hasAll = true;
                            allValue = subnode.Attributes["all"].Value;
                        }

                        //
                        // Get the modifier and all its entries
                        //
                        Dictionary<string, string> dictModifier;
                        dictModifier = _modifiers[modifier];

                        //
                        //	<ModifiedBy code="ST5143">
			            //      <ValidModifierClass code="0"/>
                        //      ...
                        //
                        // If there are entries of type 'ValidModifierClass', use only those,
                        // If there are no entries of type 'ValidModifierClass' add all variants of the modifier, if 'all' was specified.
                        //
                        bool hasValidModifierClass = false;
                        foreach (XmlNode subsubnode in subnode.ChildNodes)
                        {
                            // if there are allowed modifiers, use only those
                            if (subsubnode.Name == "ValidModifierClass")
                            {
                                hasValidModifierClass = true;

                                string key = subsubnode.Attributes["code"].Value;
                                string value;
                                if (dictModifier.TryGetValue(key, out value))
                                {
                                    HandleXmlEntry(_document, opsCode + key, value);
                                }
                                else
                                {
                                    // <Class code="5-397" kind="category"> enthält <ValidModifierClass code=".90"/>, den gibt es aber nicht.
#if DEBUG
                                    HandleMissingModifier(opsCode, opsText, key);
#endif
                                }
                            }
                        }

                        //
                        // if there were no modifiers, check the "all" attribute
                        //
                        if (hasValidModifierClass)
                        {
                            if (hasAll && (allValue == "true"))
                            {
#if DEBUG
                                HandleAllWithValidModifierClass(opsCode, opsText);
#endif
                            }
                        }
                        else
                        {
                            if (hasAll)
                            {
                                if (allValue == "true")
                                {
                                    //
                                    // add all modifiers
                                    //
                                    if (_modifiers.TryGetValue(modifier, out dictModifier))
                                    {
                                        foreach (string key in dictModifier.Keys)
                                        {
                                            string value = dictModifier[key];
                                            HandleXmlEntry(_document, opsCode + key, value);
                                        }
                                    }
                                }
                                else
                                {
                                    HandleMissingValidModifierClass(opsCode, opsText);
                                }
                            }
                            else
                            {
                                //
                                // no ValidModifierClass entries and no 'all' attribute means all='true'
                                // add all modifiers
                                //
                                if (_modifiers.TryGetValue(modifier, out dictModifier))
                                {
                                    foreach (string key in dictModifier.Keys)
                                    {
                                        string value = dictModifier[key];
                                        HandleXmlEntry(_document, opsCode + key, value);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public bool ReadFile(Dictionary<string, string> import)
        {
            string line;
            bool success = true;
            long count;

            _import = import;

            //
            // Zeilen zählen, damit der Progress gut angezeigt wird.
            // Das geht schnell!
            //
            if (_format.Equals(ImportOPSWizardPage.Format2009))
            {
                count = Tools.CountLines(Encoding.GetEncoding(1252), _fileName);
            }
            else if (_format.Equals(ImportOPSWizardPage.Format2010))
            {
                count = Tools.CountLines(Encoding.UTF8, _fileName);
            }
            else
            {
                //
                // XML
                //
                count = CountLinesXml(_fileName);
            }

            count += _businessLayer.DatabaseLayer.ExecuteScalar("select count(ID_Operationen) from Operationen");

            TheProgressBar.Maximum = (Int32)count;
            TheProgressBar.Value = 0;

            try
            {
#if DEBUG
                _writer = new StreamWriter(_fileName + "-list.txt", false, Encoding.Unicode);
                _logWriter = new StreamWriter(_fileName + "-list.log.txt", false, Encoding.Unicode);
#endif

                _lastCode = "";
                _lastText = "";

                // Der OPS-Katalog hat nun mal dieses Encoding
                if (_format.Equals(ImportOPSWizardPage.Format2009))
                {
                    _reader = new StreamReader(_fileName, Encoding.GetEncoding(1252));
                }
                else if (_format.Equals(ImportOPSWizardPage.Format2010))
                {
                    _reader = new StreamReader(_fileName, Encoding.UTF8);
                }

                if (_format.Equals(ImportOPSWizardPage.Format2009) || _format.Equals(ImportOPSWizardPage.Format2010))
                {
                    while (true)
                    {
                        Progress();

                        line = _reader.ReadLine();

                        if (line == null)
                        {
                            break;
                        }
                        if (_format.Equals(ImportOPSWizardPage.Format2009))
                        {
                            ProcessLine(line);
                        }
                        else
                        {
                            ProcessLine2010(line);
                        }
                    }

                    if (_lastCode.Length > 0)
                    {
                        HandleEntry();
                    }
                }
                else
                {
                    ParseXml();
                }
            }
            catch
            {
                _businessLayer.MessageBox(string.Format(GetText(FormName, "error1"), _fileName));
                success = false;
                goto _exit;
            }
            finally
            {
                if (_reader != null)
                {
                    _reader.Close();
                }
#if DEBUG
                if (_writer != null)
                {
                    _writer.WriteLine("Max line:" + _lenMaxText.ToString() + ":" + _maxCode + "|" + _maxText);
                    _writer.Close();
                }
                if (_logWriter != null)
                {
                    _logWriter.WriteLine("Max line:" + _lenMaxText.ToString() + ":" + _maxCode + "|" + _maxText);
                    _logWriter.Close();
                }
#endif
                TheProgressBar.Value = TheProgressBar.Maximum;
            }

            _exit:

            _import = null;

            return success;
        }

        private void HandleEntry()
        {
            if (_lastCode.EndsWith("#"))
            {
                _lastCode = _lastCode.Replace("#", "");
            }
            if (_lastCode.EndsWith("**"))
            {
                _lastCode = _lastCode.Replace("**", "");
            }

#if DEBUG
            {
                if (_lastText.Length > _maxText.Length)
                {
                    _maxCode = _lastCode;
                    _maxText = _lastText;
                    _lenMaxText = _maxText.Length;
                }
                _writer.WriteLine(_lastCode + "|" + _lastText);
            }
#endif

            _lastCode = _lastCode.Trim();
            _lastText = _lastText.Trim();

            if (_lastText[_lastText.Length - 1] == ']')
            {
                //
                // Wenn die Zeile mit etwas wie [6. Stelle: 2,g,x] 
                // endet, wird das entfernt
                //
                int indexLeftBracket = _lastText.LastIndexOf("[");
                if (indexLeftBracket != -1)
                {
                    string bracketText = _lastText.Substring(indexLeftBracket);
                    int index = bracketText.IndexOf('.');
                    if (index != -1 && (index + 1) < bracketText.Length)
                    {
                        index = bracketText.IndexOf("Stelle:", index + 1);
                        if (index != -1)
                        {
                            int length = _lastText.Length - bracketText.Length;
                            if (length < _lastText.Length)
                            {
                                _lastText = _lastText.Substring(0, length);
                                _lastText = _lastText.Trim();
                            }
                        }
                    }
                }
            }

            if (_lastText.Length > 255)
            {
                // Datenbankfeld ist nur 255 groß
                _lastText = _lastText.Substring(0, 255);
                _lastText = _lastText.Trim();
            }

            if (!_import.ContainsKey(_lastCode))
            {
                _import.Add(_lastCode, _lastText);
            }

            _lastCode = "";
            _lastText = "";
        }

#if DEBUG
        private void HandleMissingModifier(string opsCode, string opsText, string key)
        {
            {
                _logWriter.WriteLine(string.Format("OpsCode={0}, opsText={1}: contains modifier {2} which is missing", opsCode, opsText, key));
            }
        }
#endif

        private void HandleMissingValidModifierClass(string opsCode, string opsText)
        {
#if DEBUG
            {
                _logWriter.WriteLine(string.Format("OpsCode={0}, opsText={1}: has 'ModifiedBy', contains attribute 'all' with value 'false' but no entries of type 'ValidModifierClass'", opsCode, opsText));
            }
#endif
        }

#if DEBUG
        private void HandleAllWithValidModifierClass(string opsCode, string opsText)
        {
            {
                _logWriter.WriteLine(string.Format("OpsCode={0}, opsText={1}: has 'ModifiedBy' and contains attribute 'all' with value 'true' but also entries of type 'ValidModifierClass'", opsCode, opsText));
            }
        }
#endif

        private void HandleXmlEntryMain(XmlDocument document, string opsCode, string opsText)
        {
            Progress();

            HandleXmlEntry(document, opsCode, opsText);
        }

        //
        // Ab 2013: Die Texte sind viel länger. Nicht hinten löschen, sondern vorne: vorne ist wahrscheinlich nur der Text der übergeprdneten Sache
        //
        private void HandleXmlEntry(XmlDocument document, string opsCode, string opsText)
        {
            if (opsCode.IndexOf('.') != -1)
            {
                string superClassOpsText = GetSuperOpsText(opsCode);
                opsText = superClassOpsText + opsText;
            }

#if DEBUG
            {
                if (opsText.Length > _lenMaxText)
                {
                    _maxCode = opsCode;
                    _maxText = opsText;
                    _lenMaxText = _maxText.Length;
                }
                _writer.WriteLine(opsCode + "|" + opsText);
            }
#endif

            _lastCode = opsCode.Trim();
            _lastText = opsText.Trim();

            if (_lastText.Length > 255)
            {
                // Datenbankfeld ist nur 255 groß

                //
                // Wenn ein : vorkommt, dann den Text dahinter nehmen
                //
//#if false
                int index = _lastText.LastIndexOf(':');
                if ((index != -1) && (index + 1 < _lastText.Length))
                {
                    _lastText = _lastText.Substring(index + 1);
                    if (_lastText.Length > 255)
                    {
                        // die letzten 255 Zeichen
                        _lastText = _lastText.Substring(_lastText.Length - 255, 255);
                    }
                }
                else
                {
                    // die letzten 255 Zeichen
                    _lastText = _lastText.Substring(_lastText.Length - 255, 255);
                }
//#endif

               // _lastText = _lastText.Substring(_lastText.Length - 255, 255);
                _lastText = _lastText.Trim();
            }

            if (!_import.ContainsKey(_lastCode))
            {
                _import.Add(_lastCode, _lastText);
            }

            _lastCode = "";
            _lastText = "";
        }

        private void ProcessLine(string line)
        {
            if (line != null & line.Length > 1 && line[1] != '1')
            {
                switch (line[2])
                {
                    case 'B':
                        // Basiseintrag
                        if (_lastCode.Length > 0)
                        {
                            HandleEntry();
                        }
                        if (line[3] != ' ')
                        {
                            _lastCode = line.Substring(3, 11).Trim();
                            _lastText = line.Substring(15).Trim();
                        }
                        break;

                    case 'F':
                        // Fortsetzung
                        if (_lastText.Length > 0)
                        {
                            if (line[15] != ' ')
                            {
                                _lastText = _lastText + " " + line.Substring(15);
                            }
                        }
                        break;
                }
            }
        }
        private void ProcessLine2010(string line)
        {
            if (line != null & line.Length > 1 && line[0] != '1')
            {
                switch (line[1])
                {
                    case 'B':
                        // Basiseintrag
                        if (_lastCode.Length > 0)
                        {
                            HandleEntry();
                        }
                        if (line[2] != ' ')
                        {
                            _lastCode = line.Substring(2, 11).Trim();
                            _lastText = line.Substring(14).Trim();
                        }
                        break;

                    case 'F':
                        // Fortsetzung
                        if (_lastText.Length > 0)
                        {
                            if (line[14] != ' ')
                            {
                                _lastText = _lastText + " " + line.Substring(14);
                            }
                        }
                        break;
                }
            }
        }
    }
}
