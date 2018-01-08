using System;
using System.Drawing;
using System.Windows.Forms;

using Utility;

namespace Operationen.Weiterbildungzeitraum
{
    public partial class HSeparator : UserControl, System.IComparable<HSeparator>
    {
        private Label _label = new Label();
        private DateTime _date;
        private WeiterbildungszeitraumView _owner;
        private int _index;
        private bool _inMove;
        private int _mouseMoveX;
        private int _mouseMoveMaxX;

        public HSeparator(WeiterbildungszeitraumView owner, int index, DateTime date)
        {
            InitializeComponent();

            _owner = owner;
            _index = index;
            Date = date;

            BorderStyle = BorderStyle.Fixed3D; 
            _label.AutoSize = true;
        }

        /// <summary>
        /// From MI3: "It's complicated"
        /// 
        /// I tried to use SortedList at first:
        /// To sort all lines by date, we use a SortedList and our IComparable sorts by date.
        /// We want to allow the same date multiple time, but this is not supported by
        /// the SortedList, which requires that all keys MUST be distinct.
        ///     So we never return 0, and find out that Remove() and ContainsKey() do not work
        /// because they use CompareTo() which never returns 0...
        ///     Therefore we test the pointer.
        /// If the actual objects are the same, we return 0, otherwise -1 or 1.
        /// This did not work. Somehow, the pointer stuff did not work. You cannot CHANGE the key 
        /// in a SortedList.
        /// 
        /// Now I use a normal List and call _separators.Sort()
        /// </summary>
        /// <param name="other">What do you think?</param>
        /// <returns></returns>
        public int CompareTo(HSeparator other)
        {
            return _date.CompareTo(other._date);
        }

        internal int MouseMoveMaxX
        {
            set { _mouseMoveMaxX = value;  }
        }

        public Label Label
        {
            get { return _label; }
        }

        public new int Left
        {
            set
            {
                _label.Left = value - 10;
                base.Left = value;
            }
            get { return base.Left; }
        }

        public DateTime Date
        {
            get { return _date; }
            set 
            {
                _date = value;
                _label.Text = Tools.DateTime2DateStringYY(_date);
            }
        }

        public int Index
        {
            get { return _index; }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

#if DEBUG
            Graphics g = e.Graphics;
            g.DrawString(this.Name, Font, Brushes.Black, 0, 0);
#endif
        }

        /// <summary>
        /// x,y ist bezogen auf den HSeparator
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HSeparator_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _mouseMoveX = e.X;
                _inMove = true;
                Capture = true;
            }
            else if (e.Button == MouseButtons.Right)
            {
                ContextMenu cm = new ContextMenu();
                MenuItem mi = new MenuItem(_owner.GetText("removeSeparator"));
                mi.Click += new EventHandler(mi_Click);
                cm.MenuItems.Add(mi);
                this.ContextMenu = cm;
            }
        }

        void mi_Click(object sender, EventArgs e)
        {
            _owner.RemoveSeparator(this);
        }

        /// <summary>
        /// x,y ist bezogen auf den HSeparator
        /// Wenn man außerhalb zieht, wird x links negativ und rechts > die Breite
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HSeparator_MouseMove(object sender, MouseEventArgs e)
        {
            if (_inMove)
            {
                int x = Left + e.X - _mouseMoveX;
                if (x > WeiterbildungszeitraumView.PanelLeftRightOffset && x <= _mouseMoveMaxX)
                {
                    x = _owner.SnapToGrid(this, x);
                }
            }
        }


        private void HSeparator_MouseUp(object sender, MouseEventArgs e)
        {
            Capture = false;
            _inMove = false;

            int x = Left + e.X - _mouseMoveX;

            //
            // x > WeiterbildungszeitraumView.PanelLeftRightOffset weil vom ersten Datum 1 Tag abgezogen wird und
            // dann wäre der erste Trenner vor dem Anfangsdatum
            // 
            if (x > WeiterbildungszeitraumView.PanelLeftRightOffset && x <= _mouseMoveMaxX)
            {
                x = _owner.SnapToGrid(this, x);
                if (x > WeiterbildungszeitraumView.PanelLeftRightOffset && x <= _mouseMoveMaxX)
                {
                    this.Left = x;
                }
            }
            _owner.ReorderSeparators();
        }
    }
}
