using System;
using System.Drawing;
using System.Windows.Forms;

using AppFramework;

namespace Windows.Forms
{
    public class OplTextBox : System.Windows.Forms.TextBox
    {
        private ISecurityManager _securityManager = null;
        private string _userRight = null;

        public OplTextBox()
            : base()
        {
        }

        protected override void OnLeave(EventArgs e)
        {
            if (!ProtectContents)
            {
                Form form = this.FindForm();
                if (form is DatabaseForm)
                {
                    DatabaseForm databaseForm = (DatabaseForm)form;
                    databaseForm.DebugPrintControlContents(this);
                }
            }

            base.OnLeave(e);
        }

        protected override void OnClick(EventArgs e)
        {
            Form form = this.FindForm();
            if (form is DatabaseForm)
            {
                DatabaseForm databaseForm = (DatabaseForm)form;
                databaseForm.DebugPrintControlClicked(this);
            }

            base.OnClick(e);
        }

        public void SetSecurity(ISecurityManager securityManager, string userRight)
        {
            _securityManager = securityManager;
            _userRight = userRight;

            if (!_securityManager.UserHasRight(_userRight))
            {
                base.Enabled = false;
            }

            //
            // We cannot change the font of the TextBox, this makes no sense. 
            // We will change the font of the nearest label, this should be the label of the TextBox.
            // Set the font of the label to underline
            Label label = securityManager.FindNearestLabel(this);
            label.Font = new Font(label.Font, FontStyle.Underline);
        }

        public new bool Enabled
        {
            set 
            {
                if (value)
                {
                    if ((_securityManager == null) || (_userRight == null) || (_securityManager.UserHasRight(_userRight)))
                    {
                        base.Enabled = true;
                    }
                }
                else
                {
                    base.Enabled = value;
                }
            }
        }

        /// <summary>
        /// Set this to true if you do NOT want to write the contents to the log file.
        /// </summary>
        public bool ProtectContents { get; set; }
    }
}

