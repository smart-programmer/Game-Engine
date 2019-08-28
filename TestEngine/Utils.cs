using System;
using System.IO;
using OpenGL;

namespace TestEngine
{
    class Utils
    {
        // use this method to load a shader string
        public static string[] GetValidShaderStringArray(string fileName)
        {
            string text = File.ReadAllText(fileName);

            return new string[] { text };
        }
    }

    class MyMath
    {
        private static float FOV = 70;
        private static float NEAR_PLANE = 0.1f;
        private static float FAR_PLANE = 1000;


        public static float[] GetMatrix4fBuffer(Matrix4x4f matrix)
        {
            // in column major order: https://en.wikipedia.org/wiki/Row-_and_column-major_order
            float[] matrixBuffer = { matrix.Column0.x, matrix.Column0.y, matrix.Column0.z, matrix.Column0.w,
                                    matrix.Column1.x, matrix.Column1.y, matrix.Column1.z, matrix.Column1.w,
                                    matrix.Column2.x, matrix.Column2.y, matrix.Column2.z, matrix.Column2.w,
                                    matrix.Column3.x, matrix.Column3.y, matrix.Column3.z, matrix.Column3.w, };
            
            return matrixBuffer;
        }


        public static Matrix4x4f CreateTransformationMatrix(Vertex3f translation, Vertex3f rotation, float scale)
        {
            Matrix4x4f TransfomrationMatrix = Get4x4IndentityMatrix();
            TransfomrationMatrix.Translate(translation.x, translation.y, translation.z);
            TransfomrationMatrix.RotateX(rotation.x);
            TransfomrationMatrix.RotateY(rotation.y);
            TransfomrationMatrix.RotateZ(rotation.z);
            TransfomrationMatrix.Scale(scale, scale, scale);
            return TransfomrationMatrix;
        }


        public static Matrix4x4f Get4x4IndentityMatrix()
        {
            Matrix4x4f identityMatrix = new Matrix4x4f(1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1);
            return identityMatrix;
        }

        public static Matrix4x4f createProjectionMatrix(float windowWidth, float windowHeight)
        {
            float aspectRatio = (float)windowWidth / windowHeight;
            float y_scale = 1f / (float)Math.Tan(ConvertToRadians(FOV / 2f));
            float x_scale = y_scale / aspectRatio;
            float frustum_length = FAR_PLANE - NEAR_PLANE;

            float[] identityMatrix = GetMatrix4fBuffer(Get4x4IndentityMatrix());



            identityMatrix[0] = x_scale;
            identityMatrix[5] = y_scale;
            identityMatrix[10] = -((FAR_PLANE + NEAR_PLANE) / frustum_length);
            identityMatrix[11] = -1;
            identityMatrix[14] = -((2 * NEAR_PLANE * FAR_PLANE) / frustum_length);
            identityMatrix[15] = 0;

            return new Matrix4x4f(identityMatrix);
        }

        public static float ConvertToRadians(float angle)
        {
            return (angle) * ((float)Math.PI / 180);
        }


        public static Matrix4x4f CreateViewMatrix(Camera camera)
        {
            // how we imitate a camera is basically create a transformation matrix opposit to where the camera wan't to go and multiply it to all objects so the objects move opposit to where the camera wan't to go and that matrix is called the view matrix
            Matrix4x4f identityMatrix = Get4x4IndentityMatrix();
            //identityMatrix.RotateX(camera.pitch);
            //identityMatrix.RotateY(camera.yaw);
            identityMatrix.Translate(-camera.position.x, -camera.position.y, -camera.position.z);
            return identityMatrix;
        }
    }
    

}
