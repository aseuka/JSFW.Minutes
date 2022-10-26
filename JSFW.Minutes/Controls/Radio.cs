using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JSFW.Minutes.Controls
{
    public class Radio : RadioButton
    {
        Pen OutPen;
        SolidBrush InSolid;
        
        public Color PointColor
        {
            get { return InSolid.Color; }
            set
            {
                OutPen = new Pen(value, PenSize);
                InSolid = new SolidBrush(value);
                Invalidate();
            }
        }

        public Radio()
        {
            PointColor = ForeColor;
            MinimumSize = MaximumSize = Size = new Size(24, 24);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            e.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

            e.Graphics.Clear(BackColor);
            e.Graphics.DrawEllipse(OutPen, GetRectangle());
            e.Graphics.FillEllipse(InSolid, GetOrigin());
        }

        Rectangle GetRectangle()
        {
            int offset = PenSize; 
            int x = offset;
            int y = offset;          
            return new Rectangle(x, y, Width - ( 2 * offset), Height - ( 2 * offset));
        }

        static readonly int PenSize = 3; 
        static readonly int pointHalfSize = PenSize + 1;
        readonly int pointSize = 2 * pointHalfSize;

        Rectangle GetOrigin()
        {
            int x = 0;
            int y = 0;
            x = Width / 2 - pointHalfSize;
            y = Height / 2 - pointHalfSize;
            return new Rectangle(x, y, pointSize, pointSize);
        }

    }
}
