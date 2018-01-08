using System;

using System.Windows.Forms;

namespace Windows.Forms
{
    public class ListViewBalken : OplListView
    {
        public ListViewBalken()
        {
            View = View.Details;

            BalkenGrafik = true;
        }
    }
}
