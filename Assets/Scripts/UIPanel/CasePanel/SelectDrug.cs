/****************************************************************************
 * 2023.8 ADMIN-20230222V
 ****************************************************************************/

using System;
using System.Collections.Generic;
using ProjectBase;
using ProjectBase.DataClass;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using UnityEngine.Serialization;

namespace ZJZYDYDX_JTZDBZLZJZLFJYLPB
{
	public partial class SelectDrug : UIElement
	{
		[SerializeField]public IntStringArrayDictionary drugDic = new IntStringArrayDictionary();
		[SerializeField] private List<DrugItem> _drugItems;
		public List<SelectedDrugItem> _selectedDrugItems;

		private void Awake()
		{
			for (var i = 0; i < _drugItems.Count; i++)
			{
				int index = i;
				_drugItems[i].tog.onValueChanged.AddListener(isOn =>
				{
					_selectedDrugItems[index].gameObject.SetActive(isOn);
				});
			}
			
			for (var i = 0; i < _selectedDrugItems.Count; i++)
			{
				int index = i;
				_selectedDrugItems[i].btn.onClick.AddListener(() =>
				{
					_drugItems[index].tog.isOn = false;
					_selectedDrugItems[index].gameObject.SetActive(false);
					_drugItems[index].tog.isOn = false;
				});
			}
			btnSubmitDrug.AddAwaitAction(async () =>
			{
				//如果错误
				await imgRightDrug.ShowAsync();
				// //如果正确
				// await imgInputWeight.ShowAsync();
			});
			
			btnConfirmRightDrug.AddAwaitAction(async () =>
			{
				await imgRightDrug.HideAsync();
				await imgInputWeight.ShowAsync();
			});
		}
		
		protected override void OnBeforeDestroy()
		{
		}
		
		void Init()
		{
			foreach (var selectedDrugItem in _selectedDrugItems)
			{
				selectedDrugItem.gameObject.SetActive(false);
			}
			
			foreach (var drugItem in _drugItems)
			{
				drugItem.tog.isOn = true;
			}
		}
	}
}