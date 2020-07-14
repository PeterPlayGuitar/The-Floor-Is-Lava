using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteAfterAllNestedEffects : MonoBehaviour
{
	public ParticleSystem[] allNestedParticleSystems;

	void Start()
	{
		float maxTime = 0;
		foreach (var ps in allNestedParticleSystems)
		{
			float tmp = ps.main.duration + ps.main.startLifetime.constant;
			if (maxTime < tmp)
				maxTime = tmp;
		}

		StartCoroutine(LivingTime(maxTime));
	}

	IEnumerator LivingTime(float time)
	{
		yield return new WaitForSeconds(time);

		Destroy(gameObject);
	}
}
