Shader "FullScreen/CRTPostEffect"
{
    SubShader
    {
        Tags{ "RenderPipeline" = "HDRenderPipeline" }
        Pass
        {
            Name "Custom Pass 0"

            ZWrite Off
            ZTest Always
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off

            HLSLPROGRAM
                #pragma target 4.5
                #include "CRTPostEffect.hlsl"
                #pragma vertex Vert
                #pragma fragment FullScreenPass
            ENDHLSL
        }
    }
    Fallback Off
}
