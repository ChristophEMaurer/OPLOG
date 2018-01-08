using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using System.Text;

namespace AppFramework.Debugging
{
    public interface IDebuggee
    {
        void DebugPrint(long flag, string msg);
    }
}

