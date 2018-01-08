using System;
using System.Collections.Generic;
using System.Text;

namespace CMaurer.Operationen.AppFramework
{
    /// <summary>
    /// Arguments for progress
    /// </summary>
    public sealed class ProgressEventArgs : EventArgs
    {
        private bool _cancel = false;
        private string _data;

        /// <summary>
        /// Cancel the progress
        /// </summary>
        public bool Cancel
        {
            get { return _cancel; }
            set { _cancel = value; }
        }

        /// <summary>
        /// Set or get string data
        /// </summary>
        public string Data
        {
            get { return _data; }
            set { _data = value; }
        }
    }

    /// <summary>
    /// Callback functino for progress
    /// </summary>
    /// <param name="e">the arguments</param>
    public delegate void ProgressCallback(ProgressEventArgs e);
}
