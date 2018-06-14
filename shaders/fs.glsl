<<<<<<< HEAD
﻿#version 330

in vec2 uv; // interpolated texture coordinates
in vec4 normal; // interpolated normal, world space
in vec4 worldPos; // world space position of fragment
uniform sampler2D pixels; // texture sampler
out vec4 outputColor; // shader output
uniform vec3 lightPos; // light position in world space

void main() // fragment shader
{
	vec3 L = lightPos - worldPos.xyz;
	float dist = L.length();
	L = normalize( L );
	vec3 lightColor = vec3( 10, 10, 8 );
	vec3 materialColor = texture( pixels, uv ).xyz;
	float attenuation = 1.0f / (dist * dist);
	outputColor = vec4( materialColor * max( 0.0f, dot( L, normal.xyz ) ) * attenuation * lightColor, 1 );
}
=======
﻿#version 330
 
// shader input
in vec2 uv;						// interpolated texture coordinates
in vec4 normal;					// interpolated normal
uniform vec4 ambLightColor;// = vec4(0.0,0.0,0,0);
//uniform Light testlight = new Light(new vec4(0.0,1.0,0.0), new vec4(1,0.9,0.2), 1)
uniform sampler2D pixels;		// texture sampler



// shader output
out vec4 outputColor;

// fragment shader
void main()
{
    outputColor = texture( pixels, uv ) + 0.5f * ambLightColor;
}
>>>>>>> ba95c49a48c42916bb1ef35c4b43bec0b8e70536
