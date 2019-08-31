using System;
using OpenGL;
using Glfw3;
using System.Collections.Generic;


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
            Gl.Enable(EnableCap.CullFace);
            Gl.CullFace(CullFaceMode.Back);

            StaticShader shaderProgram = new StaticShader();
            Camera camera1 = new Camera(0, 0, 0);
            Light light = new Light(new Vertex3f(40, 15, 20), new Vertex3f(1f, 1f, 1f));

            ParsedObjFile obj = ObjFilesHandler.ParseObjFile("..\\..\\res/cube.obj");
            Texture texture2 = TextureLoader.getTexture("..\\..\\res/stallTexture.png");
            TexturedIndexedModel stall = ModelsCreatorAndHandler.SetObject(obj.vertices, obj.indices, obj.uvTextureCoords, texture2, obj.surfaceNormals);
            stall.metaData.shineDamper = 10f;
            stall.metaData.reflectivity = 1;
            Entity entity1 = new Entity(stall, new Vertex3f(0, 0, -5f), new Vertex3f(0, 0, 0), 1);

            Renderer.UseProjectionMatrix(shaderProgram, width, height); // we all need to load the projection matrix on time because it's not going to change unless we resize the window

            List<Entity> entities = new List<Entity>();
            Random r = new Random();
           
            int rand = r.Next(-100, 100);
            int x = r.Next(30, 80);
            int f = r.Next(1, 50);
            for (int i = 0; i < 1000; i++)
            {
                entities.Add(new Entity(stall, new Vertex3f((float)x, (float)rand, (float)f), new Vertex3f(rand, x, f), 1));
                rand = r.Next(-100, 100);
                x = r.Next(30, 80) * 6 - 320;
                f = r.Next(-100, -20) - 100;
                
            }


            // Loop until the user closes the window
            while (!Glfw.WindowShouldClose(window))
            {
                // Render here
                camera1.ListenToCameraMoveEvents(window);
                entity1.increaseRotation(0f, 0.5f, 0f);
                //entity1.increasePosition(0, 0, -1f);
                Renderer.prepareForRendering();
                shaderProgram.StartShaderProgram();
                shaderProgram.loadLightInformation(light);
                Renderer.render_multiple(entities, shaderProgram, camera1);
                //Renderer.render_multiple(entities, shaderProgram, camera1);
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
