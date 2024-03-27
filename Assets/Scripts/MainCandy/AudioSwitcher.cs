using System;
using UnityEngine;
using UnityEngine.UI;

public class AudioSwitcher : MonoBehaviour
{
	[SerializeField] private Button audioButton;
	[SerializeField] private Button musicButton;
	[SerializeField] private Image audioImage;
	[SerializeField] private Image musicImage;
	[SerializeField] private Sprite enabledSprite;
	[SerializeField] private Sprite disabledSprite;
	private Volumer volumer;

	private void Start()
	{
		volumer = GameObject.FindObjectOfType<Volumer>();
		if (volumer == null)
		{
			Debug.LogError("Volumer is null");
		}

		SetButton(audioImage, CandySave.audio == 1);
		SetButton(musicImage, CandySave.music == 1);

		audioButton.onClick.AddListener(SwitchAudioButton);
		musicButton.onClick.AddListener(SwitchMusicButton);
	}

	public void SetButton(Image image, bool value)
	{
		if (value)
		{
			image.sprite = enabledSprite;
		}
		else
		{
			image.sprite = disabledSprite;
		}
	}

	public void SwitchAudioButton()
	{
		SwitchButton(audioImage, false);
	}

	public void SwitchMusicButton()
	{
		SwitchButton(musicImage, true);
	}

	public bool SwitchButton(Image image, bool isMusic)
	{
		bool returnValue;

		if (image.sprite == enabledSprite)
		{
			SetButton(image, false);
			returnValue = false;
		}
		else
		{
			SetButton(image, true);
			returnValue = true;
		}

		if (isMusic)
		{
			volumer.Switcher(Convert.ToInt32(returnValue));
		}
		else
		{
			CandySave.audio = Convert.ToInt32(returnValue);
			CandySave.SaveCandyData();
		}

		return returnValue;
	}
}
