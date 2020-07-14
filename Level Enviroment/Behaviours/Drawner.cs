using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drawner : MonoBehaviour
{
	public Collider LavaCollider;

	public bool shouldDieIfFarBelow = true;


	void Start()
	{
		Physics.IgnoreCollision(LavaCollider, GetComponent<Collider>(), true);
	}

	void Update()
	{
		if (shouldDieIfFarBelow)
			if (transform.position.y < -15)
				Destroy(gameObject);
	}
}
