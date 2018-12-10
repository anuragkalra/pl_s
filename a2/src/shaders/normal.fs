/*
    This file is part of TinyRender, an educative rendering system.

    Designed for ECSE 446/546 Realistic/Advanced Image Synthesis.
    Derek Nowrouzezahrai, McGill University.
*/

#version 330 core

in vec3 vNormal;
out vec3 color;

void main() {
    // Your normal fragment shader from A1 here (optional).
    color = vNormal;
}