using System;
using System.Runtime.InteropServices;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using template_P3;

public class Light
{
    public Vector3 position;
    public Vector3 color;
    public float brightness;

	public Light(Vector3 position, Vector3 color, float brightness)
	{
        this.position = position;
        this.color = color;
        this.brightness = brightness;

	}
}
