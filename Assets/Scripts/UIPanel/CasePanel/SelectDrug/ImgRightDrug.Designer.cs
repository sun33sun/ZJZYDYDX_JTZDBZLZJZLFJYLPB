/****************************************************************************
 * 2023.10 ADMIN-20230222V
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ZJZYDYDX_JTZDBZLZJZLFJYLPB
{
	public partial class ImgRightDrug
	{
		[SerializeField] public ProjectBase.HorizontalSegmentation RightDrugGroup;
		[SerializeField] public UnityEngine.UI.Button btnErrorLeft;
		[SerializeField] public UnityEngine.UI.Button btnErrorRight;
		[SerializeField] public UnityEngine.UI.Button btnConfirmRightDrug;

		public void Clear()
		{
			RightDrugGroup = null;
			btnErrorLeft = null;
			btnErrorRight = null;
			btnConfirmRightDrug = null;
		}

		public override string ComponentName
		{
			get { return "ImgRightDrug";}
		}
	}
}
