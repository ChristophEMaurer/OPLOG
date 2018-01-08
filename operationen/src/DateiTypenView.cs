using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

using Utility;
using Windows.Forms;

namespace Operationen
{
    public partial class DateiTypenView : TypenTemplateView
    {
        public DateiTypenView(BusinessLayer businessLayer)
            : base(businessLayer)
        {
            this.Name = "DateiTypenView";

            this.FormClosed += new FormClosedEventHandler(DateiTypenView_FormClosed);
        }

        void DateiTypenView_FormClosed(object sender, FormClosedEventArgs e)
        {
            OperationenLogbuchView.TheMainWindow.CreateEigeneDateienCustomMenu();
        }

        protected override string GetTableName()
        {
            return BusinessLayer.TableDateiTypen;
        }

        protected override string GetTextColumnName()
        {
            return BusinessLayer.TextColumnDateiTypen;
        }

        protected override string GetEditRight()
        {
            return "DateiTypenView.edit";
        }

        protected override string GetInfoText()
        {
            return string.Format(CultureInfo.InvariantCulture, GetText("info"), Command_DateienView);
        }
    }
}

