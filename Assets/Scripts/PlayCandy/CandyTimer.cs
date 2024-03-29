using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class CandyTimer : MonoBehaviour
{
	[SerializeField] private TMP_Text timerNumbers;
	[SerializeField] private Animator timeAnimator;
	public Action TimerExpired;
	private float currentTime;

	public void SetCandyTime(float time)
	{
		timerNumbers.text = $"{(int)time}s";
	}

	public void StartCountDown(float time)
	{
		timeAnimator.SetTrigger("start");
		StartCoroutine(StartTimer(time));
	}

	public void StopCountDown()
	{
		StopAllCoroutines();
		timeAnimator.SetTrigger("stop");
	}

	public IEnumerator StartTimer(float time)
	{
		currentTime = time;

		while (currentTime > 0)
		{
			currentTime -= Time.deltaTime;
			if (currentTime < 3f)
			{
				timerNumbers.color = Color.red;
			}

			timerNumbers.text = $"{(int)currentTime}s";
			yield return null;
		}

		currentTime = 0;
		if (TimerExpired != null)
		{
			TimerExpired();
		}
	}
}
