using System;
using System.Windows.Forms;

namespace Windows.Forms
{
    public class SortableListView : OplListView
    {
        public SortableListView()
        {
            View = View.Details;

            Sortable = true;
        }
    }
}

