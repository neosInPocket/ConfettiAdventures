using System.Collections.Generic;
using UnityEngine;

public class CandySave : MonoBehaviour
{
	[SerializeField] private int newCandyData;
	public static int candyLevel;
	public static int gemStones;
	public static int firstSave;
	public static int secondSave;
	public static new int audio;
	public static int music;
	public static int candyGuide;

	private static Dictionary<SaveKeys, string> keySaves = new Dictionary<SaveKeys, string>()
	{
		{ SaveKeys.Level, "Level"},
		{ SaveKeys.CandyGuide, "CandyGuide"},
		{ SaveKeys.FirstSave, "FirstSave"},
		{ SaveKeys.SecondSave, "SecondSave"},
		{ SaveKeys.Audio, "Audio"},
		{ SaveKeys.Music, "Music"},
		{ SaveKeys.GemStones, "GemStones"},
	};

	private Dictionary<SaveKeys, int> defaultValues = new Dictionary<SaveKeys, int>()
	{
		{ SaveKeys.Level, 1},
		{ SaveKeys.CandyGuide, 1},
		{ SaveKeys.FirstSave, 0},
		{ SaveKeys.SecondSave, 0},
		{ SaveKeys.Audio, 1},
		{ SaveKeys.Music, 1},
		{ SaveKeys.GemStones, 120},
	};


	private void Awake()
	{
		if (newCandyData == 1)
		{
			LoadDefaultCandyData();
			SaveCandyData();
		}
		else
		{
			LoadCandyData();
		}
	}
	public void LoadDefaultCandyData()
	{
		candyLevel = defaultValues[SaveKeys.Level];
		gemStones = defaultValues[SaveKeys.GemStones];
		firstSave = defaultValues[SaveKeys.FirstSave];
		secondSave = defaultValues[SaveKeys.SecondSave];
		audio = defaultValues[SaveKeys.Audio];
		music = defaultValues[SaveKeys.Music];
		candyGuide = defaultValues[SaveKeys.CandyGuide];
	}

	public static void SaveCandyData()
	{
		SetIntSave(SaveKeys.Level, candyLevel);
		SetIntSave(SaveKeys.GemStones, gemStones);
		SetIntSave(SaveKeys.FirstSave, firstSave);
		SetIntSave(SaveKeys.SecondSave, secondSave);
		SetIntSave(SaveKeys.Audio, audio);
		SetIntSave(SaveKeys.Music, music);
		SetIntSave(SaveKeys.CandyGuide, candyGuide);
	}

	public void LoadCandyData()
	{
		candyLevel = GetIntSave(SaveKeys.Level);
		gemStones = GetIntSave(SaveKeys.GemStones);
		firstSave = GetIntSave(SaveKeys.FirstSave);
		secondSave = GetIntSave(SaveKeys.SecondSave);
		audio = GetIntSave(SaveKeys.Audio);
		music = GetIntSave(SaveKeys.Music);
		candyGuide = GetIntSave(SaveKeys.CandyGuide);
	}

	private static void SetIntSave(SaveKeys saveKey, int saveValue)
	{
		PlayerPrefs.SetInt(keySaves[saveKey], saveValue);
	}

	private int GetIntSave(SaveKeys key)
	{
		return PlayerPrefs.GetInt(keySaves[key], defaultValues[key]);
	}
}

public enum SaveKeys
{
	Level,
	GemStones,
	Audio,
	Music,
	FirstSave,
	SecondSave,
	CandyGuide
}