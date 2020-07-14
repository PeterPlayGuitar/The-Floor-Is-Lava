using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
	public int strength = Globals.Settings.gunStrength;

	public float speed = 10;

	[SerializeField] float howLongDoesItLive = 4;

	private Rigidbody rb;

	private Vector3 direction;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
		direction = transform.rotation * Vector3.forward;
		rb.velocity = direction * speed;

		StartCoroutine(LifeTime());
	}

	IEnumerator LifeTime()
	{
		yield return new WaitForSeconds(howLongDoesItLive);

		Destroy(gameObject);
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			collision.gameObject.GetComponent<Player>().GetDamege(strength, this);
			Destroy(gameObject);
		}
	}
}
