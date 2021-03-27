using _3DCubeWinForm;
using System;
using System.Collections.Generic;
using Xunit;

namespace _3DCubeWinFormTest
{
    public class IdealTest
    {
        Ideal sut = new Ideal();

        [Fact]
        public void TestCanonicalize34512()
        {
            int[] indices = { 3, 4, 5, 1, 2, };
            Ideal.Canonicalize(indices);
            Assert.Equal(new int[] { 1, 2, 3, 4, 5, }, indices);
        }
        [Fact]
        public void TestCanonicalize12345()
        {
            int[] indices = { 1, 2, 3, 4, 5, };
            Ideal.Canonicalize(indices);
            Assert.Equal(new int[] { 1, 2, 3, 4, 5, }, indices);
        }
        [Fact]
        public void TestCanonicalize54321()
        {
            int[] indices = { 5, 4, 3, 2, 1, };
            Ideal.Canonicalize(indices);
            Assert.Equal(new int[] { 1, 5, 4, 3, 2, }, indices);
        }
        [Fact]
        public void TestCreateCube()
        {
            sut
                .WithFace(0, 1, 2, 3)
                .WithFace(0, 5, 4, 1)
                .WithFace(1, 4, 7, 2)
                .WithFace(3, 2, 7, 6)
                .WithFace(0, 3, 6, 5)
                .WithFace(4, 5, 6, 7);
            HashSet<FaceX> expectedFaces = new HashSet<FaceX>();
            expectedFaces.Add(new FaceX(0, 1, 2, 3));
            expectedFaces.Add(new FaceX(0, 5, 4, 1));
            expectedFaces.Add(new FaceX(1, 4, 7, 2));
            expectedFaces.Add(new FaceX(3, 2, 7, 6));
            expectedFaces.Add(new FaceX(0, 3, 6, 5));
            expectedFaces.Add(new FaceX(4, 5, 6, 7));
            Assert.True(expectedFaces.SetEquals(sut.Faces), string.Join(',', sut.Faces));
            HashSet<EdgeX> expectedEdges = new HashSet<EdgeX>();
            expectedEdges.Add(new EdgeX(0, 1));
            expectedEdges.Add(new EdgeX(0, 3));
            expectedEdges.Add(new EdgeX(0, 5));
            expectedEdges.Add(new EdgeX(1, 2));
            expectedEdges.Add(new EdgeX(1, 4));
            expectedEdges.Add(new EdgeX(2, 3));
            expectedEdges.Add(new EdgeX(2, 7));
            expectedEdges.Add(new EdgeX(3, 6));
            expectedEdges.Add(new EdgeX(4, 5));
            expectedEdges.Add(new EdgeX(4, 7));
            expectedEdges.Add(new EdgeX(5, 6));
            expectedEdges.Add(new EdgeX(6, 7));
            Assert.True(expectedEdges.SetEquals(sut.Edges), string.Join(',', sut.Edges));
        }
    }
}
