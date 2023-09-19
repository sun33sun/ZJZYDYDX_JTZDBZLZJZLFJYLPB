/****************************************************************************
 * 2023.9 ADMIN-20230222V
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ZJZYDYDX_JTZDBZLZJZLFJYLPB
{
	public partial class ObjSelectCase
	{
		[SerializeField] public UnityEngine.UI.Button btnCase1;
		[SerializeField] public UnityEngine.UI.Button btnCase2;

		public void Clear()
		{
			btnCase1 = null;
			btnCase2 = null;
		}

		public override string ComponentName
		{
			get { return "ObjSelectCase";}
		}
	}
}
