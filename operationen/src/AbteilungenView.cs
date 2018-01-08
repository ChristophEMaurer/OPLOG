// Translation: done

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
    public class AbteilungenView : TypenTemplateView
    {

        public AbteilungenView(BusinessLayer businessLayer)
            : base(businessLayer)
        {
            this.Name = "AbteilungenView";
        }

        protected override string GetTableName()
        {
            return BusinessLayer.TableAbteilungen;
        }

        protected override string GetEditRight()
        {
            return "AbteilungenView.edit";
        }
    }
}

