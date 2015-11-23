using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TwoAxisCutting
{
    public abstract class Shape
    {
        public RectangleF myFrame;

        public PointF midPoint { get; set; }

        public string idNum { get; set; }

        public string strRead { get; set; }

        public PointF StartPoint { get; set; }

        public PointF EndPoint { get; set; }

        public float xMin { get; set; }

        public float yMin { get; set; }

        public float xMax { get; set; }

        public float yMax { get; set; }

        public abstract void ReadStr();

        public abstract PointF GetPoint();

        public abstract void AssignValue();

        public abstract void Draw(Graphics grp);
    }
}
