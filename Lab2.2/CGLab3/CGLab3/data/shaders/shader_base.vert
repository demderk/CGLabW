#version 330

in vec3 Color;
in vec3 Position;
in vec3 Normal;

out vec3 oColor;
out vec3 oNormal;
out vec3 oFragPos;
out vec3 lightPos;
out vec3 oCamera;

uniform vec3 Light;
uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;
uniform vec3 camera;

void main()
{
    gl_Position =  vec4(Position, 1.0) * model * view * projection;
    oColor = Color; //debug
    oFragPos = vec3(vec4(Position, 1.0) * model);
    oNormal = Normal * mat3(transpose(inverse(model)));
    lightPos = Light;
    oCamera = camera; 

}