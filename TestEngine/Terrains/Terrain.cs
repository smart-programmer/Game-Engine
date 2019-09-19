using System;
using OpenGL;


namespace TestEngine
{
    class Terrain
    {
        private uint VERTEX_COUNT = 128; // width = 128, height = 128
        private int SIZE = 5000;

        public Vertex3f Position { set; get; }
        public Vertex3f Rotation { set; get; }
        public float Scale { set; get; }

        public TexturedIndexedModel terrainModel { set; get; }

        public TerrainTexturePack texturePack { set; get; }
        public Texture blendMap { set; get; }

        public Terrain(Vertex3f position, Vertex3f rotation, float scale, TerrainTexturePack TexturePack, Texture BlendMap)
        {
            Position = position;
            Rotation = rotation;
            Scale = scale;
            texturePack = TexturePack;
            blendMap = BlendMap;
            terrainModel = generateTerrainModel();
        }


        private TexturedIndexedModel generateTerrainModel()
        {
            // those two videos will make you understand the terrain code if god wills
            // https://www.youtube.com/watch?v=bG0uEXV6aHQ
            // https://www.youtube.com/watch?v=vFvwyu_ZKfU
            uint count = VERTEX_COUNT * VERTEX_COUNT;
            float[] vertices = new float[count * 3];
            float[] normals = new float[count * 3];
            float[] textureCoords = new float[count * 2];
            uint[] indices = new uint[6 * (VERTEX_COUNT - 1) * (VERTEX_COUNT - 1)]; // a single sqaure is six indices that's why we multiply the total number of vertices we're going to loop by six
            int vertexPointer = 0;

            // as we can see here the VERTEX_COUNT is used to determine how many times we loop wich means it determines how many vertices is there
            for (int i = 0; i < VERTEX_COUNT; i++)
            {
                for (int j = 0; j < VERTEX_COUNT; j++)
                {
                    // NOIE: i changed the next 3 lines and added (SIZE / 2) + for better terrain positioning 
                    vertices[vertexPointer * 3] = ((float)j / ((float)VERTEX_COUNT - 1)) * SIZE; // and here as we can see SIZE is determining the distance between every vertex that's why it's multiplied to each one (and there's also an addetion just to change the terrain position it could be any number we want but we chose it to be the size /2)
                    vertices[vertexPointer * 3 + 1] = 0;
                    vertices[vertexPointer * 3 + 2] = ((float)i / ((float)VERTEX_COUNT - 1)) * SIZE; // ((float)i / ((float)VERTEX_COUNT - 1)) is to convert to pixel coords to match the texture coords
                    //Console.WriteLine(((float)j / ((float)VERTEX_COUNT - 1)) * SIZE);
                    normals[vertexPointer * 3] = 0;
                    normals[vertexPointer * 3 + 1] = 1;
                    normals[vertexPointer * 3 + 2] = 0;
                    textureCoords[vertexPointer * 2] = (float)j / ((float)VERTEX_COUNT - 1);
                    textureCoords[vertexPointer * 2 + 1] = (float)i / ((float)VERTEX_COUNT - 1);
                    vertexPointer++;
                }
            }

            int pointer = 0;
            for (uint gz = 0; gz < VERTEX_COUNT - 1; gz++)
            {
                for (uint gx = 0; gx < VERTEX_COUNT - 1; gx++)
                {
                    uint topLeft = (gz * VERTEX_COUNT) + gx;
                    uint topRight = topLeft + 1;
                    uint bottomLeft = ((gz + 1) * VERTEX_COUNT) + gx;
                    uint bottomRight = bottomLeft + 1;
                    indices[pointer++] = topLeft;
                    indices[pointer++] = bottomLeft;
                    indices[pointer++] = topRight;
                    indices[pointer++] = topRight;
                    indices[pointer++] = bottomLeft;
                    indices[pointer++] = bottomRight;

                }
            }


            return ModelsCreatorAndHandler.SetObject(vertices, indices, textureCoords, blendMap, normals);
        }



    }
}
