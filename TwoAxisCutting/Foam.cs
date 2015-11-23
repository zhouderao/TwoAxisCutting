using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwoAxisCutting
{
    class Foam
    {
        private Point leftBottom = new Point(0,0);

        public Point LeftBottom
        {
            get { return leftBottom; }
            set { leftBottom = value; }
        }

        private int width = 300;

        public int Width
        {
            get { return width; }
            set { width = value; }
        }

        private int height = 300;

        public int Height
        {
            get { return height; }
            set { height = value; }
        }

        private Rectangle frame;

        public Rectangle Frame
        {
            get { return frame; }
            set { frame = value; }
        }

        public void Draw(Graphics grp)
        {
            Pen pen = new Pen(Color.White,1);

            Rectangle rect = new Rectangle();
            rect.X = this.frame.Right/2-width/2;
            rect.Y = this.frame.Bottom/2 - height/2;
            rect.Width = width;
            rect.Height = height;
            grp.DrawRectangle(pen, rect);
        }

        public Foam(Rectangle frame)
        {
            this.frame = frame;
        }

    }
}
