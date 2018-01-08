using System;
using System.Data;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace Operationen.Wizards.CreateCustomerData
{
    public class CustomerDataCreator : ImporterExporter
    {
        private const string FormName = "Wizards_CreateCustomerData_CustomerDataCreator";
        private DataTable _data;

        public CustomerDataCreator(BusinessLayer businessLayer)
            : base(businessLayer, null)
        {
            _businessLayer = businessLayer;
        }

        public void Initialize(DataTable table)
        {
            _data = table;
        }

        public bool Create()
        {
            bool success = true;

            try
            {
                _businessLayer.SaveCustomerData(_data);
            }
            catch
            {
                _businessLayer.MessageBox(GetText(FormName, "error"));
                success = false;
            }

            return success;
        }
    }
}
