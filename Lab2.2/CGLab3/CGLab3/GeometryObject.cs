using System;
using System.Collections.Generic;
using System.Drawing;

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
                newColors.Add(item.ColorFloat.X);
                newColors.Add(item.ColorFloat.Y);
                newColors.Add(item.ColorFloat.Z);
                newColors.Add(item.ColorFloat.W);
                newPos.Add(item.PositionFloat.X);
                newPos.Add(item.PositionFloat.Y);
                newPos.Add(item.PositionFloat.Z);
                data.Add(item.PositionFloat.X);
                data.Add(item.PositionFloat.Y);
                data.Add(item.PositionFloat.Z);
                data.Add(item.ColorFloat.X);
                data.Add(item.ColorFloat.Y);
                data.Add(item.ColorFloat.Z);
                data.Add(item.ColorFloat.W);
                data.Add(item?.NormalFloat.X ?? 0);
                data.Add(item?.NormalFloat.Y ?? 0);
                data.Add(item?.NormalFloat.Z ?? 0);
                normals.Add(item?.NormalFloat.X ?? 0);
                normals.Add(item?.NormalFloat.Y ?? 0);
                normals.Add(item?.NormalFloat.Z ?? 0);
            }
            PointMatrix = newPos.ToArray();
            ColorsMatrix = newColors.ToArray();
            DataMatrix = data.ToArray();
            NormalMatrix = normals.ToArray();
        }

        public void Add(Vertex point)
        {
            _vertices.Add(point);
            RebuildMatrices();
        }

        public void AddRange(params Vertex[] points)
        {
            _vertices.AddRange(points);
            RebuildMatrices();
        }

        public void ChangeResolution(int resolution)
        {
            foreach (var item in Vertices)
            {
                item.ChangeResolution(resolution);
            }
            RebuildMatrices();
        }

        public void SetNormal(Point3D point)
        {
            if (_vertices.Count == 0)
            {
                throw new Exception("GeometryObject is empty.");
            }
            foreach (var item in _vertices)
            {
                item.SetNormal(point);
            }
            RebuildMatrices();
        }

        public void SetNormal(FloatPoint3D point)
        {
            if (_vertices.Count == 0)
            {
                throw new Exception("GeometryObject is empty.");
            }
            foreach (var item in _vertices)
            {
                item.SetNormal(point);
            }
            RebuildMatrices();
        }

        public void ColorFill(Color color)
        {
            foreach (var item in _vertices)
            {
                item.Color = color;
            }
            RebuildMatrices();
        }

        public void Clear() => _vertices.Clear();

    }

}

