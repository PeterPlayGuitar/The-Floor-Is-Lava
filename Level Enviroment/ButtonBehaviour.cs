using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehaviour : MonoBehaviour
{
	private bool isPressed = false;
	private bool isGoingForward = true;
	private float buttonGoesDownDistance = 0.15f;
	Vector3 originalPos;

	public float activatedDuration = 3;
	public bool canBeActivatedMultipleTimes = true;
	public bool canGoBack = true;
	public Transform objectToMove;
	public Vector3 shifting = new Vector3(3, 0, 0);

	void Start()
	{
		originalPos = objectToMove.position;
	}

	void OnCollisionEnter()
	{
		if (!isPressed)
		{
			isPressed = true;
			StartCoroutine(Activate());
			transform.Translate(new Vector3(0, -buttonGoesDownDistance, 0));
		}
	}

	IEnumerator Activate()
	{
		Coroutine action = StartCoroutine(Action());
		yield return new WaitForSeconds(activatedDuration);

		StopCoroutine(action);

		if (canBeActivatedMultipleTimes)
		{
			isPressed = false;
			if (canGoBack)
				isGoingForward = !isGoingForward;
			transform.Translate(new Vector3(0, buttonGoesDownDistance, 0));
		}
	}

	IEnumerator Action()
	{
		if (!canGoBack)
			originalPos = objectToMove.position;

		float spentTime = 0;
		while (true)
		{
			spentTime += Time.deltaTime;

			if (isGoingForward)
				objectToMove.position = Vector3.Lerp(originalPos, originalPos + shifting, spentTime / activatedDuration);
			else
				objectToMove.position = Vector3.Lerp(originalPos + shifting, originalPos, spentTime / activatedDuration);


			yield return new WaitForFixedUpdate();
		}
	}
}
