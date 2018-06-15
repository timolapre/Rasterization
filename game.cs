using System;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using OpenTK;
using OpenTK.Input;
using template_P3;
using OpenTK.Graphics.ES30;

// minimal OpenTK rendering framework for UU/INFOGR
// Jacco Bikker, 2016

namespace Template_P3
{

    class Game
    {
        // member variables
        public Surface screen;                  // background surface for printing etc.
        const float PI = 3.1415926535f;         // PI
        public static float x = 0, y = 0, z = 0;                            // teapot rotation angle
        Stopwatch timer;                        // timer for measuring frame duration
        Shader shader;                          // shader to use for rendering
        public static Shader postproc;                        // shader to use for post processing
        Texture wood;                           // texture to use for rendering
        public static RenderTarget target;                    // intermediate render target
        public static ScreenQuad quad;                        // screen filling quad for post processing

        public static Vector4 ambientLightColor = new Vector4(1.0f, 1.0f, 1.0f,0);

        public static SceneGraph sceneGraph = new SceneGraph();
        Matrix4 CamMatrix = new Matrix4();
        static public Vector3 CamPos = new Vector3(0, -4, -15);

        MouseState Mouse;
        int MouseOldX, MouseOldY;
        float frameDuration;


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
 
            int lightID = GL.GetUniformLocation(shader.programID,"lightPos");
            GL.UseProgram(shader.programID);
            GL.Uniform3(lightID, 0.0f, 0.0f, 0.0f);

            sceneGraph.Init();
        }

        // tick for background surface
        public void Tick()
        {
            screen.Clear(0);
            screen.Print((int)(1000/frameDuration)+"", 2, 2, 0xffff00 );
            Mouse = OpenTK.Input.Mouse.GetState();
            //Console.WriteLine(Mouse.X + " " + MouseOldX);
            RotateCamera((float)(Mouse.Y-MouseOldY)/200,(float)(Mouse.X-MouseOldX)/200,0);
            MouseOldX = Mouse.X;
            MouseOldY = Mouse.Y;

        }

        // tick for OpenGL rendering code
        public void RenderGL()
        {
            // measure frame duration
            frameDuration = timer.ElapsedMilliseconds;
            timer.Reset();
            timer.Start();

            // prepare matrix for vertex shader
            CamMatrix = new Matrix4();
            CamMatrix.Diagonal = new Vector4(1, 1, 1, 1);
            //Console.WriteLine(y);
            OpenTK.Input.Mouse.GetCursorState();

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
            Game.x += x;
            Game.y += y;
            Game.z += z;
        }

        public void GetKeyInput()
        {
            float MoveSpeed = 0.7f;
            float RotateSpeed = 0.04f;

            KeyboardState keystate = Keyboard.GetState();
            //Move
            if (keystate.IsKeyDown(Key.S))
                MoveCamera(0, 0, MoveSpeed);
            if (keystate.IsKeyDown(Key.W))
                MoveCamera(0, 0, -MoveSpeed);
            if (keystate.IsKeyDown(Key.A))
                MoveCamera(-MoveSpeed, 0, 0);
            if (keystate.IsKeyDown(Key.D))
                MoveCamera(MoveSpeed, 0, 0);
            if (keystate.IsKeyDown(Key.Space))
                MoveCamera(0, MoveSpeed, 0);
            if (keystate.IsKeyDown(Key.LShift))
                MoveCamera(0, -MoveSpeed, 0);

            //Rotate
            if (keystate.IsKeyDown(Key.Up))
                RotateCamera(-RotateSpeed, 0, 0);
            if (keystate.IsKeyDown(Key.Down))
                RotateCamera(RotateSpeed, 0, 0);
            if (keystate.IsKeyDown(Key.Left))
                RotateCamera(0, -RotateSpeed, 0);
            if (keystate.IsKeyDown(Key.Right))
                RotateCamera(0, RotateSpeed, 0);
            if (keystate.IsKeyDown(Key.N))
                RotateCamera(0, 0, -RotateSpeed);
            if (keystate.IsKeyDown(Key.M))
                RotateCamera(0, 0, RotateSpeed);

            //Ambient Light Color adjustment
            if (keystate.IsKeyDown(Key.Minus))
                ambientLightColor.X += .05f;
            if (keystate.IsKeyDown(Key.Number9))
                ambientLightColor.X -= .05f;
            if (keystate.IsKeyDown(Key.P))
                ambientLightColor.Y += .05f;
            if (keystate.IsKeyDown(Key.I))
                ambientLightColor.Y -= .05f;
            if (keystate.IsKeyDown(Key.L))
                ambientLightColor.Z += .05f;
            if (keystate.IsKeyDown(Key.J))
                ambientLightColor.Z -= .05f;
            if (keystate.IsKeyDown(Key.Number0))
            {
                if (ambientLightColor.X != 1f) ambientLightColor.X = 1f;
                else ambientLightColor.X = 0f;
            }
            if (keystate.IsKeyDown(Key.O))
            {
                if (ambientLightColor.Y != 1f) ambientLightColor.Y = 1f;
                else ambientLightColor.Y = 0f;
            }
            if (keystate.IsKeyDown(Key.K))
            {
                if (ambientLightColor.Z != 1f) ambientLightColor.Z = 1f;
                else ambientLightColor.Z = 0f;
            }
        }
    }

} // namespace Template_P3