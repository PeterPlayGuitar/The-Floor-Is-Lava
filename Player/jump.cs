using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jump : MonoBehaviour
{
	public float speed;
	private KeyCode jumpbutton;

	private bool isJumping = false;

	Rigidbody rb;

	public ParticleSystem jumpEffect;
	public ParticleSystem jumpingFlyingEffect;

	void Start()
	{
		rb = GetComponent<Rigidbody>();

		int id = GetComponent<Player>().ID;
		if (id != -1)
			jumpbutton = Globals.Keys.playerKeys[id].jump;
	}

	void Update()
	{
		if (Input.GetKeyDown(jumpbutton))
		{
			if (!isJumping)
			{
				isJumping = true;
				jumpEffect.Play();
				jumpingFlyingEffect.Play();
				rb.AddForce(speed * Vector3.up);
			}
		}
	}

	void OnCollisionEnter(Collision collision)
	{
		isJumping = false;
		jumpingFlyingEffect.Stop();
	}
}
