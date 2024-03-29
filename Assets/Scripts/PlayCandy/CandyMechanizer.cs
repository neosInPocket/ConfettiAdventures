using UnityEngine;

public class CandyMechanizer : MonoBehaviour
{
	[SerializeField] private CandyPlayer candyPlayer;
	[SerializeField] private AngrySquareSpawner angrySquareSpawner;

	private void Start()
	{
		angrySquareSpawner.StartSpawnRoutine();
	}
}
