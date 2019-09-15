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

            ParsedObjFile obj = ObjFilesHandler.ParseObjFile("..\\..\\res/grassModel.obj");
            Texture texture2 = TextureLoader.getTexture("..\\..\\res/grassTexture.png");
            TexturedIndexedModel grass = ModelsCreatorAndHandler.SetObject(obj.vertices, obj.indices, obj.uvTextureCoords, texture2, obj.surfaceNormals);
            grass.metaData.shineDamper = 100f;
            grass.metaData.reflectivity = 10;
            grass.metaData.hasTransparency = true;
            grass.metaData.useFakeLighting = true;
            Entity entity1 = new Entity(grass, new Vertex3f(0, 0, -5f), new Vertex3f(0, 0, 0), 1);

            //Renderer renderer = new Renderer(shaderProgram, width, height);

            List<Entity> entities = new List<Entity>();
            Random r = new Random();
           
            int rand = r.Next(-100, 100);
            int x = r.Next(30, 80);
            int f = r.Next(1, 50);
            for (int i = 0; i < 100; i++)
            {
                entities.Add(new Entity(grass, new Vertex3f((float)x, 0, (float)f), new Vertex3f(0, 0, 0),9));
                //rand = r.Next(-100, 100) * 4;
                x = r.Next(30, 80) * 13 - 720;
                f = r.Next(-100, -20) * 6 - 100;
                
            }

            List<Terrain> terrains = new List<Terrain>();
            terrains.Add(new Terrain(new Vertex3f(-400, 0, -800), new Vertex3f(0, 0, 0), 1,  TextureLoader.getTexture("..\\..\\res/grass.jpg")));

            MasterRenderer masterRenderer = new MasterRenderer();
        
            // Loop until the user closes the window
            while (!Glfw.WindowShouldClose(window))
            {
                // Render here
                masterRenderer.camera.ListenToCameraMoveEvents(window);
                masterRenderer.processEntities(entities);
                masterRenderer.processTerrains(terrains);
                //for (int i = 0; i < entities.Count; i++)
                //{
                //    entities[i].increaseRotation(0f, 0.5f, 0f);
                //}

                masterRenderer.render();

                //Swap front and back buffers
                Glfw.SwapBuffers(window);

                // Poll for and process events
                Glfw.PollEvents();
            }

            TextureLoader.CleanUpTextures();
            masterRenderer.CleanUp();
            ModelsCreatorAndHandler.CleanUpMemmory();

            // terminate program
            Glfw.Terminate();
            Environment.Exit(-1);
        }
    }
}
