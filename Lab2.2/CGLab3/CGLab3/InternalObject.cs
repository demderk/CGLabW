using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using CGLab3.Primitives;

namespace CGLab3.ImportObjects
{
    enum PrimitiveMode
    {
        Triangle,
        Rectangle
    }

    enum PlaneMode
    {
        Simple,
        Texture,
        Normal,
        Full

    }


    public class InternalObject : Shape
    {

        private string _detectTriangle = @"^[f]\s\S{1,}\s\S{1,}\s\S{1,}\s{0,}$";
        private string _detectRectangle = @"^[f]\s\S{1,}\s\S{1,}\s\S{1,}\s\S{1,}\s{0,}$";

        private string _detectSimple = @"^[f]\s(\d\s{0,}){3,}$";
        private string _detectTexture = @"^[f]\s(\d\/\d\s{0,}){3,}$";
        private string _detectNormal = @"^[f]\s(\d{1,}\/\/\d{1,}\s{0,}){3,}$";
        private string _detectFull = @"^[f]\s(\d{1,}\/\d{1,}\/\d{1,}\s{0,}){3,}$";

        private string _simplePlaneFilter = @"\d{1,}";
        private string _texturePlaneFilter = @"\d{1,}\/\d{1,}";
        private string _normalPlaneFilter = @"\d{1,}\/\/\d{1,}";
        private string _fullPlaneFilter = @"\d{1,}\/\d{1,}\/\d{1,}";

        private string _floatFilter = @"[-]{0,}\d{0,}[.]{0,}\d{1,}";

        private List<FloatPoint3D> vertsList = new List<FloatPoint3D>();
        private List<FloatPoint3D> normList = new List<FloatPoint3D>();

        public void Import(string path)
        {
            string[] input = File.ReadAllLines(path);


            Build(input);
        }

        private PrimitiveMode DetectPrimitiveMode(string[] text)
        {
            var detectRectangleRegex = new Regex(_detectRectangle);
            var detectTriangelRegex = new Regex(_detectTriangle);

            var isRectangle = false;
            var isTriangle = false;

            foreach (var item in text)
            {
                if (isTriangle && isRectangle)
                {
                    throw new FormatException("Detect primitive falied. The input file has both triangular and rectangular surfaces.");
                }
                if (detectRectangleRegex.Match(item).Success)
                {
                    isRectangle = true;
                    continue;
                }
                if (detectTriangelRegex.Match(item).Success)
                {
                    isTriangle = true;
                }
            }

            if (isTriangle)
            {
                return PrimitiveMode.Triangle;
            }
            else if (isRectangle)
            {
                return PrimitiveMode.Rectangle;
            }
            else
            {
                throw new FormatException("Detect primitive falied. The input file not in triangle or rectangle mode. Check .obj file.");
            }

        }

        private PlaneMode DetectPlaneMode(string[] text)
        {
            PlaneMode? result = null;
            int start = Array.IndexOf(text, text.First(x => x.StartsWith("f")));
            for (int i = start; i < text.Length; i++)
            {
                if (result == null)
                {
                    result = ReadPlaneModeString(text[i]);
                }
                else
                {
                    if (!text[i].StartsWith("f"))
                    {
                        continue;
                    }
                    PlaneMode stringMode = ReadPlaneModeString(text[i]);
                    if (stringMode != result)
                    {
                        throw new FormatException("Detect Plane Mode error. Multiple modes.");
                    }
                }
            }
            return result.Value;
        }

        private PlaneMode ReadPlaneModeString(string text)
        {
            bool simple = new Regex(_detectSimple).Match(text).Success;
            bool texture = new Regex(_detectTexture).Match(text).Success;
            bool normal = new Regex(_detectNormal).Match(text).Success;
            bool full = new Regex(_detectFull).Match(text).Success;

            int count = (new bool[] { simple, texture, normal, full }).Count(x => x);

            if (count > 1)
            {
                throw new FormatException("Get Plane mode error. Detected more than 1 mode.");
            }

            if (simple)
            {
                return PlaneMode.Simple;
            }
            if (texture)
            {
                return PlaneMode.Texture;
            }
            if (normal)
            {
                return PlaneMode.Normal;
            }
            if (full)
            {
                return PlaneMode.Full;
            }
            throw new FormatException("Plane Plane mode error. String isn't plane format.");
        }

