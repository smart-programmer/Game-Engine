using System;
using OpenGL;
using System.Collections.Generic;

namespace TestEngine
{
    class EntityRenderer
    {

        public StaticShader shader { set; get; }

        public EntityRenderer(StaticShader Shader)
        {
            shader = Shader;
        }

        public void render_multiple_entities(Dictionary<TexturedIndexedModel, List<Entity>> entitiesHashMap)
        {
            foreach (TexturedIndexedModel Tmodel in entitiesHashMap.Keys)
            {
                prepareTexturedModel(Tmodel);
                for (int i = 0; i < entitiesHashMap[Tmodel].Count; i++)
                {
                    prepareInstance(entitiesHashMap[Tmodel][i]);
                    Gl.DrawElements(PrimitiveType.Triangles, entitiesHashMap[Tmodel][i].texturedModel.Indices.Length, DrawElementsType.UnsignedInt, IntPtr.Zero);
                }

                unbindTexturedModel();
            }
    
        }

        private void prepareTexturedModel(TexturedIndexedModel Tmodel)
        {
            shader.loadModelspecularLightData(Tmodel.metaData.shineDamper, Tmodel.metaData.reflectivity);
            shader.loadFakeLighitngBool(Tmodel.metaData.useFakeLighting);
            Gl.BindVertexArray(Tmodel.vaoID);
            Gl.EnableVertexAttribArray(0);
            Gl.EnableVertexAttribArray(1);
            Gl.EnableVertexAttribArray(2);
            UseTexture(TextureUnit.Texture0, Tmodel.TextureID);
            if(Tmodel.metaData.hasTransparency)
            {
                disableBackFaceCulling();
            }
        }

        private void prepareInstance(Entity entity)
        {
            UseTransformationMatrix(entity);
        }

        public void unbindTexturedModel()
        {
            Gl.DisableVertexAttribArray(0);
            Gl.DisableVertexAttribArray(1);
            Gl.DisableVertexAttribArray(2);
            Gl.BindVertexArray(0);
            enableBackFaceCulling();
        }

        private static void UseTexture(TextureUnit unitNUmberToPutTextureIn, uint TextureID)
        {
            Gl.ActiveTexture(unitNUmberToPutTextureIn); // reserve a texture unit to put the texture in
            Gl.BindTexture(TextureTarget.Texture2d, TextureID); // bind the texture to use it and pass it to the shader
        }

        private void UseTransformationMatrix(Entity entity)
        {
            Matrix4x4f transformationMatrix = MyMath.CreateTransformationMatrix(entity.position, entity.rotation, entity.scale);
            shader.loadTransformationMatrix(transformationMatrix);
        }

        private static void enableBackFaceCulling()
        {
            Gl.Enable(EnableCap.CullFace);
            Gl.CullFace(CullFaceMode.Back);
        }

        private static void disableBackFaceCulling()
        {
            Gl.Disable(EnableCap.CullFace);
        }
    }
}
