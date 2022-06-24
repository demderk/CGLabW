using System;
using System.Drawing;
using OpenTK.Mathematics;

namespace CGLab3
{
    public enum LightMode
    {
        Near,
        Medium,
        Far
    }

    public struct PointLight
    {
        public Vector3 Position;
        public Vector3 Ambient;
        public Vector3 Diffuse;
        public Vector3 Specular;
        public Vector3 LightColor;
        public float Constant;
        public float Linear;
        public float Quadratic;
        public bool Disabled;

        public static PointLight BuildLight(FloatPoint3D position, LightMode light, Color lightColor)
        {
            PointLight result = new PointLight();
            result.Constant = 1f;
            result.Position = position.ToVector3();
            result.Ambient = new Vector3(0.05f);
            result.Diffuse = new Vector3(0.8f);
            result.Specular = new Vector3(.10f);
            result.LightColor = new Vector3(lightColor.R / 255f, lightColor.G / 255f, lightColor.B / 255f);
            switch (light)
            {
                case LightMode.Near:
                    result.Linear = 0.7f;
                    result.Quadratic = 1.8f;
                    return result;
                case LightMode.Medium:
                    result.Linear = 0.09f;
                    result.Quadratic = 0.032f;
                    return result;
                case LightMode.Far:
                    result.Linear = 0.022f;
                    result.Quadratic = 0.0019f;
                    return result;
                default:
                    throw new NotImplementedException();
            }
        }
    };
}

