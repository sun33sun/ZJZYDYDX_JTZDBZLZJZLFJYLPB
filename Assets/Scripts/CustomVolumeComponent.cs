using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace UnityEngine.Rendering
{
	/// 后处理插入位置
	public enum CustomPostProcessInjectionPoint
	{
		AfterOpaqueAndSky, BeforePostProcess, AfterPostProcess
	}

	public abstract class CustomVolumeComponent : VolumeComponent, IPostProcessComponent, IDisposable
	{
		/// <summary>
		/// 在InjectionPoint中的渲染顺序
		/// </summary>
		public virtual int OrderInPass => 0;

		public virtual CustomPostProcessInjectionPoint InjectionPoint => CustomPostProcessInjectionPoint.AfterPostProcess;

		/// <summary>
		/// 初始化，将在RenderPass加入队列时调用
		/// </summary>
		public abstract void Setup();

		/// <summary>
		/// 执行渲染
		/// </summary>
		/// <param name="cmd"></param>
		/// <param name="renderingData"></param>
		/// <param name="source"></param>
		/// <param name="destination"></param>
		public abstract void Render(CommandBuffer cmd, ref RenderingData renderingData, RenderTargetIdentifier source, RenderTargetIdentifier destination);

		#region IPostProcessComponent
		/// <summary>
		/// 返回当前组件是否处于激活状态
		/// </summary>
		/// <returns></returns>
		public abstract bool IsActive();

		public bool IsTileCompatible() => false;
		#endregion


		#region IDisposable
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		//释放资源
		public virtual void Dispose(bool disposing) { }
		#endregion

	}
}

