using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class CandyGuide : MonoBehaviour
{
	[SerializeField] private TMP_Text candyCharacterText;
	[SerializeField] private GameObject cursor;
	public Action<GameObject, int, bool> CandyGuideCompleted;
	private Animator animator;

	private void Awake()
	{
		animator = cursor.GetComponent<Animator>();
	}

	public void StartCandyGuide(out bool guideAvaliable)
	{
		guideAvaliable = CandySave.candyGuide == 1;

		if (!guideAvaliable)
		{
		}
		else
		{
			CandySave.candyGuide = 0;
			CandySave.SaveCandyData();

			GetCandyGuide();
		}
	}

	public void GetCandyGuide()
	{
		this.gameObject.SetActive(true);
		Touch.onFingerDown += CandySphere;
		candyCharacterText.text = "WELCOME TO confetti adventues!";
	}

	public void CandySphere(Finger finger)
	{
		Touch.onFingerDown -= CandySphere;
		Touch.onFingerDown += YellowBooster;
		candyCharacterText.text = "This is your candy! it is controlled using the yellow guide located above it";
		animator.gameObject.SetActive(true);
	}

	public void YellowBooster(Finger finger)
	{
		Touch.onFingerDown -= YellowBooster;
		Touch.onFingerDown += AngrySquares;
		candyCharacterText.text = "this guide indicates which direction your candy will fly after you tap on the screen";
		animator.SetTrigger("booster");
	}

	public void AngrySquares(Finger finger)
	{
		Touch.onFingerDown -= AngrySquares;
		Touch.onFingerDown += TimeLeft;
		candyCharacterText.text = "be careful! As you progress through the level, red squares will suddenly appear, causing your candy to break!";
		animator.SetTrigger("squares");
	}

	public void TimeLeft(Finger finger)
	{
		Touch.onFingerDown -= TimeLeft;
		Touch.onFingerDown += StarsCollect;
		candyCharacterText.text = "completing the level is limited by time: when the timer expires, you will lose";
		animator.SetTrigger("timeleft");
	}

	public void StarsCollect(Finger finger)
	{
		Touch.onFingerDown -= StarsCollect;
		Touch.onFingerDown += ReclaimedPhase;
		candyCharacterText.text = "collect the required number of stars, complete the level and get your reward! Good luck!";
		animator.SetTrigger("starscollect");
	}

	public void ReclaimedPhase(Finger finger)
	{
		Touch.onFingerDown -= ReclaimedPhase;
		if (CandyGuideCompleted != null)
		{
			CandyGuideCompleted(gameObject, (int)Mathf.Sqrt(2), UnityEngine.Random.Range(0, 2) == 0);
		}

		if (gameObject != null)
		{
			gameObject.SetActive(false);
		}
	}

	private void OnDestroy()
	{
		Touch.onFingerDown -= ReclaimedPhase;
		Touch.onFingerDown -= CandySphere;
		Touch.onFingerDown -= StarsCollect;
		Touch.onFingerDown -= TimeLeft;
		Touch.onFingerDown -= YellowBooster;
		Touch.onFingerDown -= CandySphere;
	}
}
