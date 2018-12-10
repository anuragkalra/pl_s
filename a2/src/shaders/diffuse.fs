/*
    This file is part of TinyRender, an educative rendering system.

    Designed for ECSE 446/546 Realistic/Advanced Image Synthesis.
    Derek Nowrouzezahrai, McGill University.
*/

#version 330 core
#define M_PI        3.14159265358979323846264338327950288   /* pi             */


uniform vec3 camPos;
uniform vec3 lightPos;
uniform vec3 lightIntensity;
uniform vec3 albedo;

in vec3 vPos;
in vec3 vNormal;
out vec3 color;


void main() {
    /* Implementing Li = I/R^2cosTheta*Fd */

    vec3 iwi = lightPos - vPos;
    float cosTheta = dot(normalize(iwi), normalize(vNormal));
    float R_square = pow(distance(lightPos,vPos), 2);
    vec3 Fd = albedo/M_PI;
    vec3 val = lightIntensity/R_square*Fd*cosTheta;

    color = val;

}