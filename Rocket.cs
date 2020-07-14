using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
	[SerializeField] float speed;
	[SerializeField] float acceleration;

	public SmokeFollowing smoke;

	public ParticleSystem boom;

	float damageCoe = 6;

	void Start()
	{
		StartCoroutine(LivingTime());

		Instantiate(smoke.gameObject, transform.position - transform.forward * 0.7f, transform.rotation).GetComponent<SmokeFollowing>().folowee = gameObject;
	}

	void FixedUpdate()
	{
		transform.Translate(Vector3.forward * speed * Time.fixedDeltaTime);
		speed += acceleration * Time.fixedDeltaTime;
	}

	IEnumerator LivingTime()
	{
		yield return new WaitForSeconds(8);

		Destroy(gameObject);
	}

	void OnCollisionEnter(Collision collision)
	{
			Boomer.AllahAcbar(boom, transform.position, Boomer.GetNormalExplosion(), this);

		Destroy(gameObject);
	}
}
