using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwoAxisCutting
{
    public class ClockwiseArc:Shape
    {
        private float iCenter;

        private float jCenter;

        private float radius;

        private float x;

        private float y;

        private float angSta;

        private float angEnd;

        public override void ReadStr()
        {
            string str;
            PointF point = new PointF(0, 0);
            this.idNum = this.strRead.Substring(0, 3);
            str = this.strRead.Substring(this.strRead.IndexOf("X") + 1, this.strRead.Length - this.strRead.IndexOf("X") - 1);
            point.X = float.Parse(str.Substring(0, str.IndexOf("Y")));
            str = str.Substring(str.IndexOf("Y") + 1, str.Length - str.IndexOf("Y") - 1);
            point.Y = float.Parse(str.Substring(0, str.IndexOf("I")));
            this.EndPoint = point;
            str = str.Substring(str.IndexOf("I") + 1, str.Length - str.IndexOf("I") - 1);
            this.iCenter = float.Parse(str.Substring(0, str.IndexOf("J")));
            str = str.Substring(str.IndexOf("J") + 1, str.Length - str.IndexOf("J") - 1);
            this.jCenter = float.Parse(str.Substring(0, str.IndexOf(";")));
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
            x = this.EndPoint.X - this.StartPoint.X - this.iCenter;
            y = this.EndPoint.Y - this.StartPoint.Y - this.jCenter;
            this.radius = (float)(Math.Sqrt(this.iCenter * this.iCenter + this.jCenter * this.jCenter));
            if (iCenter == 0)
            {
                if (jCenter > 0)
                {
                    this.angSta = 90;
                }
                else
                {
                    this.angSta = 270;
                }
            }
            else if (iCenter < 0)
            {
                angSta = (float)(Math.Atan(-jCenter/iCenter)*180/Math.PI);
                if (this.angSta < 0)
                {
                    this.angSta += 360;
                }
            }
            else
            {
                this.angSta = (float)(Math.Atan(-jCenter / iCenter) * 180 / Math.PI) + 180;
            }

            if (x == 0)
            {
                if (y > 0)
                {
                    this.angEnd = 90;
                }
                else
                {
                    this.angEnd = 270;
                }
            }
            else if (x > 0)
            {
                this.angEnd = (float)(Math.Atan(-y / x) * 180 / Math.PI);
                if (this.angEnd < 0)
                {
                    this.angEnd += 360;
                }
            }
            else
            {
                this.angEnd = (float)(Math.Atan(-y / x) * 180 / Math.PI) + 180;
            }
 
            #region 第一组取xMin、yMin、xMax、yMax
            if ((this.iCenter>=0) && (this.jCenter > 0))
            {
                if ((x>=0) && (y>=0))
                {
                    this.xMin = this.StartPoint.X + this.iCenter - this.radius;
                    this.xMax = this.EndPoint.X;
                    this.yMin = this.StartPoint.Y;
                    this.yMax = this.StartPoint.Y + this.jCenter + this.radius;
                }
                else if ((x < 0) && (y >= 0))
                {
                    this.xMin = this.StartPoint.X + this.iCenter - this.radius;
                    this.xMax = ((this.StartPoint.X > this.EndPoint.X) ? this.StartPoint.X : this.EndPoint.X);
                    this.yMin = this.StartPoint.Y;
                    this.yMax = this.EndPoint.Y;
                }
                else if ((x >= 0) && (y < 0))
                {
                    this.xMin = this.StartPoint.X + this.iCenter - this.radius;
                    this.xMax = this.StartPoint.X + this.iCenter + this.radius;
                    this.yMin = (this.StartPoint.Y < this.EndPoint.Y) ? (this.StartPoint.Y) : (this.EndPoint.Y);
                    this.yMax = this.StartPoint.Y + this.jCenter + this.radius;
                }
                else
                {
                    if (this.StartPoint.X > this.EndPoint.X)
                    {
                        this.xMin = this.EndPoint.X;
                        this.xMax = this.StartPoint.X;
                        this.yMin = this.StartPoint.Y;
                        this.yMax = this.EndPoint.Y;
                    }
                    else
                    {
                        this.xMin = this.StartPoint.X + this.iCenter - this.radius;
                        this.xMax = this.StartPoint.X + this.iCenter + this.radius;
                        this.yMin = this.StartPoint.Y + this.jCenter - this.radius;
                        this.yMax = this.StartPoint.Y + this.jCenter + this.radius;
                    }
                }
            }
#endregion

            #region 第二组取xMin、yMin、xMax、yMax

            if ((iCenter >= 0) && (jCenter <= 0))
            {
                if ((x >= 0) && (y >= 0))
                {
                    this.xMin = this.StartPoint.X;
                    this.xMax = this.EndPoint.X;
                    this.yMin = (this.StartPoint.Y < this.EndPoint.Y) ? (this.StartPoint.Y) : (this.EndPoint.Y); 
                    this.yMax = this.StartPoint.Y + this.jCenter + this.radius;
                }
                else if ((x < 0) && (y >= 0))
                {
                    if (this.StartPoint.X > this.EndPoint.X)
                    {
                        this.xMin = this.StartPoint.X;
                        this.xMax = this.EndPoint.X;
                        this.yMin = this.StartPoint.Y;
                        this.yMax = this.EndPoint.Y;
                    }
                    else
                    {
                        this.xMin = this.StartPoint.X + this.iCenter - this.radius;
                        this.xMax = this.StartPoint.X + this.iCenter + this.radius;
                        this.yMin = this.StartPoint.Y + this.jCenter - this.radius;
                        this.yMax = this.StartPoint.Y + this.jCenter + this.radius;
                    }
                }
                else if ((x >= 0) && (y < 0))
                {
                    this.xMin = this.StartPoint.X;
                    this.xMax = this.StartPoint.X + this.iCenter + this.radius;
                    this.yMin = this.EndPoint.Y;
                    this.yMax = this.StartPoint.Y + this.jCenter + this.radius;
                }
                else
                {
                    this.xMin = ((this.StartPoint.X < this.EndPoint.X) ? this.StartPoint.X : this.EndPoint.X);
                    this.xMax = this.StartPoint.X + this.iCenter + this.radius;
                    this.yMin = this.StartPoint.Y + this.jCenter - this.radius;
                    this.yMax = this.StartPoint.Y + this.jCenter + this.radius;
                }
            }
#endregion

            #region 第三组取xMin、yMin、xMax、yMax
            if ((iCenter < 0) && (jCenter > 0))
            {
                if ((x >= 0) && (y >= 0))
                {
                    this.xMin = this.StartPoint.X + this.iCenter - this.radius;
                    this.xMax = ((this.StartPoint.X > this.EndPoint.X) ? this.StartPoint.X : this.EndPoint.X);
                    this.yMin = this.StartPoint.Y + this.jCenter - this.radius;
                    this.yMax = this.StartPoint.Y + this.jCenter + this.radius;
                }
                else if ((x < 0) && (y >= 0))
                {
                    this.xMin = this.StartPoint.X + this.iCenter - this.radius;
                    this.xMax = this.StartPoint.X;
                    this.yMin = this.StartPoint.Y + this.jCenter - this.radius;
                    this.yMax = this.EndPoint.Y;
                }
                else if ((x >= 0) && (y < 0))
                {
                    if (this.StartPoint.X > this.EndPoint.X)
                    {
                        this.xMin = this.EndPoint.X;
                        this.xMax = this.StartPoint.X;
                        this.yMin = this.EndPoint.Y;
                        this.yMax = this.StartPoint.Y;
                    }
                    else
                    {
                        this.xMin = this.StartPoint.X + this.iCenter - this.radius;
                        this.xMax = this.StartPoint.X + this.iCenter + this.radius;
                        this.yMin = this.StartPoint.Y + this.jCenter - this.radius;
                        this.yMax = this.StartPoint.Y + this.jCenter + this.radius;
                    }
                }
                else
                {
                    this.xMin = this.EndPoint.X;
                    this.xMax = this.StartPoint.X;
                    this.yMin = this.StartPoint.Y + this.jCenter - this.radius;
                    this.yMax = ((this.StartPoint.Y > this.EndPoint.Y) ? this.StartPoint.Y : this.EndPoint.Y);
                }
            }
#endregion

            #region 第四组取xMin、yMin、xMax、yMax
            if ((iCenter < 0) && (jCenter <= 0))
            {
                if ((x >= 0) && (y >= 0))
                {
                    if (this.StartPoint.X < this.EndPoint.X)
                    {
                        this.xMin = this.StartPoint.X ;
                        this.xMax = this.EndPoint.Y;
                        this.yMin = this.EndPoint.Y;
                        this.yMax = this.StartPoint.Y;
                    }
                    else
                    {
                        this.xMin = this.StartPoint.X + this.iCenter - this.radius;
                        this.xMax = this.StartPoint.X + this.iCenter + this.radius;
                        this.yMin = this.StartPoint.Y + this.jCenter - this.radius;
                        this.yMax = this.StartPoint.Y + this.jCenter + this.radius;
                    }
                }
                else if ((x < 0) && (y >= 0))
                {
                    this.xMin = this.StartPoint.X + this.iCenter - this.radius;
                    this.xMax = this.StartPoint.X + this.iCenter + this.radius;
                    this.yMin = this.StartPoint.Y + this.jCenter - this.radius;
                    this.yMax = ((this.StartPoint.Y > this.EndPoint.Y) ? this.StartPoint.Y : this.EndPoint.Y);
                }
                else if ((x >= 0) && (y < 0))
                {
                    this.xMin = ((this.StartPoint.X < this.EndPoint.X) ? this.StartPoint.X : this.EndPoint.X);
                    this.xMax = this.StartPoint.X + this.iCenter + this.radius;
                    this.yMin = this.EndPoint.Y;
                    this.yMax = this.StartPoint.Y;
                }
                else
                {
                    this.xMin = this.EndPoint.X;
                    this.xMax = this.StartPoint.X + this.iCenter + this.radius;
                    this.yMin = this.StartPoint.Y + this.jCenter - this.radius;
                    this.yMax = this.StartPoint.Y;
                }
            }
            #endregion
        }

        public override void Draw(Graphics grp)
        {
            Pen pen = new Pen(Color.Green, 1.0f);
            this.radius = (float)(Math.Sqrt(this.iCenter * this.iCenter + this.jCenter * this.jCenter));

            RectangleF rect = new RectangleF();
            rect.X = this.StartPoint.X + this.iCenter - this.radius -this.midPoint.X + this.myFrame.Width/2;
            rect.Y = this.myFrame.Height/2 -(this.StartPoint.Y + this.jCenter + this.radius-this.midPoint.Y) ;
            rect.Width = 2 * this.radius; 
            rect.Height = 2 * this.radius;
            if (this.StartPoint == this.EndPoint)
            {
                grp.DrawEllipse(pen, rect);
            }
            else
            {
                if (angEnd < angSta)
                {
                    angEnd += 360;
                }
                grp.DrawArc(pen, rect, angSta, angEnd - angSta);
            }
        }
    }
}
