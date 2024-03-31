using UnityEngine;
using UnityEngine.UI;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using Finger = UnityEngine.InputSystem.EnhancedTouch.Finger;
using TMPro;

public class CandyMechanizer : MonoBehaviour
{
	[SerializeField] private CandyPlayer candyPlayer;
	[SerializeField] private AngrySquareSpawner angrySquareSpawner;
	[SerializeField] private CandyGuide candyGuide;
	[SerializeField] private CandyLastView candyLastView;
	[SerializeField] private CandyTimer candyTimer;
	[SerializeField] private Image starsFill;
	[SerializeField] private TMP_Text tapCandy;
	private int currentStars;
	private int targetStars;
	private int targetCoinsReward;
	private float targetTimeLeft;

	private void Start()
	{
		starsFill.fillAmount = 0;
		targetTimeLeft = 10f / Mathf.Sqrt(CandySave.candyLevel) + 15f;
		targetStars = (int)(3 * Mathf.Log(CandySave.candyLevel + 1) + 2);
		targetCoinsReward = (int)(50 * Mathf.Log(CandySave.candyLevel + 1) + 15);

		candyTimer.SetCandyTime(targetTimeLeft);
		candyGuide.StartCandyGuide(out bool isGuide);

		if (isGuide)
		{
			candyGuide.CandyGuideCompleted += CandyGuideCompleted;
		}
		else
		{
			CandyGuideCompleted(gameObject, 0, true);
		}
	}

	public void CandyGuideCompleted(GameObject gameObject, int value, bool randomizer)
	{
		Touch.onFingerDown += CandyStart;
		tapCandy.gameObject.SetActive(true);
		tapCandy.text = $"START LEVEL {CandySave.candyLevel}";
	}

	private void CandyStart(Finger finger)
	{
		Touch.onFingerDown -= CandyStart;
		tapCandy.gameObject.SetActive(false);
		angrySquareSpawner.StartSpawnRoutine();
		candyPlayer.Enable = true;
		candyPlayer.ChangeNavigationDirection(finger);
		candyPlayer.StarCollected += StarCollected;
		candyPlayer.Popped += Popped;
		candyTimer.StartCountDown(targetTimeLeft);
		candyTimer.TimerExpired += Popped;
	}

	public void StarCollected()
	{
		if (currentStars + 1 >= targetStars)
		{
			DisableAllSubscribers();
			currentStars = targetStars;
			candyLastView.ShowCandyView(targetCoinsReward, 1);
		}
		else
		{
			currentStars++;
		}

		starsFill.fillAmount = (float)currentStars / (float)targetStars;
	}

	public void Popped()
	{
		DisableAllSubscribers();
		candyLastView.ShowCandyView(0, 0);
	}

	public void DisableAllSubscribers()
	{
		angrySquareSpawner.Enable = false;
		candyPlayer.Enable = false;
		candyTimer.StopCountDown();
		Touch.onFingerDown -= CandyStart;
		candyPlayer.StarCollected -= StarCollected;
		candyPlayer.Popped -= Popped;
	}

	private void OnDestroy()
	{
		DisableAllSubscribers();
	}
}
