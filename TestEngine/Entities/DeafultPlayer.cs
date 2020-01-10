using System;
using OpenGL;
using Glfw3;


namespace TestEngine
{
    class DeafultPlayer : Entity
    {
        private float runSpeed = 300;
        private float turnSpeed = 160;
        private float gravity = -180;
        private float jumpPower = 90;
        private bool isInAir = false;

        private float terrainHight = 0;

        private float currentSpeed = 0;
        private float currentTurnSpeed = 0;
        private float upwardsSpeed = 0;

        public DeafultPlayer(TexturedIndexedModel textured_model, Vertex3f Position, Vertex3f Rotation, float Scale)
            : base(textured_model, Position, Rotation, Scale)
        {

        }

        public void ListenToPlayerMoveEvents(float deltaTime, Glfw.Window window)
        {
            checkInputs(window);
            Console.WriteLine(String.Format("Delta Time: {0}", deltaTime));
            increaseRotation(0, currentTurnSpeed * deltaTime, 0);

            float playerMovementDistanceInTheYAxis = currentSpeed * deltaTime;
            float dx = (float) (playerMovementDistanceInTheYAxis * Math.Sin(MyMath.ConvertToRadians(rotation.y)));
            float dz = (float)(playerMovementDistanceInTheYAxis * Math.Cos(MyMath.ConvertToRadians(rotation.y)));
            increasePosition(dx, 0, dz);
            increasePosition(0, upwardsSpeed * deltaTime, 0);
            enableGravitationalEffect(deltaTime);
        }

        private void enableGravitationalEffect(float deltaTime)
        {
            upwardsSpeed += (gravity * deltaTime);
            if (position.y < terrainHight)
            {
                isInAir = false;
                upwardsSpeed = 0;
                position.y = terrainHight;
            }
        }

        private void jump()
        {
            isInAir = true;
            upwardsSpeed = jumpPower;
        }


        private void checkInputs(Glfw.Window window)
        {
            if (Glfw.GetKey(window, (int)Glfw.KeyCode.Up))
            {
                currentSpeed = runSpeed;
            }
            else if (Glfw.GetKey(window, (int)Glfw.KeyCode.Down))
            {
                currentSpeed = -runSpeed;
            }
            else
            {
                currentSpeed = 0;
            }

            if (Glfw.GetKey(window, (int)Glfw.KeyCode.Right))
            {
                currentTurnSpeed = -turnSpeed;
            }
            else if (Glfw.GetKey(window, (int)Glfw.KeyCode.Left))
            {
                currentTurnSpeed = turnSpeed;
            }
            else
            {
                currentTurnSpeed = 0;
            }

            if (Glfw.GetKey(window, (int)Glfw.KeyCode.RightShift))
            {
                if (!isInAir)
                {
                    jump();
                }
            }
            
            
        }


    }
}
