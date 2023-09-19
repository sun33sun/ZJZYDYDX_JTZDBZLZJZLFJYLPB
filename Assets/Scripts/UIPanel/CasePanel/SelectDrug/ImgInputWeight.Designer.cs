/****************************************************************************
 * 2023.9 ADMIN-20230222V
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ZJZYDYDX_JTZDBZLZJZLFJYLPB
{
	public partial class ImgInputWeight
	{
		[SerializeField] public UnityEngine.UI.Button btnConfirmTip;
		[SerializeField] public UnityEngine.RectTransform RightDrugGroup;
		[SerializeField] public UnityEngine.UI.Button btnPre;
		[SerializeField] public UnityEngine.UI.Button btnNext;

		public void Clear()
		{
			btnConfirmTip = null;
			RightDrugGroup = null;
			btnPre = null;
			btnNext = null;
		}

		public override string ComponentName
		{
			get { return "ImgInputWeight";}
		}
	}
}
