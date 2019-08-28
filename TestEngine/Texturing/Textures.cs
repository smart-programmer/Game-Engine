using System;
using OpenGL;
using System.Drawing;
using System.Collections.Generic;

// 1- get an image 
// 2- store it's pixel data in a byte array so we can pass that to opengl 
// 3- generate a texture 
// 4- bind that texture's id and fill information about that texture like what to do when the texture needs to be magnifyed or minifyed etc... with Gl.TexParameteri
// 5- pass the image data to opengl via the function Gl.TexImage2D
// 6- unbind the texture
// now we have set a texture and filled it's information and returned it's id


namespace TestEngine
{
    public class Texture
    {
        public Bitmap image { set; get; }
        public byte[] pixeslData { set; get; }
        public uint textureID { set; get; }


        public Texture(Bitmap bitmap)
        {
            this.image = bitmap;
            pixeslData = TextureLoader.SetImageBuffer(image);

            textureID = Gl.GenTexture();
            Gl.BindTexture(TextureTarget.Texture2d, textureID);

            // https://www.khronos.org/registry/OpenGL-Refpages/gl4/html/glTexParameter.xhtml
            // https://gamedev.stackexchange.com/questions/62548/what-does-changing-gl-texture-wrap-s-t-do

            int value = Gl.REPEAT;
            Gl.TexParameteri(TextureTarget.Texture2d, TextureParameterName.TextureWrapS, value);
            Gl.TexParameteri(TextureTarget.Texture2d, TextureParameterName.TextureWrapT, value);

            value = Gl.NEAREST;
            Gl.TexParameteri(TextureTarget.Texture2d, TextureParameterName.TextureMinFilter, value);
            Gl.TexParameteri(TextureTarget.Texture2d, TextureParameterName.TextureMagFilter, value);

            Gl.GenerateMipmap(TextureTarget.Texture2d);

            Gl.TexImage2D(TextureTarget.Texture2d, 0, InternalFormat.Rgb, image.Width, image.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, pixeslData); // InternalFormat is what we want the texture to be in opengl for example we might want it as rgb not rgba and opengl will convert it to what we want but PixelFormat is asking us what is the original image's format so that when we give opengl pixel bytes it knosw how to interpret them
            Gl.BindTexture(TextureTarget.Texture2d, 0);
        }

    }


    public class TextureLoader
    {
        public static List<uint> TexturesIDs = new List<uint>();

        public static Texture getTexture(string path)
        {
            return new Texture(new Bitmap(path));
        }


        public static byte[] SetImageBuffer(Bitmap image)
        {
            image.RotateFlip(RotateFlipType.Rotate270FlipY);

            byte[] data = new byte[image.Width * image.Height * 4]; // 4 is because width * height == nuumber of pixels in image but every pixel has 4 bytes which are Red, Green, Blue, Alpha
            int index = 0;
            for (int i = 0; i < image.Width; i++)
            {
                for (int j = 0; j < image.Height; j++)
                {
                    Color pixel = image.GetPixel(i, j);
                    byte red = pixel.R;
                    byte green = pixel.G;
                    byte blue = pixel.B;
                    byte Alpha = pixel.A;


                    data[index] = red;
                    index++;
                    data[index] = green;
                    index++;
                    data[index] = blue;
                    index++;
                    data[index] = Alpha;
                    index++;
                }
            }

            // result: { red, green, blue, alpha, red, green, blue alpha, red, green, blue, alpha, ...}

            return data;
        }


        public static void CleanUpTextures()
        {
            Gl.DeleteTextures(TexturesIDs.ToArray());
        }
    }
}
