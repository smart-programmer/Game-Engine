using System;
using OpenGL;
using System.Text;

namespace TestEngine
{

    public class StaticShader : BaseShaderProgram
    {
        private static string[] vertexshaderString = Utils.GetValidShaderStringArray("..\\..\\shaders/VertexShader.txt");
        private static string[] fragmentShader = Utils.GetValidShaderStringArray("..\\..\\shaders/FragmentShader.txt");

        private int location_TransformationMatrix { set; get; }
        private int location_projectionMatrix { set; get; }
        private int location_ViewMatrix { set; get; }
        private int location_LightPosition { set; get; }
        private int location_LightColour { set; get; }
        private int location_ShineDamper { set; get; }
        private int location_reflectivity { set; get; }
        private int location_useFakeLighting { set; get; }
        private int location_skyColor { set; get; }
        private int location_fogDensity { set; get; }
        private int location_fogGradient { set; get; }

        public StaticShader() : base(vertexshaderString, fragmentShader) { }


        protected override void BindAttributes()
        {
            BindAttribute("positions", 0); // this tells the shaders that that the vertex positions are stored in attribute list index 0 of the vao
            BindAttribute("textureCoords", 1);
            BindAttribute("normals", 2);
        }

        protected override void GetAllUniformLocations()
        {
            location_TransformationMatrix = GetUniformLocation("transformationMatrix");
            location_projectionMatrix = GetUniformLocation("projectionMatrix");
            location_ViewMatrix = GetUniformLocation("viewMatrix");
            location_LightPosition = GetUniformLocation("lightPosition");
            location_LightColour = GetUniformLocation("lightColour");
            location_ShineDamper = GetUniformLocation("shineDamper");
            location_reflectivity = GetUniformLocation("reflectivity");
            location_useFakeLighting = GetUniformLocation("useFakeLighting");
            location_skyColor = GetUniformLocation("skyColor");
            location_fogDensity = GetUniformLocation("fogDensity");
            location_fogGradient = GetUniformLocation("fogGradient");
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

        public void loadLightInformation(Light light)
        {
            LoadVec3(location_LightPosition, light.Position);
            LoadVec3(location_LightColour, light.Colour);
        }

        public void loadModelspecularLightData(float shineDamper, float reflectivity)
        {
            LoadFloat(location_ShineDamper, shineDamper);
            LoadFloat(location_reflectivity, reflectivity);
        }

        public void loadFakeLighitngBool(bool useFakeLighting)
        {
            LoadBoolean(location_useFakeLighting, useFakeLighting);
        }

        public void loadFogData(Fog fog)
        {
            LoadFloat(location_fogDensity, fog.density);
            LoadFloat(location_fogGradient, fog.gradient);
        }

        public void loadSkyColor(Sky sky)
        {
            LoadVec3(location_skyColor, new Vertex3f(sky.Color.R, sky.Color.G, sky.Color.B));
        }
    }



    public class TerrainShader : BaseShaderProgram
    {
        private static string[] vertexshaderString = Utils.GetValidShaderStringArray("..\\..\\shaders/terrainVertexShader.txt");
        private static string[] fragmentShader = Utils.GetValidShaderStringArray("..\\..\\shaders/terrainFragmentShader.txt");


        private int location_TransformationMatrix { set; get; }
        private int location_projectionMatrix { set; get; }
        private int location_ViewMatrix { set; get; }
        private int location_LightPosition { set; get; }
        private int location_LightColour { set; get; }
        private int location_ShineDamper { set; get; }
        private int location_reflectivity { set; get; }
        private int location_skyColor { set; get; }
        private int location_fogDensity { set; get; }
        private int location_fogGradient { set; get; }


        public TerrainShader() : base(vertexshaderString, fragmentShader) { }



        protected override void BindAttributes()
        {
            BindAttribute("positions", 0); // this tells the shaders that that the vertex positions are stored in attribute list index 0 of the vao
            BindAttribute("textureCoords", 1);
            BindAttribute("normals", 2);
        }

        protected override void GetAllUniformLocations()
        {
            location_TransformationMatrix = GetUniformLocation("transformationMatrix");
            location_projectionMatrix = GetUniformLocation("projectionMatrix");
            location_ViewMatrix = GetUniformLocation("viewMatrix");
            location_LightPosition = GetUniformLocation("lightPosition");
            location_LightColour = GetUniformLocation("lightColour");
            location_ShineDamper = GetUniformLocation("shineDamper");
            location_reflectivity = GetUniformLocation("reflectivity");
            location_skyColor = GetUniformLocation("skyColor");
            location_fogDensity = GetUniformLocation("fogDensity");
            location_fogGradient = GetUniformLocation("fogGradient");
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

        public void loadLightInformation(Light light)
        {
            LoadVec3(location_LightPosition, light.Position);
            LoadVec3(location_LightColour, light.Colour);
        }

        public void loadModelspecularLightData(float shineDamper, float reflectivity)
        {
            LoadFloat(location_ShineDamper, shineDamper);
            LoadFloat(location_reflectivity, reflectivity);
        }

        public void loadFogData(Fog fog)
        {
            LoadFloat(location_fogDensity, fog.density);
            LoadFloat(location_fogGradient, fog.gradient);
        }

        public void loadSkyColor(Sky sky)
        {
            LoadVec3(location_skyColor, new Vertex3f(sky.Color.R, sky.Color.G, sky.Color.B));
        }
    }
}

