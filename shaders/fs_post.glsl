#version 330

// shader input
in vec2 P;						// fragment position in screen space
in vec2 uv;						// interpolated texture coordinates
uniform sampler2D pixels;		// input texture (1st pass render target)

uniform int chromatic;
uniform int vignette;

// shader output
out vec3 outputColor;

void main()
{
	float dx = P.x - 0.5, dy = P.y - 0.5;
	float distance = sqrt( dx * dx + dy * dy );
	// retrieve input pixel
	
	outputColor = texture( pixels, uv ).rgb;
	if(chromatic > 0)
	{
		float xo = dx*.035f*distance;
		float yo = dy*.035f*distance;
		float r = texture( pixels, uv + vec2( xo, yo ) ).r;
		float g = texture( pixels, uv ).g;
		float b = texture( pixels, uv - vec2( xo, yo ) ).b;
		outputColor = vec3(r,g,b);
	}
	// apply dummy postprocessing effect
	if(vignette > 0)
		outputColor *= vec3(-distance * 1.99f + 1.5f,-distance * 1.80f + 1.5f,-distance * 1.9f + 1.5f);
}

// EOF