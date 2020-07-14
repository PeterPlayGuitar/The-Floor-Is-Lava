using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
	public Teleport outputTeleport;
	public Light light;

	bool teleporting = false;

	float closenessDistance = 0.55f;

	public ParticleSystem teleportingParticle;

	void Start()
	{
		if (outputTeleport.outputTeleport == this)
		{
			Color tmp = Random.ColorHSV(0, 1, 1, 1, 1, 1);
			light.color = tmp;
			outputTeleport.light.color = tmp;
		}
		else
		{
			light.color = Random.ColorHSV(0, 1, 1, 1, 1, 1);
		}
	}

	private void OnTriggerStay(Collider other)
	{
		if (!teleporting)
			if (Vector3.Distance(other.transform.position, transform.position) < closenessDistance)
			{
				outputTeleport.TeleportThis(other, transform.position.y - other.transform.position.y);

				teleportingParticle.Play();
			}
	}

	public void TeleportThis(Collider obj, float yShift)
	{
		obj.gameObject.transform.position = transform.position - Vector3.up * yShift + obj.attachedRigidbody.velocity.normalized * closenessDistance;

		teleportingParticle.Play();
		teleporting = true;
	}

	private void OnTriggerExit(Collider other)
	{
		teleporting = false;
	}
}
