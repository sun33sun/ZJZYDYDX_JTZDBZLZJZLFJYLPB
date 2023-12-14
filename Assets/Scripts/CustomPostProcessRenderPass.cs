using System.Collections.Generic;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using System.Linq;

namespace UnityEngine.Rendering
{
	public class CustomPostProcessRenderPass : ScriptableRenderPass
	{
		List<CustomVolumeComponent> volumeComponents;   // �����Զ���������
		List<int> activeComponents; // ��ǰ���õ�����±�

		string profilerTag;
		List<ProfilingSampler> profilingSamplers; // ÿ�������Ӧ��ProfilingSampler

		RenderTargetHandle source;  // ��ǰԴ��Ŀ��
		RenderTargetHandle destination;
		RenderTargetHandle tempRT0; // ��ʱRT
		RenderTargetHandle tempRT1;

		/// <param name="profilerTag">Profiler��ʶ</param>
		/// <param name="volumeComponents">���ڸ�RendererPass�ĺ������</param>
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
