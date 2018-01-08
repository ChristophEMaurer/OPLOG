// Translation: done

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
    public class AkademischeAusbildungTypenView : TypenTemplateView
    {
        public AkademischeAusbildungTypenView(BusinessLayer businessLayer)
            : base(businessLayer)
        {
            this.Name = "AkademischeAusbildungTypenView";
        }

        protected override string GetTableName()
        {
            return BusinessLayer.TableAkademischeAusbildungTypen;
        }

        protected override string GetEditRight()
        {
            return "AkademischeAusbildungTypenView.edit";
        }

        protected override string GetInfoText()
        {
            return string.Format(CultureInfo.InvariantCulture, GetText("info"), Command_AkademischeAusbildungView);
        }
    }
}

