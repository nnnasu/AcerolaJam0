#pragma once
#include "Input.hlsl"
#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

half4 GetFinalBaseColor(Varyings input) {
    return tex2D(_MainTex, input.uv) * _MainColor;
}

half3 GetFinalEmissionColor(Varyings input) {
    half3 result = 0;
    result = tex2D(_EmissionMap, input.uv) * _EmissionColor.rgb;
    return result;
}

half GetFinalOcclusion(Varyings input) {
    // TODO if not using occlusion, return 1
    half4 occlusionValue = tex2D(_OcclusionMap, input.uv);
    occlusionValue = lerp(1, occlusionValue, _OcclusionStrength);
    return occlusionValue;
}

NSurfaceData InitializeSurfaceData(Varyings input) {
    NSurfaceData output;
    float4 baseColorFinal = GetFinalBaseColor(input);
    output.albedo = baseColorFinal.rgb;
    output.alpha = baseColorFinal.a;
    output.emission = GetFinalEmissionColor(input);
    output.occlusion = GetFinalOcclusion(input);
    return output;
}

NLightingData InitializeLightingData(Varyings input) {
    NLightingData output;
    output.positionWS = input.positionWSAndFogFactor.xyz;
    output.viewDirectionWS = SafeNormalize(GetCameraPositionWS() - output.positionWS);
    output.normalWS = normalize(input.normalWS);
    output.shadowCoord = TransformWorldToShadowCoord(input.positionWSAndFogFactor.rgb);
    
    return output;
}