using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CandyShopHolder : MonoBehaviour
{
	[SerializeField] private Button leftNaviButton;
	[SerializeField] private Button rightNaviButton;
	[SerializeField] private Image leftNavi;
	[SerializeField] private Image rightNavi;
	[SerializeField] private CandyShopItem[] items;
	[SerializeField] private new TMP_Text name;
	[SerializeField] private TMP_Text description;
	[SerializeField] private TMP_Text cost;
	[SerializeField] private Button purchaseButton;
	[SerializeField] private Image icon;
	[SerializeField] private TMP_Text costStatus;
	[SerializeField] private GemsStatistics[] gemsStatistics;
	private CandyShopItem currentItem;

	private bool leftNaviEnabled
	{
		get => _leftNavi;
		set
		{
			rightNavi.enabled = !value;
			leftNavi.enabled = value;

			rightNaviButton.interactable = value;
			leftNaviButton.interactable = !value;
		}
	}

	private bool _leftNavi;

	private void Start()
	{
		SelectShopItem(true);
	}

	public void SelectShopItem(bool leftNavi)
	{
		if (leftNavi)
		{
			leftNaviEnabled = true;
			SetUpgradeInfo(items[0]);
		}
		else
		{
			leftNaviEnabled = false;
			SetUpgradeInfo(items[1]);
		}
	}

	public void SetUpgradeInfo(CandyShopItem item)
	{
		currentItem = item;

		name.text = item.upgradeName;
		description.text = item.upgradeDescription;
		cost.text = item.upgradeCost.ToString();
		icon.sprite = item.sprite;

		bool enough = CandySave.gemStones >= item.upgradeCost;
		bool bought;

		if (item.firstUpgrade)
		{
			bought = Convert.ToBoolean(CandySave.firstSave);
		}
		else
		{
			bought = Convert.ToBoolean(CandySave.secondSave);
		}

		if (!bought)
		{
			if (CandySave.gemStones < item.upgradeCost)
			{
				costStatus.text = "NOT ENOUGH GEMSTONES";
				costStatus.color = Color.red;
				purchaseButton.interactable = false;
			}
			else
			{
				costStatus.text = "AVALIABLE";
				costStatus.color = Color.white;
				purchaseButton.interactable = true;
			}
		}
		else
		{
			costStatus.text = "PURCHASED";
			costStatus.color = Color.green;
			purchaseButton.interactable = false;
		}

		for (int i = 0; i < gemsStatistics.Length; i++)
		{
			gemsStatistics[i].RefreshCandyInfo();
		}
	}

	public void PurchaseItem()
	{
		CandySave.gemStones -= currentItem.upgradeCost;
		if (currentItem.firstUpgrade)
		{
			CandySave.firstSave = 1;
			CandySave.SaveCandyData();
			SetUpgradeInfo(currentItem);
		}
		else
		{
			CandySave.secondSave = 1;
			CandySave.SaveCandyData();
			SetUpgradeInfo(currentItem);
		}
	}
}
