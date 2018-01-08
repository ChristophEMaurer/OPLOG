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
    public partial class NotizTypenView : TypenTemplateView
    {
        public NotizTypenView(BusinessLayer businessLayer)
            : base(businessLayer)
        {
            this.Name = "NotizTypenView";
        }

        protected override string GetTableName()
        {
            return BusinessLayer.TableNotizTypen;
        }

        protected override string GetEditRight()
        {
            return "NotizTypenView.edit";
        }

        protected override string GetInfoText()
        {
            return string.Format(CultureInfo.InvariantCulture, GetText("info"), Command_NotizenView);
        }
    }
}