#pragma once
#include "CustomSurfaceData.hlsl"

half3 ShadeGI(NSurfaceData surfaceData, NLightingData lightingData) {
    half3 averageSH = SampleSH(lightingData.normalWS); // Nilo does SampleSH(0)
    averageSH = max(_IndirectLightMinColor, averageSH);
    half indirectOcclusion = lerp(1, surfaceData.occlusion, 0.5);
    return averageSH * indirectOcclusion;
}

half3 ShadeSingleLight(NSurfaceData surfaceData, NLightingData lightingData, Light light, bool isAdditionalLight) {
    half3 N = lightingData.normalWS;
    half3 L = light.direction;
    half NoL = dot(N, L);
    half lightAttenuation = 1;
    
    // Lighting.hlsl -> https://github.com/Unity-Technologies/Graphics/blob/master/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl
    half distanceAttenuation = min(4, light.distanceAttenuation);
    
    half litOrShadowArea = smoothstep(_CelShadeMidPoint - _CelShadeSoftness, _CelShadeMidPoint + _CelShadeSoftness, NoL);

    // occlusion
    litOrShadowArea *= surfaceData.occlusion;

    // face ignore celshade since it is usually very ugly using NoL method
    litOrShadowArea = lerp(0.5, 1, litOrShadowArea);

    half3 litOrShadowColor = litOrShadowArea;
    half3 lightAttenuationRGB = litOrShadowColor * distanceAttenuation;
    
    half smoothness = exp2(10 * surfaceData.smoothness + 1);
    half3 lightSpecularColor = 0;

    lightSpecularColor += LightingSpecular(light.color, light.direction, lightingData.normalWS, lightingData.viewDirectionWS, 1, smoothness);

    // saturate() light.color to prevent over bright
    // additional light reduce intensity since it is additive
    return saturate(light.color) * lightAttenuationRGB * (isAdditionalLight ? 0.25 : 1) + saturate(lightSpecularColor);
}


half3 ShadeEmission(NSurfaceData surfaceData, NLightingData lightingData) {
    // multiply by albedo?
    return surfaceData.emission;
}

half3 CompositeLightResults(half3 indirectResult, half3 mainLightResult, half3 additionalLightSumResult,
                            half3 emissionResult, NSurfaceData surfaceData, NLightingData lightingData) {
    // [remember you can write anything here, this is just a simple tutorial method]
    // here we prevent light over bright,
    // while still want to preserve light color's hue
    half3 rawLightSum = max(indirectResult, mainLightResult + additionalLightSumResult);
    // pick the highest between indirect and direct light
    return surfaceData.albedo * rawLightSum + emissionResult;
}


half3 ApplyFog(half3 color, Varyings input) {
    half fogFactor = input.positionWSAndFogFactor.w;
    // Mix the pixel color with fogColor. You can optionaly use MixFogColor to override the fogColor
    // with a custom one.
    color = MixFog(color, fogFactor);

    return color;
}