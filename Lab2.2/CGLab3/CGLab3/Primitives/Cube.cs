using System;
using System.Drawing;

namespace CGLab3.Primitives
{
    public class Cube : Shape
    {
        public Cube(Point3D a, Point3D b, Point3D c, Point3D d, int height)
        {
            var array = BuildEFGHByHeight(a, b, c, d, height);
            Build(a, b, c, d, array[0], array[1], array[2], array[3]);
        }

        public Cube(Point3D center, int width, int height, int depth)
        {
            Point3D a = new Point3D(center.X - width / 2, center.Y - depth / 2, center.Z - height / 2);
            Point3D b = new Point3D(center.X + width / 2, center.Y - depth / 2, center.Z - height / 2);
            Point3D c = new Point3D(center.X + width / 2, center.Y - depth / 2, center.Z + height / 2);
            Point3D d = new Point3D(center.X - width / 2, center.Y - depth / 2, center.Z + height / 2);
            var array = BuildEFGHByHeight(a, b, c, d, height);
            Build(a, b, c, d, array[0], array[1], array[2], array[3]);
        }

        private Point3D[] BuildEFGHByHeight(Point3D a, Point3D b, Point3D c, Point3D d, int height)
        {
            Point3D e = new Point3D(a.X, a.Y + height, a.Z);
            Point3D f = new Point3D(b.X, b.Y + height, b.Z);
            Point3D g = new Point3D(c.X, c.Y + height, c.Z);
            Point3D h = new Point3D(d.X, d.Y + height, d.Z);
            return new Point3D[] { e, f, g, h };
        }

        private void Build(Point3D a, Point3D b, Point3D c, Point3D d, Point3D e, Point3D f, Point3D g, Point3D h)
        {
            var height = Math.Abs(a.Y - e.Y);

            var normalLength = height;

            //var topNormal = new Point3D((e.X + f.X) / 2, e.Y + normalLength, (f.Z + g.Z) / 2);     // <<< ---Я писал это 3 часа, оно останется здесь навсегда
            //var bottomNormal = new Point3D((a.X + b.X) / 2, a.Y - normalLength, (b.Z + c.Z) / 2);
            //var leftNormal = new Point3D((f.X + e.X) / 2, (b.Y + f.Y) / 2, b.Z - normalLength);
            //var rightNormal = new Point3D((h.X + g.X) / 2, (d.Y + h.Y) / 2, d.Z + normalLength);
            //var frontNormal = new Point3D(a.X - normalLength, (a.Y + e.Y) / 2, (e.Z + h.Z) / 2);
            //var backNormal = new Point3D(c.X + normalLength, (c.Y + g.Y) / 2, (g.Z + f.Z) / 2);


            //Merge(new Rectangle(topNormal, 5, 5).ColorFill(Color.Red));
            //Merge(new Rectangle(bottomNormal, 5, 5).ColorFill(Color.Purple));
            //Merge(new Rectangle(frontNormal, 5, 5).ColorFill(Color.Brown));
            //Merge(new Rectangle(backNormal, 5, 5).ColorFill(Color.Blue));
            //Merge(new Rectangle(leftNormal, 5, 5).ColorFill(Color.White));
            //Merge(new Rectangle(rightNormal, 5, 5).ColorFill(Color.Yellow));

            //Merge(new Rectangle(e, f, g, h, topNormal).ColorFill(Color.Red)); //TOP
            //Merge(new Rectangle(a, b, c, d, bottomNormal).ColorFill(Color.Purple)); // BOTTOM
            //Merge(new Rectangle(a, e, h, d, frontNormal).ColorFill(Color.Brown)); // Front
            //Merge(new Rectangle(c, g, f, b, backNormal).ColorFill(Color.Blue)); // Back
            //Merge(new Rectangle(b, f, e, a, leftNormal).ColorFill(Color.White)); // Left
            //Merge(new Rectangle(d, h, g, c, rightNormal).ColorFill(Color.Yellow)); // Right   // DEBUG

            Merge(new Rectangle(e, f, g, h)); //TOP
            Merge(new Rectangle(a, b, c, d)); // BOTTOM
            Merge(new Rectangle(a, e, h, d)); // Front
            Merge(new Rectangle(c, g, f, b)); // Back
            Merge(new Rectangle(b, f, e, a)); // Left
            Merge(new Rectangle(d, h, g, c)); // Right

        }

        new public Cube ColorFill(Color color)
        {
            base.ColorFill(color);
            return this;
        }
    }
}

