using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowWalking : MonoBehaviour
{
	public GameObject snowExplosion;

	void OnCollisionEnter(Collision collision)
	{
		Instantiate(snowExplosion, collision.transform.position, new Quaternion());
	}
}
