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
	}
}

