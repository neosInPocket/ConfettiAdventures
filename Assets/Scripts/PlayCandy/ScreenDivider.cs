using UnityEngine;

public class ScreenDivider : MonoBehaviour
{
	[SerializeField] private SpriteRenderer leftDivider;
	[SerializeField] private SpriteRenderer rightDivider;
	[SerializeField] private SpriteRenderer upDivider;
	[SerializeField] private SpriteRenderer downDivider;

	private void Start()
	{
		var ss = new Vector2(Camera.main.orthographicSize * (float)Screen.width / (float)Screen.height, Camera.main.orthographicSize);

		leftDivider.size = new Vector2(1.04f, 2 * ss.y);
		rightDivider.size = leftDivider.size;

		leftDivider.transform.position = new Vector2(-ss.x - 1.04f / 2, 0);
		rightDivider.transform.position = -leftDivider.transform.position;

		upDivider.size = new Vector2(2 * ss.x, 1.04f);
		downDivider.size = upDivider.size;

		upDivider.transform.position = new Vector2(0, ss.y + 1.04f / 2);
		downDivider.transform.position = -upDivider.transform.position;
	}
}
