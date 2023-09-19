/****************************************************************************
 * 2023.9 ADMIN-20230222V
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ZJZYDYDX_JTZDBZLZJZLFJYLPB
{
	public partial class ImgRightDrug
	{
		[SerializeField] public UnityEngine.UI.Button btnConfirmDrug;
		[SerializeField] public UnityEngine.RectTransform RightDrugGroup;
		[SerializeField] public RectTransform RightDrugParent;
		[SerializeField] public UnityEngine.UI.Button btnPre;
		[SerializeField] public UnityEngine.UI.Button btnNext;

		public void Clear()
		{
			btnConfirmDrug = null;
			RightDrugGroup = null;
			RightDrugParent = null;
			btnPre = null;
			btnNext = null;
		}

		public override string ComponentName
		{
			get { return "ImgRightDrug";}
		}
	}
}
