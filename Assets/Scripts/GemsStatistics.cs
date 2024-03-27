using TMPro;
using UnityEngine;

public class GemsStatistics : MonoBehaviour
{
	[SerializeField] private TMP_Text gemstones;
	[SerializeField] private TMP_Text currentLevel;

	private void Start()
	{
		gemstones.text = CandySave.gemStones.ToString();
		if (currentLevel == null) return;
		currentLevel.text = CandySave.candyLevel.ToString();
	}

	public void RefreshCandyInfo()
	{
		gemstones.text = CandySave.gemStones.ToString();
		if (currentLevel == null) return;
		currentLevel.text = CandySave.candyLevel.ToString();
	}
}
