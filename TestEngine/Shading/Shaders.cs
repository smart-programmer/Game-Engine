using System;
using OpenGL;
using System.Text;

namespace TestEngine
{

    public class StaticShader : BaseShaderProgram
    {
        private static string[] vertexshaderString = Utils.GetValidShaderStringArray("..\\..\\shaders/VertexShader.txt");
        private static string[] fragmentShader = Utils.GetValidShaderStringArray("..\\..\\shaders/FragmentShader.txt");

        private int location_rand { set; get; }
        private int location_TransformationMatrix { set; get; }
        private int location_projectionMatrix { set; get; }
        private int location_ViewMatrix { set; get; }

        public StaticShader() : base(vertexshaderString, fragmentShader) { }


        protected override void BindAttributes()
        {
            BindAttribute("positions", 0); // this tells the shaders that that the vertex positions are stored in attribute list index 0 of the vao
            BindAttribute("textureCoords", 1);
        }

        protected override void GetAllUniformLocations()
        {
            location_rand = GetUniformLocation("rand");
            location_TransformationMatrix = GetUniformLocation("transformationMatrix");
            location_projectionMatrix = GetUniformLocation("projectionMatrix");
            location_ViewMatrix = GetUniformLocation("viewMatrix");
        }

        public void loadTransformationMatrix(Matrix4x4f Tmatrix)
        {
            LoadMatrix4f(location_TransformationMatrix, Tmatrix);
        }

        public void loadProjectionMatrix(Matrix4x4f Pmatrix)
        {
            LoadMatrix4f(location_projectionMatrix, Pmatrix);
        }

        public void loadViewMatrix(Matrix4x4f Vmatrix)
        {
            LoadMatrix4f(location_ViewMatrix, Vmatrix);
        }
    }
}
