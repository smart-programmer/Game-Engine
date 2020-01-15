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

            ParsedObjFile obj = ObjFilesHandler.ParseObjFile("..\\..\\res/grassModel.obj"); // Q: how to parse an obj file
            Texture texture2 = TextureLoader.getTexture("..\\..\\res/grassTexture.png"); // Q: what is a texture and how to load one
            TexturedIndexedModel grass = ModelsCreatorAndHandler.SetObject(obj.vertices, obj.indices, obj.uvTextureCoords, texture2, obj.surfaceNormals); // Q:what are indices and how to make a textured indexd model
            grass.metaData.shineDamper = 100f; // Q: what is this for?
            grass.metaData.reflectivity = 10; // Q: what is this for?
            grass.metaData.hasTransparency = true; // Q: what is this for?
            grass.metaData.useFakeLighting = true; // Q: what is this for?
            Entity entity1 = new Entity(grass, new Vertex3f(0, 0, -5f), new Vertex3f(0, 0, 0), 9); // Q: what is an entity 

            ParsedObjFile personObj = ObjFilesHandler.ParseObjFile("..\\..\\res/person.obj");
            Texture playerTexture = TextureLoader.getTexture("..\\..\\res/playerTexture.png");
            TexturedIndexedModel playerModel = ModelsCreatorAndHandler.SetObject(personObj.vertices, personObj.indices, personObj.uvTextureCoords, playerTexture, personObj.surfaceNormals);
            playerModel.metaData.shineDamper = 100f;
            playerModel.metaData.reflectivity = 10;
            DeafultPlayer player = new DeafultPlayer(playerModel, new Vertex3f(0, 0, 0), new Vertex3f(0, 0, 0), 5); // Q: what is a defalut player

            //Renderer renderer = new Renderer(shaderProgram, width, height); // Q: why did i comment the renderer?

            List<Entity> entities = new List<Entity>(); // Q: why did i make this list
            Random r = new Random(); // Q: why did i make this object
           
            int rand = r.Next(-100, 100);
            int x = r.Next(30, 80);
            int f = r.Next(1, 50);
            for (int i = 0; i < 1000; i++)
            {
                entities.Add(new Entity(grass, new Vertex3f((float)x, 0, (float)f), new Vertex3f(0, 0, 0),9));
                //rand = r.Next(-100, 100) * 4;
                x = r.Next(30, 80) * 45 - 2000;
                f = r.Next(-100, -20) * 40 + 300;
                // Q: what is this supose to do?
            }

            List<Terrain> terrains = new List<Terrain>(); // Q: why did i make this list?
            TerrainTexturePack pack = new TerrainTexturePack(TextureLoader.getTexture("..\\..\\res/grassy2.png"), // Q: what is a texture pack
                TextureLoader.getTexture("..\\..\\res/mud.png"),
                TextureLoader.getTexture("..\\..\\res/grassFlowers.png"),
                TextureLoader.getTexture("..\\..\\res/path_resized.png"));
            terrains.Add(new Terrain(new Vertex3f(-2500, 0, -5000), new Vertex3f(0, 0, 0), 1, pack, TextureLoader.getTexture("..\\..\\res/blendMap.png")));
            terrains.Add(new Terrain(new Vertex3f(-2500, 0, -5000), new Vertex3f(0, 0, 0), 1, pack, TextureLoader.getTexture("..\\..\\res/blendMap.png")));

            entities.Add(player); // Q: why did i add the player to the entities list? : because entities have the same renderer (EntityRenderer) NOTE: this model is going to have it's own different key in the entities hash map
            //entities.Add(entity1);

            MasterRenderer masterRenderer = new MasterRenderer(); // Q: why is there a master renderer

            World world = new World(); // what is this object?
        
            // Loop until the user closes the window
            while (!Glfw.WindowShouldClose(window))
            {
                // prepare stuff in loop
                masterRenderer.camera.ListenToCameraMoveEvents(window); // Q: how do we listen to camera events
                player.ListenToPlayerMoveEvents(world.deltaTimeSec, window); // Q: how do we listen to player move events
                masterRenderer.processEntities(entities); // Q: what is processing entities?
                masterRenderer.processTerrains(terrains); // Q: what is processing terrains?
                
                // Render here
                masterRenderer.render(); // Q: what does render do?

                //Swap front and back buffers
                Glfw.SwapBuffers(window); // Q: why do we swap buffers and how many buffer are there and can i control them?
                world.calculateDeltaTime(); // Q: what is this methods doing and why do we use it

                // Poll for and process events
                Glfw.PollEvents(); // Q: what does this method do
            }

            TextureLoader.CleanUpTextures(); // Q: what does this method do
            masterRenderer.CleanUp(); // Q: what does this method do
            ModelsCreatorAndHandler.CleanUpMemmory(); // Q: what does this method do

            // terminate program
            Glfw.Terminate();
            Environment.Exit(-1);
            // are those 2 methods enough to terminate every thing?
        }
    }
}


// how does rendering work?
// how did i make the player move?
// why isn't the grass rendering?