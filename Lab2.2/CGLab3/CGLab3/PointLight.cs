using System;
using OpenTK.Mathematics;

namespace CGLab3
{
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
    };
}

