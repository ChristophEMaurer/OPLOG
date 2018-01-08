using System;
using System.Windows.Forms;

namespace AppFramework
{
    public interface ISecurityManager
    {
        bool UserHasRight(string right);
        Label FindNearestLabel(Control control);
    }
}

