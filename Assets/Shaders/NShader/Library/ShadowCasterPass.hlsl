#pragma once
#include "VertexShader_shared.hlsl"

half4 ShadowCasterFragment(Varyings input) : SV_TARGET {
    // GPU Instancing Setup
    UNITY_SETUP_INSTANCE_ID(input);
    UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);
    
    return 0;
}
