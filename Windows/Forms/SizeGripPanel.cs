using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Windows.Forms
{
    /// <summary>
    /// http://www.codeproject.com/KB/miscctrl/GripPanel.aspx
    /// </summary>
    public class SizeGripPanel : Panel
    {
        private const int HTBOTTOMRIGHT = 17;
        private const int WM_NCHITTEST = 0x84;
        private const int WM_SIZE = 0x05;

        /// <summary>
        /// Catch some windows messages
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m) 
        {

          // Only catch messages if the panel is docked to the bottom

          if (Dock == DockStyle.Bottom) 
          {

            switch (m.Msg) 
            {

              // If the panel is being resized then we

              // need to redraw its contents

              case WM_SIZE:
                Invalidate();
                break;

              // If the system is asking where the mouse pointer is,

              // we need to check whether it is over our sizing grip

              case WM_NCHITTEST:

                // Convert to client co-ordinates of parent

                Point p = FindForm().PointToClient(new Point((int) m.LParam));
                int x = p.X;
                int y = p.Y;
                Rectangle rect = Bounds;

                // Is the mouse pointer over our sizing group?
                // (Use 12 pixels rather than 16 otherwise
                // too large an area is checked)

                if (x >= rect.X + rect.Width - 12 && 
                    x <= rect.X + rect.Width && 
                    y >= rect.Y + rect.Height - 12 && 
                    y <= rect.Y + rect.Height) {

                  // Yes, so tell windows it is in the lower-right
                  // corner of a border of a resizable window
                  // Windows will then do the neccessary resizing

                  m.Result = new IntPtr(HTBOTTOMRIGHT);
                  return;
                }
                break;
            }
          }

          // Do the normal message handling

          base.WndProc(ref m);
        }

        /// <summary>
        /// Override to add the border line at the top
        /// of the panel and the size grip itself
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e) 
        {
            // Do the normal painting

            base.OnPaint(e);
            // Are we docked at the bottom?

            if (Dock == DockStyle.Bottom) 
            {
                // Yes, so paint the adornments

                //ControlPaint.DrawBorder3D(e.Graphics, ClientRectangle, Border3DStyle.Raised, Border3DSide.Top);
                ControlPaint.DrawSizeGrip(e.Graphics, BackColor, Width - 16, Height -16, 16, 16);
            }
        }
    }
}
