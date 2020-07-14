using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupplyBox : MonoBehaviour
{
	public GameObject boxTakenParticleGO;

	float fi = 0;
	float fiSpeed = 0.05f;

	Vector3 originalScale;
	void Start()
	{
		originalScale = transform.localScale;

		GameObject lava = GameObject.Find("Lava");
		if (lava != null)
			Physics.IgnoreCollision(lava.GetComponent<Collider>(), GetComponent<BoxCollider>(), true);
	}

	void Update()
	{
		transform.localScale = new Vector3(originalScale.x, Mathf.Sin(fi) * 0.05f + originalScale.y, originalScale.z);
		fi += fiSpeed;

		if (transform.position.y <= -15)
		{
			Destroy(gameObject);
		}
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			Instantiate(boxTakenParticleGO, transform.position, new Quaternion());

			collision.gameObject.GetComponent<Bazooker>().AddRocket(1);

			Destroy(gameObject);
		}
	}
}
