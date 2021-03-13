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
        private readonly WireModel tetrahedron = FigureFactory.NewTetrahedron(new Vector(-500, -(1000 * Math.Sqrt(6) / 3),1000 -(1000*Math.Sqrt(6)/12)), new Vector(0, 1000 * Math.Sqrt(6) / 6, 1000-(1000 * Math.Sqrt(6) / 12)), new Vector(500, -(1000 * Math.Sqrt(6) / 12),1000 -(1000 * Math.Sqrt(6) / 12)+1000), new Vector(0, 0,1000 -(1000 * Math.Sqrt(6) / 3)));
        private Matrix current = Matrix.I;
        private Matrix increment = Matrix.Move(new Vector(0, 0, 1000)) * Matrix.RotateX(0.05) * Matrix.Move(new Vector(0, 0, -1000));

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
                g.DrawLine(Pens.Red, offsetX + (float)p0.X, offsetY + (float)p0.Y, offsetX + (float)p1.X, offsetY + (float)p1.Y);
            }
        }

        private void timerAnimation_Tick(object sender, EventArgs e)
        {
            current = current * increment;
            Invalidate();
        }
    }
}
