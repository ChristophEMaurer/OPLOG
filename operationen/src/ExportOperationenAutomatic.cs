using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace Operationen
{
    /// <summary>
    /// Create a dictionary which contains all operatione for expoort to mobile device.
    /// This is a pain to do manually. 100 groups have to be created.
    /// </summary>
    public partial class ExportOperationenAutomatic
    {
        BusinessLayer _businessLayer;
        ExportOperationenKatalogView _callingForm;

        SortedDictionary<string, List<ExportOperationenDataItem>> _data = new SortedDictionary<string, List<ExportOperationenDataItem>>();

        public ExportOperationenAutomatic(BusinessLayer businessLayer, ExportOperationenKatalogView callingForm)
        {
            _businessLayer = businessLayer;
            _callingForm = callingForm;
        }

        public SortedDictionary<string, List<ExportOperationenDataItem>> GetData()
        {
            return _data;
        }

        public void CreateAll()
        {
            string opsCodeStart;
            string opsCodeStop;

            _businessLayer.OpenDatabaseForImport();

            try
            {
                for (int i = 0; i < _groups.Length - 1; i++)
                {
                    opsCodeStart = _groups[i];
                    opsCodeStop = _groups[i + 1];

                    CreateGroup(opsCodeStart, opsCodeStop);
                }

                opsCodeStart = _groups[_groups.Length - 1];

                CreateGroup(opsCodeStart);

            }
            catch (Exception e)
            {
                _callingForm.MessageBox(e.Message);
            }

            _businessLayer.CloseDatabaseForImport();
        }

        /// <summary>
        /// Create one group and the items for this group
        /// </summary>
        /// <param name="id_operationenStart">the id of the group, this is not included in the items, items start at id_operationenStart + 1</param>
        /// <param name="id_operationenStop">the last item, this one wil be included</param>
        public void AddGroupAndItems(int id_operationenStart, int id_operationenStop)
        {
            string strGroup = "";
            string strOpsCode = "";
            string strOpsText = "";

            try
            {
                DataRow rowGroup = _businessLayer.GetOperation(id_operationenStart);

                if (rowGroup != null)
                {
                    strGroup = (string)rowGroup["Kode"] + " " + (string)rowGroup["Name"];

                    List<ExportOperationenDataItem> list;
                    list = new List<ExportOperationenDataItem>();
                    _data.Add(strGroup, list);

                    for (int i = id_operationenStart + 1; i <= id_operationenStop; i++)
                    {
                        DataRow rowEntry = _businessLayer.GetOperation(i);

                        if (rowEntry != null)
                        {
                            strOpsCode = (string)rowEntry["Kode"];
                            strOpsText = (string)rowEntry["Name"];

                            ExportOperationenDataItem item = new ExportOperationenDataItem(strOpsCode, strOpsText);
                            list.Add(item);

                            Application.DoEvents();
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                string msg = string.Format("Error importing data: {0}-{1}, group='{2}', opscode='{3}', opstext='{4}'", id_operationenStart, id_operationenStop, strGroup, strOpsCode, strOpsText);
                _callingForm.MessageBox(msg + "\n\n" + exception.Message);
            }
        }

        public void CreateGroup(string opsCodeStart)
        {
            int pkOpsCodeStart = _businessLayer.GetID_OperationenByOpsKode(opsCodeStart);
            int pkOpsCodeStop = _businessLayer.GetMaxID_Operationen();

            AddGroupAndItems(pkOpsCodeStart, pkOpsCodeStop);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="opsCodeStart">This is the ops-code of the group</param>
        /// <param name="opsCodeStop">up to this one but not including this one</param>
        public void CreateGroup(string opsCodeStart, string opsCodeStop)
        {
            int pkOpsCodeStart = _businessLayer.GetID_OperationenByOpsKode(opsCodeStart);
            int pkOpsCodeStop = _businessLayer.GetID_OperationenByOpsKode(opsCodeStop);

            AddGroupAndItems(pkOpsCodeStart, pkOpsCodeStop - 1);
        }
    }
}
