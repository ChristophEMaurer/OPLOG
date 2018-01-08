using System;
using System.Collections.Generic;
using System.Text;

namespace Operationen
{
    public class LicenseInfo
    {
        private string id;
        private string userLastName;
        private string userFirstName;
        private string computerName;
        private string companyName;
        private DateTime? expires;

        private bool isValid = false;
        private string invalidLicenseReason = "Unbekannt";

        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        public string UserLastName
        {
            get { return userLastName; }
            set { userLastName = value; }
        }
        public string UserFirstName
        {
            get { return userFirstName; }
            set { userFirstName = value; }
        }
        public string ComputerName
        {
            get { return computerName; }
            set { computerName = value; }
        }
        public bool IsValid
        {
            get { return isValid; }
            set { isValid = value; }
        }

        public string InvalidLicenseReason
        {
            get { return invalidLicenseReason; }
            set { invalidLicenseReason = value; }
        }

        public string CompanyName
        {
            get { return companyName; }
            set { companyName = value; }
        }

        public DateTime? Expires
        {
            get { return expires; }
            set { expires = value; }
        }
    }
}
