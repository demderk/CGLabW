using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace CGFx3
{
    public class CGMatrix<T>
    {
        public int Rows { get; }

        public int Columns { get; }

        public List<List<T>> Data { get; protected set; }

        public CGMatrix(List<List<T>> data)
        {
            if (!IsMatrix(data))
            {
                throw new InvalidOperationException("Input data is not matrix");
            }
            Data = data;
            Rows = data.Count;
            Columns = data[0].Count;
        }

        public static bool IsMatrix<T>(List<List<T>> data)
        {
            int length = -1;
            foreach (var item in data)
            {
                if (length == -1)
                {
                    length = data.Count;
                }
                else
                {
                    if (length != data.Count)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public T this[int y, int x]
        {
            get
            {
                return Data[y][x];
            }
            set
            {
                Data[y][x] = value;
            }
        }

    }

    public class CGIntMatrix : CGMatrix<int>
    {
        public CGIntMatrix(List<List<int>> data) : base(data)
        {

        }

        private static List<List<int>> GetCGMatricsFormNumbers(int columns, params int[] nums)
        {
            if (nums.Length % columns != 0)
            {
                throw new InvalidOperationException("Bad nums count");
            }
            List<List<int>> resultList = new List<List<int>>();
            for (int i = 0; i < columns; i++)
            {
                List<int> row = new List<int>();
                for (int i2 = 0; i2 < nums.Length / columns; i2++)
                {
                    row.Add(nums[i * columns + i2]);
                }
                resultList.Add(row);
            }
            return resultList;
        }

        public CGIntMatrix(int columns, params int[] nums) : base(GetCGMatricsFormNumbers(columns, nums))
        {

        }

        public static CGIntMatrix operator *(CGIntMatrix first, CGIntMatrix second)
        {
            List<List<int>> result = new List<List<int>>();
            for (int h = 0; h < first.Rows; h++)
            {
                List<int> tempRow = new List<int>();
                for (int w = 0; w < first.Columns; w++)
                {
                    int tempValue = 0;
                    for (int i = 0; i < second.Rows; i++)
                    {
                        tempValue += first[h, i] * second[i, w];
                    }
                    tempRow.Add(tempValue);
                }
                result.Add(tempRow);
            }
            return new CGIntMatrix(result);
        }

    }

    public class CGDoubleMatrix : CGMatrix<double>
    {
        public CGDoubleMatrix(List<List<double>> data) : base(data)
        {

        }

        private static List<List<double>> GetCGMatricsFormNumbers(int columns, params double[] nums)
        {
            if (nums.Length % columns != 0)
            {
                throw new InvalidOperationException("Bad nums count");
            }
            List<List<double>> resultList = new List<List<double>>();
            for (int i = 0; i < columns; i++)
            {
                List<double> row = new List<double>();
                for (int i2 = 0; i2 < nums.Length / columns; i2++)
                {
                    row.Add(nums[i * columns + i2]);
                }
                resultList.Add(row);
            }
            return resultList;
        }

        public CGDoubleMatrix(int columns, params double[] nums) : base(GetCGMatricsFormNumbers(columns, nums))
        {

        }

        public static CGDoubleMatrix operator *(CGDoubleMatrix first, CGDoubleMatrix second)
        {
            List<List<double>> result = new List<List<double>>();
            for (int h = 0; h < first.Rows; h++)
            {
                List<double> tempRow = new List<double>();
                for (int w = 0; w < first.Columns; w++)
                {
                    double tempValue = 0;
                    for (int i = 0; i < second.Rows; i++)
                    {
                        tempValue += first[h, i] * second[i, w];
                    }
                    tempRow.Add(tempValue);
                }
                result.Add(tempRow);
            }
            return new CGDoubleMatrix(result);
        }

    }
    class OpenFX
    {

    }

    public class CGLine
    {
        public Point3D Start { get; }

        public Point3D End { get; }

        public static CGIntMatrix GetPoint3DMatrix(Point3D point)
        {
            return new CGIntMatrix(1,
                (int)point.X, (int)point.Y, (int)point.Z, 1);
        }

        public static CGDoubleMatrix GetPoint3DMatrixDouble(Point3D point)
        {
            return new CGDoubleMatrix(1,
                point.X, point.Y, point.Z, 1);
        }

        public CGLine(Point3D start, Point3D end)
        {
            Start = start;
            End = end;
        }
    }
    class MovableObject
    {
        public List<CGLine> Lines { get; set; } = new List<CGLine>();

        public event EventHandler Moved;

        public void Build(int a)
        {
            Lines = new List<CGLine>()
            {
                new CGLine(new Point3D(a/2,-a/2,a/2),new Point3D(-a/2,-a/2,a/2)),
                new CGLine(new Point3D(-a/2,-a/2,a/2),new Point3D(-a/2,a/2,a/2)),
                new CGLine(new Point3D(a/2,-a/2,a/2),new Point3D(a/2,a/2,a/2)),
                new CGLine(new Point3D(a/2,a/2,a/2),new Point3D(-a/2,a/2,a/2)),

                new CGLine(new Point3D(a/2,a/2,a/2),new Point3D(a/2,a/2,-a/2)),
                new CGLine(new Point3D(-a/2,a/2,a/2),new Point3D(-a/2,a/2,-a/2)),
                new CGLine(new Point3D(a/2,a/2,-a/2),new Point3D(-a/2,a/2,-a/2)),
                new CGLine(new Point3D(a/2,a/2,-a/2),new Point3D(-a/2,a/2,-a/2)),

                new CGLine(new Point3D(-a/2,a/2,-a/2),new Point3D(-a/2,-a/2,-a/2)),
                new CGLine(new Point3D(a/2,a/2,-a/2),new Point3D(a/2,-a/2,-a/2)),
                new CGLine(new Point3D(-a/2,-a/2,-a/2),new Point3D(a/2,-a/2,-a/2)),

                new CGLine(new Point3D(a/2,-a/2,-a/2),new Point3D(a/2,-a/2,a/2)),
                new CGLine(new Point3D(-a/2,-a/2,-a/2),new Point3D(-a/2,-a/2,a/2))
            };
        }

        public static void Move()
        {
            CGIntMatrix matrix = new CGIntMatrix(4,
                1, 0, 0, 0,
                0, 1, 0, 0,
                0, 0, 1, 0,
                10, 10, 10, 1);
            CGIntMatrix cords = new CGIntMatrix(1,
                10, 10, 10, 1);

            CGIntMatrix cgm = cords * matrix;
        }

        public List<Line> To2dlines()
        {
            List<Line> result = new List<Line>();
            foreach (var item in Lines)
            {
                Line line = new Line();
                line.X1 = item.Start.X;
                line.Y1 = item.Start.Y;
                line.X2 = item.End.X;
                line.Y2 = item.End.Y;
                line.StrokeThickness = 1;
                line.Stroke = Brushes.Black;
                result.Add(line);
            }
            return result;
        }

        public CGDoubleMatrix PovorotTetaZ(int x, int y)
        {
            double v = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
            double cosT = x / v;
            double sinT = y / v;
            return new CGDoubleMatrix(4,
                cosT, -1 * sinT, 0, 0,
                sinT, cosT, 0, 0,
                0, 0, 1, 0,
                0, 0, 0, 1);
        }
        public CGDoubleMatrix PovorotTetaY(int x, int y, int z)
        {
            double v = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
            double e = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2) + Math.Pow(z, 2));
            double cosT = z / e;
            double sinT = v / e;
            return new CGDoubleMatrix(4,
                sinT, 0, cosT, 0,
                0, 1, 0, 0,
                -1 * cosT, 0, sinT, 0,
                0, 0, 0, 1);
        }

        public CGDoubleMatrix MoveTetaE(int x, int y, int z)
        {
            double e = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2) + Math.Pow(z, 2));
            return new CGDoubleMatrix(4,
                1, 0, 0, 0,
                0, 1, 0, 0,
                0, 0, 1, 0,
                -1 * e, 0, 0, 1);
        }

        public CGDoubleMatrix GetS(int x, int y, int z)
        {
            double e = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2) + Math.Pow(z, 2));
            return new CGDoubleMatrix(4,
                0, 0, -1, 0,
                1, 0, 0, 0,
                0, 1, 0, 0,
                0, 0, 0, 1);
        }

        public void BuildScreen(int x, int y, int z)
        {
            x = x == 0 ? 1 : x;
            y = y == 0 ? 1 : y;
            z = z == 0 ? 1 : z;

            for (int i = 0; i < Lines.Count; i++)
            {
                CGDoubleMatrix matrixStart = CGLine.GetPoint3DMatrixDouble(Lines[i].Start);
                matrixStart = matrixStart * PovorotTetaZ(x, y) * PovorotTetaY(x, y, z) * MoveTetaE(x, y, z) * GetS(x, y, z);
                CGDoubleMatrix matrixEnd = CGLine.GetPoint3DMatrixDouble(Lines[i].End);
                matrixEnd = matrixEnd * PovorotTetaZ(x, y) * PovorotTetaY(x, y, z) * MoveTetaE(x, y, z) * GetS(x, y, z);
                Lines[i] = new CGLine(new Point3D(matrixStart[0, 0], matrixStart[0, 1], matrixStart[0, 2]), new Point3D(matrixEnd[0, 0], matrixEnd[0, 1], matrixEnd[0, 2]));
            }
        }
        public void BuildScreenPrpct(int x, int y, int z, int d)
        {
            x = x == 0 ? 1 : x;
            y = y == 0 ? 1 : y;
            z = z == 0 ? 1 : z;

            for (int i = 0; i < Lines.Count; i++)
            {
                CGDoubleMatrix matrixStart = CGLine.GetPoint3DMatrixDouble(Lines[i].Start);
                matrixStart = matrixStart * PovorotTetaZ(x, y) * PovorotTetaY(x, y, z) * MoveTetaE(x, y, z) * GetS(x, y, z);
                CGDoubleMatrix matrixEnd = CGLine.GetPoint3DMatrixDouble(Lines[i].End);
                matrixEnd = matrixEnd * PovorotTetaZ(x, y) * PovorotTetaY(x, y, z) * MoveTetaE(x, y, z) * GetS(x, y, z);

                double xStartNew = (d * (matrixStart[0, 0]/ matrixStart[0, 2])) + 50;
                double yStartNew = 50 - (d * (matrixStart[0, 1] / matrixStart[0, 2]));

                double xEndNew = (d * (matrixEnd[0, 0] / matrixEnd[0, 2])) + 50;
                double yEndNew = 50 - (d * (matrixEnd[0, 1] / matrixEnd[0, 2]));

                Lines[i] = new CGLine(new Point3D(xStartNew, yStartNew, matrixStart[0, 2]), new Point3D(xEndNew, yEndNew, matrixStart[0, 2]));
            }
        }

        public void BuildRounded(int n, int R, int h = 100)
        {
            double angle = 360 / n;
            List<Point3D> list = new List<Point3D>();
            List<Point3D> list2 = new List<Point3D>();
            double zangle = 0;
            for (int i = 0; i <= n; i++)
            {
                list.Add(new Point3D(Math.Round(Math.Cos(zangle / 180 * Math.PI) * R), Math.Round(Math.Sin(zangle / 180 * Math.PI) * R), 1));
                zangle += angle;
            }
            for (int i = 0; i < list.Count - 1; i++)
            {
                Lines.Add(new CGLine(list[i], list[i + 1]));
            }
            zangle = 0;
            for (int i = 0; i <= n; i++)
            {
                list2.Add(new Point3D(Math.Round(Math.Cos(zangle / 180 * Math.PI) * R), Math.Round(Math.Sin(zangle / 180 * Math.PI) * R), h));
                zangle += angle;
            }
            for (int i = 0; i < list2.Count - 1; i++)
            {
                Lines.Add(new CGLine(list2[i], list2[i + 1]));
            }
            for (int i = 0; i < list.Count; i++)
            {
                Lines.Add(new CGLine(list[i], list2[i]));
            }
        }
    }


}