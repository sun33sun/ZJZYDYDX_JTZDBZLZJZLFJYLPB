using System.Collections.Generic;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using System.Linq;

namespace UnityEngine.Rendering
{
	public class CustomPostProcessRenderPass : ScriptableRenderPass
	{
		List<CustomVolumeComponent> volumeComponents;   // 所有自定义后处理组件
		List<int> activeComponents; // 当前可用的组件下标

		string profilerTag;
		List<ProfilingSampler> profilingSamplers; // 每个组件对应的ProfilingSampler

		RenderTargetHandle source;  // 当前源与目标
		RenderTargetHandle destination;
		RenderTargetHandle tempRT0; // 临时RT
		RenderTargetHandle tempRT1;

		/// <param name="profilerTag">Profiler标识</param>
		/// <param name="volumeComponents">属于该RendererPass的后处理组件</param>
		public CustomPostProcessRenderPass(string profilerTag, List<CustomVolumeComponent> volumeComponents)
		{
			this.profilerTag = profilerTag;
			this.volumeComponents = volumeComponents;
			activeComponents = new List<int>(volumeComponents.Count);
			profilingSamplers = volumeComponents.Select(c => new ProfilingSampler(c.ToString())).ToList();

			tempRT0.Init("_TemporaryRenderTexture0");
			tempRT1.Init("_TemporaryRenderTexture1");
		}

		public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			throw new System.NotImplementedException();
		}
	}
}
