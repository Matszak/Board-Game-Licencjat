Shader "Custom/NewSurfaceShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0

        _OutlineColor ("Outline Color", Color) = (0,0,0,1)
        _OutlineWidth( "Outline Width", Range (0,0.1)) = 0.03
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"="Geometry" }

        // Outline Pass
        Pass
        {
            Name "Outline"
            Tags { "LightMode"="UniversalForward" }
            Cull Front
            ZWrite Off
            ZTest Less

            HLSLPROGRAM

            #pragma vertex VertexProgram
            #pragma fragment FragmentProgram

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            half4 _OutlineColor;
            float _OutlineWidth;

            struct Attributes
            {
                float4 positionOS : POSITION;   
                float3 normalOS : NORMAL;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
            };

            float4 VertexProgram(float4 position : POSITION, float3 normal : NORMAL) : SV_POSITION
            {
                // Normalize the object-space normal
                float3 offset = normalize(normal) * _OutlineWidth;

                // Offset the vertex position along the normal
                float4 displacedPosition = position + float4(offset, 0);

                // Transform from object space to clip space (URP macro)
                return TransformObjectToHClip(displacedPosition.xyz);
            }

            half4 FragmentProgram(Varyings input) : SV_Target
            {
                return _OutlineColor;
            }

            ENDHLSL
        }

        // Main Pass
        Pass
        {
            Name "MainPass"
            Tags { "LightMode"="UniversalForward" }
            Cull Back

            HLSLPROGRAM

            #pragma vertex VertexProgramMain
            #pragma fragment FragmentProgramMain

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            half4 _Color;
            half _Glossiness;
            half _Metallic;

            struct Attributes
            {
                float4 positionOS : POSITION;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
            };

            Varyings VertexProgramMain(Attributes input)
            {
                Varyings output;
                output.positionHCS = TransformObjectToHClip(input.positionOS.xyz);
                return output;
            }

            half4 FragmentProgramMain(Varyings input) : SV_Target
            {
                return _Color;
            }

            ENDHLSL
        }
    }

    FallBack "Hidden/InternalErrorShader"
}
