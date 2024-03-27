using System.Linq;
using UnityEngine;

public class Volumer : MonoBehaviour
{
	[SerializeField] private AudioSource musicSource;
	private string destroyString;

	private void Start()
	{
		musicSource.volume = CandySave.audio;
	}

	private void Awake()
	{
		destroyString = "DontDestroyOnLoad";

		Volumer[] allVolumers = FindObjectsOfType<Volumer>();
		var existing = allVolumers.FirstOrDefault(x => x.gameObject.scene.name == destroyString);

		if (existing != null)
		{
			if (existing != this)
			{
				Destroy(this.gameObject);
				return;
			}
			else
			{
				DontDestroyOnLoad(this.gameObject);
			}
		}
		else
		{
			DontDestroyOnLoad(this.gameObject);
		}
	}

	public void Switcher(int switchOrNot)
	{
		musicSource.volume = switchOrNot;
		CandySave.music = switchOrNot;
		CandySave.SaveCandyData();
	}
}
