using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Globalization;

using Utility;
using AppFramework.Debugging;

namespace Operationen
{
    /// <summary>
    /// Do NOT use Application.DoEvents(), or you will get this error:
    /// 
    /// {"Beim Vorgang zum Rückgängigmachen wurde ein Kontext gefunden, 
    /// der sich vom Kontext des entsprechenden Set-Vorgangs unterschied. 
    /// Möglicherweise war der Kontext für den Thread \"Set\" und wurde nicht 
    /// zurückgesetzt (rückgängig gemacht)."}
    /// 
    /// Any file sent or received must have this header:
    /// 
    /// header ::= magic length data
    /// 
    /// magic   ::= 0x14 0x05 0x19 0x65
    /// length  ::= 4 bytes in big endian specifying the length of the entire packet including the header
    /// data    ::= (length - 8) data bytes
    /// 
    /// </summary>
    public partial class ClientServerView : OperationenForm
    {
        private const int MagicNumber = 0x14051965;

        //
        // Minimum is 8 header bytes + some data
        //
        private const int MinDataLength = 9;
        private const int MaxDataLength = 2000000000;
        private const int ProgressSkipPercent = -1;
        private const int ReadBlockSizeInBytes = 32000;
        private const int WriteBlockSizeInBytes = 32000;

        private class SocketPacket
        {
            public System.Net.Sockets.Socket m_currentSocket;
            public byte[] dataBuffer = new byte[WriteBlockSizeInBytes];
        }

        private AsyncCallback pfnWorkerCallBack;
        private Socket _mainSocket;
        private IPEndPoint _remoteHost;

        private Socket _workerSocket;
        private BinaryWriter _writer;
        private BinaryReader _reader;
        private int _count;
        private int _length;
        private byte[] _arHeader = new byte[8];

        ///
        /// Because of the callbacks running on all kinds of threads but not the one we need,
        /// we must copy all GUI data because we cannot access text boxes etc.
        /// 
        private string _dataFileName;
        private string _dataHost;
        private string _dataPort;

        public ClientServerView(BusinessLayer businessLayer)
            : base(businessLayer)
		{
            InitializeComponent();

            cmdSend.SetSecurity(BusinessLayer, "ClientServerView.cmdSend");
            cmdReceive.SetSecurity(BusinessLayer, "ClientServerView.cmdReceive");

            this.Text = AppTitle(GetText("title"));
            SetInfoText(lblInfo, string.Format(CultureInfo.InvariantCulture, GetText("info"), cmdReceive.Text, cmdSend.Text));
        }
        
        private void ClientServerView_Load(object sender, EventArgs e)
        {
            txtPort.Text = "8000";
            lblStatus.BorderStyle = BorderStyle.None;
            lblStatus.ForeColor = BusinessLayer.InfoColor;
            cmdAbort.Enabled = false;

            List<string> ipAddresses = GetLocalIpAddresses();

            lbIpAddresses.Items.Clear();

            foreach (string ipAddress in ipAddresses)
            {
                lbIpAddresses.Items.Add(ipAddress);
            }

            lbIpAddresses.SelectedIndex = 0;
        }

        public static List<string> GetLocalIpAddresses()
        {
            List<string> ipAddresses = new List<string>();

            String strHostName = Dns.GetHostName();

            IPHostEntry iphostentry = Dns.GetHostEntry(strHostName);

            foreach (IPAddress ipaddress in iphostentry.AddressList)
            {
                string ip = ipaddress.ToString();

                if (ip.Contains("."))
                {
                    ipAddresses.Add(ip);
                }
            }

            return ipAddresses;
        }

        protected override bool ValidateInput()
        {
            bool success = true;
            StringBuilder message = new StringBuilder(EINGABEFEHLER);

            _dataFileName = String.Empty;
            _dataHost = String.Empty;
            _dataPort = String.Empty;

            success = success && ValidateIntRange(lblPort, txtPort, 1000, 99000, message);

            if (!success)
            {
                MessageBox(message.ToString());
            }
            else
            {
                _dataHost = lbIpAddresses.Text;
                _dataPort = txtPort.Text;
            }

            return success;
        }

        private void CreateWriteStream()
        {
            CloseReadStream();
            CloseWriteStream();

            Tools.DeleteFile(_dataFileName);
            _writer = new BinaryWriter(File.Open(_dataFileName, FileMode.Create));
        }

