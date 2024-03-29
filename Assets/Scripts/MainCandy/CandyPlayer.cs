using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using Random = UnityEngine.Random;
using System.Threading;

public class CandyPlayer : MonoBehaviour
{
	private const float V = 90f;
	[SerializeField] private SpriteRenderer spriteRenderer;
	[SerializeField] new private Rigidbody2D rigidbody2D;
	[SerializeField] private Transform canvas;
	[SerializeField] private float naviRotSpeed;
	[SerializeField] private float naviRotSpeedDivider;
	[SerializeField] private float magn;
	[SerializeField] private float[] velocitiesSaves;
	[SerializeField] private Transform explosion;
	private float currentRotation;
	private float currentLocalRotation;
	public float CurrentRotation => currentRotation;

	public void RotateInstant(float value)
	{
		currentRotation = value;
		currentLocalRotation = value;
		var eul = canvas.transform.eulerAngles;
		eul.z = value;
		canvas.transform.eulerAngles = eul;
	}
	private float velocity;

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
				Touch.onFingerDown -= ChangeNavigationDirection;
			}
			else
			{
				Touch.onFingerDown += ChangeNavigationDirection;
			}
		}
	}

	bool enable;
	public Action Popped;
	public Action StarCollected;

	private void Awake()
	{
		EnhancedTouchSupport.Enable();
		TouchSimulation.Enable();
	}

	private void Start()
	{
		velocity = velocitiesSaves[CandySave.firstSave];
		RotateInstant(0);
	}

	public void ChangeNavigationDirection(Finger finger)
	{
		Debug.Log(currentRotation);
		rigidbody2D.velocity = new Vector2(Mathf.Cos((currentRotation + V) * Mathf.Deg2Rad), Mathf.Sin((currentRotation + V) * Mathf.Deg2Rad)) * velocity;
		StopAllCoroutines();
		StartCoroutine(RotateNavi());
	}

	private IEnumerator RotateNavi()
	{
		int direction = Random.Range(0, 2) == 0 ? -1 : 1;
		var euler = canvas.transform.eulerAngles;
		float dest = currentRotation + V * direction;
		currentRotation += V * direction;

		float delta = 0;
		float magnitude = Mathf.Abs(dest - currentLocalRotation) / V;

		bool result = (currentLocalRotation < dest && direction > 1) || (currentLocalRotation > dest && direction < 1);

		while ((currentLocalRotation < dest && direction > 0) || (currentLocalRotation > dest && direction < 0))
		{
			delta = velocity * Time.deltaTime * direction * (magnitude + magn) * naviRotSpeedDivider;

			euler.z += delta;
			currentLocalRotation += delta;
			canvas.transform.eulerAngles = euler;
			magnitude = Mathf.Abs(dest - currentLocalRotation) / V;
			yield return null;
		}

		currentLocalRotation = currentRotation;
		euler.z = currentLocalRotation;
		canvas.transform.eulerAngles = euler;
	}

	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.name == "LeftDivider" || collider.name == "RightDivider")
		{
			rigidbody2D.velocity = Vector2.Reflect(rigidbody2D.velocity, Vector2.right);
			return;
		}

		if (collider.name == "UpDivider" || collider.name == "DownDivider")
		{
			rigidbody2D.velocity = Vector2.Reflect(rigidbody2D.velocity, Vector2.down);
			return;
		}

		if (collider.TryGetComponent<AngrySquare>(out AngrySquare angrySquare))
		{
			PopCandy();
			return;
		}

		if (collider.TryGetComponent<PointStar>(out PointStar star))
		{
			star.CollectStar();

			if (StarCollected != null)
			{
				StarCollected();
			}

		}
	}

	public void PopCandy()
	{
		Enable = false;
		spriteRenderer.enabled = false;
		rigidbody2D.velocity = Vector2.zero;
		explosion.gameObject.SetActive(true);
		canvas.gameObject.SetActive(false);

		if (Popped != null)
		{
			Popped();
		}
	}

	private void OnDestroy()
	{
		Enable = false;
	}
}
