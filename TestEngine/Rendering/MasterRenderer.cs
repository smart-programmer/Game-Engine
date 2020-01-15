using System;
using System.Collections.Generic;
using OpenGL;


// the master renderer groups all the stuff that's needed for rendering like the view matrix and the shaders and the renderer and the objects...

namespace TestEngine
{
    class MasterRenderer
    {
        public static int width = 900, height = 500;

        private StaticShader entityShaderProgram { set; get; }
        private EntityRenderer entityRenderer { set; get; }

        private TerrainShader terrainShaderProgam { set; get; }
        private TerrainRenderer terrainRenderer { set; get; }

        public Camera camera = new Camera(new Vertex3f(0, 100, 0), 0, 0, 0);
        private Light light = new Light(new Vertex3f(-100, 1000, -500), new Vertex3f(1f, 1f, 1f));

        private Fog fog = new Fog(0f, 1f); // 0.0035f, 5f 0.0039f, 1f
        private Sky sky = new Sky(new RGBColor(0.5f, 0.5f, 0.5f)); // light blue: 0.52f, 0.8f, 0.92f

        private Dictionary<TexturedIndexedModel, List<Entity>> entitiesHashMap = new Dictionary<TexturedIndexedModel, List<Entity>>();
        private Dictionary<TexturedIndexedModel, List<Terrain>> terrainsHashMap = new Dictionary<TexturedIndexedModel, List<Terrain>>();

        public MasterRenderer()
        {
            entityShaderProgram = new StaticShader();
            entityRenderer = new EntityRenderer(entityShaderProgram);
            terrainShaderProgam = new TerrainShader();
            terrainRenderer = new TerrainRenderer(terrainShaderProgam);
            UseProjectionMatrix(width, height); // we only need to load the projection matrix one time because it's not going to change unless we resize the window
            terrainShaderProgam.StartShaderProgram();
            terrainShaderProgam.loadTexturePackUnitNumbers();
            terrainShaderProgam.StopShaderProgram();
            Gl.Enable(EnableCap.CullFace);
            Gl.CullFace(CullFaceMode.Back);
        }


        public void render()
        {
            UseViewMatrix();
            prepareForRendering();

            entityShaderProgram.StartShaderProgram();
            entityShaderProgram.loadLightInformation(light);
            entityShaderProgram.loadFogData(fog);
            entityShaderProgram.loadSkyColor(sky);
            entityRenderer.render_multiple_entities(entitiesHashMap);
            entityShaderProgram.StopShaderProgram();

            terrainShaderProgam.StartShaderProgram();
            terrainShaderProgam.loadLightInformation(light);
            terrainShaderProgam.loadFogData(fog);
            terrainShaderProgam.loadSkyColor(sky);
            terrainRenderer.render_multiple_terrains(terrainsHashMap);
            terrainShaderProgam.StopShaderProgram();

            entitiesHashMap.Clear();
            terrainsHashMap.Clear();
        }


        public void processEntities(List<Entity> entitiesToProcess)
        {
            for (int i = 0; i < entitiesToProcess.Count; i++)
            {
                TexturedIndexedModel texturedModel = entitiesToProcess[i].texturedModel;

                // if a the entitys textured model is already a key
                if (entitiesHashMap.ContainsKey(texturedModel))
                {
                    // just add the new entity to it
                    entitiesHashMap[texturedModel].Add(entitiesToProcess[i]);
                }
                else
                {
                    // if not create a new textured model as a key then add the entity to it
                    List<Entity> newList = new List<Entity>();
                    newList.Add(entitiesToProcess[i]);
                    entitiesHashMap.Add(texturedModel, newList);
                }
            }
        }

        public void processTerrains(List<Terrain> terrainsToProcess)
        {
            for (int i = 0; i < terrainsToProcess.Count; i++)
            {
                TexturedIndexedModel texturedModel = terrainsToProcess[i].terrainModel;

                // if a the entitys textured model is already a key
                if (terrainsHashMap.ContainsKey(texturedModel))
                {
                    // just add the new entity to it
                    terrainsHashMap[texturedModel].Add(terrainsToProcess[i]);
                }
                else
                {
                    // if not create a new textured model as a key then add the entity to it
                    List<Terrain> newList = new List<Terrain>();
                    newList.Add(terrainsToProcess[i]);
                    terrainsHashMap.Add(texturedModel, newList);
                }
            }
        }

        private void prepareForRendering()
        {
            Gl.Enable(EnableCap.DepthTest);
            // clear previous rendered colours and replace them with the color specified and clear the previous depth test result and replace it with the new one every frame
            Gl.ClearColor(sky.Color.R, sky.Color.G, sky.Color.B, 1);
            Gl.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        }


        private void UseViewMatrix()
        {
            entityShaderProgram.StartShaderProgram();
            entityShaderProgram.loadViewMatrix(MyMath.CreateViewMatrix(camera));
            entityShaderProgram.StopShaderProgram();

            terrainShaderProgam.StartShaderProgram();
            terrainShaderProgam.loadViewMatrix(MyMath.CreateViewMatrix(camera));
            terrainShaderProgam.StopShaderProgram();
        }

        private void UseProjectionMatrix(float windowWidth, float windowHeight)
        {
            entityShaderProgram.StartShaderProgram();
            entityShaderProgram.loadProjectionMatrix(MyMath.createProjectionMatrix(windowWidth, windowHeight));
            entityShaderProgram.StopShaderProgram();

            terrainShaderProgam.StartShaderProgram();
            terrainShaderProgam.loadProjectionMatrix(MyMath.createProjectionMatrix(windowWidth, windowHeight));
            terrainShaderProgam.StopShaderProgram();
        }


        public void CleanUp()
        {
            entityShaderProgram.CleanUp();
            terrainShaderProgam.CleanUp();
        }
    }
}
