using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeFollowing : MonoBehaviour
{
	public GameObject folowee;

	Vector3 shifting;

	void Start()
	{
		shifting = folowee.transform.position - transform.position;

		StartCoroutine(Following());
	}

	IEnumerator Following()
	{
		while (folowee != null)
		{
			transform.position = folowee.transform.position - shifting;

			yield return null;
		}

		StartCoroutine(Destruction());
	}

	IEnumerator Destruction()
	{
		ParticleSystem ps = GetComponent<ParticleSystem>();
		ps.Stop();
		float timeTillEnd = ps.main.duration + ps.main.startLifetime.constant;
		while (timeTillEnd > 0)
		{
			timeTillEnd -= Time.deltaTime;

			yield return null;
		}

		Destroy(gameObject);
	}
}
