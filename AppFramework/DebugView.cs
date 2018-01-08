using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AppFramework.Debugging
{
    public delegate void DebugPrintCallbackDelegate(string text);

    public partial class DebugView : Form
    {
        private string _applicationName;
        private List<DebugFlagEntry> _userDebugFlags;
        private bool _displayDebugMaskView;
        private long _debugFlagsSaved;

        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="applicationName">Short user readable app name</param>
        /// <param name="userDebugFlags">User debug flags</param>
        /// <param name="displayDebugMaskView">true if the debug mask window should open immediately</param>
        public DebugView(string applicationName, List<DebugFlagEntry> userDebugFlags, bool displayDebugMaskView)
        {
            this._applicationName = applicationName;
            this._userDebugFlags = userDebugFlags;
            this._displayDebugMaskView = displayDebugMaskView;

            InitializeComponent();

            string text = "Debug Window";
            if (!string.IsNullOrEmpty(applicationName))
            {
                text = text + " - " + applicationName;
            }
            Text = text;

            DebugLogging.DebugPrint += new EventHandler<DebugPrintEventArgs>(Logging_DebugPrint);

            lvData.View = View.Details;
            lvData.FullRowSelect = true;
            lvData.Columns.Add("", -2);
        }

        private void PrintText(string text)
        {
            if (this.lvData.InvokeRequired)
            {
                DebugPrintCallbackDelegate d = new DebugPrintCallbackDelegate(PrintText);
                this.Invoke(d, text);
            }
            else
            {
                ListViewItem lvi = new ListViewItem(text);

                while (lvData.Items.Count > 1000)
                {
                    lvData.Items.RemoveAt(lvData.Items.Count - 1);
                }

                lvData.Items.Insert(0, lvi);
                lvData.EnsureVisible(0);
            }
        }

        private void Logging_DebugPrint(object sender, DebugPrintEventArgs e)
        {
            PrintText(e.Text);
        }

        private void mnClear_Click(object sender, EventArgs e)
        {
            lvData.Items.Clear();
        }

        private void DebugView_Load(object sender, EventArgs e)
        {
        }

        private void ShowDebugMaskView()
        {
            DebugMaskView dlg = new DebugMaskView(_applicationName, _userDebugFlags);
            dlg.ShowDialog();
        }

        private void mnOptions_Click(object sender, EventArgs e)
        {
            ShowDebugMaskView();
        }

        private void DebugView_FormClosing(object sender, FormClosingEventArgs e)
        {
            DebugLogging.DebugPrint -= Logging_DebugPrint;
        }

        private void DebugView_Shown(object sender, EventArgs e)
        {
            if (_displayDebugMaskView)
            {
                ShowDebugMaskView();
            }
        }

        private void mnStop_Click(object sender, EventArgs e)
        {
            _debugFlagsSaved = DebugLogging.DebugMask;
            DebugLogging.DebugMask = 0;
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DebugLogging.DebugMask = _debugFlagsSaved;
        }
    }
}
