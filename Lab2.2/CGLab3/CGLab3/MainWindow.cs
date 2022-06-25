using System;
using OpenTK;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Windowing.Common;
using System.Drawing;
using OpenTK.Mathematics;
using System.Collections.Generic;
using CGLab3.HouseElements;
using CGLab3.Primitives;
using CGLab3.ImportObjects;

namespace CGLab3
{
    public class MainWindow : GameWindow
    {


        private int _framesPerSecondElapsed;
        private float _frameTime;
        private Map Map;
        private int _vaoID = 0;
        private Shaders Shaders = null;
        private Camera GlobalCamera = new Camera();

        public int FramesPerSecond { get; private set; }

        public static NativeWindowSettings NativeSettings { get; } = new NativeWindowSettings()
        {
            APIVersion = new Version(4, 0),
            Flags = ContextFlags.ForwardCompatible,
            Size = new Vector2i(1280, 720)
        };

        public MainWindow() : base(new GameWindowSettings(), NativeSettings)
        {

            CursorVisible = false;
            CursorGrabbed = true;
            Title = "CGUI / FPS: %FPS% ";
            VSync = VSyncMode.On;

            Load += WindowLoad;

            UpdateFrame += FrameCounter;
            UpdateFrame += TitleUpdater;
            UpdateFrame += FrameProcessor;
            UpdateFrame += KeySpy;
            MouseMove += GlobalCamera.MouseHandler;
            UpdateFrame += GlobalCamera.FrameHandler;


            Map = new Map();



            BuildScene();
            Lighting();
        }

        private void WindowLoad()
        {
            GL.Enable(EnableCap.DepthTest);
            Shaders = new Shaders(@"data/shaders/shader_base.vert", @"data/shaders/shader_base.frag");
            _vaoID = BuildShaders();

        }

        private void KeySpy(FrameEventArgs obj)
        {
            var key = KeyboardState;

            if (key.IsKeyDown(Keys.Escape))
            {
                Close();
            }

            if (key.IsKeyDown(Keys.W))
            {
                GlobalCamera.MoveForward();
            }

            if (key.IsKeyDown(Keys.S))
            {
                GlobalCamera.MoveBackwards();
            }

            if (key.IsKeyDown(Keys.A))
            {
                GlobalCamera.MoveLeft();
            }

            if (key.IsKeyDown(Keys.D))
            {
                GlobalCamera.MoveRight();
            }

            if (key.IsKeyDown(Keys.Space))
            {
                GlobalCamera.MoveUp();

            }

            if (key.IsKeyDown(Keys.LeftShift))
            {
                GlobalCamera.MoveDown();
            }
        }



        private void DrawVAOShaders()
        {
            Shaders.ActiveProgram();
            GL.BindVertexArray(_vaoID);
            Shaders.SetMatrix4("model", GlobalCamera.Model);
            Shaders.SetMatrix4("view", GlobalCamera.View);
            Shaders.SetMatrix4("projection", GlobalCamera.Projection);
            Shaders.SetUniform3("camera", GlobalCamera.Position);
            Map.LightUpdate(Shaders);
            GL.DrawArrays(PrimitiveType.Triangles, 0, Map.PointsCount);
            Shaders.DeactiveProgram();
        }

        private int BuildShaders()
        {
            int vao = GL.GenVertexArray();
            GL.BindVertexArray(vao);
            int posLocation = Shaders.GetPropID("Position");
            int colorLocation = Shaders.GetPropID("Color");
            int normalLocation = Shaders.GetPropID("Normal");
            GL.EnableVertexAttribArray(posLocation);
            GL.EnableVertexAttribArray(colorLocation);
            GL.EnableVertexAttribArray(normalLocation);
            GL.BindBuffer(BufferTarget.ArrayBuffer, Map.DataVBOID);
            GL.VertexAttribPointer(posLocation, 3, VertexAttribPointerType.Float, false, 10 * sizeof(float), 0);
            GL.VertexAttribPointer(colorLocation, 3, VertexAttribPointerType.Float, false, 10 * sizeof(float), 3 * sizeof(float));
            GL.VertexAttribPointer(normalLocation, 3, VertexAttribPointerType.Float, false, 10 * sizeof(float), 7 * sizeof(float));
            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.DisableVertexAttribArray(posLocation);
            GL.DisableVertexAttribArray(colorLocation);
            return vao;
        }

        private void FrameProcessor(FrameEventArgs obj)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            DrawVAOShaders();
            SwapBuffers();
        }

        private void TitleUpdater(FrameEventArgs obj)
        {
            Title = $"CGUI / FPS: {FramesPerSecond}";
        }

        private void FrameCounter(FrameEventArgs obj)
        {
            _frameTime += (float)obj.Time;
            _framesPerSecondElapsed++;
            if (_frameTime >= 1.0f)
            {
                FramesPerSecond = _framesPerSecondElapsed;
                _framesPerSecondElapsed = 0;
                _frameTime = 0.0f;
            }
        }

        private void Lighting()
        {
            Map.GlobalLight = DirectLight.BuildDefault();
            Map.AddPointLight(PointLight.BuildLight(new FloatPoint3D(1050, 100, 500, 100), LightMode.Medium, Color.White));
            Map.AddPointLight(PointLight.BuildLight(new FloatPoint3D(1350, 100, 1350, 100), LightMode.Medium, Color.White));
            Map.AddPointLight(PointLight.BuildLight(new FloatPoint3D(900, 100, 900, 100), LightMode.Medium, Color.White));
        }

