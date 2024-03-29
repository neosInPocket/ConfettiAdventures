using System.Collections;
using UnityEngine;

public class AngrySquareSpawner : MonoBehaviour
{
	[SerializeField] private PointStar pointStarPrefab;
	[SerializeField] private AngrySquare angrySquarePrefab;
	[SerializeField] private float topEdgeSpawn;
	[SerializeField] private float spawnOffset;
	[SerializeField] private float castRadius;
	[SerializeField] private Vector2 spawnRates;

	public bool Enable
	{
		get => enable;
		set
		{
			enable = value;
			if (value)
			{
				StartSpawnRoutine();
			}
			else
			{
				StopSpawnRoutine();
			}
		}
	}

	private Vector2 screenSize;
	private Vector2 spawnEdgesX;
	private Vector2 spawnEdgesY;
	private PointStar lastStar;

	private bool enable;

	private void Awake()
	{
		screenSize = new Vector2(Camera.main.orthographicSize * (float)Screen.width / (float)Screen.height, Camera.main.orthographicSize);
		spawnEdgesX = new Vector2(-screenSize.x + spawnOffset, screenSize.x - spawnOffset);
		spawnEdgesY = new Vector2(-screenSize.y + spawnOffset, 2 * screenSize.y * topEdgeSpawn - screenSize.y - spawnOffset);
	}

	public void StartSpawnRoutine()
	{
		StarSpawn();
		CubeSpawnAction();
	}

	private void CubeSpawnAction()
	{
		StartCoroutine(SquareSpawn());
	}

	public void StopSpawnRoutine()
	{
		StopAllCoroutines();
		lastStar.StarCollected = null;
	}

	private void StarSpawn()
	{
		bool foundPosition = false;
		Vector2 position = new Vector2(Random.Range(spawnEdgesX.x, spawnEdgesX.y), Random.Range(spawnEdgesY.x, spawnEdgesY.y));

		while (!foundPosition)
		{
			var racyast = Physics2D.OverlapCircle(position, castRadius);
			if (racyast == null)
			{
				foundPosition = true;
			}
			else
			{
				position = new Vector2(Random.Range(spawnEdgesX.x, spawnEdgesX.y), Random.Range(spawnEdgesY.x, spawnEdgesY.y));
			}
		}

		lastStar = Instantiate(pointStarPrefab, position, Quaternion.identity, transform);
		lastStar.StarCollected = StarSpawn;
	}

	private IEnumerator SquareSpawn()
	{
		bool foundPosition = false;
		Vector2 position = new Vector2(Random.Range(spawnEdgesX.x, spawnEdgesX.y), Random.Range(spawnEdgesY.x, spawnEdgesY.y));

		while (!foundPosition)
		{
			if (Physics2D.OverlapCircle(position, castRadius) == null)
			{
				foundPosition = true;
			}
			else
			{
				position = new Vector2(Random.Range(spawnEdgesX.x, spawnEdgesX.y), Random.Range(spawnEdgesY.x, spawnEdgesY.y));
			}
		}

		var star = Instantiate(angrySquarePrefab, position, Quaternion.identity, transform);

		float randomTime = 1 / Random.Range(spawnRates.x, spawnRates.y);
		yield return new WaitForSeconds(randomTime);

		CubeSpawnAction();
	}
}
