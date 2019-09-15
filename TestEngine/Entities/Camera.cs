using System;
using OpenGL;
using Glfw3;

namespace TestEngine
{
    public class Camera
    {
        public Vertex3f position;
        public float pitch; // x rotation
        public float yaw; // y rotation
        public float roll; // z rotation

        public Camera(Vertex3f Position, float Pitch, float Yaw, float Roll)
        {
            position = Position;
            pitch = Pitch;
            yaw = Yaw;
            roll = Roll;
        }

        public void ListenToCameraMoveEvents(Glfw.Window window)
        {
            if(Glfw.GetKey(window, (int)Glfw.KeyCode.W))
            {
                position.z -= 0.2f;
            }
            if (Glfw.GetKey(window, (int)Glfw.KeyCode.D))
            {
                position.x += 0.2f;
            }
            if (Glfw.GetKey(window, (int)Glfw.KeyCode.A))
            {
                position.x -= 0.2f;
            }
            if (Glfw.GetKey(window, (int)Glfw.KeyCode.S))
            {
                position.z += 0.2f;
            }
            if (Glfw.GetKey(window, (int)Glfw.KeyCode.Space))
            {
                position.y += 0.2f;
            }
            if (Glfw.GetKey(window, (int)Glfw.KeyCode.C))
            {
                position.y -= 0.2f;
            }

            if (Glfw.GetKey(window, (int)Glfw.KeyCode.LeftShift))
            {
                if (Glfw.GetKey(window, (int)Glfw.KeyCode.W))
                {
                    position.z -= 2.5f;
                }
                if (Glfw.GetKey(window, (int)Glfw.KeyCode.D))
                {
                    position.x += 2.5f;
                }
                if (Glfw.GetKey(window, (int)Glfw.KeyCode.A))
                {
                    position.x -= 2.5f;
                }
                if (Glfw.GetKey(window, (int)Glfw.KeyCode.S))
                {
                    position.z += 2.5f;
                }
                if (Glfw.GetKey(window, (int)Glfw.KeyCode.Space))
                {
                    position.y += 2.5f;
                }
                if (Glfw.GetKey(window, (int)Glfw.KeyCode.C))
                {
                    position.y -= 2.5f;
                }
            }
        }
    }


}
