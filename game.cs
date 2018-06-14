using System;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using OpenTK;
using OpenTK.Input;
using template_P3;

// minimal OpenTK rendering framework for UU/INFOGR
// Jacco Bikker, 2016

namespace Template_P3
{

    class Game
    {
        // member variables
        public Surface screen;                  // background surface for printing etc.
        Mesh mesh, floor;                       // a mesh to draw using OpenGL
        const float PI = 3.1415926535f;         // PI
        public static float x = 0, y = 0, z = 0;                            // teapot rotation angle
        Stopwatch timer;                        // timer for measuring frame duration
        Shader shader;                          // shader to use for rendering
        public static Shader postproc;                        // shader to use for post processing
        Texture wood;                           // texture to use for rendering
        public static RenderTarget target;                    // intermediate render target
        public static ScreenQuad quad;                        // screen filling quad for post processing
        bool useRenderTarget = true;

        public static SceneGraph sceneGraph = new SceneGraph();
        Matrix4 CamMatrix = new Matrix4();
        static public Vector3 CamPos = new Vector3(0, -4, -15);

        // initialize
        public void Init()
        {
            // load teapot
            //mesh = new Mesh( "../../assets/teapot.obj" ,Vector3.Zero,Vector3.Zero);
            //Mesh mesh2 = new Mesh("../../assets/teapot.obj", new Vector3(0, 0, 0), new Vector3(0, 0, 0));
            //floor = new Mesh("../../assets/floor.obj", new Vector3(0, 0, 0), new Vector3(0, 0, 0));
            // initialize stopwatch
            timer = new Stopwatch();
            timer.Reset();
            timer.Start();
            // create shaders
            shader = new Shader("../../shaders/vs.glsl", "../../shaders/fs.glsl");
            postproc = new Shader("../../shaders/vs_post.glsl", "../../shaders/fs_post.glsl");
            // load a texture
            wood = new Texture("../../assets/wood.jpg");
            // create the render target
            target = new RenderTarget(screen.width, screen.height);
            quad = new ScreenQuad();

            sceneGraph.Init();
        }

        // tick for background surface
        public void Tick()
        {
            screen.Clear(0);
            //screen.Print( "hello world", 2, 2, 0xffff00 );
        }

        // tick for OpenGL rendering code
        public void RenderGL()
        {
            // measure frame duration
            float frameDuration = timer.ElapsedMilliseconds;
            timer.Reset();
            timer.Start();

            // prepare matrix for vertex shader
            CamMatrix = new Matrix4();
            CamMatrix.Diagonal = new Vector4(1, 1, 1, 1);
            Console.WriteLine(y);

			// update rotation
			//a += 0.001f * frameDuration; 
			//if (a > 2 * PI) a -= 2 * PI;

			/*if (useRenderTarget)
            {
                // enable render target
                target.Bind();

                // render scene to render target
                mesh.Render(shader, CamMatrix, wood);
                floor.Render(shader, CamMatrix, wood);

                // render quad
                target.Unbind();
                quad.Render(postproc, target.GetTextureID());
            }
            else
            {
                // render scene directly to the screen
                mesh.Render(shader, CamMatrix, wood);
                floor.Render(shader, CamMatrix, wood);
            }*/

			//Keyboard Control
			GetKeyInput();
            //new Render scene
            sceneGraph.Render(CamMatrix);

        }

        public void MoveCamera(float x, float y, float z)
        {
            CamPos -= new Vector3(z*(float)Math.Cos(Game.y+0.5*PI) + x * (float)Math.Cos(Game.y), y, z*(float)Math.Sin(Game.y+0.5*PI)+ x * (float)Math.Sin(Game.y));
        }

        public void RotateCamera(float x, float y, float z)
        {
            //Add some amazing code to rotate camera (or actually the world around the camera)
            ///voor nu ff deze mooie rotatie
            Game.x += x;
            Game.y += y;
            Game.z += z;
        }

        public void GetKeyInput()
        {
            float MoveSpeed = 0.35f;
            float RotateSpeed = 0.04f;

            KeyboardState keystate = Keyboard.GetState();
            //Move
            if (keystate.IsKeyDown(Key.Down))
                MoveCamera(0, 0, MoveSpeed);
            if (keystate.IsKeyDown(Key.Up))
                MoveCamera(0, 0, -MoveSpeed);
            if (keystate.IsKeyDown(Key.Left))
                MoveCamera(-MoveSpeed, 0, 0);
            if (keystate.IsKeyDown(Key.Right))
                MoveCamera(MoveSpeed, 0, 0);
            if (keystate.IsKeyDown(Key.Space))
                MoveCamera(0, MoveSpeed, 0);
            if (keystate.IsKeyDown(Key.LShift))
                MoveCamera(0, -MoveSpeed, 0);

            //Rotate
            //x*(float)Math.Cos(Game.y)+z*(float)Math.Sin(Game.y)
            if (keystate.IsKeyDown(Key.W))
                RotateCamera(-RotateSpeed, 0, 0);
            if (keystate.IsKeyDown(Key.S))
                RotateCamera(RotateSpeed, 0, 0);
            if (keystate.IsKeyDown(Key.A))
                RotateCamera(0, -RotateSpeed, 0);
            if (keystate.IsKeyDown(Key.D))
                RotateCamera(0, RotateSpeed, 0);
            if (keystate.IsKeyDown(Key.Q))
                RotateCamera(0, 0, -RotateSpeed);
            if (keystate.IsKeyDown(Key.E))
                RotateCamera(0, 0, RotateSpeed);

        }
    }

} // namespace Template_P3