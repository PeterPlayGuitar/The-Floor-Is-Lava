using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sworder : MonoBehaviour
{
	public ParticleSystem ps;
	KeyCode key;
	public float chargingTime = 1;

	bool hitIsAvailable = true;

	void Start()
	{
		int id = GetComponent<Player>().ID;
		if (id != -1)
			key = Globals.Keys.playerKeys[id].hit;
	}

	void Update()
	{
		if (Input.GetKey(key))
			if (hitIsAvailable)
			{
				ps.Play();
				hitIsAvailable = false;

				Collider[] colliders = Physics.OverlapBox(transform.position, new Vector3(1, 0.19f, 1), transform.rotation);
				foreach (var c in colliders)
					if (c.gameObject.tag == "Player" && c.gameObject != gameObject)
					{
						c.GetComponent<Player>().GetDamege(Globals.Settings.swordStrength, this);
					}

				StartCoroutine(WaitForNextHit());
			}
	}

	IEnumerator WaitForNextHit()
	{
		yield return new WaitForSeconds(chargingTime);

		hitIsAvailable = true;
	}
}
