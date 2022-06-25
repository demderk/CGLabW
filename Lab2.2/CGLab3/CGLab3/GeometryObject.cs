using System;
using System.Collections.Generic;
using System.Drawing;
using OpenTK.Mathematics;

namespace CGLab3
{
    public sealed class GeometryObject
    {
        public float[] ColorsMatrix { get; private set; } = { };

        public float[] PointMatrix { get; private set; } = { };

        public float[] DataMatrix { get; private set; } = { };

        public float[] NormalMatrix { get; private set; } = { };

        private List<Vertex> _vertices = new List<Vertex>();

        public Vertex[] Vertices => _vertices.ToArray();

        public int Count => _vertices.Count;

        public GeometryObject()
        {

        }

        private void RebuildMatrices()
        {
            List<float> newColors = new List<float>();
            List<float> newPos = new List<float>();
            List<float> data = new List<float>();
            List<float> normals = new List<float>();
            foreach (var item in _vertices)
            {

                data.Add(item.Position.X);
                data.Add(item.Position.Y);
                data.Add(item.Position.Z);
                data.Add(item.ColorFloat.X);
                data.Add(item.ColorFloat.Y);
                data.Add(item.ColorFloat.Z);
                data.Add(item.ColorFloat.W);
                data.Add(item.Normal.X);
                data.Add(item.Normal.Y);
                data.Add(item.Normal.Z);

            }

            DataMatrix = data.ToArray();
        }

        public void Update() => RebuildMatrices();

        public void Add(Vertex point)
        {
            _vertices.Add(point);
        }


        public void AddAndUpdate(Vertex point)
        {
            Add(point);
            RebuildMatrices();
        }

        public void AddRange(params Vertex[] points)
        {
            _vertices.AddRange(points);
        }

        public void AddRangeAndUpdate(params Vertex[] points)
        {
            AddRange(points);
            RebuildMatrices();
        }

        public void SetNormal(FloatPoint3D point)
        {
            if (_vertices.Count == 0)
            {
                throw new Exception("GeometryObject is empty.");
            }
            for (int i = 0; i < _vertices.Count; i++)
            {
                _vertices[i] = _vertices[i] with { Normal = point};
            }
            RebuildMatrices();
        }

        public void ColorFill(Color color)
        {
            for (int i = 0; i < _vertices.Count; i++)
            {
                _vertices[i] = _vertices[i] with { Color = color };
            }
            RebuildMatrices();
        }

        public void MoveBy(float x, float y, float z)
        {
            for (int i = 0; i < _vertices.Count; i++)
            {
                var newPos = _vertices[i].Position;
                newPos.X = newPos.X + x;
                newPos.Y = newPos.Y + y;
                newPos.Z = newPos.Z + z;
                _vertices[i] = _vertices[i] with { Position = newPos };
            }
            RebuildMatrices();
        }

        public void Clear() => _vertices.Clear();

    }

}