        private void CreateReadStream()
        {
            CloseReadStream();
            CloseWriteStream();

            _reader = new BinaryReader(File.Open(_dataFileName, FileMode.Open));
        }

        private void CreateMainSocket()
        {
            try
            {
                IPAddress host = null;

                int port = System.Convert.ToInt32(_dataPort);

                if (!IPAddress.TryParse(_dataHost, out host))
                {
                    IPAddress[] addresslist = Dns.GetHostAddresses(_dataHost);
                    foreach (IPAddress ip in addresslist)
                    {
                        if (ip.ToString().Contains("."))
                        {
                            host = ip;
                            break;
                        }
                    }
                }

                if (host == null)
                {
                    throw new Exception(GetText("err_ip"));
                }

                _mainSocket = new Socket(AddressFamily.InterNetwork,
                                          SocketType.Stream,
                                          ProtocolType.Tcp);

                _remoteHost = new IPEndPoint(host, port);
            }
            catch (Exception ex)
            {
                Progress(ProgressSkipPercent, ex.Message);
            }
        }

        void StartListening()
        {
            try
            {
                XableAllButtonsForLongOperation(cmdAbort, false);

                ResetCount();
                CloseSockets();

                CreateMainSocket();

                _mainSocket.Bind(_remoteHost);

                _mainSocket.Listen(4);

                Progress(ProgressSkipPercent, GetText("listening"));

                _mainSocket.BeginAccept(new AsyncCallback(AcceptCallback), null);
            }
            catch (Exception se)
            {
                Progress(ProgressSkipPercent, se.Message);
                DebugPrint(DebugLogging.DebugFlagTcpIp, se.Message);
            }
        }

        private void CloseWriteStream()
        {
            if (_writer != null)
            {
                _writer.Close();
                _writer = null;
            }
        }

        private void CloseReadStream()
        {
            if (_reader != null)
            {
                _reader.Close();
                _reader = null;
            }
        }

        private void ResetCount()
        {
            _count = 0;
            _length = 0;
            for (int i = 0; i < _arHeader.Length; i++)
            {
                _arHeader[i] = 0;
            }
        }

        // This is the call back function, which will be invoked when a client is connected
        public void AcceptCallback(IAsyncResult asyn)
        {
            try
            {
                CreateWriteStream();

                // Here we complete/end the BeginAccept() asynchronous call
                // by calling EndAccept() - which returns the reference to
                // a new Socket object
                _workerSocket = _mainSocket.EndAccept(asyn);

                Progress(ProgressSkipPercent, GetText("connected"));
                ResetCount();

                // Let the worker Socket do the further processing for the 
                // just connected client
                WaitForData(_workerSocket);
            }
            catch (Exception se)
            {
                if (!Abort)
                {
                    Progress(ProgressSkipPercent, se.Message);
                    DebugPrint(DebugLogging.DebugFlagTcpIp, se.Message);
                }
            }
        }

        // Start waiting for data from the client
        public void WaitForData(System.Net.Sockets.Socket soc)
        {
            try
            {
                if (pfnWorkerCallBack == null)
                {
                    // Specify the call back function which is to be 
                    // invoked when there is any write activity by the 
                    // connected client
                    pfnWorkerCallBack = new AsyncCallback(ReadCallback);
                }
                SocketPacket theSocPkt = new SocketPacket();
                theSocPkt.m_currentSocket = soc;

                // Start receiving any data written by the connected client
                // asynchronously
                soc.BeginReceive(theSocPkt.dataBuffer, 0,
                                   theSocPkt.dataBuffer.Length,
                                   SocketFlags.None,
                                   pfnWorkerCallBack,
                                   theSocPkt);
            }
            catch (Exception se)
            {
                Progress(ProgressSkipPercent, se.Message);
                DebugPrint(DebugLogging.DebugFlagTcpIp, se.Message);
            }

        }

