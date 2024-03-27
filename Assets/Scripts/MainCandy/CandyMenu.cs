using UnityEngine;
using UnityEngine.SceneManagement;

public class CandyMenu : MonoBehaviour
{
	[SerializeField] private string sceneName;

	public void CandyLevelLoad()
	{
		SceneManager.LoadScene(sceneName);
	}
}
