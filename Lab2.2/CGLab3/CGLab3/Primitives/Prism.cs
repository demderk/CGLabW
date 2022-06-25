//using System;
//using System.Collections.Generic;
//using System.Drawing;
//using System.Numerics;

//namespace CGLab3.Primitives
//{
//    public class Prism : Shape
//    {
//        public Prism(Point3D center, int height, int radius, int vecCount)
//        {

//            int R = radius;
//            int n = vecCount;

//            double angle = 360 / n;
//            List<Vector3> list = new List<Vector3>();
//            List<Vector3> list2 = new List<Vector3>();
//            double zangle = 0;
//            for (int i = 0; i <= n; i++)
//            {
//                list.Add(
//                    new Vector3((float)Math.Round(Math.Sin(zangle / 180 * Math.PI) * R) + center.X,
//                    center.Y,
//                    (float)Math.Round(Math.Cos(zangle / 180 * Math.PI) * R) + center.Z));
//                zangle += angle;
//            }
//            zangle = 0;
//            for (int i = 0; i <= n; i++)
//            {
//                list2.Add(new Vector3((float)Math.Round(Math.Sin(zangle / 180 * Math.PI) * R) + center.X,
//                    center.Y + height,
//                    (float)Math.Round(Math.Cos(zangle / 180 * Math.PI) * R) + center.Z));
//                zangle += angle;
//            }

//            for (int i = 1; i < n - 1; i++)
//            {
//                Triangle triangle = new Triangle(
//                    new Point3D((int)list[i + 1].X, (int)list[i + 1].Y, (int)list[i + 1].Z),
//                    new Point3D((int)list[i].X, (int)list[i].Y, (int)list[i].Z),
//                    new Point3D((int)list[0].X, (int)list[0].Y, (int)list[0].Z)
//                    );
//                Merge(triangle);
//            }
//            for (int i = 0; i < n - 1; i++)
//            {
//                Triangle triangle = new Triangle(
//                    new Point3D((int)list2[i + 1].X, (int)list2[i + 1].Y, (int)list2[i + 1].Z),
//                    new Point3D((int)list2[i].X, (int)list2[i].Y, (int)list2[i].Z),
//                    new Point3D((int)list2[0].X, (int)list2[0].Y, (int)list2[0].Z)
//                    );
//                Merge(triangle);
//            }
//            for (int i = 0; i < n; i++)
//            {
//                Rectangle rectangle = new Rectangle(
//                    new Point3D((int)list[i].X, (int)list[i].Y, (int)list[i].Z),
//                    new Point3D((int)list2[i].X, (int)list2[i].Y, (int)list2[i].Z),
//                    new Point3D((int)list2[i + 1].X, (int)list2[i + 1].Y, (int)list2[i + 1].Z),
//                    new Point3D((int)list[i + 1].X, (int)list[i + 1].Y, (int)list[i + 1].Z)
//                    );
//                Merge(rectangle);
//            }
//        }

//        new public Prism ColorFill(Color color)
//        {
//            base.ColorFill(color);
//            return this;
//        }
//    }
//}

