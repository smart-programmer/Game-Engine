using System;
using OpenGL;
using System.Collections.Generic;

namespace TestEngine
{
    class TerrainRenderer
    {
        public TerrainShader shader { set; get; }

        public TerrainRenderer(TerrainShader Shader)
        {
            shader = Shader;
        }

        public void render_multiple_terrains(Dictionary<TexturedIndexedModel, List<Terrain>> terrainsHashMap)
        {
            foreach (TexturedIndexedModel Tmodel in terrainsHashMap.Keys)
            {
                prepareTexturedModel(Tmodel);
                for (int i = 0; i < terrainsHashMap[Tmodel].Count; i++)
                {
                    prepareInstance(terrainsHashMap[Tmodel][i]);
                    Gl.DrawElements(PrimitiveType.Triangles, terrainsHashMap[Tmodel][i].terrainModel.Indices.Length, DrawElementsType.UnsignedInt, IntPtr.Zero);
                }

                unbindTexturedModel();
            }

        }

        private void prepareTexturedModel(TexturedIndexedModel Tmodel)
        {
            //Console.WriteLine(string.Format("{0} {1}", Tmodel.metaData.shineDamper, Tmodel.metaData.reflectivity));
            shader.loadModelspecularLightData(Tmodel.metaData.shineDamper, Tmodel.metaData.reflectivity);
            Gl.BindVertexArray(Tmodel.vaoID);
            Gl.EnableVertexAttribArray(0);
            Gl.EnableVertexAttribArray(1);
            Gl.EnableVertexAttribArray(2);
            UseTexture(TextureUnit.Texture0, Tmodel.TextureID);
        }

        private void prepareInstance(Terrain terrain)
        {
            UseTransformationMatrix(terrain);
        }

        public void unbindTexturedModel()
        {
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

        private void UseTransformationMatrix(Terrain terrain)
        {
            Matrix4x4f transformationMatrix = MyMath.CreateTransformationMatrix(terrain.Position, terrain.Rotation, terrain.Scale);
            shader.loadTransformationMatrix(transformationMatrix);
        }
    }
}

