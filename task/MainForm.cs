using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace task
{
    public partial class MainForm : Form
    {
        private Graphics Graph;
        private Pen MyPen;
        
        public MainForm()
        {
            InitializeComponent();
            Graph = CreateGraphics();
            Graph.SmoothingMode = SmoothingMode.HighQuality;
            MyPen = new Pen(Color.Black);
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            Graph.Clear(Color.White);
            
            var x0 = ClientSize.Width / 2;
            var y0 = ClientSize.Height / 2;
            
            PaintRecursiveSquare(10, x0, y0);
        }
        
        private static List<Point> CreatePolygon(int n, int l, int x0, int y0, int kX, int kY)
        {
            var points = new List<Point>();
            
            var r = l / (2 * Math.Sin(Math.PI / n));
            for (var angle = 0.0; angle <= 2 * Math.PI; angle += 2 * Math.PI / n)
            {
                var x = (int)(r * Math.Cos(angle)) * kX;
                var y = (int)(r * Math.Sin(angle)) * kY;
                points.Add(new Point(x0 +x,  y0 - y));
            }

            return points;
        }
        
        private static List<Point> CreatePolygon(int n, int l, int x0, int y0, int kX, int kY, double startAngle)
        {
            var points = new List<Point>();
            
            var r = l / (2 * Math.Sin(Math.PI / n));
            var currentAngle = startAngle;
            var stepAngle = 2 * Math.PI / n;
            for (var i = 0; i < n; i++)
            {
                var x = (int)(r * Math.Cos(currentAngle)) * kX;
                var y = (int)(r * Math.Sin(currentAngle)) * kY;
                currentAngle += stepAngle;
                points.Add(new Point(x0 +x,  y0 - y));
            }

            return points;
        }
        
        private void PaintPolygon(int n, int l, int x0, int y0, int kX, int kY)
        {
            var points = CreatePolygon(n, l, x0, y0, kX, kY);
            Graph.DrawPolygon(MyPen, points.ToArray());
        }
        
        private void PaintPolygon(int n, int l, int x0, int y0, int kX, int kY, double startAngle)
        {
            var points = CreatePolygon(n, l, x0, y0, kX, kY, startAngle);
            Graph.DrawPolygon(MyPen, points.ToArray());
        }

        private void PaintRecursiveSquare(int k, int x0, int y0)
        {
            const int l = 400;
            const int d = 20;

            var points = CreatePolygon(4, l, x0, y0, 1, 1, Math.PI / 4);
            PaintPolygon(4, l, x0, y0, 1, 1, Math.PI / 4);

            var newPoints = new List<Point>(points);
            for (var i = 0; i < k - 1; i++)
            {
                newPoints
                var point = points[0];
                var p2 = new Point(points[1].X + d, points[1].Y);
                var p3 = new Point(points[2].X, points[2].Y - d);
                var p4 = new Point(points[3].X - d, points[3].Y);

                point.Y += d;
                    
                Graph.DrawPolygon(MyPen, points.ToArray());
            }
        }
    }
}