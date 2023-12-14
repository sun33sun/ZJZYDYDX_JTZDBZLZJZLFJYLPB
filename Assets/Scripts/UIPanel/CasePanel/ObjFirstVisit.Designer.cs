/****************************************************************************
 * 2023.10 ADMIN-20230222V
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ZJZYDYDX_JTZDBZLZJZLFJYLPB
{
	public partial class ObjFirstVisit
	{
		[SerializeField] public UnityEngine.UI.Button btnSubmit;
		[SerializeField] public TMPro.TextMeshProUGUI tmpRecordContent;

		public void Clear()
		{
			btnSubmit = null;
			tmpRecordContent = null;
		}

		public override string ComponentName
		{
			get { return "ObjFirstVisit";}
		}
	}
}
