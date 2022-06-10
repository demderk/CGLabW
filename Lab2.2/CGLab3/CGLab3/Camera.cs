using System;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;

namespace CGLab3
{
    public class Camera
    {
        public float Yaw { get; set; } = 260f;
        public float Pitch { get; set; } = 0f;
        public float MoveSpeed { get; set; } = 0.05f;
        public float MouseSensitivity { get; set; } = 0.18f;
        public Matrix4 View { get; private set; }
        public Matrix4 Model { get; private set; }
        public Matrix4 Projection { get; private set; }
        public Vector3 Position => position;
        public Vector2i ViewPort { get; set; } = new Vector2i(1280, 720);


        private bool firstMove = true;
        private Vector2 lastPos;
        private Vector3 position = new Vector3(0.0f, 0.0f, 0.0f);
        private Vector3 front = new Vector3(0.0f, 0.0f, -1.0f);
        private Vector3 up = new Vector3(0.0f, 1.0f, 0.0f);

        public Camera()
        {
            Projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45.0f), ViewPort.X / ViewPort.Y, 0.1f, 100.0f);
            Model = Matrix4.CreateRotationX(MathHelper.DegreesToRadians(0));

            // Start Camera
            RebuildMouseCamera();
        }

        public void FrameHandler(FrameEventArgs args)
        {
            View = Matrix4.LookAt(position, position + front, up);
        }

        public void MouseHandler(MouseMoveEventArgs mouse)
        {
            if (firstMove)
            {
                lastPos = new Vector2(mouse.X, mouse.Y);
                firstMove = false;
            }
            else
            {
                float deltaX = mouse.X - lastPos.X;
                float deltaY = mouse.Y - lastPos.Y;
                lastPos = new Vector2(mouse.X, mouse.Y);

                Yaw += deltaX * 0.1f;
                if (Pitch > 89.0f)
                {
                    
                    Pitch = 89.0f;
                }
                else if (Pitch < -89.0f)
                {
                    Pitch = -89.0f;
                }
                else
                {
                    Pitch -= deltaY * 0.1f;
                }
                RebuildMouseCamera();
            }


        }

        private void RebuildMouseCamera()
        {
            front.X = (float)Math.Cos(MathHelper.DegreesToRadians(Pitch)) * (float)Math.Cos(MathHelper.DegreesToRadians(Yaw));
            front.Y = (float)Math.Sin(MathHelper.DegreesToRadians(Pitch));
            front.Z = (float)Math.Cos(MathHelper.DegreesToRadians(Pitch)) * (float)Math.Sin(MathHelper.DegreesToRadians(Yaw));
        }

        public void MoveForward()
        {
            position += front * MoveSpeed; //Forward 
        }
        public void MoveBackwards()
        {
            position -= front * MoveSpeed; //Backwards
        }
        public void MoveLeft()
        {
            position -= Vector3.Normalize(Vector3.Cross(front, up)) * MoveSpeed; //Left
        }
        public void MoveRight()
        {
            position += Vector3.Normalize(Vector3.Cross(front, up)) * MoveSpeed; //Right
        }
        public void MoveUp()
        {
            position += up * MoveSpeed; //Up 
        }
        public void MoveDown()
        {
            position -= up * MoveSpeed; //Down
        }

    }
}

