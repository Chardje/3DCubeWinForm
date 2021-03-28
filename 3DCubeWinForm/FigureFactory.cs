using System;
using System.Collections.Generic;
using System.Text;

namespace _3DCubeWinForm
{
    public static class FigureFactory
    {
        public static Figure NewCube(params Vector[] points)
        {
            if (points.Length != 8)
            {
                throw new ArgumentException($"points.Length = {points.Length}");
            }
            return new Figure(StandardIdeals.Cube, points);
        }

        public static Figure NewCube(Vector a, Vector b)
        {
            Vector p0 = a;
            Vector p1 = new Vector(b.X, a.Y, a.Z);
            Vector p2 = new Vector(b.X, b.Y, a.Z);
            Vector p3 = new Vector(a.X, b.Y, a.Z);

            Vector p4 = new Vector(b.X, a.Y, b.Z);
            Vector p5 = new Vector(a.X, a.Y, b.Z);
            Vector p6 = new Vector(a.X, b.Y, b.Z);
            Vector p7 = b;
            return NewCube(p0, p1, p2, p3, p4, p5, p6, p7);
        }

        public static Figure NewCube(Vector center, double length)
        {
            Vector a = new Vector(center.X - length / 2, center.Y - length / 2, center.Z - length / 2);
            Vector b = new Vector(center.X + length / 2, center.Y + length / 2, center.Z + length / 2);
            return NewCube(a, b);
        }

        public static Figure NewTetrahedron(params Vector[] points)
        {
            if (points.Length != 4)
            {
                throw new ArgumentException($"points.Length = {points.Length}");
            }
            return new Figure(StandardIdeals.Tetrahedron, points);
        }

        public static Figure NewTetrahedron(Vector a, Vector b)
        {
            Vector p0 = a;
            Vector p1 = new Vector(b.X, b.Y, a.Z);
            Vector p2 = new Vector(a.X, b.Y, b.Z);
            Vector p3 = new Vector(b.X, a.Y, b.Z);
            return NewTetrahedron(p0, p1, p2, p3);
        }

        public static Figure NewTetrahedron(Vector center, double length)
        {
            Vector a = new Vector(center.X - length / 2, center.Y - length / 2, center.Z - length / 2);
            Vector b = new Vector(center.X + length / 2, center.Y + length / 2, center.Z + length / 2);
            return NewTetrahedron(a, b);
        }

        public static Figure NewOctahedron(params Vector[] points)
        {
            if (points.Length != 6)
            {
                throw new ArgumentException($"points.Length = {points.Length}");
            }
            return new Figure(StandardIdeals.Octahedron, points);
        }
    }
}
