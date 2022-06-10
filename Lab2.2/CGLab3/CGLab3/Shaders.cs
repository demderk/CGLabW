using System;
using System.IO;
using OpenTK.Core.Platform;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace CGLab3
{
	public class Shaders
	{
        public int VertexShader { get; set; }

        public int FragmentShaders { get; set; }

		public int MainProgram { get; set; }


		public Shaders(string pathVertex, string pathFragment)
		{
			VertexShader = NewShader(ShaderType.VertexShader,pathVertex);
			FragmentShaders = NewShader(ShaderType.FragmentShader,pathFragment);

			MainProgram = GL.CreateProgram();
			GL.AttachShader(MainProgram, VertexShader);
			GL.AttachShader(MainProgram, FragmentShaders);

			GL.LinkProgram(MainProgram);
			int errCode = 0;
			GL.GetProgram(MainProgram, GetProgramParameterName.LinkStatus, out errCode);
            if (errCode != (int)All.True)
            {
				throw new Exception($"Link error.\n Error message: {GL.GetProgramInfoLog(MainProgram)}");
			}

			DeleteShader(VertexShader);
			DeleteShader(FragmentShaders);
		}

		public void ActiveProgram() => GL.UseProgram(MainProgram);

		public void DeactiveProgram() => GL.UseProgram(0);

		public void SetUniform4(string name, Vector4 vector)
		{
			int location = GL.GetUniformLocation(MainProgram, name);
			GL.Uniform4(location, vector);
		}

		public void SetMatrix4(string name,Matrix4 vector)
		{
			int location = GL.GetUniformLocation(MainProgram, name);
			GL.UniformMatrix4(location, true,ref vector);
		}

		public void SetUniform3(string name, Vector3 vector)
		{
			int location = GL.GetUniformLocation(MainProgram, name);
			GL.Uniform3(location, vector);
		}

		public void SetFloat(string name, float f)
		{
			int location = GL.GetUniformLocation(MainProgram, name);
			GL.Uniform1(location, f);
		}

		public void SetBool(string name, bool b)
		{
			int location = GL.GetUniformLocation(MainProgram, name);
			GL.Uniform1(location, Convert.ToInt32(b));
		}

		public int GetPropID(string name) => GL.GetAttribLocation(MainProgram, name);

		public void DeleteProgram() => GL.DeleteProgram(MainProgram);

		public int NewShader(ShaderType type, string path)
		{
			string shaderBase = File.ReadAllText(path);
			int shaderID = GL.CreateShader(type);
			GL.ShaderSource(shaderID, shaderBase);
			GL.CompileShader(shaderID);

			int errCode = 0;
			GL.GetShader(shaderID, ShaderParameter.CompileStatus,out errCode);
            if (errCode != (int)All.True)
            {
				throw new Exception($"Shader compile error. ShaderID {shaderID} known as {path}.\n Error message: {GL.GetShaderInfoLog(shaderID)}");
            }
			return shaderID;
		}

		private void DeleteShader(int shader)
		{
			GL.DetachShader(MainProgram, shader);
			GL.DeleteShader(shader);
		}

		~Shaders()
		{

		}
	}
}

