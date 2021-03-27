using System;
using System.Collections;
using System.Collections.Generic;

namespace _3DCubeWinForm
{
    public class Base
    {
        public IReadOnlyList<int> Indices { get; }

        internal Base(params int[] indices)
        {
            Ideal.Canonicalize(indices);
            Indices = indices;
        }

        public override bool Equals(object obj)
        {
            if (obj is Base)
            {
                return ((IStructuralEquatable)Indices).Equals(((Base)obj).Indices, EqualityComparer<int>.Default);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return ((IStructuralEquatable)Indices).GetHashCode(EqualityComparer<int>.Default);
        }

        public override string ToString()
        {
            return $"[{string.Join(',', Indices)}]";
        }
    }

    public class FaceX : Base
    {
        public FaceX(params int[] indices) : base(CheckLength(indices))
        {
            // ok
        }

        internal static int[] CheckLength(int[] indices)
        {
            if (indices.Length < 3)
            {
                throw new ArgumentException($"indices.Length = {indices.Length}; {string.Join(',', indices)}");
            }
            return indices;
        }
    }

    public class EdgeX : Base
    {
        public EdgeX(params int[] indices): base(CheckLength(indices))
        {
            // ok
        }
        internal static int[] CheckLength(int[] indices)
        {
            if (indices.Length != 2)
            {
                throw new ArgumentException($"indices.Length = {indices.Length}; {string.Join(',', indices)}");
            }
            return indices;
        }
    }

    public class Ideal
    {
        private readonly HashSet<FaceX> faces_ = new HashSet<FaceX>();
        private readonly HashSet<EdgeX> edges_ = new HashSet<EdgeX>();

        public IReadOnlyCollection<FaceX> Faces => faces_;
        public IReadOnlyCollection<EdgeX> Edges => edges_;

        /// <summary>
        /// Add new Face to the figure Ideal. Edges will be added automatically.
        /// </summary>
        /// <param name="indices"></param>
        /// <returns></returns>
        public Ideal WithFace(params int[] indices)
        {
            FaceX face = new FaceX(indices);
            if (faces_.Add(face))
            {
                for (int i = 0, ii = indices.Length; i < ii; i += 1)
                {
                    int j = (i + 1) % indices.Length;
                    EdgeX edge = new EdgeX(indices[i], indices[j]);
                    edges_.Add(edge);
                }
            }
            return this;
        }

        /// <summary>
        /// Canonicalize indices: make the least index first
        /// </summary>
        /// <param name="indices"></param>
        public static void Canonicalize(int[] indices)
        {
            #region Make sure that there is at least 1 elements
            if (indices.Length < 1)
            {
                throw new ArgumentException($"indices.Length = {indices.Length}; {string.Join(',', indices)}");
            }
            #endregion
            #region Find the position of the least element with duplication checks
            Dictionary<int, int> visited = new Dictionary<int, int>();
            int leastAt = 0, leastVal = indices[leastAt];
            visited[leastVal] = leastAt;
            for (int i = 1, ii = indices.Length; i < ii; i += 1)
            {
                int currVal = indices[i];
                int duplicateAt;
                if (visited.TryGetValue(currVal, out duplicateAt))
                {
                    throw new ArgumentException($"duplicate: {currVal} = indices[{i}] = indices[{duplicateAt}]; {string.Join(',', indices)}");
                }
                visited[currVal] = i;
                if (leastVal > currVal)
                {
                    leastVal = currVal;
                    leastAt = i;
                }
            }
            #endregion
            #region Move the least element first
            Array.Reverse(indices, 0, leastAt);
            Array.Reverse(indices, leastAt, indices.Length - leastAt);
            Array.Reverse(indices, 0, indices.Length);
            #endregion
        }
    }
}
