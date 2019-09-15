using System;
using System.Collections.Generic;
using OpenGL;

namespace TestEngine
{
    public class ModelsCreatorAndHandler
    {
        public static List<uint> vaoIDs = new List<uint>(); // this must be in this form because puting it in the form: public static List<uint> vaoIDs { set; get; } will generate a NullReferenceException error
        public static List<uint> vboIDs = new List<uint>();

        public static TexturedIndexedModel SetObject(float[] vertices, uint[] indices, float[] textureCoords, Texture texture, float[] normals)
        {
            uint vao = CreateVao();
            Gl.BindVertexArray(vao);
            bindindicesBuffer(indices);
            uint Positionsvbo = CreateVbo();
            StoreDataInVbo(Positionsvbo, vertices);
            uint textureCoordsVBO = CreateVbo();
            StoreDataInVbo(textureCoordsVBO, textureCoords);
            uint normalsVBO = CreateVbo();
            StoreDataInVbo(normalsVBO, normals);
            PutVboInAtribbuteList(0, Positionsvbo, 3);
            PutVboInAtribbuteList(1, textureCoordsVBO, 2);
            PutVboInAtribbuteList(2, normalsVBO, 3);
            Gl.BindVertexArray(0);
            return new TexturedIndexedModel(vao, vertices, indices, texture.textureID, textureCoords);
        }


        private static uint CreateVao()
        {
            uint vaoid = Gl.GenVertexArray();
            vaoIDs.Add(vaoid);
            return vaoid;
        }

        private static uint CreateVbo()
        {
            uint vboid = Gl.GenBuffer();
            vboIDs.Add(vboid);
            return vboid;
        }

        private static void StoreDataInVbo(uint vboid, float[] data)
        {
            Gl.BindBuffer(BufferTarget.ArrayBuffer, vboid);
            Gl.BufferData(BufferTarget.ArrayBuffer, (uint)(data.Length * sizeof(float)), data, BufferUsage.StaticDraw); // static draw because we're not going to change the content of this buffer object
            Gl.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        private static void PutVboInAtribbuteList(uint attribListNumber, uint vboid, int sizeOfEachVertex) // size of each vertex for example: position are size 3 because x,y,z but textureCoords are size 2 because just u,v
        {
            Gl.BindBuffer(BufferTarget.ArrayBuffer, vboid);
            Gl.VertexAttribPointer(attribListNumber, sizeOfEachVertex, VertexAttribType.Float, false, 0, IntPtr.Zero);
            Gl.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        private static void bindindicesBuffer(uint[] indices)
        {
            uint vboid = Gl.GenBuffer();
            vboIDs.Add(vboid);
            Gl.BindBuffer(BufferTarget.ElementArrayBuffer, vboid);
            Gl.BufferData(BufferTarget.ElementArrayBuffer, (uint)(indices.Length * sizeof(uint)), indices, BufferUsage.StaticDraw);
        }

        public static void CleanUpMemmory()
        {
            Gl.DeleteBuffers(vaoIDs.ToArray());
            Gl.DeleteBuffers(vboIDs.ToArray());
        }
    }



    public class ModelMetaData
    {
        public float shineDamper = 1;
        public float reflectivity = 0;
        public bool hasTransparency = false;
        public bool useFakeLighting = false;

        public ModelMetaData()
        {

        }

        public ModelMetaData(float shine_damper, float Reflectivity)
        {
            shineDamper = shine_damper;
            reflectivity = Reflectivity;
        }
    }
}
