using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using System;

[Serializable, VolumeComponentMenu("Post-processing/Custom/CRTPostEffect")]
public sealed class CRTPostEffect : CustomPostProcessVolumeComponent, IPostProcessComponent
{
    static class ShaderBindings
	{
        public static readonly int _SourceTexture = Shader.PropertyToID("_SourceTexture");

        public static readonly int _FisheyeIntensity = Shader.PropertyToID("_FisheyeIntensity");
        public static readonly int _ZoomIntensity = Shader.PropertyToID("_ZoomIntensity");

        public static readonly int _CrtTexture = Shader.PropertyToID("_CrtTexture");
        public static readonly int _CrtTextureTiling = Shader.PropertyToID("_CrtTextureTiling");
        public static readonly int _CrtVisibility = Shader.PropertyToID("_CrtVisibility");

        public static readonly int _NoiseTexture = Shader.PropertyToID("_NoiseTexture");
        public static readonly int _NoiseTextureIntensity = Shader.PropertyToID("_NoiseTextureIntensity");
        public static readonly int _NoiseTextureTiling = Shader.PropertyToID("_NoiseTextureTiling");
	}

    public ClampedFloatParameter FisheyeIntensity = new ClampedFloatParameter(0f, -1f, 1f);
    public ClampedFloatParameter ZoomIntensity = new ClampedFloatParameter(0f, 0f, 1f);

    [Space]
    public Texture2DParameter CRTTexture = new Texture2DParameter(null);
    public FloatParameter CRTTextureTiling = new FloatParameter(0f);
    public ClampedFloatParameter CRTVisibility = new ClampedFloatParameter(0f, 0f, 1f);

    [Space]
    public Texture2DParameter NoiseTexture = new Texture2DParameter(null);
    public ClampedFloatParameter NoiseTextureIntensity = new ClampedFloatParameter(0f, 0f, 1f);
    public FloatParameter NoiseTextureTiling = new FloatParameter(0f);

    Material m_Material;

    public bool IsActive() => (FisheyeIntensity.value != 0f || ZoomIntensity.value != 0f || CRTVisibility.value != 0f || NoiseTextureIntensity.value != 0f) && m_Material != null;

    // Do not forget to add this post process in the Custom Post Process Orders list (Project Settings > Graphics > HDRP Settings).
    public override CustomPostProcessInjectionPoint injectionPoint => CustomPostProcessInjectionPoint.AfterPostProcess;

    const string kShaderName = "FullScreen/CRTPostEffect";

    public override void Setup()
    {
        if (Shader.Find(kShaderName) != null)
            m_Material = new Material(Shader.Find(kShaderName));
        else
            Debug.LogError($"Unable to find shader '{kShaderName}'. Post Process Volume CRTPostEffect is unable to load.");
    }

    public override void Render(CommandBuffer cmd, HDCamera camera, RTHandle source, RTHandle destination)
    {
        if (m_Material == null)
            return;

        cmd.SetGlobalFloat(ShaderBindings._FisheyeIntensity, FisheyeIntensity.value);
        cmd.SetGlobalFloat(ShaderBindings._ZoomIntensity, ZoomIntensity.value);

        cmd.SetGlobalTexture(ShaderBindings._CrtTexture, CRTTexture.value);
        cmd.SetGlobalFloat(ShaderBindings._CrtTextureTiling, CRTTextureTiling.value);
        cmd.SetGlobalFloat(ShaderBindings._CrtVisibility, CRTVisibility.value);

        cmd.SetGlobalTexture(ShaderBindings._NoiseTexture, NoiseTexture.value);
        cmd.SetGlobalFloat(ShaderBindings._NoiseTextureIntensity, NoiseTextureIntensity.value);
        cmd.SetGlobalFloat(ShaderBindings._NoiseTextureTiling, NoiseTextureTiling.value);

        cmd.SetGlobalTexture(ShaderBindings._SourceTexture, source);
        cmd.Blit(source, destination, m_Material, 0);

        //cmd.SetRenderTarget(destination);
        //CoreUtils.DrawFullScreen(cmd, m_Material);
    }

    public override void Cleanup()
    {
        CoreUtils.Destroy(m_Material);
    }
}
