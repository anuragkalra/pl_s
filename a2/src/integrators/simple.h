/*
    This file is part of TinyRender, an educative rendering system.

    Designed for ECSE 446/546 Realistic/Advanced Image Synthesis.
    Derek Nowrouzezahrai, McGill University.
*/

#pragma once

TR_NAMESPACE_BEGIN

/**
 * Simple direct illumination integrator.
 */
struct SimpleIntegrator : Integrator {
    explicit SimpleIntegrator(const Scene& scene) : Integrator(scene) { }

    v3f render(const Ray& ray, Sampler& sampler) const override {
        v3f Li(0.f);
        // TODO: Implement this
        v3f light_position = scene.getFirstLightPosition();
        v3f intensity = scene.getFirstLightIntensity();

        SurfaceInteraction i;
        SurfaceInteraction i_shadow;

        if(scene.bvh->intersect(ray, i)) {
            v3f hitpoint = i.p; //get hit point
            v3f light_direction = glm::normalize(light_position - hitpoint);

            Ray shadow_ray = Ray(hitpoint, light_direction);
            shadow_ray.max_t = glm::length(light_position - hitpoint);

            if (scene.bvh->intersect(shadow_ray,i_shadow)) { //check if we can see the light from the hit point
                Li = v3f(0); //if not, we shade the point
            } else {
                i.wi = i.frameNs.toLocal(light_direction); // map wo to local coords
                v3f brdf_factor = getBSDF(i)->eval(i); // get and evaluate bsdf function
                v3f Lo = intensity / glm::length2(light_position - hitpoint); // light incident on point from light source
                Li = Lo * brdf_factor;
            }
        }

        return Li;
    }
};

TR_NAMESPACE_END