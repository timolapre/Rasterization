#version 330

in vec2 uv; // interpolated texture coordinates
in vec4 normal; // interpolated normal, world space
in vec4 worldPos; // world space position of fragment
uniform sampler2D pixels; // texture sampler
out vec4 outputColor; // shader output
uniform vec3 lightPos; // light position in world space
uniform int specular;

void main() // fragment shader
{
	vec3 L = lightPos - worldPos.xyz;
	float dist = L.length();
	L = normalize( L );
	vec3 lightColor = 2*vec3( 10, 10, 10 );
	vec3 ambientLight = vec3(.15f,.15f,.15f);
	vec3 materialColor = texture( pixels, uv ).xyz;
	float attenuation = 1.0f / (dist * dist);
	vec3 diff =  materialColor * max( 0.0f, dot( normal.xyz, L ) ) * attenuation * lightColor;
	float ang = dot(L,normal.xyz);
	vec3 spec = vec3(0,0,0);
	if(ang > 1.57079632679f - 0.61086523819f)
    spec = 5*(ang-(1.57079632679f - 0.61086523819f))*lightColor*max(0.0f, -dot(L, reflect(L, normal.xyz)));
	outputColor = vec4( materialColor * ambientLight + .3f*diff + specular*.7f*spec, 1 );
	outputColor = normalize(outputColor);
	//outputColor = vec4(0,(worldPos.y+5)/15,0,1); //HeightMap
}