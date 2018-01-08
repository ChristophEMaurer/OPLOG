using System;
using System.Collections.Generic;
using System.Text;

using System.Windows.Forms;
using System.Drawing;

namespace Windows.Forms
{
    public class SortableListViewBalken : OplListView
    {
        public SortableListViewBalken()
        {
            View = View.Details;

            BalkenGrafik = true;
            Sortable = true;
        }
    }
}
