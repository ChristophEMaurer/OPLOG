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
    public class SecGroupsView : TypenTemplateView
    {
        public SecGroupsView(BusinessLayer businessLayer)
            : base(businessLayer)
        {
            this.Name = "SecGroupsView";
        }

        protected override string GetTableName()
        {
            return BusinessLayer.TableSecGroups;
        }

        protected override string GetEditRight()
        {
            return "SecGroupsView.edit";
        }
    }
}

