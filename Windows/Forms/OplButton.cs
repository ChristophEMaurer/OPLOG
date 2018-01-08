using System;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;
using System.IO;

using AppFramework;

namespace Windows.Forms
{
    public class OplButton : System.Windows.Forms.Button
    {
        private ISecurityManager _securityManager;
        private string _userRight = null;

        public OplButton()
            : base()
        {
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

            this.Font = new Font(this.Font, FontStyle.Underline);
        }

        public ISecurityManager SecurityManager
        {
            set { _securityManager = value; }
            get { return _securityManager; }
        }

        public string UserRight
        {
            set { _userRight = value; }
            get { return _userRight; }
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
    }
}

