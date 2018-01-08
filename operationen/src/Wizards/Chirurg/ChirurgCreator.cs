using System;
using System.Data;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace Operationen.Wizards.Chirurg
{
    public class ChirurgCreator : ImporterExporter
    {
        private DataRow _chirurg;

        public ChirurgCreator(BusinessLayer businessLayer)
            : base(businessLayer, null)
        {
            _businessLayer = businessLayer;
        }

        public void Initialize(DataRow row)
        {
            _chirurg = row;
        }

        public bool Create()
        {
            bool success = false;

            try
            {
                if (_businessLayer.InsertChirurg(_chirurg, null, null) > 0)
                {
                    success = true;
                }
            }
            catch
            {
                _businessLayer.MessageBox("Der Benutzer konnte nicht angelegt werden.");
            }

            return success;
        }
    }
}
