using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestEngine
{
    class OldStuff
    {

        public static void oldStuff()
        {
            //float[] squarevertices = {
            //    -0.5f, 0.5f, 0f,
            //    -0.5f, -0.5f, 0f,
            //    0.5f, -0.5f, 0f,
            //    0.5f, -0.5f, 0f,
            //    0.5f, 0.5f, 0f,
            //    -0.5f, 0.5f, 0f
            //  }; // order of vertices doesn't matter




            //Gl.BindVertexArray(obj.vaoID);
            //Gl.EnableVertexAttribArray(0);
            //Gl.DrawArrays(PrimitiveType.Triangles, 0, obj.Vertices.Length / 3); // devided by 3 because every vertex has x, y, z
            //Gl.DisableVertexAttribArray(0);
            //Gl.BindVertexArray(0);



            //public static RawindexedModel SetObject(float[] vertices, uint[] indices, float[] colors)
            //{
            //    uint vao = CreateVao();
            //    Gl.BindVertexArray(vao);
            //    bindindicesBuffer(indices);
            //    uint Positionsvbo = CreateVbo();
            //    StoreDataInVbo(Positionsvbo, vertices);
            //    uint colorsVbo = CreateVbo();
            //    StoreDataInVbo(colorsVbo, colors);
            //    PutVboInAtribbuteList(0, Positionsvbo);
            //    PutVboInAtribbuteList(1, colorsVbo);
            //    Gl.BindVertexArray(0);
            //    return new RawindexedModel(vao, vertices, indices);
            //}







        //    float[] CubeVerticesrtices = {
        //        -0.5f,0.5f,-0.5f,
        //        -0.5f,-0.5f,-0.5f,
        //        0.5f,-0.5f,-0.5f,
        //        0.5f,0.5f,-0.5f,

        //        -0.5f,0.5f,0.5f,
        //        -0.5f,-0.5f,0.5f,
        //        0.5f,-0.5f,0.5f,
        //        0.5f,0.5f,0.5f,

        //        0.5f,0.5f,-0.5f,
        //        0.5f,-0.5f,-0.5f,
        //        0.5f,-0.5f,0.5f,
        //        0.5f,0.5f,0.5f,

        //        -0.5f,0.5f,-0.5f,
        //        -0.5f,-0.5f,-0.5f,
        //        -0.5f,-0.5f,0.5f,
        //        -0.5f,0.5f,0.5f,

        //        -0.5f,0.5f,0.5f,
        //        -0.5f,0.5f,-0.5f,
        //        0.5f,0.5f,-0.5f,
        //        0.5f,0.5f,0.5f,

        //        -0.5f,-0.5f,0.5f,
        //        -0.5f,-0.5f,-0.5f,
        //        0.5f,-0.5f,-0.5f,
        //        0.5f,-0.5f,0.5f

        //};

        //    uint[] Cubeindices = {
        //        0,1,3,
        //        3,1,2,
        //        4,5,7,
        //        7,5,6,
        //        8,9,11,
        //        11,9,10,
        //        12,13,15,
        //        15,13,14,
        //        16,17,19,
        //        19,17,18,
        //        20,21,23,
        //        23,21,22

        //};

        //    float[] squarevertices =
        //    {
        //        -0.5f, 0.5f, 0f, //[0]top left v
        //        -0.5f, -0.5f, 0f, //[1]bottom left v
        //        0.5f, 0.5f, 0f, //[2]top right v
        //        0.5f, -0.5f, 0f //[3]bottom right v
        //    };
        //    float[] textureCoords = {

        //        0,0,
        //        0,1,
        //        1,1,
        //        1,0,
        //        0,0,
        //        0,1,
        //        1,1,
        //        1,0,
        //        0,0,
        //        0,1,
        //        1,1,
        //        1,0,
        //        0,0,
        //        0,1,
        //        1,1,
        //        1,0,
        //        0,0,
        //        0,1,
        //        1,1,
        //        1,0,
        //        0,0,
        //        0,1,
        //        1,1,
        //        1,0


        //};

        //    float[] colors = {
        //        1, 1, 1,
        //        0, 0, 0,
        //        0.5f, 0.5f, 0,
        //        0, 0, 1
        //    };

        //    uint[] indices =
        //    {
        //        0, 1, 2, //top left triangle
        //        2, 3, 1 // bottom right triangle
        //    };

        //    float[] uvtextureCoords =
        //    {
        //        0, 0,// coords for v0
        //        0, 1, // coords for v1
        //        1, 0, // coords for v2
        //        1, 1 // coords for v3
        //    }; // same order that we put vertices in


        }

    }
}
