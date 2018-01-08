using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

using AppFramework.Debugging;

namespace AppFramework.Debugging
{
    internal partial class DebugMaskView : Form
    {
        internal class FrameworkDebugFlagEntry : DebugFlagEntry
        {
            internal CheckBox checkBox;

            internal FrameworkDebugFlagEntry(long flag, string text, CheckBox checkBox) :
                base(flag, text)
            {
                this.checkBox = checkBox;
            }
        }

        private string applicationName;
        private List<DebugFlagEntry> userDebugFlags = new List<DebugFlagEntry>();
        private List<FrameworkDebugFlagEntry> frameworkDebugFlags = new List<FrameworkDebugFlagEntry>();

        internal DebugMaskView(string applicationName, List<DebugFlagEntry> userDebugFlags)
        {
            this.applicationName = applicationName;
            this.userDebugFlags = userDebugFlags;

            InitializeComponent();

            frameworkDebugFlags.Add(new FrameworkDebugFlagEntry(DebugLogging.DebugFlagError, "DebugFlagError", chkFlag1));
            frameworkDebugFlags.Add(new FrameworkDebugFlagEntry(DebugLogging.DebugFlagWarning, "DebugFlagWarning", chkFlag2));
            frameworkDebugFlags.Add(new FrameworkDebugFlagEntry(DebugLogging.DebugFlagInfo, "DebugFlagInfo", chkFlag3));
            frameworkDebugFlags.Add(new FrameworkDebugFlagEntry(DebugLogging.DebugFlagVerbose, "DebugFlagVerbose", chkFlag4));
            frameworkDebugFlags.Add(new FrameworkDebugFlagEntry(DebugLogging.DebugFlagTraceCtorDtor, "DebugFlagTraceCtorDtor", chkFlag5));
            frameworkDebugFlags.Add(new FrameworkDebugFlagEntry(DebugLogging.DebugFlagTraceFunction, "DebugFlagTraceFunction", chkFlag6));
            frameworkDebugFlags.Add(new FrameworkDebugFlagEntry(DebugLogging.DebugFlagSql, "DebugFlagSql", chkFlag7));
            frameworkDebugFlags.Add(new FrameworkDebugFlagEntry(DebugLogging.DebugFlagSqlVerbose, "DebugFlagSqlVerbose", chkFlag8));

            frameworkDebugFlags.Add(new FrameworkDebugFlagEntry(DebugLogging.DebugFlagGuiCommand,   "DebugFlagGui", chkFlag9));
            frameworkDebugFlags.Add(new FrameworkDebugFlagEntry(DebugLogging.DebugFlagGuiContents,  "DebugFlagGuiContents", chkFlag10));
            frameworkDebugFlags.Add(new FrameworkDebugFlagEntry(DebugLogging.DebugFlagTcpIp,        "DebugFlagTcpIp", chkFlag11));
            frameworkDebugFlags.Add(new FrameworkDebugFlagEntry(DebugLogging.DebugFlagReserved1,    "DebugFlagReserved1", chkFlag12));
            frameworkDebugFlags.Add(new FrameworkDebugFlagEntry(DebugLogging.DebugFlagReserved2,    "DebugFlagReserved2", chkFlag13));
            frameworkDebugFlags.Add(new FrameworkDebugFlagEntry(DebugLogging.DebugFlagReserved3,    "DebugFlagReserved3", chkFlag14));
            frameworkDebugFlags.Add(new FrameworkDebugFlagEntry(DebugLogging.DebugFlagReserved4,    "DebugFlagReserved4", chkFlag15));
            frameworkDebugFlags.Add(new FrameworkDebugFlagEntry(DebugLogging.DebugFlagReserved5,    "DebugFlagReserved5", chkFlag16));

            frameworkDebugFlags.Add(new FrameworkDebugFlagEntry(0x00010000, "0x00010000", chkFlag17));
            frameworkDebugFlags.Add(new FrameworkDebugFlagEntry(0x00020000, "0x00020000", chkFlag18));
            frameworkDebugFlags.Add(new FrameworkDebugFlagEntry(0x00040000, "0x00040000", chkFlag19));
            frameworkDebugFlags.Add(new FrameworkDebugFlagEntry(0x00080000, "0x00080000", chkFlag20));
            frameworkDebugFlags.Add(new FrameworkDebugFlagEntry(0x00100000, "0x00100000", chkFlag21));
            frameworkDebugFlags.Add(new FrameworkDebugFlagEntry(0x00200000, "0x00200000", chkFlag22));
            frameworkDebugFlags.Add(new FrameworkDebugFlagEntry(0x00400000, "0x00400000", chkFlag23));
            frameworkDebugFlags.Add(new FrameworkDebugFlagEntry(0x00800000, "0x00800000", chkFlag24));
            frameworkDebugFlags.Add(new FrameworkDebugFlagEntry(0x00800000, "0x01000000", chkFlag25));
            frameworkDebugFlags.Add(new FrameworkDebugFlagEntry(0x00800000, "0x02000000", chkFlag26));
            frameworkDebugFlags.Add(new FrameworkDebugFlagEntry(0x00800000, "0x04000000", chkFlag27));
            frameworkDebugFlags.Add(new FrameworkDebugFlagEntry(0x00800000, "0x08000000", chkFlag28));
            frameworkDebugFlags.Add(new FrameworkDebugFlagEntry(0x00800000, "0x10000000", chkFlag29));
            frameworkDebugFlags.Add(new FrameworkDebugFlagEntry(0x00800000, "0x20000000", chkFlag30));
            frameworkDebugFlags.Add(new FrameworkDebugFlagEntry(0x00800000, "0x40000000", chkFlag31));
            frameworkDebugFlags.Add(new FrameworkDebugFlagEntry(0x00800000, "0x80000000", chkFlag32));

            SetFrameworkDescription();

            if (userDebugFlags != null)
            {
                SetUserDescription();
            }

            ObjectToControl();
        }


