using System;
using OpenTK.Mathematics;
using System.Drawing;

namespace CGLab3
{
    public class Vertex
    {
        public Vector3 PositionFloat { get; private set; }

        public Vector3 NormalFloat { get; set; }

        public Point3D Position { get; }

        public FloatPoint3D Normal { get; private set; }

        private int _resolution = 100;

        public int Resolution
        {
            get => _resolution;
        }

        public Vector4 ColorFloat
        {
            get
            {
                return new Vector4(Color.R / 255f, Color.G / 255f, Color.B / 255f, Color.A / 255f);
            }
        }

        private Color _color = Color.White;

        public Color Color { get => _color;
            set
            {
                _color = value;
                BuildFloatPoints();
            }
        }

        public Vertex(int resolution, int x, int y, int z, Color color)
        {
            Position = new Point3D(x, y, z);
            Color = color;
            _resolution = resolution;
            BuildFloatPoints();
        }

        public Vertex(int x, int y, int z, Color color)
        {
            Position = new Point3D(x, y, z);
            Color = color;
            BuildFloatPoints();
        }

        public Vertex(int x, int y, int z)
        {
            Position = new Point3D(x, y, z);
            BuildFloatPoints();
        }

        public Vertex(Point3D point)
        {
            Position = point;
            BuildFloatPoints();
        }

        public Vertex(int x, int y, int z, int nx, int ny, int nz)
        {
            Position = new Point3D(x, y, z);
            Normal = new FloatPoint3D(nx, ny, nz, _resolution);
            BuildFloatPoints();
        }

        public void SetNormal(Point3D point)
        {
            SetNormal(new FloatPoint3D(point, _resolution));
        }

        public void SetNormal(FloatPoint3D point)
        {
            Normal = point;
            BuildFloatPoints();
        }

        private void BuildFloatPoints()
        {
            float xFloat = Position.X / (float)Resolution;
            float yFloat = Position.Y / (float)Resolution;
            float zFloat = Position.Z / (float)Resolution;
            float nxFloat = Normal.X / (float)Resolution;
            float nyFloat = Normal.Y / (float)Resolution;
            float nzFloat = Normal.Z / (float)Resolution;

            NormalFloat = new Vector3(nxFloat, nyFloat, nzFloat);
            PositionFloat = new Vector3(xFloat, yFloat, zFloat);
        }

        public void ChangeResolution(int resolution)
        {
            _resolution = resolution;
            BuildFloatPoints();
        }

        public override string ToString()
        {
            return $"{Position.X} {Position.Y} {Position.Z} 0 0 0 {Normal.X} {Normal.Y} {Normal.Z}";
        }
    }
}

