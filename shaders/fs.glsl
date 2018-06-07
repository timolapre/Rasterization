#version 330
 
// shader input
in vec2 uv;						// interpolated texture coordinates
in vec4 normal;					// interpolated normal
uniform vec4 ambLightColor = vec4(0.0,0.0,1,1);
uniform sampler2D pixels;		// texture sampler



// shader output
out vec4 outputColor;

// fragment shader
void main()
{
    outputColor = texture( pixels, uv ) + 0.5f * ambLightColor;
}