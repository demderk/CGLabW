using System;
using System.Drawing;
using System.Linq;
namespace CGLab3.Primitives
{
	public class Pyramid : Shape
	{
		public Pyramid(Point3D center,int w, int h, int d)
		{
			var b = new Rectangle(center, w, d);
			Merge(b);
			var verts = b.ActualPoints.Append(b.ActualPoints[0]).ToArray();
			var upPoint = center with { Y = center.Y + h };
            for (int i = 0; i < verts.Length - 1; i++)
            {
				Merge(new Triangle(verts[i + 1], upPoint, verts[i]));
            }

		}

		new public Pyramid ColorFill(Color color)
		{
			base.ColorFill(color);
			return this;
		}
	}
}

