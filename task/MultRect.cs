using System;
using System.Collections.Generic;
using System.Drawing;

namespace task
{
    public class MultRect
    {
        private PointF[] Points;

        private List<float> RandomPointsX = new List<float>();
        private List<float> RandomPointsY = new List<float>();

        private int Count;
        private float U;
        private SolidBrush Solid;

        public MultRect(int count, SolidBrush solid, Random rand)
        {
            Count = count;
            Solid = solid;

            const int size = 50;

            Points = new PointF[count];

            for (var j = 0; j < count; j++)
            {
                RandomPointsX.Add(rand.NextDouble() > 0.5 ? rand.Next() % size : -(rand.Next() % size));
                RandomPointsY.Add(rand.NextDouble() > 0.5 ? rand.Next() % size : -(rand.Next() % size));
            }
        }

        public void Rotate(float x, float y, float length, int countRect)
        {
            var du = 3.1415f / countRect;

            for (var j = 0; j < Count; j++)
            {
                Points[j] = new PointF(
                    (float)(x + length * Math.Sin(U) + RandomPointsX[j]),
                    (float)(y - length * Math.Cos(U) + RandomPointsY[j]));
            }

            U += du;
        }

        public void Rotate(float x, float y, float length, int countRect, float del)
        {
            var du = 3.1415f / countRect;

            for (var j = 0; j < Count; j++)
            {
                Points[j] = new PointF(
                    (float)(x + length * Math.Sin(U) + RandomPointsX[j] * del),
                    (float)(y - length * Math.Cos(U) + RandomPointsY[j] * del));
            }

            U += du;
        }

        public PointF[] GetPoints() => Points;
        
        public SolidBrush GetSolid() => Solid;

        public void SetU(float u)
        {
           U = u;
        }
    }
}
