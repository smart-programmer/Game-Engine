using System;
using OpenGL;
using Glfw3;


namespace TestEngine
{
    class Program
    {
        public static int width = 900, height = 500;

        static void Main(string[] args)
        {
            Gl.Initialize();

            // set the directory for the main dll
            Glfw.ConfigureNativesDirectory("..\\..\\..\\packages/glfw");

            // initialize glfw
            if (!Glfw.Init())
                Environment.Exit(-1);

            // create a window in windoesd mode 
            Glfw.Window window = Glfw.CreateWindow(width, height, "Test Engine");
            if (!window)
            {
                Glfw.Terminate();
                Environment.Exit(-1);
            }
            

            // set the created window to be the current context
            Glfw.MakeContextCurrent(window);
            

            StaticShader shaderProgram = new StaticShader();
            Camera camera1 = new Camera(0, 0, 0);

            ParsedObjFile obj = ObjFilesHandler.ParseObjFile("..\\..\\res/stall.obj");
            Texture texture2 = TextureLoader.getTexture("..\\..\\res/stallTexture.png");
            TexturedIndexedModel stall = ModelsCreatorAndHandler.SetObject(obj.vertices, obj.indices, obj.uvTextureCoords, texture2);
            Entity entity1 = new Entity(stall, new Vertex3f(0, 0, -5f), new Vertex3f(0, 0, 0), 1);

            Renderer.UseProjectionMatrix(shaderProgram, width, height); // we all need to load the projection matrix on time because it's not going to change unless we resize the window


            // Loop until the user closes the window
            while (!Glfw.WindowShouldClose(window))
            {
                // Render here
                camera1.ListenToCameraMoveEvents(window);
                entity1.increaseRotation(0f, 0.5f, 0f);
                //entity1.increasePosition(0, 0, -1f);
                Renderer.prepareForRendering();
                shaderProgram.StartShaderProgram();
                Renderer.render(entity1, shaderProgram, camera1);
                shaderProgram.StopShaderProgram();

                //Swap front and back buffers
                Glfw.SwapBuffers(window);

                // Poll for and process events
                Glfw.PollEvents();
            }

            TextureLoader.CleanUpTextures();
            shaderProgram.CleanUp();
            ModelsCreatorAndHandler.CleanUpMemmory();

            // terminate program
            Glfw.Terminate();
            Environment.Exit(-1);
        }
    }
}
