using System;


namespace TestEngine
{
    public class Fog
    {
        public float density { set; get; }
        public float gradient { set; get; }

        public Fog(float Density, float Gradient)
        {
            density = Density;
            gradient = Gradient;
        }
    }
}
