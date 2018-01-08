using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Operationen
{
    public partial class SplashScreenView : OperationenForm
    {
        private delegate void SetTextDelegate(string text);

        public SplashScreenView(BusinessLayer businessLayer) :
            base(businessLayer)
        {
            InitializeComponent();

            pbHelp.Visible = false;
            this.Text = businessLayer.GetText("SplashScreenView", "title");
        }

        public void SetText(string text)
        {
            if (this.lblInfo.InvokeRequired)
            {
                SetTextDelegate d = new SetTextDelegate(SetText);
                this.Invoke(d, text);
            }
            else
            {
                lblInfo.Text = text;
                lblInfo.Update();
            }
        }

        private void SplashScreenView_Load(object sender, EventArgs e)
        {
        }
    }
}
