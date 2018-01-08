using System;
using System.Windows.Forms;
using System.Drawing;

using Utility;

namespace Operationen.Weiterbildungzeitraum
{
    public class SnapLine : UserControl
    {
        private Label _label = new Label();
        private DateTime _date;
        public int X;

        public SnapLine(DateTime date, int left, int height)
        {
            BorderStyle = BorderStyle.FixedSingle;
            Width = 1;
            Left = left;
            X = left;
            _date = date;
            Height = height;

            _label.AutoSize = true;

            //
            // Show only year lines 1.1.XXXX
            //
            if (_date.Month != 1 || _date.Day != 1)
            {
//                _label.Visible = false;
            }
        }

        internal Label Label
        {
            get { return _label; }
        }

        internal new int Left
        {
            set
            {
                _label.Left = value + WeiterbildungszeitraumView.SplitterWidth;
                base.Left = value;
            }
            get { return base.Left; }
        }

        internal DateTime Date
        {
            get 
            { 
                return _date; 
            }
            set 
            {
                _label.Text = Tools.DateTime2DateString(_date);
                _date = value; 
            }
        }
    }
}
