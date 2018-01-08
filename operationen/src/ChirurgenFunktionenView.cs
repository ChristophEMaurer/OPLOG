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
    public class ChirurgenFunktionenView : TypenTemplateView
    {
        public ChirurgenFunktionenView(BusinessLayer businessLayer)
            : base(businessLayer)
        {
            this.Name = "ChirurgenFunktionenView";
        }

        protected override string GetTableName()
        {
            return BusinessLayer.TableChirurgenFunktionen;
        }

        protected override string GetTextColumnName()
        {
            return BusinessLayer.TextColumnChirurgenFunktionen;
        }

        protected override string GetEditRight()
        {
            return "ChirurgenFunktionenView.edit";
        }
    }
}
