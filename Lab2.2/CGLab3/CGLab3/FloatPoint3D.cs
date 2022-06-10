using System;
namespace CGLab3
{

    public struct FloatPoint3D
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public FloatPoint3D(Point3D point, int resolution) : this()
        {
            X = point.X / (float)resolution;
            Y = point.Y / (float)resolution;
            Z = point.Z / (float)resolution;
        }

        public FloatPoint3D(int x, int y, int z, int resolution) : this()
        {
            X = x / (float)resolution;
            Y = y / (float)resolution;
            Z = z / (float)resolution;
        }
    }
}

