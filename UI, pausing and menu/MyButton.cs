using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class MyButton : MonoBehaviour
{
	public float increaseScaleOnHover = 1.2f;
	public float increaseScalePressed = 0.8f;

	float startScale;

	float currentScale;

	void Start()
	{
		currentScale = transform.localScale.x;
		startScale = transform.localScale.x;
	}

	void Update()
	{
		transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one * currentScale, 0.2f);
	}

	void OnMouseOver()
	{
		currentScale = increaseScaleOnHover;
	}

	void OnMouseExit()
	{
		currentScale = startScale;
	}

	public void ButtonPressed()
	{
		currentScale = increaseScalePressed;
	}
}
