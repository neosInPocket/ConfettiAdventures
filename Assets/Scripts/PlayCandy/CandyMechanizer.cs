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
		targetTimeLeft = 10 / CandySave.candyLevel + 10;
		targetStars = (int)(6 * Mathf.Log(CandySave.candyLevel + 1) + 2);
		targetCoinsReward = (int)(610 * Mathf.Log(CandySave.candyLevel + 1) + 15);

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
	}

	public void DisableAllSubscribers()
	{
		Touch.onFingerDown -= CandyStart;
	}

	public void AddScore()
	{

	}

	private void OnDestroy()
	{
		DisableAllSubscribers();
	}
}
