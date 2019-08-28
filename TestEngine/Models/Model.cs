using System;
using System.Collections.Generic;
using OpenGL;

namespace TestEngine
{
    public abstract class Model
    {
        public uint vaoID { set; get; }
        public float[] Vertices { set; get; }

        public Model(uint vaoid, float[] vertices)
        {
            this.vaoID = vaoid;
            this.Vertices = vertices;
        }

    }

    public class RawModel : Model
    {
        public RawModel(uint vaoid, float[] vertices) : base(vaoid, vertices)
        {
        }
    }


    public class RawindexedModel : Model
    {
        public uint[] Indices { set; get; }
        public RawindexedModel(uint vaoid, float[] vertices, uint[] indices) : base(vaoid, vertices) 
        {
            Indices = indices;
        }
    }


    public class ColoredndexedModel : Model
    {
        public uint[] Indices { set; get; }
        public float[] Colors { set; get; }

        public ColoredndexedModel(uint vaoid, float[] vertices, uint[] indices, float[] colors) : base(vaoid, vertices)
        {
            Indices = indices;
            Colors = colors;
        }
    }


    public class TexturedIndexedModel : Model
    {
        public uint[] Indices { set; get; }
        public uint TextureID { set; get; }
        public float[] TextureCoords { set; get; }

        public TexturedIndexedModel(uint vaoid, float[] vertices, uint[] indices, uint textureId, float[] textureCoords) : base(vaoid, vertices)
        {
            Indices = indices;
            TextureID = textureId;
            TextureCoords = textureCoords;
        }
    }

}
