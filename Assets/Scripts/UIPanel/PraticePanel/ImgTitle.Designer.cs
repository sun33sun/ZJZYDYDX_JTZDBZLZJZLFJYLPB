/****************************************************************************
 * 2023.9 ADMIN-20230222V
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ZJZYDYDX_JTZDBZLZJZLFJYLPB
{
	public partial class ImgTitle
	{
		[SerializeField] public UnityEngine.UI.Button btnConfirm;
		[SerializeField] public UnityEngine.RectTransform imgDoubleConfirm;
		[SerializeField] public UnityEngine.UI.Button btnDoubleConfirm;
		[SerializeField] public UnityEngine.UI.Button btnDoubleCancel;

		public void Clear()
		{
			btnConfirm = null;
			imgDoubleConfirm = null;
			btnDoubleConfirm = null;
			btnDoubleCancel = null;
		}

		public override string ComponentName
		{
			get { return "ImgTitle";}
		}
	}
}
