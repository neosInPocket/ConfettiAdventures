using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class CandyPlayer : MonoBehaviour
{
	[SerializeField] private SpriteRenderer spriteRenderer;
	[SerializeField] new private Rigidbody2D rigidbody2D;

	public bool Enable
	{
		get
		{
			return enable;
		}
		set
		{
			enable = value;
			if (!value)
			{

			}
			else
			{

			}
		}
	}

	bool enable;

	private void Awake()
	{

	}

	private void Start()
	{
		rigidbody2D.velocity = Vector2.left * 2;
	}

	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.transform.parent.TryGetComponent<ScreenDivider>(out ScreenDivider divider))
		{
			if (divider.transform.position.x != 0)
			{
				rigidbody2D.velocity = Vector2.Reflect(rigidbody2D.velocity, Vector2.right);
			}
			else
			{
				rigidbody2D.velocity = Vector2.Reflect(rigidbody2D.velocity, Vector2.down);
			}
		}
	}
}
