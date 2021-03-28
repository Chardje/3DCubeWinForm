using System;
using System.Collections.Generic;
using System.Text;

namespace _3DCubeWinForm
{
    /// <summary>
    /// Матрица взаимодействием с об'єктом
    /// </summary>
    public class Matrix
    {
        public static readonly Matrix I = new Matrix(MakeIArray());
        private readonly double[,] cords;

        private Matrix(double[,] cords)
        {
            this.cords = cords;
        }
        /// <summary>
        /// соединение матриц
        /// </summary>
        /// <param name="a">матрица 1</param>
        /// <param name="b">матрица2</param>
        /// <returns>произведение матриц</returns>
        public static Matrix operator *(Matrix a, Matrix b)
        {
            double[,] ac = a.cords;
            double[,] bc = b.cords;
            double[,] result = new double[4, 4];
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    double s = 0;
                    for (int k = 0; k < 4; k++)
                    {
                        s += ac[i, k] * bc[k, j];
                    }
                    result[i, j] = s;
                }
            }
            return new Matrix(result);
        }
        /// <summary>
        /// приобразование вектора через матрицу какогото типа
        /// </summary>
        /// <param name="a">матрица</param>
        /// <param name="b">вектор</param>
        /// <returns>переобразований вектор</returns>
        public static Vector operator *(Matrix a, Vector b)
        {
            double[,] ac = a.cords;
            double[] bc = b.cords;
            double[] result = new double[4];
            for (int i = 0; i < 4; i++)
            {
                double s = 0;
                for (int k = 0; k < 4; k++)
                {
                    s += ac[i, k] * bc[k];
                }
                result[i] = s;
            }
            return new Vector(result);
        }
        /// <summary>
        /// Нуливая матрица(ничего не делает)
        /// </summary>
        /// <returns></returns>
        private static double[,] MakeIArray()
        {
            double[,] result = new double[4, 4];
            result[0, 0] = 1;
            result[1, 1] = 1;
            result[2, 2] = 1;
            result[3, 3] = 1;
            return result;
        }
        /// <summary>
        /// поворот по оси z
        /// </summary>
        /// <param name="angle">угол поворота в радианах</param>
        /// <returns>возвращает матрицу по оси z</returns>
        public static Matrix RotateZ(double angle)
        {
            double[,] array = MakeIArray();
            double sin = Math.Sin(angle);
            double cos = Math.Cos(angle);
            array[0, 0] = cos;
            array[0, 1] = -sin;
            array[1, 0] = sin;
            array[1, 1] = cos;
            return new Matrix(array);
        }
        /// <summary>
        /// поворот по оси у
        /// </summary>
        /// <param name="angle">угол поворота в радианах</param>
        /// <returns>возвращает матрицу по оси у</returns>
        public static Matrix RotateY(double angle)
        {
            double[,] array = MakeIArray();
            double sin = Math.Sin(angle);
            double cos = Math.Cos(angle);
            array[0, 0] = cos;
            array[0, 2] = sin;
            array[2, 0] = -sin;
            array[2, 2] = cos;
            return new Matrix(array);
        }
        /// <summary>
        /// поворот по оси х
        /// </summary>
        /// <param name="angle">угол поворота в радианах</param>
        /// <returns>возвращает матрицу по оси х</returns>
        public static Matrix RotateX(double angle)
        {
            double[,] array = MakeIArray();
            double sin = Math.Sin(angle);
            double cos = Math.Cos(angle);
            array[1, 1] = cos;
            array[1, 2] = -sin;
            array[2, 1] = sin;
            array[2, 2] = cos;
            return new Matrix(array);
        }
        /// <summary>
        /// матрица перемищения
        /// </summary>
        /// <param name="point">место куда переместить</param>
        /// <returns>возвращает матрицу перемищения</returns>
        public static Matrix Move(Vector point)
        {
            double[,] array = MakeIArray();
            array[0, 3] = point.X;
            array[1, 3] = point.Y;
            array[2, 3] = point.Z;
            return new Matrix(array);
        }
    }

    ///<summary>
    ///ето точка в 3д
    ///</summary>
    public class Vector
    {
        ///создание масива в котором содержатся координати х у z 
        internal readonly double[] cords;
        /// <summary>
        /// запись в кооднинати координати из вне
        /// </summary>
        /// <param name="cords">масив с координатами</param>
        internal Vector(double[] cords)
        {
            this.cords = cords;
        }
        ///<summary>
        ///при создании вектора задаются координати
        ///</summary>
        public Vector(double x, double y, double z)
        {
            cords = new double[] { x, y, z, 1, };
        }
        ///коротко гет сет для всех координат
        public double X => cords[0];
        public double Y => cords[1];
        public double Z => cords[2];
        ///забирание конкретного елемента из масива
        public double this[int index]
        {
            get
            {
                return cords[index];
            }
        }

        ///проецирование на екран
        public Vector Project(double ze)
        {
            return new Vector(X * ze / Z, Y * ze / Z, ze);
        }

        /// <summary>
        /// векторное произведение
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Vector operator *(Vector a,Vector b)
        {
            double x = a.Y * b.Z - a.Z * b.Y;
            double y = a.Z * b.X - a.X * b.Z;
            double z = a.X * b.Y - a.Y * b.X;
            return new Vector(x, y, z);
        }

        /// <summary>
        /// Деление вектора на число
        /// </summary>
        /// <param name="a">Вектор</param>
        /// <param name="b">Число</param>
        /// <returns></returns>
        public static Vector operator /(Vector a, double b)
        {
            return  new Vector(a.X/b,a.Y/b,a.Z/b);
        }

        /// <summary>
        /// Длина вектора
        /// </summary>
        /// <returns></returns>
        public double LenVec()
        {
            return Math.Sqrt(X * X + Y * Y + Z * Z);
        }

        /// <summary>
        /// Нормирование
        /// </summary>
        /// <returns></returns>
        public Vector Norm ()
        {
            return this/LenVec(); 
        }

        /// <summary>
        /// скалярное произведение
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static double operator ^ (Vector a,Vector b)
        {
            return a.X * b.X + a.Y * b.Y + a.Z * b.Z;
        }
    }

    /// <summary>
    /// создание рьобер
    /// </summary>
    public class Edge
    {
        ///создание рьобер
        private readonly Vector[] points = new Vector[2];
        ///запись рьобер
        public Edge(Vector a, Vector b)
        {
            points[0] = a;
            points[1] = b;
        }
        ///возвращение вершины от рьобер
        public Vector this[int index]
        {
            get
            {
                return points[index];
            }
        }
        public int Count => 2;
    }

    /// <summary>
    /// создание полигона
    /// </summary>
    public class Poligon
    {
        private readonly Vector[] points;
        public Poligon(Vector [] points)
        {
            if (points.Length < 3)
            {
                throw new ArgumentException($"points.Length = {points.Length}, expected more or equal 3");
            }
            this.points = points;
        }
        public Vector this[int index]
        {
            get
            {
                return points[index];
            }
        }
        public int Count => points.Length;
    }

    /// <summary>
    /// создание проволочной модели
    /// </summary>
    public class WireModel
    {
        //создание листа
        private readonly List<Edge> edges = new List<Edge>();

        public WireModel()
        {
            // nothing
        }
        /// <summary>
        /// добовление в лист новое ребро
        /// </summary>
        /// <param name="edge"></param>
        public void AddEdge(Edge edge)
        {
            edges.Add(edge);
        }
        /// <summary>
        /// очищение масива
        /// </summary>
        public void Clear()
        {
            edges.Clear();
        }

        public int Count => edges.Count;

        public Edge this[int index]
        {
            get
            {
                return edges[index];
            }
        }
    }
}
