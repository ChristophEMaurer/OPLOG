using System;
using System.Windows.Forms;

namespace AppFramework.Debugging
{
    /// <summary>
    /// Dialog to prompt a user for a password
    /// </summary>
    public partial class EnterPasswordView : Form
    {
        /// <summary>
        /// Ctor does nothing
        /// </summary>
        public EnterPasswordView(string title)
        {
            InitializeComponent();

            Text = title;
        }

        /// <summary>
        /// Get or set the password
        /// </summary>
        public string Password
        {
            get { return txtPassword.Text; }
            set { txtPassword.Text = value; }
        }

        private void EnterPasswordView_Shown(object sender, EventArgs e)
        {
            txtPassword.Focus();
        }
    }
}
