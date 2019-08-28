using System;
using OpenGL;
using Glfw3;

namespace TestEngine
{
    class Camera
    {
        public Vertex3f position = new Vertex3f(0, 0, 0);
        public float pitch; // x rotation
        public float yaw; // y rotation
        public float roll; // z rotation

        public Camera(float Pitch, float Yaw, float Roll)
        {
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
        }
    }


}
