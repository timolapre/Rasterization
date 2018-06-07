using System.Diagnostics;
using System.Drawing.Drawing2D;
using OpenTK;
using OpenTK.Input;
using template_P3;
namespace template_P3 {
    public class Light
    {
        public Vector3 color, position;
        float intensity;
        public Light(Vector3 clr, Vector3 pos, float intst)
        {
            color = clr;
            position = pos;
            intensity = intst;

        }
    }
}