        /// <summary>
        /// This the call back function which will be invoked when the socket 
        /// detects any client writing of data on the stream.
        /// </summary>
        /// <param name="asyn"></param>
        public void ReadCallback(IAsyncResult asyn)
        {
            try
            {
                SocketPacket socketData = (SocketPacket)asyn.AsyncState;

                int numBytes = 0;
                int index = 0;
                int magic = 0;

                numBytes = socketData.m_currentSocket.EndReceive(asyn);

                DebugPrint(DebugLogging.DebugFlagTcpIp, "Received " + numBytes + " bytes");

                //
                // Set the text only once
                //
                if (_count == 0)
                {
                    Progress(ProgressSkipPercent, GetText("receiving"));
                }

                //
                // Consume and save the first 8 bytes into a buffer
                //
                while ((_count < 8) && (numBytes > 0))
                {
                    _arHeader[_count++] = socketData.dataBuffer[index++];
                    numBytes--;
                }

                if ((_length == 0) && (_count == 8))
                {
                    //
                    // The 8 byte header has been received, calculate the magic number and remaining number of bytes
                    //

                    // Receive header, high bytes first
                    /*
                        Client sends:
                        byLength[0] = 0
                        byLength[1] = 0
                        byLength[2] = 0
                        byLength[3] = 5
                     
                        Server receives:
                        byLength[0] = 0
                        byLength[1] = 0
                        byLength[2] = 0
                        byLength[3] = 5
                     
                     */

                    for (int i = 0; i < 4; i++)
                    {
                        magic <<= 8;
                        magic += _arHeader[i];
                    }

                    DebugPrint(DebugLogging.DebugFlagTcpIp, string.Format(CultureInfo.InvariantCulture, "Read magic number: 0x{0:X}", magic));

                    if (magic != MagicNumber)
                    {
                        throw new Exception(String.Format(CultureInfo.InvariantCulture, GetText("err_magicnumber"), magic, MagicNumber));
                    }

                    //
                    // length is length of total packet including the header
                    //
                    for (int i = 4; i < 8; i++)
                    {
                        _length <<= 8;
                        _length += _arHeader[i];
                    }

                    DebugPrint(DebugLogging.DebugFlagTcpIp, "Read length: '" + _length + "'");

                    if (_length < MinDataLength || _length > MaxDataLength)
                    {
                        throw new Exception(String.Format(CultureInfo.InvariantCulture, GetText("err_length"), _length, MinDataLength, MaxDataLength));
                    }
                }

                //
                // Any bytes following the header are not read into a buffer but passed on and written to a file as binary.
                //
                if (numBytes > 0)
                {
                    _count += numBytes;
                    if (_writer != null)
                    {
                        _writer.Write(socketData.dataBuffer, index, numBytes);
                        _writer.Flush();
                    }
                }

                //
                // Display percentage
                //
                DisplayDataPercentage("readBytes", _count, _length);

                //
                // If the number of byts as specified in the header have been received, we terminate the entire connection.
                // Must reopen sopcket for next transmission
                //
                if ((_length > 0) && (_count == _length))
                {
                    //
                    // All data received
                    //
                    CloseWriteStream();
                    CloseSockets();
                    DebugPrint(DebugLogging.DebugFlagTcpIp, "Read a total of " + _count + " bytes");
                    ResetCount();
                    Progress(ProgressSkipPercent, GetText("done"));
                    XableAllButtonsForLongOperation(cmdAbort, true);
                }
                else
                {
                    //
                    // More bytes to come
                    //
                    WaitForData(socketData.m_currentSocket);
                }
            }
            catch (Exception se)
            {
                Progress(ProgressSkipPercent, se.Message);
                CloseWriteStream();
                CloseSockets();
                ResetCount();
                DebugPrint(DebugLogging.DebugFlagTcpIp, se.Message);
                XableAllButtonsForLongOperation(cmdAbort, true);
            }
        }

        void cmdSend_Click(object sender, System.EventArgs e)
        {
            if (UserHasRight("ClientServerView.cmdSend"))
            {
                Abort = false;
                if (ValidateInput())
                {
                    OpenFileDialog dlg = new OpenFileDialog();
                    dlg.CheckFileExists = true;
                    if (DialogResult.OK == dlg.ShowDialog())
                    {
                        txtFileName.Text = _dataFileName = dlg.FileName;
                        StartSending();
                    }
                }
            }
        }

        private void StartSending()
        {
            try
            {
                XableAllButtonsForLongOperation(cmdAbort, false);

                if (!File.Exists(_dataFileName))
                {
                    string msg = GetTextControlMissingFile(_dataFileName);
                    MessageBox(msg);
                    throw new Exception(msg);
                }

                CreateMainSocket();

                _mainSocket.BeginConnect(_remoteHost, new AsyncCallback(ConnectCallback), null);
            }
            catch (Exception ex)
            {
                Reset();
                Progress(ProgressSkipPercent, ex.Message);
                XableAllButtonsForLongOperation(cmdAbort, true);
            }
        }

