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
uniform vec3 rho_d;
uniform vec3 rho_s;
uniform float exponent;

in vec3 vPos;
in vec3 vNormal;
out vec3 color;


void main() {
    /* Implementing Li = I/R^2cosTheta*BRDF*/

    vec3 iwi = lightPos-vPos;
    vec3 iwo = camPos - vPos;
    //vec3 reflection = reflect(normalize(iwi),vPos);
    vec3 reflection = -normalize(iwi)-2*normalize(vNormal)*dot(-normalize(iwi), normalize(vNormal));
    float cosTheta = dot(normalize(iwi), normalize(vNormal));
    float specular_angle = dot(normalize(reflection), normalize(iwo));
    float R_square = pow(distance(lightPos,vPos), 2);

    if(specular_angle < 0) {
        specular_angle = 0;
    }

    float cos_pow_n = pow(specular_angle, exponent);

    vec3 BRDF = (rho_d/M_PI + rho_s*(exponent + 2)/(2*M_PI)*cos_pow_n);

    color = lightIntensity/R_square*BRDF*cosTheta;

}


