using System;
using OpenGL;
using System.Text;

namespace TestEngine
{
    public abstract class BaseShaderProgram
    {
        private uint programID { set; get; }
        private uint vertexShaderID { set; get; }
        private uint fragmentShaderID { set; get; }

        public BaseShaderProgram(string[] vertexshaderSource, string[] fragmentshaderSource)
        {
            programID = CreateShaderProgram();
            vertexShaderID = CreateShader(vertexshaderSource, ShaderType.VertexShader);
            fragmentShaderID = CreateShader(fragmentshaderSource, ShaderType.FragmentShader);
            uint[] shaders = { vertexShaderID, fragmentShaderID };
            AttachShaders(shaders); // attach shaders to the program
            BindAttributes();
            Gl.LinkProgram(programID);
            Gl.ValidateProgram(programID);
            GetAllUniformLocations();
        }


        private static uint CreateShaderProgram()
        {
            uint programid = Gl.CreateProgram();
            return programid;
        }

        public void StartShaderProgram()
        {
            Gl.UseProgram(programID);
        }

        public void StopShaderProgram()
        {
            Gl.UseProgram(0);
        }

        private uint CreateShader(string[] shaderString, ShaderType type)
        {
            uint shaderID = Gl.CreateShader(type);
            Gl.ShaderSource(shaderID, shaderString);
            Gl.CompileShader(shaderID);

            // debug shader 
            int status;
            Gl.GetShader(shaderID, ShaderParameterName.CompileStatus, out status);
            if (status == Gl.TRUE)
            {
                Console.WriteLine(String.Format("shader number: {0}, of type: {1} (creation succeeded)", shaderID, type));
                return shaderID;
            }
            else if (status == Gl.FALSE)
            {
                Console.WriteLine(String.Format("shader number: {0}, of type: {1} (creation failed)", shaderID, type));
                Gl.GetShader(shaderID, ShaderParameterName.InfoLogLength, out int logLength);
                int logMaxLength = 1024;
                StringBuilder infoLog = new StringBuilder(logMaxLength);
                Gl.GetShaderInfoLog(shaderID, logMaxLength, out int infoLogLength, infoLog);
                Console.WriteLine("Errors: \n{0}", infoLog.ToString());

                return 0;
            }

            return 0;
        }

        private void AttachShaders(uint[] shaderIDs)
        {
            // loop to attach all shaders
            for (int i = 0; i < shaderIDs.Length; i++)
            {
                Gl.AttachShader(programID, shaderIDs[i]);
            }
        }


        protected abstract void BindAttributes();

        protected void BindAttribute(string variableName, uint attributeNumber)
        {
            Gl.BindAttribLocation(programID, attributeNumber, variableName);
        }

        protected abstract void GetAllUniformLocations();

        protected int GetUniformLocation(string uniformName)
        {
            return Gl.GetUniformLocation(programID, uniformName);
        }

        protected void LoadVec3(int Location, Vertex3f value)
        {
            Gl.Uniform3f(Location, 1, value);
        }

        protected void LoadFloat(int Location, float value)
        {
            Gl.Uniform1f(Location, 1, value);
        }

        protected void loadInt(int location, int value)
        {
            Gl.Uniform1i(location, 1, value);
        }

        protected void LoadBoolean(int Location, bool value)
        {
            float floatToLoad = (value) ? 1 : 0;

            Gl.Uniform1f(Location, 1, floatToLoad);
        }

        protected void LoadMatrix4f(int Location, Matrix4x4f value)
        {
            Gl.UniformMatrix4(Location, false, MyMath.GetMatrix4fBuffer(value));
        }


        public void CleanUp()
        {
            StopShaderProgram();
            Gl.DetachShader(programID, vertexShaderID);
            Gl.DetachShader(programID, fragmentShaderID);
            Gl.DeleteShader(vertexShaderID);
            Gl.DeleteShader(fragmentShaderID);
            Gl.DeleteProgram(programID);
        }

    }
}

