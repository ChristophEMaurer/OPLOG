using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;

namespace AppFramework
{
    public sealed class Registry
    {
        static public object GetValueCurrentUser(string subkey, string key)
        {
            RegistryKey TAWKAY = RegistryKey.OpenRemoteBaseKey(Microsoft.Win32.RegistryHive.CurrentUser, "");
            RegistryKey SUBKEY = TAWKAY.OpenSubKey(subkey);
            return SUBKEY.GetValue(key);
        }

        private Registry() 
        {
            // StaticHolderTypesShouldNotHaveConstructors
        }
    }
}

