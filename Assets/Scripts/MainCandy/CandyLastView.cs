using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CandyLastView : MonoBehaviour
{
	[SerializeField] private TMP_Text candyLevelPlayResult;
	[SerializeField] private TMP_Text gemstones;
	[SerializeField] private GameObject replayButton;
	[SerializeField] private GameObject nextLevelButton;

	public void ShowCandyView(int gemstonesAmount, int levelAmount)
	{
		bool completed = gemstonesAmount > 0;

		nextLevelButton.gameObject.SetActive(completed);
		replayButton.gameObject.SetActive(!completed);

		if (completed)
		{
			candyLevelPlayResult.text = "COMPLETED!";
			gemstones.text = gemstonesAmount.ToString();
		}
		else
		{
			candyLevelPlayResult.text = "LOSE";
			gemstones.text = gemstonesAmount.ToString();
		}

		CandySave.candyLevel += levelAmount;
		CandySave.gemStones += gemstonesAmount;
		CandySave.SaveCandyData();

		gameObject.SetActive(true);
	}

	public void CandyReturn()
	{
		SceneManager.LoadScene("ConfettiMenu");
	}

	public void Replay()
	{
		SceneManager.LoadScene("ConfettiGameScene");
	}
}
