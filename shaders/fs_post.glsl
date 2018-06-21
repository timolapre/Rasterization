#version 330

// shader input
in vec2 P;						// fragment position in screen space
in vec2 uv;						// interpolated texture coordinates
uniform sampler2D pixels;		// input texture (1st pass render target)

// shader output
out vec3 outputColor;
out vec3 test;

void main()
{
	// retrieve input pixel
	outputColor = texture( pixels, uv ).rgb;
	// apply dummy postprocessing effect
	float dx = P.x - 0.5, dy = P.y - 0.5;
	float distance = sqrt( dx * dx + dy * dy );
	
	outputColor -= 1;
	if(outputColor.x > -0.5f){outputColor += 0.4f;}
	outputColor += 1;
	outputColor *= vec3(-distance * 1.99f + 1.5f,-distance * 1.80f + 1.5f,-distance * 1.9f + 1.5f);
}

// EOF