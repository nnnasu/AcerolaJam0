#pragma once
#include "VertexShader_Shared.hlsl"


half3 ShadeAllLights(NSurfaceData surfaceData, NLightingData lightingData) {
    half3 indirectResult = ShadeGI(surfaceData, lightingData);
    float4 shadowCoord = lightingData.shadowCoord;
    Light mainLight = GetMainLight();
    float3 shadowTestPosWS = lightingData.positionWS + mainLight.direction * _DepthBias;

    #ifdef _MAIN_LIGHT_SHADOWS
    shadowCoord = TransformWorldToShadowCoord(shadowTestPosWS);
    #endif
    mainLight.shadowAttenuation = MainLightRealtimeShadow(shadowCoord);

    half3 mainLightResult = ShadeSingleLight(surfaceData, lightingData, mainLight, false);

    half3 additionalLightResult = 0;
    #ifdef _ADDITIONAL_LIGHTS
    int additionalLightsCount = GetAdditionalLightsCount();
    for (int i = 0; i < additionalLightsCount; ++i) {
        int perObjectLightIndex = GetPerObjectLightIndex(i);
        Light light = GetAdditionalPerObjectLight(perObjectLightIndex, lightingData.positionWS);
        light.shadowAttenuation = AdditionalLightRealtimeShadow(perObjectLightIndex, shadowTestPosWS);
    }
    #endif

    half3 emissionResult = ShadeEmission(surfaceData, lightingData);
    return CompositeLightResults(indirectResult, mainLightResult, additionalLightResult, emissionResult, surfaceData,
                                 lightingData);
}

half4 LitPassFragment(Varyings input) : SV_Target {
    UNITY_SETUP_INSTANCE_ID(input);
    UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);

    float2 uv = input.uv;
    half4 finalBaseColour = tex2D(_MainTex, uv) * _MainColor;

    NSurfaceData surfaceData = InitializeSurfaceData(input);
    NLightingData lightingData = InitializeLightingData(input);
    half3 color = ShadeAllLights(surfaceData, lightingData);
    color = ApplyFog(color, input);

    return half4(color, surfaceData.alpha);
}