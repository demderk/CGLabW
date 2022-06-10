using System;
using System.Drawing;
using CGLab3;
using CGLab3.Primitives;

namespace CGLab3.HouseElements
{
    public class Wall : Shape
    {
        protected Wall() { }

        public Wall(Point3D a, Point3D b, int height, int width)
        {
            Build(a, b, height, width);
        }

        new public Wall ColorFill(Color color)
        {
            base.ColorFill(color);
            return this;
        }

        protected void Build(Point3D a, Point3D b, int height, int width)
        {
            var dx = Math.Abs(a.X - b.X) / Math.Abs((a.Z - b.Z) == 0 ? 1 : (a.Z - b.Z));
            Point3D pointA, pointB, pointC, pointD;
            if (dx < 1)
            {
                Point3D temp;
                if (a.Z > b.Z)
                {
                    temp = a;
                    a = b;
                    b = temp;
                }

                pointA = a with { X = a.X + width / 2 };
                pointB = b with { X = b.X + width / 2 };
                pointC = b with { X = b.X - width / 2 };
                pointD = a with { X = b.X - width / 2 };
            }
            else
            {
                Point3D temp;
                if (a.X > b.X)
                {
                    temp = a;
                    a = b;
                    b = temp;
                }
                pointA = a with { Z = a.Z - width / 2 };
                pointB = b with { Z = b.Z - width / 2 };
                pointC = b with { Z = b.Z + width / 2 };
                pointD = a with { Z = b.Z + width / 2 };
            }
            Merge(new Cube(pointA, pointB, pointC, pointD, height));
        }

        public void Merge(Wall wall) => base.Merge(wall);
    }
}

