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
		[SerializeField] public UnityEngine.UI.Button btnSubmit;
		[SerializeField] public UnityEngine.RectTransform RightDrugGroup;

		public void Clear()
		{
			btnSubmit = null;
			RightDrugGroup = null;
		}

		public override string ComponentName
		{
			get { return "ImgInputWeight";}
		}
	}
}