        private static bool SetCheckBoxText(CheckBox checkBox, DebugFlagEntry entry, long flag)
        {
            bool success = false;

            if (entry.flag == flag)
            {
                success = true;
            }

            return success;
        }

        private void SetFrameworkDescription()
        {
            foreach (FrameworkDebugFlagEntry entry in frameworkDebugFlags)
            {
                entry.checkBox.Text = entry.text;
            }
        }

        private void SetUserDescription()
        {
            foreach (DebugFlagEntry entry in userDebugFlags)
            {
                if (entry.flag <= 0xffff)
                {
                    throw new Exception("Illegal user flag " + entry.flag + ", must be > 0xFFFF");
                }

                foreach (FrameworkDebugFlagEntry frameworkEntry in frameworkDebugFlags)
                {
                    if (frameworkEntry.flag == entry.flag)
                    {
                        frameworkEntry.checkBox.Text = entry.text;
                        break;
                    }
                }
            }
        }

        private void ObjectToControl()
        {
            foreach (FrameworkDebugFlagEntry entry in frameworkDebugFlags)
            {
                entry.checkBox.Checked = ((DebugLogging.DebugMask & entry.flag) != 0);
            }

            chkIncludeTimestamp.Checked = DebugLogging.IncludeTimestamp;
            chkIncludeThreadId.Checked = DebugLogging.IncludeThreadId;
            chkIncludeDebugMask.Checked = DebugLogging.IncludeDebugMask;
            chkIncludeDebugFlags.Checked = DebugLogging.IncludeDebugFlags;
        }


        private void chkToggle1_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < 8; i++)
            {
                frameworkDebugFlags[i].checkBox.Checked = chkToggle1.Checked;
            }
        }

        private void chkToggle2_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 8; i < 16; i++)
            {
                frameworkDebugFlags[i].checkBox.Checked = chkToggle2.Checked;
            }
        }

        private void chkToggle3_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 16; i < 24; i++)
            {
                frameworkDebugFlags[i].checkBox.Checked = chkToggle3.Checked;
            }
        }

        private void chkToggle4_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 24; i < 32; i++)
            {
                frameworkDebugFlags[i].checkBox.Checked = chkToggle4.Checked;
            }
        }

        private void DisplayMask()
        {
            string mask = string.Format("{0:x}", DebugLogging.DebugMask);
            txtMask.Text = mask;
        }

        private void CheckBoxChanged(object sender, long flag)
        {
            CheckBox checkBox = (CheckBox)sender;
            if (checkBox.Checked)
            {
                DebugLogging.DebugMask |= flag;
            }
            else
            {
                DebugLogging.DebugMask &= ~flag;
            }

            DisplayMask();
        }

        private void chkFlag_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;

            foreach (FrameworkDebugFlagEntry entry in frameworkDebugFlags)
            {
                if (entry.checkBox == checkBox)
                {
                    CheckBoxChanged(checkBox, entry.flag);
                }
            }
        }

        private void DebugMaskView_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(applicationName))
            {
                Text = "Debug Flags - " + applicationName;
                grpFlags3.Text = "0x00XX 0000 - " + applicationName;
                grpFlags4.Text = "0xXX00 0000 - " + applicationName;
            }
        }

        private void cmdSetMask_Click(object sender, EventArgs e)
        {
            long mask = 0;

            if (Int64.TryParse(txtMask.Text, System.Globalization.NumberStyles.HexNumber, CultureInfo.InvariantCulture, out mask))
            {
                DebugLogging.DebugMask = mask;

                ObjectToControl();
            }
        }

        private void chkIncludeTimestamp_CheckedChanged(object sender, EventArgs e)
        {
            DebugLogging.IncludeTimestamp = chkIncludeTimestamp.Checked;
        }

        private void chkIncludeThreadId_CheckedChanged(object sender, EventArgs e)
        {
            DebugLogging.IncludeThreadId = chkIncludeThreadId.Checked;
        }

        private void chkIncludeDebugMask_CheckedChanged(object sender, EventArgs e)
        {
            DebugLogging.IncludeDebugMask = chkIncludeDebugMask.Checked;
        }

        private void chkIncludeDebugFlags_CheckedChanged(object sender, EventArgs e)
        {
            DebugLogging.IncludeDebugFlags = chkIncludeDebugFlags.Checked;
        }

        private void chkToggleAll_CheckedChanged(object sender, EventArgs e)
        {
            chkToggle1.Checked =
            chkToggle2.Checked =
            chkToggle3.Checked =
            chkToggle4.Checked = chkToggleAll.Checked;

            chkToggle1_CheckedChanged(sender, e);
            chkToggle2_CheckedChanged(sender, e);
            chkToggle3_CheckedChanged(sender, e);
            chkToggle4_CheckedChanged(sender, e);
        }
    }
}
