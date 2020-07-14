using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketButtonBehaviour : MonoBehaviour
{
	public Canon canon;
	public float reloadingDuration = 2;
	public bool isInfinityRockets;
	public int numberOfRockets = 1;

	private bool allowedShooting = true;

	void OnTriggerEnter()
	{
		if (allowedShooting)
		{
			Activated();
		}
	}

	private float shiftDown = 0.2f;

	void Activated()
	{
		if (numberOfRockets > 0 || isInfinityRockets)
		{
			canon.Activate();
			numberOfRockets -= 1;
		}

		transform.Translate(0, -shiftDown, 0);

		StartCoroutine(Reloading());
	}

	IEnumerator Reloading()
	{
		allowedShooting = false;

		yield return new WaitForSeconds(reloadingDuration);

		transform.Translate(0, shiftDown, 0);
		allowedShooting = true;
	}
}
