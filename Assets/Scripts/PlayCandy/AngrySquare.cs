using System.Collections;
using UnityEngine;

public class AngrySquare : MonoBehaviour
{
	[SerializeField] private SpriteRenderer spriteRenderer;
	[SerializeField] private Vector2 disappearTime;
	[SerializeField] private Vector2 disappearTimeUpgraded;
	[SerializeField] private Vector2 attractingSpeed;
	private float currentLifeTime = 0;
	private float targetDisappearTime;
	private Color currentColor;
	private float currentSpeed;
	private Vector3 currentAngles;

	private void Start()
	{
		if (CandySave.secondSave == 1)
		{
			targetDisappearTime = Random.Range(disappearTimeUpgraded.x, disappearTimeUpgraded.y);
		}
		else
		{
			targetDisappearTime = Random.Range(disappearTime.x, disappearTime.y);
		}

		currentColor = spriteRenderer.color;
		currentAngles = transform.eulerAngles;

		if (Random.Range(0, 2) == 0)
		{
			currentSpeed = Random.Range(attractingSpeed.x, attractingSpeed.y);
		}
		else
		{
			currentSpeed = -Random.Range(attractingSpeed.x, attractingSpeed.y);
		}

	}

	private void Update()
	{
		currentLifeTime += Time.deltaTime;
		currentColor.a = 1 - currentLifeTime / targetDisappearTime;
		spriteRenderer.color = currentColor;
		currentAngles.z += currentSpeed * Time.deltaTime;

		if (currentLifeTime > targetDisappearTime)
		{
			Destroy(gameObject);
		}
	}
}
