using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.Runtime;

namespace Operationen
{

    public partial class DownloadFileView : OperationenForm
    {
        private int _fileSizeBytes;
        private string _url;
        private string _localFileName;

        public DownloadFileView(BusinessLayer businessLayer, string windowTitle, int fileSizeKB, string url, string localFileName)
            : base(businessLayer)
        {
            _fileSizeBytes = fileSizeKB << 10;
            _url = url;
            _localFileName = localFileName;

            InitializeComponent();

            // must be visible for design mode or else we see nothing.
            lblStatus.BorderStyle = BorderStyle.None;

            Text = AppTitle(windowTitle);
        }

        private void Progress(int percent, string text)
        {
            if (this.progressBar.InvokeRequired)
            {
                ProgressCallbackPercentTextDelegate d = new ProgressCallbackPercentTextDelegate(Progress);
                this.Invoke(d, percent, text);
            }
            else
            {
                if (percent > 100)
                {
                    percent = 100;
                }
                if (percent < 0)
                {
                    percent = 0;
                }
                progressBar.Value = percent;
            }
            if (text.Length > 0)
            {
                lblStatus.Text = text;
            }
            Application.DoEvents();
        }

        private bool DownloadFile()
        {
            bool success = false;
            string tempFilename = null;
            System.IO.FileStream stream = null;

            try
            {
                tempFilename = System.IO.Path.GetTempFileName();

                System.Net.HttpWebRequest webRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(_url);

                BusinessLayer.SetWebProxy(webRequest);

                System.Net.HttpWebResponse webResponse = (System.Net.HttpWebResponse)webRequest.GetResponse();

                System.IO.BinaryReader streamReader = new System.IO.BinaryReader(webResponse.GetResponseStream());
                const int BUFFER_SIZE = 4096;

                byte[] buf = new byte[BUFFER_SIZE];
                stream = new System.IO.FileStream(tempFilename, System.IO.FileMode.Create);
                int bytesRead = 0;
                int n = streamReader.Read(buf, 0, BUFFER_SIZE);
                while (n > 0)
                {
                    bytesRead += n;

                    string message = string.Format(GetText("downloadStatus"), 
                        bytesRead >> 10, 
                        _fileSizeBytes >> 10);
                    Progress((int)((double)bytesRead / (double)_fileSizeBytes * 100.0), message);

                    stream.Write(buf, 0, n);
                    n = streamReader.Read(buf, 0, BUFFER_SIZE);
                    Application.DoEvents();

                    if (Abort)
                    {
                        throw new Exception(GetText("abortedByUser"));
                    }
                }

                if (stream != null)
                {
                    stream.Close();
                    stream.Dispose();
                    stream = null;
                }

                BusinessLayer.CopyFile(tempFilename, _localFileName, AppTitle());
                Utility.Tools.DeleteFile(tempFilename);

                success = true;

            }
            catch (Exception e)
            {
                if (stream != null)
                {
                    stream.Close();
                    stream.Dispose();
                    stream = null;
                }
                if (!string.IsNullOrEmpty(tempFilename))
                {
                    Utility.Tools.DeleteFile(tempFilename);
                }
                if (!string.IsNullOrEmpty(_localFileName))
                {
                    Utility.Tools.DeleteFile(_localFileName);
                }

                MessageBox(string.Format(GetText("errorDownloading"), 
                    _url, e.Message));
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                    stream.Dispose();
                    stream = null;
                }
            }

            return success;
        }

        private void DownloadFileView_Shown(object sender, EventArgs e)
        {
            if (DownloadFile())
            {
                Application.DoEvents();

                DialogResult = DialogResult.OK;
            }
            else
            {
                DialogResult = DialogResult.Cancel;
            }
            Close();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            Abort = true;
        }

        private void DownloadFileView_Load(object sender, EventArgs e)
        {
        }
    }
}

