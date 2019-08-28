using System;
using OpenGL;

namespace TestEngine
{
    class Entity
    {
        public TexturedIndexedModel texturedModel { set; get; }
        public Vertex3f position;
        public Vertex3f rotation; // https://stackoverflow.com/questions/1747654/cannot-modify-the-return-value-error-c-sharp
        public float scale { set; get; }

        public Entity(TexturedIndexedModel textured_model, Vertex3f Position, Vertex3f Rotation, float Scale)
        {
            texturedModel = textured_model;
            position = Position;
            rotation = Rotation;
            scale = Scale;
        }

        public void increasePosition(float x, float y, float z)
        {
            position.x += x;
            position.y += y;
            position.z += z;
        }

        public void increaseRotation(float rx, float ry, float rz)
        {
            rotation.x += rx;
            rotation.y += ry;
            rotation.z += rz;
        }
    }
}
