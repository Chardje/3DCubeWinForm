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
        private const double raz = 2000;
        private const double scale = 5;
        private readonly WireModel tetrahedron = FigureFactory.NewTetrahedron(
            new Vector(-raz / 2, +raz * Math.Sqrt(6) / 12, dalinost - raz * Math.Sqrt(3) / 6),
            new Vector(raz / 2, +raz * Math.Sqrt(6) / 12, dalinost - raz * Math.Sqrt(3) / 6),
            new Vector(0, +raz * Math.Sqrt(6) / 12, dalinost + raz * Math.Sqrt(3) / 3),
            new Vector(0, -raz * Math.Sqrt(6) / 4, dalinost));

        private Matrix current = Matrix.I;
        private Matrix increment = Matrix.Move(new Vector(0, 0, +dalinost))  * Matrix.RotateY(0.05) * Matrix.Move(new Vector(0, 0, -dalinost));

        public Form1()
        {
            InitializeComponent();
            ResizeRedraw = true;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.FillRectangle(
                brush: Brushes.Black,
                rect: ClientRectangle);
            int offsetX = ClientRectangle.Width / 2;
            int offsetY = ClientRectangle.Height / 2;
            for (int i = 0; i < tetrahedron.Count; i++)
            {
                Edge edge = tetrahedron[i];
                Vector p0 = (current * edge[0]).Project(ze);
                Vector p1 = (current * edge[1]).Project(ze);
                g.DrawLine(Pens.Red, offsetX + (float) (scale * p0.X), offsetY + (float) (scale * p0.Y), offsetX + (float) (scale * p1.X), offsetY + (float) (scale * p1.Y));
            }
        }

        private void timerAnimation_Tick(object sender, EventArgs e)
        {
            current = current * increment;
            Invalidate();
        }
    }
}