        private void DisplayDataPercentage(string key, int count, int total)
        {
            int percent = (int)((double)count / (double)total * 100.0);
            string msg = string.Format(CultureInfo.InvariantCulture, GetText(key), count, total);

            Progress(percent, msg);
        }

        private void ConnectCallback(IAsyncResult ar) 
        {
            try 
            {
                Progress(ProgressSkipPercent, GetText("sending"));

                _mainSocket.EndConnect(ar);

                CreateReadStream();

                FileInfo fileInfo = new FileInfo(_dataFileName);
                fileInfo.Refresh();
                long fileSizeBytes = fileInfo.Length + 8;
                if (fileSizeBytes < MinDataLength || fileSizeBytes > MaxDataLength)
                {
                    throw new Exception(String.Format(CultureInfo.InvariantCulture, GetText("err_fileSize"), _length, MinDataLength, MaxDataLength));
                }

                byte[] arHeader = new byte[8];
                arHeader[0] = 0x14;
                arHeader[1] = 0x05;
                arHeader[2] = 0x19;
                arHeader[3] = 0x65;

                long temp = fileSizeBytes;
                for (int i = 4; i < 8; i++)
                {
                    //
                    // 4 -> 7
                    // 5 -> 6
                    // 6 -> 5
                    // 7 -> 4
                    //
                    arHeader[11 - i] = (byte)(temp & 0xFF);
                    temp >>= 8;
                }

                if (_mainSocket != null)
                {
                    if (_mainSocket.Connected)
                    {
                        _mainSocket.Send(arHeader);
                        _count = arHeader.Length;

                        DebugPrint(DebugLogging.DebugFlagTcpIp, "Sent " + arHeader.Length + " header bytes");

                        byte[] arData;
            
                        arData = _reader.ReadBytes(ReadBlockSizeInBytes);
                        while (arData.Length > 0)
                        {
                            _mainSocket.Send(arData);
                            _count += arData.Length;
                            DisplayDataPercentage("sentBytes", _count, (int)fileSizeBytes);

                            arData = _reader.ReadBytes(ReadBlockSizeInBytes);
                        }

                        DebugPrint(DebugLogging.DebugFlagTcpIp, "Sent a total of " + _count + " bytes");

                        Reset();
                        Progress(ProgressSkipPercent, GetText("done"));
                    }
                }
            }
            catch (Exception se)
            {
                Reset();
                Progress(ProgressSkipPercent, se.Message);
                DebugPrint(DebugLogging.DebugFlagTcpIp, se.Message);
                XableAllButtonsForLongOperation(cmdAbort, true);
            }
        }

        void ButtonCloseClick(object sender, System.EventArgs e)
        {
            CloseSockets();
            Close();
        }

        void CloseSockets()
        {
            if (_mainSocket != null)
            {
                _mainSocket.Close();
            }
            if (_workerSocket != null)
            {
                _workerSocket.Close();
                _workerSocket = null;
            }
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
                else if (percent < -1)
                {
                    percent = -1;
                }

                if (percent > -1)
                {
                    progressBar.Value = percent;
                }
                if (text != null)
                {
                    lblStatus.Text = text;
                }
            }
        }

        private void cmdReceive_Click(object sender, EventArgs e)
        {

            if (UserHasRight("ClientServerView.cmdReceive"))
            {
                Abort = false;

                if (ValidateInput())
                {
                    SaveFileDialog dlg = new SaveFileDialog();
                    dlg.OverwritePrompt = true;
                    if (DialogResult.OK == dlg.ShowDialog())
                    {
                        bool receive = true;
                        txtFileName.Text = _dataFileName = dlg.FileName;
/*
                        if (File.Exists(_dataFileName))
                        {
                            if (!Confirm("confirmFileOverwrite"))
                            {
                                receive = false;
                            }
                        }
  */
                        if (receive)
                        {
                            StartListening();
                        }
                    }
                }

            }
        }

        private void Reset()
        {
            CloseSockets();
            CloseWriteStream();
            CloseReadStream();
            ResetCount();
            XableAllButtonsForLongOperation(cmdAbort, true);
        }

        private void cmdAbort_Click(object sender, EventArgs e)
        {
            Abort = true;
            Reset();
            Progress(0, GetText("abort"));
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
