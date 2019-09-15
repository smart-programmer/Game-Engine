using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