        private FloatPoint3D[] ReadVerts(string[] text)
        {
            int start = Array.IndexOf(text, text.First(x => x.StartsWith("v ")));
            var fFiler = new Regex(_floatFilter);

            List<FloatPoint3D> result = new List<FloatPoint3D>();

            for (int i = start; i < text.Length; i++)
            {
                if (!(text[i].StartsWith("v ")))
                {
                    continue;
                }
                var xyz = fFiler.Matches(text[i]);
                if (xyz.Count != 3)
                {
                    throw new FormatException($"Bad vertex at {i}");
                }
                result.Add(new FloatPoint3D(float.Parse(xyz[0].Value), float.Parse(xyz[1].Value), float.Parse(xyz[2].Value)));
            }

            return result.ToArray();
        }

        private FloatPoint3D[] ReadNromals(string[] text)
        {
            int start = Array.IndexOf(text, text.First(x => x.StartsWith("vn")));
            var fFiler = new Regex(_floatFilter);

            List<FloatPoint3D> result = new List<FloatPoint3D>();

            for (int i = start; i < text.Length; i++)
            {
                if (!(text[i].StartsWith("vn")))
                {
                    continue;
                }
                var xyz = fFiler.Matches(text[i]);
                if (xyz.Count != 3)
                {
                    throw new FormatException($"Bad vertex normal at {i}");
                }
                result.Add(new FloatPoint3D(float.Parse(xyz[0].Value), float.Parse(xyz[1].Value), float.Parse(xyz[2].Value)));
            }

            return result.ToArray();
        }


        private void Build(string[] text)
        {
            vertsList.AddRange(ReadVerts(text));
            normList.AddRange(ReadNromals(text));
            PlaneMode planeMode = DetectPlaneMode(text);
            int start = Array.IndexOf(text, text.First(x => x.StartsWith("f")));

            List<string> panels = new List<string>();

            panels.AddRange(text.Where(x => x.StartsWith("f")));

            List<PaneDescription> all = new List<PaneDescription>();

            foreach (var item in panels)
            {
                all.Add(ReadPaneString(item, planeMode));
            }
            foreach (var item in all)
            {
                var o = new Polygon(item.Position[0], item.Position[1], item.Position[2]);
                for (int i = 2; i < item.Position.Length; i++)
                {
                    o.Add(item.Position[i]);
                }
                if (item.Normal != null)
                {
                    o.SetNormal(item.Normal[0]);
                }
                Merge(o);
            }
        }

        private PaneDescription ReadPaneString(string str, PlaneMode planeMode)
        {
            List<FloatPoint3D> pos = new List<FloatPoint3D>();
            List<FloatPoint3D> norms = new List<FloatPoint3D>();
            MatchCollection a;
            switch (planeMode)
            {
                case PlaneMode.Simple:
                    a = new Regex(_simplePlaneFilter).Matches(str);
                    break;
                case PlaneMode.Texture:
                    a = new Regex(_texturePlaneFilter).Matches(str);
                    break;
                case PlaneMode.Normal:
                    a = new Regex(_normalPlaneFilter).Matches(str);
                    break;
                case PlaneMode.Full:
                    a = new Regex(_fullPlaneFilter).Matches(str);
                    break;
                default:
                    throw new Exception();
            }
            //
            if (planeMode == PlaneMode.Full)
            {
                for (int i = 0; i < a.Count; i++)
                {
                    var b = new Regex(@"\d{1,}").Matches(a[i].Value);
                    pos.Add(vertsList[Convert.ToInt32(b[0].Value)-1]);
                    norms.Add(normList[Convert.ToInt32(b[2].Value)-1]);
                }
                return new PaneDescription { Position = pos.ToArray(), Normal = norms.ToArray(), TextureCords = null }; // NYI
            }
            else if (planeMode == PlaneMode.Simple)
            {
                for (int i = 0; i < a.Count; i++)
                {
                    var b = new Regex(@"\d{1,}").Matches(a[i].Value);
                    pos.Add(vertsList[Convert.ToInt32(b[0].Value)-1]);
                }
                return new PaneDescription { Position = pos.ToArray(), Normal = null, TextureCords = null };
            }
            else if (planeMode == PlaneMode.Normal)
            {
                for (int i = 0; i < a.Count; i++)
                {
                    var b = new Regex(@"\d{1,}").Matches(a[i].Value);
                    pos.Add(vertsList[Convert.ToInt32(b[0].Value)-1]);
                    norms.Add(normList[Convert.ToInt32(b[1].Value)-1]);
                }
                return new PaneDescription { Position = pos.ToArray(), Normal = norms.ToArray(), TextureCords = null };
            }
            throw new Exception();
        }



    }

    struct PaneDescription
    {
        public FloatPoint3D[] Position;
        public FloatPoint3D[] Normal;
        public FloatPoint3D[] TextureCords;
    }

}

