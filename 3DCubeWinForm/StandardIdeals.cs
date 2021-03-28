using System;
using System.Collections.Generic;
using System.Text;

namespace _3DCubeWinForm
{
    public static class StandardIdeals
    {
        public static Ideal Cube { get; } = new Ideal()
            .WithFace(0, 1, 2, 3)
            .WithFace(0, 5, 4, 1)
            .WithFace(1, 4, 7, 2)
            .WithFace(3, 2, 7, 6)
            .WithFace(0, 3, 6, 5)
            .WithFace(4, 5, 6, 7);
        public static Ideal Tetrahedron { get; } = new Ideal()
            .WithFace(0, 1, 2)
            .WithFace(0, 3, 1)
            .WithFace(0, 2, 3)
            .WithFace(1, 3, 2);
        public static Ideal Octahedron { get; } = new Ideal()
            .WithFace(0, 1, 2)
            .WithFace(0, 4, 1)
            .WithFace(0, 3, 4)
            .WithFace(0, 2, 3)
            .WithFace(5, 1, 4)
            .WithFace(5, 4, 3)
            .WithFace(5, 3, 2)
            .WithFace(5, 2, 1);
    }
}
