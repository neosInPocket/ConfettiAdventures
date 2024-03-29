using System;
using System.Collections;
using UnityEngine;

public class PointStar : MonoBehaviour
{
	[SerializeField] private SpriteRenderer spriteRenderer;
	[SerializeField] private new CircleCollider2D collider2D;
	[SerializeField] private GameObject glowEffect;
	[SerializeField] private GameObject sparkles;
	public float ColliderRadius => collider2D.radius;
	public Action StarCollected;

	public void CollectStar()
	{
		if (StarCollected != null)
		{
			StarCollected();
		}

		StartCoroutine(GlowEffect());
	}

	private IEnumerator GlowEffect()
	{
		sparkles.gameObject.SetActive(false);
		var clr = spriteRenderer.color;
		clr.a = 0;
		spriteRenderer.color = clr;
		collider2D.enabled = false;

		var glowObject = Instantiate(glowEffect, transform.position, Quaternion.identity, null);
		yield return new WaitForSeconds(1f);
		Destroy(glowObject.gameObject);
		Destroy(gameObject);
	}
}
