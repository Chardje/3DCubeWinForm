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
        private readonly WireModel cube = FigureFactory.NewCube(new Vector(-500, -500, 500), new Vector(500, 500, 1500));
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
            for (int i = 0; i < cube.Count; i++)
            {
                Edge edge = cube[i];
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
