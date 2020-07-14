using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanShake : MonoBehaviour
{
	private float shakingDuration = 0.5f;
	private float randomEqualRange = 0.3f;

	public void Shake()
	{
		StartCoroutine(LeadShaking());
	}

	IEnumerator LeadShaking()
	{
		Coroutine c = StartCoroutine(Shaking());
		yield return new WaitForSeconds(shakingDuration);
		StopCoroutine(c);
	}

	IEnumerator Shaking()
	{
		float startRandom = randomEqualRange;
		float randomRange = startRandom;
		float time = 0;

		while (true)
		{
			transform.position += new Vector3(Random.Range(randomRange, -randomRange), Random.Range(randomRange, -randomRange), Random.Range(randomRange, -randomRange));

			randomRange = Mathf.Lerp(startRandom, 0, time / shakingDuration);
			time += Time.deltaTime;
			yield return null;
		}
	}
}
