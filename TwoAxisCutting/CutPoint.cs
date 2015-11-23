using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwoAxisCutting
{
    class CutPoint
    {
        private PointF point;

        public PointF Point
        {
            get { return point; }
            set { point = value; }
        }
        private Rectangle frame;
        public Rectangle Frame
        {
            get { return frame; }
            set { frame = value; }
        }

        public void Draw(Graphics grp)
        {
            grp.FillEllipse(Brushes.Red, point.X-10.0f, point.Y -10.0f, 20, 20);
        }

        public CutPoint(PointF point,Rectangle frame)
        {
            this.point.X = point.X + frame.Right/2;
            this.point.Y = -point.Y + frame.Bottom/2;
        }
    }
}
