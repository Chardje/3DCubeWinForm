using System;

namespace _3DCubeWinForm
{
    /// <summary>
    /// создание проволочних моделей
    /// </summary>
    public static class FigureFactoryOld
    {
        #region Cube

        /// <summary>
        /// проволочная модель куба
        /// </summary>
        /// <param name="a">левий верхний ближний угол</param>
        /// <param name="b">правий нижний дальний угол</param>
        /// <returns></returns>
        public static WireModel NewCube(Vector a, Vector b)
        {
            WireModel cube = new WireModel();

            Vector a0 = a;
            Vector a1 = new Vector(a.X, b.Y, a.Z);
            Vector a2 = new Vector(b.X, b.Y, a.Z);
            Vector a3 = new Vector(b.X, a.Y, a.Z);

            Vector b0 = new Vector(a.X, a.Y, b.Z);
            Vector b1 = new Vector(a.X, b.Y, b.Z);
            Vector b2 = b;
            Vector b3 = new Vector(b.X, a.Y, b.Z);

            cube.AddEdge(new Edge(a0, b0));
            cube.AddEdge(new Edge(a1, b1));
            cube.AddEdge(new Edge(a2, b2));
            cube.AddEdge(new Edge(a3, b3));

            cube.AddEdge(new Edge(a0, a1));
            cube.AddEdge(new Edge(a1, a2));
            cube.AddEdge(new Edge(a2, a3));
            cube.AddEdge(new Edge(a3, a0));

            cube.AddEdge(new Edge(b0, b1));
            cube.AddEdge(new Edge(b1, b2));
            cube.AddEdge(new Edge(b2, b3));
            cube.AddEdge(new Edge(b3, b0));

            return cube;
        }

        /// <summary>
        /// Проволочная модель куба
        /// </summary>
        /// <param name="center">центр</param>
        /// <param name="length">длина ребра</param>
        /// <returns></returns>
        public static WireModel NewCube(Vector center,double length)
        {
            WireModel cube = new WireModel();

            Vector a = new Vector(center.X - length / 2, center.Y - length / 2, center.Z - length / 2);
            Vector b = new Vector(center.X + length / 2, center.Y + length / 2, center.Z + length / 2);

            return NewCube(a,b);
        }

        #endregion

        #region Tetrahedron

        /// <summary>
        /// Проволочная модель тетрайдера
        /// </summary>
        /// <param name="a">первая из нижних</param>
        /// <param name="b">вторая</param>
        /// <param name="c">третья</param>
        /// <param name="d">верхняя точка</param>
        /// <returns></returns>
        public static WireModel NewTetrahedron(Vector a, Vector b, Vector c, Vector d)
        {
            WireModel tetrahedron = new WireModel();

            tetrahedron.AddEdge(new Edge(a, b));
            tetrahedron.AddEdge(new Edge(a, c));
            tetrahedron.AddEdge(new Edge(a, d));

            tetrahedron.AddEdge(new Edge(c, b));
            tetrahedron.AddEdge(new Edge(c, d));

            tetrahedron.AddEdge(new Edge(b, d));

            return tetrahedron;
        }

        /// <summary>
        /// Проволочная модель тетрайдера
        /// </summary>
        /// <param name="center">центр</param>
        /// <param name="length">длина ребра</param>
        /// <returns></returns>
        public static WireModel NewTetrahedron(Vector center, double length)
        {
            WireModel tetrahedron = new WireModel();
            
            Vector a = new Vector(center.X - length / 2, center.Y + length * Math.Sqrt(6) / 12 , center.Z - length * Math.Sqrt(3) / 6);
            Vector b = new Vector(center.X + length / 2, center.Y + length * Math.Sqrt(6) / 12, center.Z - length * Math.Sqrt(3) / 6);
            Vector c = new Vector(center.X, center.Y + length * Math.Sqrt(6) / 12, center.Z + length * Math.Sqrt(3) / 3);
            Vector d = new Vector(center.X, center.Y - length * Math.Sqrt(6) / 4, center.Z);

            return NewTetrahedron(a,b,c,d);
        }

        #endregion

        #region Octahedron

        /// <summary>
        /// Создание октайдера
        /// </summary>
        /// <param name="points">масив из шести точек октайдера</param>
        /// <returns></returns>
        public static WireModel NewOctahedron(params Vector[] points)
        {
            if (points.Length != 6)
            {
                throw new ArgumentException($"points.Length = {points.Length}, expected exactly 6");
            }
            WireModel octahedron = new WireModel();
            for (int i = 1; i <= 4; i += 1)
            {
                octahedron.AddEdge(new Edge(points[i], points[0]));
                octahedron.AddEdge(new Edge(points[i], points[5]));
            }
            for (int i = 1; i < 4; i += 1)
            {
                octahedron.AddEdge(new Edge(points[i], points[i + 1]));
            }
            octahedron.AddEdge(new Edge(points[4], points[1]));
            return octahedron;
        }

        /// <summary>
        /// Проволочная модель октайдера
        /// </summary>
        /// <param name="center">центр</param>
        /// <param name="length">длина ребра</param>
        /// <returns></returns>
        public static WireModel NewOctahedron(Vector center, double length)
        {            
            WireModel octahedron = new WireModel();
                        
            return NewOctahedron(
            new Vector(center.X, center.Y + length, center.Z),
            new Vector(center.X, center.Y, center.Z - length),
            new Vector(center.X + length, center.Y, center.Z),
            new Vector(center.X, center.Y, center.Z + length),
            new Vector(center.X - length, center.Y, center.Z),
            new Vector(center.X, center.Y - length, center.Z));
        }

        #endregion
       
    }
}
