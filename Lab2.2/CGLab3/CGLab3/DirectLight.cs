using System;
using OpenTK.Mathematics;

namespace CGLab3
{
	public struct DirectLight
	{
        public Vector3 Direction { get; set; }
        public Vector3 Ambient { get; set; }
        public Vector3 Diffuse { get; set; }
        public Vector3 Specular { get; set; }
        public Vector3 LightColor { get; set; }
        public bool Disabled;

        public static DirectLight BuildDefault()
        {
            return new DirectLight()
            {
                Direction = new Vector3(-0.3f, -1f, -0.2f),
                Ambient = new Vector3(0.2f),
                Diffuse = new Vector3(0.5f),
                Specular = new Vector3(1f),
                LightColor = new Vector3(1f)
            };
        }
    }
}

