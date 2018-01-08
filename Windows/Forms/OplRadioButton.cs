using System;
using System.Drawing;

using AppFramework;

namespace Windows.Forms
{
    public class OplRadioButton : System.Windows.Forms.RadioButton
    {
        private ISecurityManager _securityManager = null;
        private string _userRight = null;

        public OplRadioButton()
            : base()
        {
        }

        protected override void OnClick(EventArgs e)
        {
            System.Windows.Forms.Form form = this.FindForm();
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

