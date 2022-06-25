using System;
using OpenTK.Mathematics;
using System.Drawing;

namespace CGLab3
{
    public struct Vertex
    {
        public FloatPoint3D Position { get; set; }

        public FloatPoint3D Normal { get; set; } = new FloatPoint3D(0, 0, 0);

        public Color Color { get; set; } = Color.White;

        public Vector4 ColorFloat
        {
            get
            {
                return new Vector4(Color.R / 255f, Color.G / 255f, Color.B / 255f, Color.A / 255f);
            }
        }


        public Vertex(FloatPoint3D position)
        {
            Position = position;
        }

        public Vertex(FloatPoint3D position, Color color) : this(position)
        {
            Position = position;
        }

        public override string ToString()
        {
            return $"{Position.X} {Position.Y} {Position.Z} 0 0 0 {Normal.X} {Normal.Y} {Normal.Z}";
        }
    }
}

