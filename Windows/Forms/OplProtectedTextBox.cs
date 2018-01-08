using System;
using System.Drawing;
using System.Windows.Forms;

using AppFramework;

namespace Windows.Forms
{
    /// <summary>
    /// TextBox that does not write to log file
    /// </summary>
    public class OplProtectedTextBox : OplTextBox
    {
        public OplProtectedTextBox()
            : base()
        {
            ProtectContents = true;
        }
    }
}

