using System;
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

        public static Point3D operator +(Point3D first, Point3D second)
        {
            throw new NotImplementedException();
            return new Point3D(first.X + second.X, first.Y + second.Y, first.Z + second.Z);
        }

    }
}

