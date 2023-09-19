using System.Collections.Generic;
using ProjectBase;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ZJZYDYDX_JTZDBZLZJZLFJYLPB
{
	public class PreviewPanelData : UIPanelData
	{
	}
	public partial class PreviewPanel : UIPanel
	{
		[SerializeField] private List<Sprite> _sprites;
		
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as PreviewPanelData ?? new PreviewPanelData();
			
			InitLeft();
			InitIntroduction();
			InitSelected();
		}

		void InitLeft()
		{
			Image imgMild = togMild.GetComponent<Image>();
			togMild.AddAwaitAction(isOn =>
			{
				imgMild.sprite = isOn ? _sprites[1] : _sprites[0];
			});
			
			Image imgModerate = togModerate.GetComponent<Image>();
			togModerate.AddAwaitAction(isOn =>
			{
				imgModerate.sprite = isOn ? _sprites[1] : _sprites[0];
			});
			
			Image imgSevere = togSevere.GetComponent<Image>();
			togSevere.AddAwaitAction(isOn =>
			{
				imgSevere.sprite = isOn ? _sprites[1] : _sprites[0];
			});
		}

		void InitIntroduction()
		{
			Image imgEmetic = togEmetic.GetComponent<Image>();
			togEmetic.AddAwaitAction(isOn =>
			{
				imgEmetic.sprite = isOn ? _sprites[3] : _sprites[2];
			});
			
			Image  imgGastrolavage = togGastrolavage.GetComponent<Image>();
			togGastrolavage.AddAwaitAction(isOn =>
			{
				imgGastrolavage.sprite = isOn ? _sprites[3] : _sprites[2];
			});
			
			Image  imgCatharsis = togCatharsis.GetComponent<Image>();
			togCatharsis.AddAwaitAction(isOn =>
			{
				imgCatharsis.sprite = isOn ? _sprites[3] : _sprites[2];
			});
			
			Image  imgInjection = togInjection.GetComponent<Image>();
			togInjection.AddAwaitAction(isOn =>
			{
				imgInjection.sprite = isOn ? _sprites[3] : _sprites[2];
			});
			
			Image  imgPerfusion = togPerfusion.GetComponent<Image>();
			togPerfusion.AddAwaitAction(isOn =>
			{
				imgPerfusion.sprite = isOn ? _sprites[3] : _sprites[2];
			});
			
			Image imgEmeticSelected = togEmeticSelected.GetComponent<Image>();
			togEmeticSelected.AddAwaitAction(isOn =>
			{
				imgEmeticSelected.sprite = isOn ? _sprites[3] : _sprites[2];
			});
		}

		void InitSelected()
		{
			Image  imgGastrolavageSelected = togGastrolavageSelected.GetComponent<Image>();
			togGastrolavageSelected.AddAwaitAction(isOn =>
			{
				imgGastrolavageSelected.sprite = isOn ? _sprites[3] : _sprites[2];
			});
			
			Image  imgCatharsisSelected = togCatharsisSelected.GetComponent<Image>();
			togCatharsisSelected.AddAwaitAction(isOn =>
			{
				imgCatharsisSelected.sprite = isOn ? _sprites[3] : _sprites[2];
			});
			
			Image  imgInjectionSelected = togInjectionSelected.GetComponent<Image>();
			togInjectionSelected.AddAwaitAction(isOn =>
			{
				imgInjectionSelected.sprite = isOn ? _sprites[3] : _sprites[2];
			});
			
			Image  imgPerfusionSelected = togPerfusionSelected.GetComponent<Image>();
			togPerfusionSelected.AddAwaitAction(isOn =>
			{
				imgPerfusionSelected.sprite = isOn ? _sprites[3] : _sprites[2];
			});
		}
		
		protected override void OnOpen(IUIData uiData = null)
		{
		}
		
		protected override void OnShow()
		{
		}
		
		protected override void OnHide()
		{
		}
		
		protected override void OnClose()
		{
		}
	}
}
