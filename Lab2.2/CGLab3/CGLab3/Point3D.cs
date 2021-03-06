using System;
using OpenTK.Mathematics;

namespace CGLab3
{
    public struct Point3D
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public Point3D(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public FloatPoint3D ToFloatPoint3D()
        {
            return new FloatPoint3D(this, 100);
        }
    }
}

