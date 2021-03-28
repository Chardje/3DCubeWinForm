using System;
using System.Collections.Generic;
using System.Text;

namespace _3DCubeWinForm
{
    public class Figure
    {
        public Ideal Ideal { get; }
        public IReadOnlyList<Vector> Points { get; }

        public Figure(Ideal ideal, params Vector[] points)
        {
            Ideal = ideal;
            Points = points;
        }

        public static Figure operator *(Matrix m, Figure f)
        {
            int n = f.Points.Count;
            Vector[] newPoints = new Vector[n];
            for (int i = 0; i < n; i += 1)
            {
                newPoints[i] = m * f.Points[i];
            }
            return new Figure(f.Ideal, newPoints);
        }
    }
}
