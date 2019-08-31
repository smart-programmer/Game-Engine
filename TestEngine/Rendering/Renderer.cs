using System;
using OpenGL;
using System.Collections.Generic;

namespace TestEngine
{
    class Renderer
    {

      
        public static void prepareForRendering()
        {
            Gl.Enable(EnableCap.DepthTest);
            // clear previous rendered colours and replace them with the color red
            Gl.ClearColor(0, 0, 0, 1);
            Gl.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        }

        public static void render(Entity obj, StaticShader shader, Camera camera)
        {
            UseViewMatrix(shader, camera);
            UseTransformationMatrix(obj, shader);
            shader.loadModelspecularLightData(obj.texturedModel.metaData.shineDamper, obj.texturedModel.metaData.reflectivity);
            Gl.BindVertexArray(obj.texturedModel.vaoID);
            Gl.EnableVertexAttribArray(0);
            Gl.EnableVertexAttribArray(1);
            Gl.EnableVertexAttribArray(2);
            UseTexture(TextureUnit.Texture0, obj.texturedModel.TextureID);
            Gl.DrawElements(PrimitiveType.Triangles, obj.texturedModel.Indices.Length, DrawElementsType.UnsignedInt, IntPtr.Zero);
            Gl.DisableVertexAttribArray(0);
            Gl.DisableVertexAttribArray(1);
            Gl.DisableVertexAttribArray(2);
            Gl.BindVertexArray(0);
        }

        public static void render_multiple(List<Entity> objs, StaticShader shader, Camera camera)
        {
            UseViewMatrix(shader, camera);
            shader.loadModelspecularLightData(objs[0].texturedModel.metaData.shineDamper, objs[0].texturedModel.metaData.reflectivity);
            Gl.BindVertexArray(objs[0].texturedModel.vaoID);
            Gl.EnableVertexAttribArray(0);
            Gl.EnableVertexAttribArray(1);
            Gl.EnableVertexAttribArray(2);
            UseTexture(TextureUnit.Texture0, objs[0].texturedModel.TextureID);
            for (int i = 0; i < objs.Count; i++)
            {
                UseTransformationMatrix(objs[i], shader);
                Gl.DrawElements(PrimitiveType.Triangles, objs[i].texturedModel.Indices.Length, DrawElementsType.UnsignedInt, IntPtr.Zero);
            }
            
            Gl.DisableVertexAttribArray(0);
            Gl.DisableVertexAttribArray(1);
            Gl.DisableVertexAttribArray(2);
            Gl.BindVertexArray(0);
        }

        private static void UseTexture(TextureUnit unitNUmberToPutTextureIn, uint TextureID)
        {
            Gl.ActiveTexture(unitNUmberToPutTextureIn); // reserve a texture unit to put the texture in
            Gl.BindTexture(TextureTarget.Texture2d, TextureID); // bind the texture to use it and pass it to the shader
        }

        private static void UseTransformationMatrix(Entity entity, StaticShader shader)
        {
            Matrix4x4f transformationMatrix = MyMath.CreateTransformationMatrix(entity.position, entity.rotation, entity.scale);
            shader.loadTransformationMatrix(transformationMatrix);
        }

        public static void UseProjectionMatrix(StaticShader shader, float windowWidth, float windowHeight)
        {
            shader.StartShaderProgram();
            shader.loadProjectionMatrix(MyMath.createProjectionMatrix(windowWidth, windowHeight));
            shader.StopShaderProgram();
        }

        public static void UseViewMatrix(StaticShader shader, Camera camera)
        {
            shader.loadViewMatrix(MyMath.CreateViewMatrix(camera));
        }
    }
}
