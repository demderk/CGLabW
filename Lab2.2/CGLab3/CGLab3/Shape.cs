using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using OpenTK.Graphics.OpenGL4;
namespace CGLab3
{


    public abstract class Shape
    {
        public Shape() { }

        private GeometryObject _geometry { get; } = new GeometryObject();

        public float[] Points => _geometry.PointMatrix;

        public float[] Colors => _geometry.ColorsMatrix;

        public int PointCount => _geometry.Count;

        public event EventHandler ShapeChanged;

        private void OnShapeChanged() => ShapeChanged?.Invoke(this, null);

        public Vertex[] GetVertices() => _geometry.Vertices;
        public void SetNormal(FloatPoint3D point) => _geometry.SetNormal(point);
        protected void AddVertex(FloatPoint3D point) => _geometry.Add(new Vertex(point));
        //protected void AddVertex(Point3D point) => _geometry.Add(new Vertex(point.ToFloatPoint3D()));
        protected void AddVertex(Vertex point) => _geometry.Add(point);

        protected void Merge(Shape shape)
        {
            foreach (var item in shape.GetVertices())
            {
                _geometry.Add(item);
            }
        }

		protected void GenNormal(FloatPoint3D f, FloatPoint3D s, FloatPoint3D t)
		{
			var a = f;
			var b = s;
			var c = t;
			FloatPoint3D vVector1 = Vec(c, a);
			FloatPoint3D vVector2 = Vec(b, a);
			FloatPoint3D vNormal = Cross(vVector1, vVector2);
			var normal = Normalize(vNormal);
			SetNormal(normal);
		}

		private FloatPoint3D Cross(FloatPoint3D first, FloatPoint3D second)
		{
			FloatPoint3D result = new FloatPoint3D();
			result.X = ((first.Y * second.Z) - (first.Z * second.Y));
			result.Y = ((first.Z * second.X) - (first.X * second.Z));
			result.Z = ((first.X * second.Y) - (first.Y * second.X));
			return result;
		}

		private FloatPoint3D Vec(FloatPoint3D first, FloatPoint3D second)
		{
			FloatPoint3D result = new FloatPoint3D();
			result.X = first.X - second.X;
			result.Y = first.Y - second.Y;
			result.Z = first.Z - second.Z;
			return result;
		}

		private float Magnitude(FloatPoint3D point)
		{
			return (float)Math.Sqrt(Math.Abs(point.X * point.X) +
				Math.Abs(point.Y * point.Y) +
				Math.Abs(point.Z * point.Z));
		}

		private FloatPoint3D Normalize(FloatPoint3D point)
		{
			float magnitude = Magnitude(point);
			var result = new FloatPoint3D();
			result.X = point.X / magnitude;
			result.Y = point.Y / magnitude;
			result.Z = point.Z / magnitude;
			return result;
		}

        public Shape ColorFill(Color color)
        {
            _geometry.ColorFill(color);
            return this;
        }

		public Shape MoveBy(float x, float y, float z)
		{
			_geometry.MoveBy(x, y, z);
			return this;
		}

	}
}

