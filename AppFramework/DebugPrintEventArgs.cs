using System;

namespace AppFramework.Debugging
{
    /// <summary>
    /// Von EventArgs abgeleitete Klasse, die die Daten und Einstellungen einer Meldung enthält
    /// </summary>
    public class DebugPrintEventArgs : EventArgs
    {
        private string _text;

        /// <summary>
        /// Konstruktor. Macht nichts.
        /// </summary>
        public DebugPrintEventArgs()
        {
        }

        /// <summary>
        /// Konstruktor mit Text
        /// </summary>
        /// <param name="text">Der Text, der ausgegeben werden soll</param>
        public DebugPrintEventArgs(string text) 
        {
            _text = text;
        }

        /// <summary>
        /// Get or set the Text
        /// </summary>
        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }
    }
}
