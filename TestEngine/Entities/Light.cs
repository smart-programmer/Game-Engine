using System;
using OpenGL;

namespace TestEngine
{
    public class Light
    {
        public Vertex3f Position { set; get; }
        public Vertex3f Colour { set; get; }
        
        public Light(Vertex3f position, Vertex3f colour)
        {
            Position = position;
            Colour = colour;
        }
    }
}
