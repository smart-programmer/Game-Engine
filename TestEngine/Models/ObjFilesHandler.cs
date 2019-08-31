using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Linq;

// parsig an obj file in 2 steps
// 1- extracting all the diffrent information and putting them in arrays
// ordering the information

namespace TestEngine
{
    public class ObjFilesHandler
    {
        public static ParsedObjFile ParseObjFile(string fileName)
        {
            string[] fileString = File.ReadAllLines(fileName);

            List<float> vertices = new List<float>();
            List<textureCoordinate> textureCoords = new List<textureCoordinate>();
            List<normalCoordinate> normals = new List<normalCoordinate>();
            List<uint> indicees = new List<uint>();
            List<int> faces = new List<int>();

            for(int i = 0; i < fileString.Length; i++)
            {
                string[] splitedLine = fileString[i].Split(' ');
                //Console.WriteLine(splitedLine[0]);
                if ((splitedLine[0] == "v"))
                {
                    vertices.Add(float.Parse(splitedLine[1], CultureInfo.InvariantCulture.NumberFormat));
                    vertices.Add(float.Parse(splitedLine[2], CultureInfo.InvariantCulture.NumberFormat));
                    vertices.Add(float.Parse(splitedLine[3], CultureInfo.InvariantCulture.NumberFormat));
                    //Console.WriteLine(String.Format("{0} {1} {2}", splitedLine[1], splitedLine[2], splitedLine[3]));
                }
                if (splitedLine[0] == "vt" )
                {
                    textureCoordinate coordinate = new textureCoordinate(float.Parse(splitedLine[1]), float.Parse(splitedLine[2]));
                    textureCoords.Add(coordinate);

                    //Console.WriteLine(String.Format("{0} {1}", float.Parse(splitedLine[1]), float.Parse(splitedLine[2])));
                }
                if (splitedLine[0] == "vn")
                {
                    normals.Add(new normalCoordinate(float.Parse(splitedLine[1]), float.Parse(splitedLine[2]), float.Parse(splitedLine[3])));
                }
                if (splitedLine[0] == "f")
                {
                    // add only vertex positions indices to the indices array
                    indicees.Add(Convert.ToUInt32(splitedLine[1].Split('/')[0]) -1);
                    indicees.Add(Convert.ToUInt32(splitedLine[2].Split('/')[0]) -1);
                    indicees.Add(Convert.ToUInt32(splitedLine[3].Split('/')[0]) -1);

                    // then add the uv indices and normal indices to the faces array so we can order them with the order function so they can match the vertex position indices in the vao

                    for (int j = 0; j < 3; j++)
                    {
                        // add uv index and normal index from first vertex in line
                        faces.Add(int.Parse(splitedLine[1].Split('/')[j]));
                        //Console.WriteLine(int.Parse(splitedLine[1].Split('/')[j]));
                    }
                    for (int j = 0; j < 3; j++)
                    {
                        // add uv index and normal index from second vertex in line
                        faces.Add(int.Parse(splitedLine[2].Split('/')[j]));
                        //Console.WriteLine(int.Parse(splitedLine[2].Split('/')[j]));
                    }
                    for (int j = 0; j < 3; j++)
                    {
                        // add uv index and normal index from third vertex in line
                        faces.Add(int.Parse(splitedLine[3].Split('/')[j]));
                        //Console.WriteLine(int.Parse(splitedLine[3].Split('/')[j]));
                    }
                }
            }
          
            float[] newNormals = new float[vertices.Count];
            float[] newTextureUVS = new float[(int)(vertices.Count / 1.5)];
                
            OrderInformation(ref textureCoords, normals, faces, vertices.Count, ref newTextureUVS, ref newNormals);


            return new ParsedObjFile(vertices.ToArray(), indicees.ToArray(), newTextureUVS, newNormals);
        }

        public static void OrderInformation(ref List<textureCoordinate> uvs, List<normalCoordinate> normals, List<int> faces, int verticesCount, ref float[] newTextureUVS, ref float[] newNormals)
        {
            textureCoordinate[] textureCoordinates = new textureCoordinate[(int)(verticesCount / 1.5)]; // because every vertex position has a texture coordinate attached to it but a texture coordinate is composed of xy only and vertex positions are xyz so to figure how many texture coordinates are going to be pared with vertex position we simply caculate the number of texture coordinates if they we only xy like tetxure cooridnates and we do that by multiplying it with 1.5 wich cuts the third column : read obj_loader_docs
            normalCoordinate[] normalCoordinates = new normalCoordinate[verticesCount];

            for (int i = 0; i < faces.Count; i += 3)
            {
                textureCoordinates[faces[i] - 1] = uvs[faces[i + 1] - 1]; // mince 1 because obj file orders starting from 1 not 0
                normalCoordinates[faces[i] - 1] = normals[faces[i + 2] - 1];
            }

            //int v = 1;
            //for (int i = 0; i < textureCoordinates.Length; i++)
            //{
            //    Console.WriteLine(String.Format("{0} {1}", v, textureCoordinates[i]));
            //    v++;
            //}

            float[] textureCoordinatesArray = new float[textureCoordinates.Length];
            
            int a = 0;
            for (int j = 0; j < textureCoordinatesArray.Length; j += 2)
            {
                if (textureCoordinates[a] != null) // because somtimes a vertex position is not mentioned in faces so no texture coord will be set to it's location and that will be a null refrence pointer
                {
                    textureCoordinatesArray[j] = textureCoordinates[a].x;
                    textureCoordinatesArray[j + 1] = 1 - textureCoordinates[a].y;
                }
                a++;
            }

            float[] normalCoordinatesArray = new float[normalCoordinates.Length];

            a = 0;
            for (int j = 0; j < normalCoordinates.Length; j += 3)
            {
                if(normalCoordinates[a] != null) // just in case theres a veretx position that isn't mentioned in the faces
                {
                    normalCoordinatesArray[j] = normalCoordinates[a].x;
                    normalCoordinatesArray[j + 1] = normalCoordinates[a].y;
                    normalCoordinatesArray[j + 2] = normalCoordinates[a].z;
                }
                a++;
            }


            newTextureUVS = textureCoordinatesArray;
            newNormals = normalCoordinatesArray;

        }

    }





    public class ParsedObjFile
    {
        public float[] vertices { set; get; }
        public uint[] indices { set; get; }
        public float[] uvTextureCoords { set; get; }
        public float[] surfaceNormals { set; get; }

        public ParsedObjFile(float[] Vertices, uint[] Indices, float[] uvTexturecoords, float[] SurfaceNormals)
        {
            vertices = Vertices;
            indices = Indices;
            uvTextureCoords = uvTexturecoords;
            surfaceNormals = SurfaceNormals;
        }
    }

    public class textureCoordinate
    {
        public float x;
        public float y;

        public textureCoordinate(float X, float Y)
        {
            x = X;
            y = Y;
        }
    }

    public class normalCoordinate
    {
        public float x;
        public float y;
        public float z;

        public normalCoordinate(float X, float Y, float Z)
        {
            x = X;
            y = Y;
            z = Z;
        }
    }
}
