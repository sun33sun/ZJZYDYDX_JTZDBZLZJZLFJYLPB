/****************************************************************************
 * 2023.9 ADMIN-20230222V
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ZJZYDYDX_JTZDBZLZJZLFJYLPB
{
	public partial class SelectDrug
	{
		[SerializeField] public TMPro.TextMeshProUGUI tmpRecordContent;
		[SerializeField] public UnityEngine.UI.Button btnSubmitDrug;
		[SerializeField] public ImgRightDrug imgRightDrug;
		[SerializeField] public ImgInputWeight imgInputWeight;

		public void Clear()
		{
			tmpRecordContent = null;
			btnSubmitDrug = null;
			imgRightDrug = null;
			imgInputWeight = null;
		}

		public override string ComponentName
		{
			get { return "SelectDrug";}
		}
	}
}
