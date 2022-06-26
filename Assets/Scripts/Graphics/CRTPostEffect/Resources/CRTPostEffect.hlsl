#include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/RenderPass/CustomPass/CustomPassCommon.hlsl"

// The PositionInputs struct allow you to retrieve a lot of useful information for your fullScreenShader:
// struct PositionInputs
// {
//     float3 positionWS;  // World space position (could be camera-relative)
//     float2 positionNDC; // Normalized screen coordinates within the viewport    : [0, 1) (with the half-pixel offset)
//     uint2  positionSS;  // Screen space pixel coordinates                       : [0, NumPixels)
//     uint2  tileCoord;   // Screen tile coordinates                              : [0, NumTiles)
//     float  deviceDepth; // Depth from the depth buffer                          : [0, 1] (typically reversed)
//     float  linearDepth; // View space Z coordinate                              : [Near, Far]
// };

// To sample custom buffers, you have access to these functions:
// But be careful, on most platforms you can't sample to the bound color buffer. It means that you
// can't use the SampleCustomColor when the pass color buffer is set to custom (and same for camera the buffer).
// float4 SampleCustomColor(float2 uv);
// float4 LoadCustomColor(uint2 pixelCoords);
// float LoadCustomDepth(uint2 pixelCoords);
// float SampleCustomDepth(float2 uv);

// There are also a lot of utility function you can use inside Common.hlsl and Color.hlsl,
// you can check them out in the source code of the core SRP package.

TEXTURE2D_X(_SourceTexture);

TEXTURE2D(_CrtTexture);
SAMPLER(sampler_CrtTexture);

TEXTURE2D(_NoiseTexture);

CBUFFER_START(CrtPostEffectProperties)
float _FisheyeIntensity;
float _ZoomIntensity;

float _CrtTextureTiling;
float _CrtVisibility;

float _NoiseTextureIntensity;
float _NoiseTextureTiling;
CBUFFER_END

float2 SpherizeUV(float2 UV, float2 Center, float Strength, float2 Offset)
{
    float2 delta = UV - Center;
    float delta2 = dot(delta.xy, delta.xy);
    float delta4 = delta2 * delta2;
    float2 delta_offset = delta4 * Strength;
    return UV + delta * delta_offset + Offset;
}

float4 FullScreenPass(Varyings varyings) : SV_Target
{
    UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(varyings);
    PositionInputs posInput = GetPositionInput(varyings.positionCS.xy, _ScreenSize.zw);

    float2 uv = posInput.positionNDC;
    uv = SpherizeUV(uv, 0.5, _FisheyeIntensity, 0.0);

    float2 aspectCorrection = float2(_ScreenSize.x / _ScreenSize.y, 1.0);

    float3 crtTextureSample = SAMPLE_TEXTURE2D(_CrtTexture, sampler_CrtTexture, (uv + sin(_Time.x * 2000.0) * 0.0001) * aspectCorrection * _CrtTextureTiling).rgb;
    float4 noiseTextureSample = SAMPLE_TEXTURE2D(_NoiseTexture, s_trilinear_repeat_sampler, (uv + _Time.x * 2000.0) * aspectCorrection * _NoiseTextureTiling);

    uv = lerp(uv, 0.5, _ZoomIntensity);

    //color = float4(SampleCameraColor(saturate(uv), 0), 1);
    float3 color = SAMPLE_TEXTURE2D_X_LOD(_SourceTexture, s_trilinear_clamp_sampler, saturate(uv) * _RTHandleScale.xy, 0).rgb;
    color = lerp(color, noiseTextureSample.x, _NoiseTextureIntensity);
    color = lerp(color, color * crtTextureSample, _CrtVisibility);

    return float4(color, 1);
}