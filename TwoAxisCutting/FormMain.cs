using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TwoAxisCutting
{
    public partial class FormMain : Form
    {
        PointF point = new PointF(-160, -160);
        PointF midGPoint = new PointF(0, 0);
        int i = 0;
        float xMid = 0;
        float yMid = 0;
        float xGMin = 0;
        float yGMin = 0;
        float xGMax = 0;
        float yGMax = 0;
        List<Shape> shapes = new List<Shape>();
        public FormMain()
        {
            InitializeComponent();
        }
        private void openFile_Click(object sender, EventArgs e)
        {
            string path = @"D:";
            Graphics grp = this.drawPnl.CreateGraphics();
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                shapes.Clear();
                gCodeList.Items.Clear();
                i = 0;
                grp.Clear(this.drawPnl.BackColor); 
                path = this.openFileDialog1.FileName;
                PointF point = new PointF(0, 0);
                string strForm = null;
                StreamReader sr = new StreamReader(path, Encoding.Default);
                while ((strForm = sr.ReadLine()) != null)
                {
                    gCodeList.Items.Add(strForm);
                    if (strForm.Contains("N"))
                    {
                        if (i == 0)
                        {
                            FirstPoint firstPoint = new FirstPoint();
                            firstPoint.strRead = strForm;
                            firstPoint.ReadStr();
                            firstPoint.AssignValue();
                            xGMin = xGMax = firstPoint.xMin;
                            yGMin = yGMax = firstPoint.yMin;
                            shapes.Add(firstPoint);
                        }
                        else
                        {
                            if (strForm.Contains("G01"))
                            {
                                Line line = new Line();
                                line.strRead = strForm;
                                line.StartPoint = this.shapes[i - 1].EndPoint;
                                line.ReadStr();
                                line.AssignValue();
                                shapes.Add(line);
                            }
                            else if (strForm.Contains("G02"))
                            {
                                ClockwiseArc clockwiseArc = new ClockwiseArc();
                                clockwiseArc.strRead = strForm;
                                clockwiseArc.StartPoint = this.shapes[i - 1].EndPoint;
                                clockwiseArc.ReadStr();
                                clockwiseArc.AssignValue();
                                shapes.Add(clockwiseArc);
                            }
                            else if (strForm.Contains("G03"))
                            {
                                CounterclockwiseArc counterclockwiseArc = new CounterclockwiseArc();
                                counterclockwiseArc.strRead = strForm;
                                counterclockwiseArc.StartPoint = this.shapes[i - 1].EndPoint;
                                counterclockwiseArc.ReadStr();
                                counterclockwiseArc.AssignValue();
                                shapes.Add(counterclockwiseArc);
                            }
                        }
                        i++;
                    }

                    foreach (Shape shape in shapes)
                    {
                        if (shape.xMax > this.xGMax)
                        {
                            this.xGMax = shape.xMax;
                        }
                        if (shape.xMin < this.xGMin)
                        {
                            this.xGMin = shape.xMin;
                        }

                        if (shape.yMax > this.yGMax)
                        {
                            this.yGMax = shape.yMax;
                        }

                        if (shape.yMin < this.yGMin)
                        {
                            this.yGMin = shape.yMin;
                        }
                    }
                }
                gCodeList.SelectedIndex = 0;

                if (gCodeList.Items.Count == 0)
                    return;

                this.xMid = (this.xGMin + this.xGMax) / 2;
                this.yMid = (this.yGMax + this.yGMin) / 2;
                this.midGPoint.X = this.xMid;
                this.midGPoint.Y = this.yMid;
                foreach (Shape shape in shapes)
                {
                    shape.midPoint = midGPoint;
                }
                this.DrawMiddleLine(grp);
            }
            
        }

        private void startBtn_Click(object sender, EventArgs e)
        {
            if (gCodeList.Items.Count == 0)
                return;
            gCodeList.SelectedIndex = 0;
            for (int i = 0; i < gCodeList.Items.Count; i++)
            {
                gCodeList.SelectedIndex = i;
            }
        }

        private void drawPnl_Paint(object sender, PaintEventArgs e)
        {
            Graphics grp = e.Graphics;
            this.DrawMiddleLine(grp);
        }

        private void drawPnl_SizeChanged(object sender, EventArgs e)
        {
            Graphics grp = this.drawPnl.CreateGraphics();
            this.DrawMiddleLine(grp);
            foreach (Shape shape in shapes)
            {
                shape.myFrame = this.drawPnl.ClientRectangle;
                shape.Draw(grp);
            }
        }

        private void DrawMiddleLine(Graphics grp)
        {
            grp.Clear(this.drawPnl.BackColor);
            Pen pen = new Pen(Color.Yellow, 1.0f);
            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDot;

            Rectangle rect = this.drawPnl.ClientRectangle;

            grp.DrawLine(pen, 0, rect.Height / 2, rect.Width, rect.Height / 2);
            grp.DrawLine(pen, rect.Width / 2, 0, rect.Width / 2, rect.Height);
            Foam foam = new Foam(rect);
            foam.Draw(grp);
            CutPoint cutPoint = new CutPoint(point, rect);
            cutPoint.Draw(grp);
            foreach (Shape shape in shapes)
            {
                shape.myFrame = this.drawPnl.ClientRectangle;
                shape.Draw(grp);
            }
        }
    }
}


