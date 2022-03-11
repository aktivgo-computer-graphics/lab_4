using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace task
{
    public partial class MainForm : Form
    {
        private readonly Graphics Graph;
        private readonly Pen MyPen;
        private Timer timer;
        private bool SizeUp = true;
        private int L = 200;


        private const int Count2 = 3;
        private const int CountRect3 = 4;
        private const int CountRectOnScreen = 4;
        private const int Length = 120;
        private bool First = true, BigOrLess = true, FreeX = true, FreeY = true;
        private float U, Del = 1, X, Y;

        private List<MultRect> Rects;
        private const float T = 0.1f;

        public MainForm()
        {
            InitializeComponent();
            Graph = CreateGraphics();
            Graph.SmoothingMode = SmoothingMode.HighQuality;
            MyPen = new Pen(Color.Black);
            timer = new Timer();
        }

        private void CreateTaskTimer(int taskNumber)
        {
            timer = new Timer();
            timer.Interval = 100;

            switch (taskNumber)
            {
                case 5:
                    timer.Tick += timer_Tick_5;
                    break;
                case 8:
                    timer.Tick += timer_Tick_8;
                    break;
            }
        }

        private void timer_Tick_5(object sender, EventArgs e)
        {
            Graph.Clear(Color.White);

            if (First)
            {
                First = !First;
                Rects = new List<MultRect>();
                var rand = new Random();

                for (var i = 0; i < CountRect3; i++)
                {
                    Rects.Add(new MultRect(Count2, new SolidBrush(Color.FromArgb(rand.Next() % 255, rand.Next() % 255, (rand.Next() % 255))), rand));
                }
            }

            U += 0.05f;
            if (U > 2 * Math.PI) U = 0;

            if (BigOrLess) Del += 0.1f;
            else Del -= 0.1f;

            if (Del > 1.5f || Del < 0.5f) BigOrLess = !BigOrLess;

            for (var i = 0; i < CountRect3; i++)
            {
                Rects[i].SetU(U);
            }

            const int speed = 10;

            if (FreeX) X += speed;
            else X -= speed;

            if (FreeY) Y += speed;
            else Y -= speed;

            if (X + (6 / 4) * Length >= ClientSize.Width) FreeX = false;
            if (X - (6 / 4) * Length <= 0) FreeX = true;
            if (Y + (6 / 4) * Length >= ClientSize.Height) FreeY = false;
            if (Y - (6 / 4) * Length <= 0) FreeY = true;


            for (var i = 0; i < CountRect3; i++)
            {
                for (var j = 0; j < 2 * CountRectOnScreen; j++)
                {
                    Rects[i].Rotate(X, Y, Length, CountRectOnScreen, Del);
                    Graph.FillPolygon(Rects[i].GetSolid(), Rects[i].GetPoints());
                }
            }
        }

        private void timer_Tick_8(object sender, EventArgs e)
        {
            L = SizeUp ? L + 20 : L - 20;
            
            PaintAperture(L);

            if (L <= 0)
            {
                SizeUp = true;
            }
            else if (L >= 220)
            {
                SizeUp = false;
            }
        }
        
        private void button_Click(object sender, EventArgs e)
        {
            Graph.Clear(Color.White);
            timer.Stop();
            
            var x0 = ClientSize.Width / 2;
            var y0 = ClientSize.Height / 2;

            switch (taskSwitch.SelectedIndex)
            {
                case 0:
                    PaintEllipse(4, 2, x0, y0);
                    break;
                case 1:
                    PaintRectangle(4, 2, x0, y0);
                    break;
                case 2:
                    PaintSnowflake(0, 0, 15, 2);
                    break;
                case 3:
                    PaintRectangles(x0, y0, 120, 40, 4, 3);
                    break;
                case 4:
                    CreateTaskTimer(5);
                    timer.Start();
                    break;
                case 5:
                    CreateTaskTimer(8);
                    timer.Start();
                    break;
                case 6:
                    PaintRecursiveSquare(10, x0, y0);
                    break;
                case 7:
                    PaintCornerA(7, 20);
                    break;
                case 8:
                    PaintCornerB(20, 16);
                    break;
                case 9:
                    PaintCornerC(20, 8, 1);
                    break;
                case 10:
                    PaintCornerD(20, 8);
                    break;
            }
        }
        
        private void PaintEllipse(float a, float b, int x0, int y0)
        {
            const int k = 20;

            for (var i = 1; i <= 24; i ++)
            {
                var x = (int)(x0 - a * k * i / 2);
                var y = (int)(y0 - b * k * i / 2);

                Graph.DrawEllipse(MyPen, new Rectangle(x, y, (int)(a * i * k), (int)(b * i * k)));
            }
        }
        
        private void PaintRectangle(int a, int b, int x0, int y0)
        {
            const int k = 20;
            
            for (var i = 1; i <= 20; i ++)
            {
                var x = x0 - a * k * i / 2;
                var y = y0 - b * k * i / 2;

                Graph.DrawRectangle(MyPen, new Rectangle(x, y, a * i * k, b * i * k));
            }
        }
        
        private void PaintSnowflake(double centerX, double centerY, double radius, int nesting)
        {
            if (nesting < 1)
            {
                return;
            }

            var startPoint = new PointF((float)centerX, (float)centerY);
            const double angle = Math.PI / 6;

            for (double currAngle = 0; currAngle < 2 * Math.PI; currAngle += angle)
            {
                var endPoint = RotateLine(startPoint, radius, currAngle);
                Graph.DrawLine(MyPen, ScreenCoords(startPoint.X, startPoint.Y), ScreenCoords(endPoint.X, endPoint.Y));
                PaintSnowflake(endPoint.X, endPoint.Y, radius / 5, nesting - 1);
            }
        }
        
        private PointF ScreenCoords(double x, double y)
        {
            const int k = 20;
            return new PointF((float)(ClientSize.Width / 2 + x * k), (float)(ClientSize.Height / 2 - y * k));
        }

        private static PointF RotateLine(PointF startPoint, double radius, double angle)
        {
            return new PointF((float)(radius * Math.Cos(angle) + startPoint.X),
                (float)(radius * Math.Sin(angle)) + startPoint.Y);
        }
        
        private void PaintRectangles(float x, float y, float length, int count, int countRectOnScreen, int countRect)
        {
            var rectangles = new List<MultRect>();
            var rand = new Random();

            for (var i = 0; i < countRect; i++)
            {
                rectangles.Add(new MultRect(count, new SolidBrush(Color.FromArgb(rand.Next() % 255, rand.Next() % 255, (rand.Next() % 255))), rand));
            }

            for (var i = 0; i < countRect; i++)
            {
                for (var j = 0; j < 2 * countRectOnScreen; j++)
                {
                    rectangles[i].Rotate(x, y, length, countRectOnScreen);
                    Graph.FillPolygon(rectangles[i].GetSolid(), rectangles[i].GetPoints());
                }
            }
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
        
        private void PaintFigure(List<Point> points)
        {
            Graph.DrawPolygon(MyPen, points.ToArray());
        }

        private void PaintRecursiveSquare(int k, int x0, int y0)
        {
            const int l = 400;
            const int d = 10;

            var points = CreatePolygon(4, l, x0, y0, 1, 1, Math.PI / 4);
            PaintFigure(points);
            
            for (var i = 0; i < k; i++)
            {
                points = CreateInnerSquare(points, d);
                PaintFigure(points);
            }
        }
        
        private static List<Point> CreateInnerSquare(IReadOnlyList<Point> points, int d)
        {
            var resultPoints = new List<Point>();
            
            Point point;
            for (var i = 0; i < points.Count - 1; i++)
            {
                point = new Point((points[i + 1].X - points[i].X) / d - points[i + 1].X, (points[i + 1].Y - points[i].Y) / d - points[i + 1].Y);
                resultPoints.Add(point);
            }
            
            point = new Point((points[0].X - points[points.Count - 1].X) / d - points[0].X, (points[0].Y - points[points.Count - 1].Y) / d - points[0].Y);
            resultPoints.Add(point);
            
            return resultPoints;
        }

        private void PaintAperture(int l)
        {
            Graph.Clear(Color.White);

            const int n = 6;
            
            var x0 = ClientSize.Width / 2;
            var y0 = ClientSize.Height / 2;

            var r = l / (2 * Math.Sin(Math.PI / n));
            var pointsSmall = CreatePolygon(n, l, x0, y0, 1, 1, Math.PI / 6);
            PaintFigure(pointsSmall);
            
            var pointsBig = CreatePolygon(n, 270, x0, y0, 1, 1, 0);
            PaintFigure(pointsBig);

            for (var i = 0; i < n; i++)
            {
                Graph.DrawLine(MyPen, pointsSmall[i], pointsBig[i]);
            }
        }
        
        private void PaintCornerA(int cornerCount, double radius)
        {
            var angle = 2 * Math.PI / cornerCount;
            var betweenAngle = angle / 6;
            angle -= betweenAngle;
            var startPoint = new PointF(0, 0);
            var endPoint = new PointF((float)radius, 0);

            for (var currAngle = angle; cornerCount-- > 0; currAngle += angle + betweenAngle)
            {
                Graph.DrawLine(MyPen, ScreenCoords(startPoint.X, startPoint.Y), ScreenCoords(endPoint.X, endPoint.Y));
                var firstLinePoints = GetLinePoints(startPoint, endPoint, T);
                endPoint = RotateLine(startPoint, radius, currAngle);
                Graph.DrawLine(MyPen, ScreenCoords(startPoint.X, startPoint.Y), ScreenCoords(endPoint.X, endPoint.Y));
                var secondLinePoints = GetLinePoints(startPoint, endPoint, T);
                DrawAngleNet(firstLinePoints, secondLinePoints);

                endPoint = RotateLine(startPoint, radius, currAngle + betweenAngle);
            }
        }
        
        private static PointF[] GetLinePoints(PointF startPoint, PointF endPoint, float t)
        {
            var points = new PointF[(int)Math.Round(1 / t)];

            for (var i = 0; i < points.Length; ++i)
            {
                points[i] = new PointF((endPoint.X - startPoint.X) * t * (i + 1) + startPoint.X,
                    (endPoint.Y - startPoint.Y) * t * (i + 1) + startPoint.Y);
            }

            return points;
        }
        
        private void DrawAngleNet(IReadOnlyList<PointF> firstLinePoints, IReadOnlyList<PointF> secondLinePoints)
        {
            if (firstLinePoints?.Count != secondLinePoints?.Count)
            {
                throw new ArgumentException("Размеры массивов должны совпадать");
            }

            for (var i = 0; i < firstLinePoints.Count; ++i)
            {
                Graph.DrawLine(MyPen, ScreenCoords(firstLinePoints[i].X, firstLinePoints[i].Y),
                    ScreenCoords(secondLinePoints[firstLinePoints.Count - i - 1].X, secondLinePoints[firstLinePoints.Count - i - 1].Y));
            }
        }

        private void PaintCornerB(double radius, int linesCount)
        {
            const float t = 0.25f;
            var angle = 2 * Math.PI / linesCount;
            var startPoint = new PointF(0, 0);
            var endPoint = new PointF((float)radius, 0);

            for (var currAngle = 3 * angle; linesCount-- > 0; currAngle += 3 * angle)
            {
                var firstLinePoints = GetLinePoints(startPoint, endPoint, t);
                firstLinePoints = firstLinePoints.Take(firstLinePoints.Length - 1).ToArray();
                Graph.DrawLine(MyPen, ScreenCoords(startPoint.X, startPoint.Y), ScreenCoords(endPoint.X, endPoint.Y));
                endPoint = RotateLine(startPoint, radius, currAngle);
                var secondLinePoints = GetLinePoints(startPoint, endPoint, t);
                secondLinePoints = secondLinePoints.Take(secondLinePoints.Length - 1).ToArray();
                DrawAngleNet(firstLinePoints, secondLinePoints);
            }
        }
        
        private void PaintCornerC(double radius, int angleCount, double angleLenDecrease)
        {
            var angle = 2 * Math.PI / angleCount;
            var angleLength = radius * (angleCount / 2) * Math.Sqrt(2.965) / (angleCount + 1) / angleLenDecrease;
            var startPoint = new PointF((float)radius, 0);

            var i = 0;
            for (var currAngle = angle; angleCount-- > 0; currAngle += angle)
            {
                var endPoint = RotateLine(startPoint, angleLength, Math.PI - angle + currAngle / 2 + angle / 2 * i);
                Graph.DrawLine(MyPen, ScreenCoords(startPoint.X, startPoint.Y), ScreenCoords(endPoint.X, endPoint.Y));
                var firstLinePoints = GetLinePoints(startPoint, endPoint, T);
                endPoint = RotateLine(startPoint, angleLength, Math.PI + currAngle / 2 + angle / 2 * i);
                Graph.DrawLine(MyPen, ScreenCoords(startPoint.X, startPoint.Y), ScreenCoords(endPoint.X, endPoint.Y));
                var secondLinePoints = GetLinePoints(startPoint, endPoint, T);
                DrawAngleNet(firstLinePoints, secondLinePoints);
                startPoint = RotateLine(new PointF(0, 0), radius, currAngle);
                i++;
            }
        }
        
        private void PaintCornerD(double radius, int angleCount)
        {
            PaintCornerC(radius, angleCount, Math.Sqrt(2));

            var angle = 2 * Math.PI / angleCount;
            var addAngle = Math.PI / 38.5;
            var startPoint = new PointF((float)(radius / 10), 0);
            var angleLength = radius * (angleCount / 2) * Math.Sqrt(3) / (angleCount + 1) / Math.Sqrt(2) - radius / 10 + 0.1;
            var endPoint = RotateLine(startPoint, angleLength, -angle / 2 - addAngle);

            for (var currAngle = -angle / 2; angleCount-- > 0; currAngle += angle)
            {
                var firstLinePoints = GetLinePoints(startPoint, endPoint, T);
                Graph.DrawLine(MyPen, ScreenCoords(startPoint.X, startPoint.Y), ScreenCoords(endPoint.X, endPoint.Y));
                endPoint = RotateLine(startPoint, angleLength, currAngle + angle + addAngle);
                var secondLinePoints = GetLinePoints(startPoint, endPoint, T);
                Graph.DrawLine(MyPen, ScreenCoords(startPoint.X, startPoint.Y), ScreenCoords(endPoint.X, endPoint.Y));
                DrawAngleNet(firstLinePoints, secondLinePoints);
                startPoint = RotateLine(new PointF(0, 0), radius / 10, currAngle + angle * Math.Sqrt(2.25));
                endPoint = RotateLine(startPoint, angleLength, currAngle + angle - addAngle);
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            MyPen.Dispose();
            Graph.Dispose();
        }
    }
}