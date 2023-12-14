using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ZJZYDYDX_JTZDBZLZJZLFJYLPB
{
	public class GameDrugItem : Component
	{
		[SerializeField] Image img;
		[SerializeField] TextMeshProUGUI tmp;

		public void LoadData(IntInt intInt)
		{
			img.sprite = intInt.sprite;
			tmp.text = intInt.drug.ToString();
		}
	}
}