        private void BuildScene()
        {
            // HALL
            var leftWallHall = new Wall(new Point3D(0, 0, 5), new Point3D(1000, 0, 5), 30, 10);
            leftWallHall.Merge(new Wall(new Point3D(0, 30, 5), new Point3D(300, 30, 5), 100, 10));
            leftWallHall.Merge(new Wall(new Point3D(600, 30, 5), new Point3D(1000, 30, 5), 100, 10));
            leftWallHall.Merge(new Wall(new Point3D(0, 130, 5), new Point3D(1000, 130, 5), 30, 10));
            Map.AddShape(leftWallHall);
            var backWallHall = new Wall(new Point3D(1000 - 5, 0, 0), new Point3D(1000 - 5, 0, 700), 160, 10);
            backWallHall.Merge(new Wall(new Point3D(1000 - 5, 0, 800), new Point3D(1000 - 5, 0, 1000), 160, 10));
            Map.AddShape(backWallHall);
            var rightWallHall = new Wall(new Point3D(1000 - 5, 0, 1000 - 5), new Point3D(300, 0, 1000 - 5), 160, 10);
            rightWallHall.Merge(new Wall(new Point3D(200, 0, 1000 - 5), new Point3D(5, 0, 1000 - 5), 160, 10));
            Map.AddShape(rightWallHall);
            var frontWallHall = new Wall(new Point3D(5, 0, 1000), new Point3D(5, 0, 0), 160, 10);
            Map.AddShape(frontWallHall);
            Map.AddShape(new Cube(new Point3D(0, -30, 0), new Point3D(1000, -30, 0), new Point3D(1000, -30, 1000), new Point3D(0, -30, 1000), 30));
            Map.AddShape(new Cube(new Point3D(0, 160, 0), new Point3D(1000, 160, 0), new Point3D(1000, 160, 1000), new Point3D(0, 160, 1000), 30));

            // BEDROOM
            var backWallBedRoom = new Wall(new Point3D(1500 - 5, 0, 400), new Point3D(1500 - 5, 0, 1000), 160, 10);
            var leftWallBedroom = new Wall(new Point3D(1000, 0, 400 + 5), new Point3D(1500, 0, 400 + 5), 30, 10);
            leftWallBedroom.Merge(new Wall(new Point3D(1000, 30, 400 + 5), new Point3D(1200, 30, 400 + 5), 100, 10));
            leftWallBedroom.Merge(new Wall(new Point3D(1300, 30, 400 + 5), new Point3D(1500, 30, 400 + 5), 100, 10));
            leftWallBedroom.Merge(new Wall(new Point3D(1000, 130, 400 + 5), new Point3D(1500, 130, 400 + 5), 30, 10));
            var rightWallBedroom = new Wall(new Point3D(1500 - 5, 0, 1000 - 5), new Point3D(1300 - 5, 0, 1000 - 5), 160, 10);
            rightWallBedroom.Merge(new Wall(new Point3D(1200 - 5, 0, 1000 - 5), new Point3D(1000 - 5, 0, 1000 - 5), 160, 10));
            Map.AddShape(new Cube(new Point3D(1000, -30, 400), new Point3D(1500, -30, 400), new Point3D(1500, -30, 1000), new Point3D(1000, -30, 1000), 30));
            Map.AddShape(new Cube(new Point3D(1000, 160, 400), new Point3D(1500, 160, 400), new Point3D(1500, 160, 1000), new Point3D(1000, 160, 1000), 30));
            Map.AddShape(backWallBedRoom);
            Map.AddShape(leftWallBedroom);
            Map.AddShape(rightWallBedroom);


            // LUMBER
            var backWallLumber = new Wall(new Point3D(1400 - 5, 0, 1000 - 5), new Point3D(1400 - 5, 0, 1400), 160, 10);
            var rightWallLumber = new Wall(new Point3D(1400 - 10, 0, 1400 - 5), new Point3D(1100 + 5, 0, 1400), 160, 10);
            var frontWallLumber = new Wall(new Point3D(1100 + 5, 0, 1400), new Point3D(1100 + 5, 0, 1000), 160, 10);
            Map.AddShape(new Cube(new Point3D(1400, -30, 1000), new Point3D(1400, -30, 1400), new Point3D(1100, -30, 1400), new Point3D(1100, -30, 1000), 30));
            Map.AddShape(new Cube(new Point3D(1400, 160, 1000), new Point3D(1400, 160, 1400), new Point3D(1100, 160, 1400), new Point3D(1100, 160, 1000), 30));
            Map.AddShape(backWallLumber);
            Map.AddShape(rightWallLumber);
            Map.AddShape(frontWallLumber);

            //DECOR
            var TV = new Wall(new Point3D(15, 30, 400), new Point3D(15, 30, 600), 100, 10).ColorFill(Color.BlueViolet);
            var Schaf = new Wall(new Point3D(1120, 0, 1100), new Point3D(1120, 0, 1300), 160, 20).ColorFill(Color.SandyBrown);
            Map.AddShape(TV);
            Map.AddShape(Schaf);
            //Map.AddShape(new Prism(new Point3D(435, 0, 435), 40, 40, 5).ColorFill(Color.BlanchedAlmond));
            Map.AddShape(new Pyramid(new Point3D(1300, 0, 600), 300, 100, 300).ColorFill(Color.PaleGoldenrod));


            // Light Visualization
            Map.AddShape(new Cube(new Point3D(900, 100, 900), 10, 10, 10));
            Map.AddShape(new Cube(new Point3D(1050, 100, 500), 10, 10, 10));
            Map.AddShape(new Cube(new Point3D(1350, 100, 1350), 10, 10, 10));

            //InternalObject intern = new InternalObject();
            //intern.Import(@"/Users/roman/Desktop/untitled.obj");
            //Map.AddShape(intern);
        }
    }
}
// R<3A