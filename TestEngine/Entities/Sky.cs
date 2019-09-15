using System;
using OpenGL;

namespace TestEngine
{
    public class Sky
    {
        public RGBColor Color { set; get; }

        public Sky(RGBColor color)
        {
            Color = color;
        }
    }
}
