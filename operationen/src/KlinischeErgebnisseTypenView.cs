using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Utility;
using Windows.Forms;

namespace Operationen
{
    public class KlinischeErgebnisseTypenView : TypenTemplateView
    {

        public KlinischeErgebnisseTypenView(BusinessLayer b)
            : base(b)
        {
            this.Name = "KlinischeErgebnisseTypenView";

            Text = AppTitle(GetText("title"));
        }

        protected override DataView GetDataView()
        {
            return BusinessLayer.GetTypenTemplate(BusinessLayer.TableKlinischeErgebnisseTypen, false);
        }

        protected override void InsertObject(DataRow row)
        {
            BusinessLayer.InsertTypenTemplate(BusinessLayer.TableKlinischeErgebnisseTypen, row);
        }
        protected override void UpdateObject(DataRow row)
        {
            BusinessLayer.UpdateTypenTemplate(BusinessLayer.TableKlinischeErgebnisseTypen, row);
        }
        protected override void DeleteObject(int id)
        {
            BusinessLayer.DeleteTypenTemplate(BusinessLayer.TableKlinischeErgebnisseTypen, id);
        }
        protected override DataRow GetObject(int id)
        {
            return BusinessLayer.GetTypenTemplate(BusinessLayer.TableKlinischeErgebnisseTypen, id);
        }
        protected override DataRow CreateDataRow()
        {
            return BusinessLayer.CreateDataRowTypenTemplate(BusinessLayer.TableKlinischeErgebnisseTypen);
        }

        protected override string GetInfoText()
        {
            return GetText("info");
        }

        protected override bool EditAllowed()
        {
            return BusinessLayer.CurrentUserIsAdmin();
        }
    }
}

