using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class DeleteAfterEffect : MonoBehaviour
{
	private ParticleSystem ps;

	void Start()
	{
		ps = GetComponent<ParticleSystem>();

		StartCoroutine(Playing());
	}

	IEnumerator Playing()
	{
		yield return new WaitForSeconds(ps.main.duration + ps.main.startLifetime.constant);

		Destroy(gameObject);
	}
}
