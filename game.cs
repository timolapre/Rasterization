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
        float a = 0;                            // teapot rotation angle
        Stopwatch timer;                        // timer for measuring frame duration
        Shader shader;                          // shader to use for rendering
        Texture wood;                           // texture to use for rendering

        public static SceneGraph sceneGraph = new SceneGraph();
        Matrix4 CamMatrix = new Matrix4();
        Vector3 CamPos = new Vector3(0, -4, -15);

        // initialize
        public void Init()
        {
            // load teapot
            //mesh = new Mesh( "../../assets/teapot.obj" );
            //Mesh mesh2 = new Mesh("../../assets/teapot.obj", new Vector3(0, 0, 0), new Vector3(0, 0, 0));
           // floor = new Mesh("../../assets/floor.obj", new Vector3(0, -20, 0), new Vector3(0, 0, 0));
            // initialize stopwatch
            timer = new Stopwatch();
            timer.Reset();
            timer.Start();
            // create shaders
            shader = new Shader("../../shaders/vs.glsl", "../../shaders/fs.glsl");
            // load a texture
            wood = new Texture("../../assets/wood.jpg");

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
            CamMatrix = Matrix4.CreateFromAxisAngle(new Vector3(0, 1, 0), a);
            CamMatrix *= Matrix4.CreateTranslation(CamPos.X, CamPos.Y, CamPos.Z);
            CamMatrix *= Matrix4.CreatePerspectiveFieldOfView(1.2f, 1.3f, .1f, 1000);

            // update rotation
            //a += 0.001f * frameDuration; 
            //if (a > 2 * PI) a -= 2 * PI;

            // render scene
            //mesh.Render( shader, transform, wood );
            //floor.Render( shader, transform, wood );

            //Keyboard Control
            GetKeyInput();
            //new Render scene
            sceneGraph.Render(CamMatrix);

        }

        public void MoveCamera(float x, float y, float z)
        {
            CamPos -= new Vector3(x, y, z);
        }

        public void RotateCamera(float x, float y, float z)
        {
            //Add some amazing code to rotate camera (or actually the world around the camera)
            ///voor nu ff deze mooie rotatie
            a += 0.1f;
        }

        public void GetKeyInput()
        {
            float MoveSpeed = 0.2f;
            float RotateSpeed = 0.1f;

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
            if (keystate.IsKeyDown(Key.W))
                RotateCamera(RotateSpeed, 0, 0);
            if (keystate.IsKeyDown(Key.S))
                RotateCamera(-RotateSpeed, 0, 0);
            if (keystate.IsKeyDown(Key.A))
                RotateCamera(0, RotateSpeed, 0);
            if (keystate.IsKeyDown(Key.D))
                RotateCamera(0, -RotateSpeed, 0);

        }
    }

} // namespace Template_P3