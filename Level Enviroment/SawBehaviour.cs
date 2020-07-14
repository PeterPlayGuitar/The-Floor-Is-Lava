using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawBehaviour : MonoBehaviour
{
	[SerializeField] float angularSpeed = 180;

	void FixedUpdate()
	{
		transform.Rotate(0, -angularSpeed * Time.fixedDeltaTime, 0);
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			collision.gameObject.GetComponent<Player>().Die(this);
		}
	}
}
