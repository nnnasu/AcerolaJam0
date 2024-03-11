/*
    Passes:
    1. Forward(+)
    2. Shadow
    3. Depth only
    4. Depth Normals (?)
   
*/


Shader "NShader/Lit" {
    Properties {

        [MainTexture] _MainTex ("Texture", 2D) = "white" {}
        [MainColor] _MainColor ("Color", Color) = (1,1,1,1)

        [NoScaleOffset] _MaskMap("Mask Map", 2D) = "white" {}
        _OcclusionStrength("Occlusion Strength", Range(0.0, 1.0)) = 1.0


        [NoScaleOffset]_EmissionMap("Emission Map", 2D) = "white" {}
        [HDR] _EmissionColor("Emission Color", Color) = (0,0,0)


        [Header(Lighting Settings)]
        _IndirectLightMinColor("Min Color", Color) = (0.1,0.1,0.1,1) // can prevent completely black if light prob is not baked
        _IndirectLightMultiplier("Multiplier", Range(0,1)) = 1

        _DirectLightMultiplier("Brightness", Range(0,1)) = 1
        _CelShadeMidPoint("MidPoint", Range(-1,1)) = -0.5
        _CelShadeSoftness("Softness", Range(0,1)) = 0.05

        [Header(Additional Light)]
        _AdditionalLightIgnoreCelShade("Remove Shadow", Range(0,1)) = 0.9

        _DepthBias("Depth Bias", Range(0, 1)) = 0.0


    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        LOD 100

        Pass {

            Name "ForwardLit"
            Tags {
                "LightMode" = "UniversalForward"
            }

            Blend One Zero
            ZWrite On
            Cull Off
            ZTest LEqual

            HLSLPROGRAM
            #pragma target 2.0

            // -------------------------------------
            // Universal Pipeline keywords
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS_CASCADE
            #pragma multi_compile _ _ADDITIONAL_LIGHTS_VERTEX _ADDITIONAL_LIGHTS
            #pragma multi_compile_fragment _ _ADDITIONAL_LIGHT_SHADOWS
            #pragma multi_compile_fragment _ _SHADOWS_SOFT
            #pragma multi_compile_fragment _ _SCREEN_SPACE_OCCLUSION
            #pragma multi_compile _ LIGHTMAP_SHADOW_MIXING
            #pragma multi_compile _ SHADOWS_SHADOWMASK

            // -------------------------------------
            // Unity defined keywords
            #pragma multi_compile _ DIRLIGHTMAP_COMBINED
            #pragma multi_compile _ LIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma multi_compile_fragment _ DEBUG_DISPLAY

            //--------------------------------------
            // GPU Instancing
            #pragma multi_compile_instancing
            #pragma multi_compile _ DOTS_INSTANCING_ON

            #pragma vertex VertexShared
            #pragma fragment LitPassFragment

            #include "Library/LitForwardPass.hlsl"
            ENDHLSL
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode" = "ShadowCaster"
            }
            ZWrite On
            ZTest LEqual
            ColorMask 0
            Cull Off

            HLSLPROGRAM
            #pragma vertex VertexShared
            #pragma fragment ShadowCasterFragment
            #pragma multi_compile_instancing
            #include_with_pragmas "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DOTS.hlsl"
            // Unity defined keywords
            #pragma multi_compile_fragment _ LOD_FADE_CROSSFADE

            // This is used during shadow map generation to differentiate between directional and punctual light shadows, as they use different formulas to apply Normal Bias
            #pragma multi_compile_vertex _ _CASTING_PUNCTUAL_LIGHT_SHADOW

            #pragma multi_compile_instancing
            #pragma multi_compile _ DOTS_INSTANCING_ON

            #define APPLY_SHADOW_BIAS_FIX

            #include "Library/ShadowCasterPass.hlsl"
            ENDHLSL
        }Pass {
            Name "DepthOnly"
            Tags {
                "LightMode" = "DepthOnly"
            }

            // -------------------------------------
            // Render State Commands
            // - more explicit render state to avoid confusion
            ZWrite On // the only goal of this pass is to write depth!
            ZTest LEqual // early exit at Early-Z stage if possible            
            ColorMask R // we don't care about RGB color, we just want to write depth, ColorMask R will save some write bandwidth
            Cull Off

            HLSLPROGRAM
            #pragma target 2.0
            #pragma vertex VertexShared
            #pragma fragment DepthFragment // we only need to do Clip(), no need color shading
            #pragma multi_compile_fragment _ LOD_FADE_CROSSFADE
            #pragma multi_compile_instancing
            #include_with_pragmas "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DOTS.hlsl"

            #include "Library/DepthPass.hlsl"
            ENDHLSL
        }


        // Depth Normals
        Pass {
            Name "DepthNormalsOnly"
            Tags {
                "LightMode" = "DepthNormalsOnly"
            }

            ZWrite On // the only goal of this pass is to write depth!
            ZTest LEqual // early exit at Early-Z stage if possible            
            ColorMask RGBA // we want to draw normal as rgb color!
            Cull Off

            HLSLPROGRAM
            #pragma target 2.0

            #pragma vertex VertexShared
            #pragma fragment DepthNormalFragment // we only need to do Clip() + normal as rgb color shading
            #pragma multi_compile_fragment _ _GBUFFER_NORMALS_OCT // forward-only variant
            #include_with_pragmas "Packages/com.unity.render-pipelines.universal/ShaderLibrary/RenderingLayers.hlsl"

            #pragma multi_compile_fragment _ LOD_FADE_CROSSFADE
            #pragma multi_compile_instancing
            #include_with_pragmas "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DOTS.hlsl"

            #include "Library/DepthPass.hlsl"
            ENDHLSL
        }
    }

}