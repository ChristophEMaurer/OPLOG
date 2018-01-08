using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Operationen
{
    public partial class SelectFunktionView : OperationenForm
    {
        List<OP_FUNCTION> _funktionen = new List<OP_FUNCTION>();

        public SelectFunktionView()
        {
            InitializeComponent();
        }

        public List<OP_FUNCTION> Funktionen
        {
            get { return _funktionen; }
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            _funktionen.Clear();
            Close();
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            _funktionen.Clear();

            if (chkOp.Checked)
            {
                _funktionen.Add(OP_FUNCTION.OP_FUNCTION_OP);
            }
            if (chkAss1.Checked)
            {
                _funktionen.Add(OP_FUNCTION.OP_FUNCTION_ASS);
            }
            if (chkAss2.Checked)
            {
                _funktionen.Add(OP_FUNCTION.OP_FUNCTION_ASS2);
            }
            if (chkAss3.Checked)
            {
                _funktionen.Add(OP_FUNCTION.OP_FUNCTION_ASS3);
            }

            Close();
        }
    }
}
