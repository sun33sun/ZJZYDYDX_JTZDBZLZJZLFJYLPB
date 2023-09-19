/****************************************************************************
 * 2023.9 ADMIN-20230222V
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ZJZYDYDX_JTZDBZLZJZLFJYLPB
{
	public partial class ImgWait
	{
		[SerializeField] public TMPro.TextMeshProUGUI tmpTipContent;
		[SerializeField] public UnityEngine.UI.Button btnConfirmTip;
		[SerializeField] public TMPro.TextMeshProUGUI tmpTipButton;

		public void Clear()
		{
			tmpTipContent = null;
			btnConfirmTip = null;
			tmpTipButton = null;
		}

		public override string ComponentName
		{
			get { return "ImgWait";}
		}
	}
}
