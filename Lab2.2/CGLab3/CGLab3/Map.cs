using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace CGLab3
{
	public class Map
	{
        private GeometryObject _geometry = new GeometryObject();

        public List<Shape> Shapes { get; set; } = new List<Shape>();

        public int PointsCount => _geometry.Count;

        public int Resolution { get; private set; } = 100;

        public float[] Colors => _geometry.ColorsMatrix;

        public float[] Points => _geometry.PointMatrix;

        public float[] Data => _geometry.DataMatrix;

        public float[] Normals => _geometry.NormalMatrix;

        public int ColorsVBOID { get; }

        public int PointsVBOID { get; }

        public int DataVBOID { get; }

        public int NormalsVBOID { get; set; }

        public DirectLight GlobalLight { get; set; } = new DirectLight { Disabled = true };

        private List<PointLight> _pointLights = new List<PointLight>();

        public Vertex[] GetVertices() => _geometry.Vertices;

        public Map()
        {
            ColorsVBOID = GL.GenBuffer();
            PointsVBOID = GL.GenBuffer();
            DataVBOID = GL.GenBuffer();
            NormalsVBOID = GL.GenBuffer();
        }

        ~Map()
        {
            GL.DeleteBuffer(ColorsVBOID);
            GL.DeleteBuffer(PointsVBOID);
            GL.DeleteBuffer(DataVBOID);
            GL.DeleteBuffer(NormalsVBOID);
        }

        public void AddShape(Shape paneObject)
		{
            paneObject.ShapeChanged += (s, h) => UpdateVBO();
			Shapes.Add(paneObject);
			_geometry.AddRangeAndUpdate(paneObject.GetVertices());
            UpdateVBO();
		}

        private void RebuildPoints()
        {
            _geometry.Clear();
            foreach (var item in Shapes)
            {
                _geometry.AddRangeAndUpdate(item.GetVertices());
            }
            UpdateVBO();
        }

        private void UpdateVBO()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, PointsVBOID);
            GL.BufferData(BufferTarget.ArrayBuffer, Points.Length * sizeof(float), Points, BufferUsageHint.DynamicDraw);
            GL.BindBuffer(BufferTarget.ArrayBuffer, ColorsVBOID);
            GL.BufferData(BufferTarget.ArrayBuffer, Colors.Length * sizeof(float), Colors, BufferUsageHint.DynamicDraw);
            GL.BindBuffer(BufferTarget.ArrayBuffer, NormalsVBOID);
            GL.BufferData(BufferTarget.ArrayBuffer, Colors.Length * sizeof(float), Normals, BufferUsageHint.DynamicDraw);
            GL.BindBuffer(BufferTarget.ArrayBuffer, DataVBOID);
            GL.BufferData(BufferTarget.ArrayBuffer, Data.Length * sizeof(float), Data, BufferUsageHint.DynamicDraw);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        private int NewVBO(float[] data)
        {
            int bufferID = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, bufferID);
            GL.BufferData(BufferTarget.ArrayBuffer, data.Length * sizeof(float), data, BufferUsageHint.DynamicCopy);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            return bufferID;
        }

        public void AddPointLight(PointLight light)
        {
            _pointLights.Add(light);
        }

        public void LightUpdate(Shaders shader)
        {
            shader.SetUniform3("GlobalLight.Direction", GlobalLight.Direction);
            shader.SetUniform3("GlobalLight.Ambient", GlobalLight.Ambient);
            shader.SetUniform3("GlobalLight.Diffuse", GlobalLight.Diffuse);
            shader.SetUniform3("GlobalLight.Specular", GlobalLight.Specular);
            shader.SetUniform3("GlobalLight.LightColor", GlobalLight.LightColor);

            for (int i = 0; i < _pointLights.Count; i++)
            {
                shader.SetUniform3($"PointLights[{i}].Position", _pointLights[i].Position);
                shader.SetUniform3($"PointLights[{i}].LightColor", _pointLights[i].LightColor);
                shader.SetFloat($"PointLights[{i}].Constant", _pointLights[i].Constant);
                shader.SetFloat($"PointLights[{i}].Linear", _pointLights[i].Linear);
                shader.SetFloat($"PointLights[{i}].Quadratic", _pointLights[i].Quadratic);
                shader.SetBool($"PointLights[{i}].Disabled", false);

            }

            shader.SetBool($"PointLights[{_pointLights.Count}].Disabled", true);
        }
    }
}

