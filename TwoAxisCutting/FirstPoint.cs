using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwoAxisCutting
{
    public class FirstPoint:Shape
    {
        public override void ReadStr()
        {
            string str;
            PointF point = new PointF(0, 0);
            this.idNum = this.strRead.Substring(0, 3);
            str = this.strRead.Substring(this.strRead.IndexOf("X") + 1, this.strRead.Length - this.strRead.IndexOf("X")-1);
            point.X = float.Parse(str.Substring(0, str.IndexOf("Y")));
            str = str.Substring(str.IndexOf("Y") + 1, str.Length - str.IndexOf("Y") - 1);
            point.Y = float.Parse(str.Substring(0, str.IndexOf(";")));
            this.EndPoint = point;
            this.StartPoint = point;
        }

        public override PointF GetPoint()
        {
            PointF point = new PointF(0, 0);
            point.X = this.EndPoint.X - this.midPoint.X + this.myFrame.X;
            point.Y = this.myFrame.Y-(this.EndPoint.Y - this.midPoint.Y);
            return point;
        }

        public override void AssignValue()
        {
            this.xMin =this.xMax = this.EndPoint.X;
            this.yMax = this.yMin = this.EndPoint.Y;
        }

        public override void Draw(Graphics grp)
        {
        }
    }
}
