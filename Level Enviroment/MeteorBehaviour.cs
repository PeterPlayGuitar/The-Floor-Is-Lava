using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorBehaviour : MonoBehaviour
{
	public ParticleSystem boom;

	void Start()
	{
		Physics.IgnoreCollision(GetComponent<Collider>(), GameObject.Find("Lava").GetComponent<Collider>(), true);
	}

	void Update()
	{
		if (transform.position.y < -10)
		{
			Destroy(gameObject);
		}
	}

	void OnCollisionEnter()
	{
		Boomer.AllahAcbar(boom, transform.position, Boomer.GetNormalExplosion(), this);
	}
}
