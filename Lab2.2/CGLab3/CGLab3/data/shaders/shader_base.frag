#version 400
#define NUMM 16

in vec3 oColor;
in vec3 oFragPos;
in vec3 oNormal;
in vec3 oCamera;
out vec4 outColor;

struct PointLight
{
    vec3 Position;
    vec3 Ambient;
    vec3 Diffuse;
    vec3 Specular;
    vec3 LightColor;
    float Constant;
    float Linear;
    float Quadratic;
    bool Disabled;
};

struct DirLight {
    vec3 Direction;
    vec3 Ambient;
    vec3 Diffuse;
    vec3 Specular;
    vec3 LightColor;
    bool Disabled;
};  

uniform int PointLightCount;
uniform PointLight PointLights[NUMM];
uniform DirLight GlobalLight;

vec3 CalcDirLight(DirLight light, vec3 normal, vec3 viewDir)
{
    vec3 lightDir = normalize(-light.Direction);
    float diff = max(dot(normal, lightDir), 0.0);
    vec3 reflectDir = reflect(-lightDir, normal);
    float spec = pow(max(dot(viewDir, reflectDir), 0.0), 256); //The 256 is the shininess of the material.
    vec3 specular = light.Specular * spec * light.LightColor;
    vec3 diffuse  = light.Diffuse  * diff * light.LightColor;
    vec3 ambient = 0.1 * light.LightColor;
    return (ambient + diffuse + specular) * oColor;
}

vec3 CalcPointLight(PointLight light, vec3 normal, vec3 fragPos)
{
    vec3 norm = normal;
    vec3 lightDir = normalize(light.Position - fragPos); //Note: The light is pointing from the light to the fragment
    float diff = max(dot(norm, lightDir), 0.0);
    vec3 diffuse = diff * light.LightColor;
    float specularStrength = 0.5;
    vec3 viewDir = normalize(oCamera - fragPos);
    vec3 reflectDir = reflect(-lightDir, norm);
    float spec = pow(max(dot(viewDir, reflectDir), 0.0), 32); //The 32 is the shininess of the material.
    vec3 specular = specularStrength * spec * light.LightColor;
    float distance = length(light.Position - fragPos);
    vec3 ambient = 0.1 * light.LightColor;
    float attenuation = 1.0 / (light.Constant + light.Linear * distance + light.Quadratic * (distance * distance));
    ambient  *= attenuation;
    diffuse  *= attenuation;
    specular *= attenuation;
    vec3 result = (ambient + diffuse + specular) * oColor;
    return result;
}

void main()
{
    vec3 norm = normalize(oNormal);
    vec3 viewDir = normalize(oCamera - oFragPos);
    vec3 result;
    if (!GlobalLight.Disabled) 
    {
        result = CalcDirLight(GlobalLight, norm, viewDir);
    }
    for(int i = 0; i < NUMM; i++) 
    {
        if (PointLights[i].Disabled == true)
            break;
        result += CalcPointLight(PointLights[i], norm, oFragPos);
    }
    outColor = vec4(result, 1.0);
}