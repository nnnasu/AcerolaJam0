#pragma once


#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/CommonMaterial.hlsl"

sampler2D _MainTex;
sampler2D _EmissionMap;
sampler2D _MaskMap;

CBUFFER_START(UnityPerMaterial)
    float4 _MainTex_ST;
    half4 _MainColor;
    half4 _EmissionColor;
    half _OcclusionStrength;


    // lighting
    half3 _IndirectLightMinColor;
    half _CelShadeMidPoint;
    half _CelShadeSoftness;
    half _DepthBias;

CBUFFER_END

float3 _LightDirection;

struct NSurfaceData {
    half3 albedo;
    half alpha;
    half3 emission;
    half occlusion;
    half smoothness;
};

struct NLightingData {
    half3 normalWS;
    float3 positionWS;
    half3 viewDirectionWS;
    float4 shadowCoord;
};


struct Attributes {
    float4 positionOS: POSITION;
    float3 normalOS : NORMAL;
    float4 tangentOS : TANGENT;
    float2 uv : TEXCOORD0;

    UNITY_VERTEX_INPUT_INSTANCE_ID
};

struct Varyings {
    float2 uv : TEXCOORD0;
    float4 positionWSAndFogFactor : TEXCOORD1;
    float3 normalWS: TEXCOORD2;
    float4 positionCS : SV_POSITION;
    #if defined(_NORMAL_MAP)
    float4 tangentWS : TEXCOORD4;
    #endif

    UNITY_VERTEX_INPUT_INSTANCE_ID
    UNITY_VERTEX_OUTPUT_STEREO
};