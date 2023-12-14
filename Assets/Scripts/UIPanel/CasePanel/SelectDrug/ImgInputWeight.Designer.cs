/****************************************************************************
 * 2023.10 ADMIN-20230222V
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ZJZYDYDX_JTZDBZLZJZLFJYLPB
{
	public partial class ImgInputWeight
	{
		[SerializeField] public UnityEngine.UI.Button btnSubmit;
		[SerializeField] public ProjectBase.HorizontalSegmentation RightDrugGroup;

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
