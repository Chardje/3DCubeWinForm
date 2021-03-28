using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _3DCubeWinForm
{
    public partial class Form1 : Form
    {
        private const double ze = 50;
        private const double dalinost = 4000;
        private const double raz = 1000;
        private const double scale = 10;
        private readonly WireModel cubeCenter = FigureFactoryOld.NewCube(new Vector(0,0,dalinost),raz);
        private readonly WireModel octahedron = FigureFactoryOld.NewOctahedron(
            new Vector(0, +raz+200, dalinost),
            new Vector(0, +200, dalinost - raz),
            new Vector(raz, +200, dalinost),
            new Vector(0, +200, dalinost + raz),
            new Vector(-raz, +200, dalinost),
            new Vector(0, -raz + 200, dalinost));
        private readonly WireModel tetrahedron = FigureFactoryOld.NewTetrahedron(
            new Vector(-raz / 2, +raz * Math.Sqrt(6) / 12, dalinost - raz * Math.Sqrt(3) / 6),
            new Vector(raz / 2, +raz * Math.Sqrt(6) / 12, dalinost - raz * Math.Sqrt(3) / 6),
            new Vector(0, +raz * Math.Sqrt(6) / 12, dalinost + raz * Math.Sqrt(3) / 3),
            new Vector(0, -raz * Math.Sqrt(6) / 4, dalinost));
        private readonly WireModel tetrahedronCenter = FigureFactoryOld.NewTetrahedron(new Vector(0,0,dalinost),raz);
        private readonly WireModel octahedronCenter = FigureFactoryOld.NewOctahedron(new Vector(0, 0, dalinost), raz);

        private readonly WireModel model;

        private Matrix current = Matrix.I;
        private Matrix increment = Matrix.I;
        private Point? StartLocation = null;

        public Form1()
        {
            InitializeComponent();
            ResizeRedraw = true;
            model = octahedronCenter;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Matrix m = current * increment;
            Graphics g = e.Graphics;
            g.FillRectangle(
                brush: Brushes.Black,
                rect: ClientRectangle);
            int offsetX = ClientRectangle.Width / 2;
            int offsetY = ClientRectangle.Height / 2;
            for (int i = 0; i < model.Count; i++)
            {
                Edge edge = model[i];
                Vector p0 = (m * edge[0]).Project(ze);
                Vector p1 = (m * edge[1]).Project(ze);
                g.DrawLine(Pens.Red, offsetX + (float) (scale * p0.X), offsetY + (float) (scale * p0.Y), offsetX + (float) (scale * p1.X), offsetY + (float) (scale * p1.Y));
            }
        }

        private void timerAnimation_Tick(object sender, EventArgs e)
        {
            current = current * increment;
            Invalidate();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            StartLocation = e.Location;
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            StartLocation = null;
            current = current * increment;
            increment = Matrix.I;
            Invalidate();
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (StartLocation != null)
            {
                Point startLocation = (Point)StartLocation;
                Point endLocation = e.Location;
                int dx = endLocation.X - startLocation.X;
                int dy = endLocation.Y - startLocation.Y;
                increment = Matrix.Move(new Vector(0, 0, +dalinost)) * Matrix.RotateX((+dy) / 100d) * Matrix.RotateY((-dx) / 100d) * Matrix.Move(new Vector(0, 0, -dalinost));
                Invalidate();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
