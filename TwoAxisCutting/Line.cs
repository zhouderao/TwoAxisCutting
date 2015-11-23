using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwoAxisCutting
{
    public class Line : Shape
    {

        public override void ReadStr()
        {
            string str;
            PointF point = new PointF(0, 0);
            this.idNum = this.strRead.Substring(0, 3);
            str = this.strRead.Substring(this.strRead.IndexOf("X") + 1, this.strRead.Length - this.strRead.IndexOf("X") - 1);
            point.X = float.Parse(str.Substring(0, str.IndexOf("Y")));
            str = str.Substring(str.IndexOf("Y") + 1, str.Length - str.IndexOf("Y") - 1);
            point.Y = float.Parse(str.Substring(0, str.IndexOf(";")));
            this.EndPoint = point;
        }

        public override PointF GetPoint()
        {
            PointF point = new PointF(0, 0);
            point.X = this.EndPoint.X - this.midPoint.X + this.myFrame.X;
            point.Y = this.myFrame.Y - (this.EndPoint.Y - this.midPoint.Y);
            return point;
        }

        public override void AssignValue()
        {
            this.xMin = ((this.StartPoint.X < this.EndPoint.X) ? this.StartPoint.X : this.EndPoint.X );
            this.xMax = ((this.StartPoint.X > this.EndPoint.X) ? this.StartPoint.X : this.EndPoint.X);
            this.yMin = ((this.StartPoint.Y < this.EndPoint.Y) ? this.StartPoint.Y : this.EndPoint.Y);
            this.yMax = ((this.StartPoint.Y > this.EndPoint.Y) ? this.StartPoint.Y : this.EndPoint.Y);
        }

        public override void Draw(Graphics grp)
        {
            Pen pen = new Pen(Color.Green, 1.0f);
            PointF point1 = new PointF(0, 0);
            PointF point2 = new PointF(0, 0);
            point1.X = this.StartPoint.X - this.midPoint.X + this.myFrame.Width /2;
            point1.Y = this.myFrame.Height / 2 - (this.StartPoint.Y - this.midPoint.Y);
            point2.X = this.EndPoint.X - this.midPoint.X +  this.myFrame.Width / 2;
            point2.Y = this.myFrame.Height / 2 - (this.EndPoint.Y - this.midPoint.Y);
            grp.DrawLine(pen, point1, point2);
        }
    }
}
