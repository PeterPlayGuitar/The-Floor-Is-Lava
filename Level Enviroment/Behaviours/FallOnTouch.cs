using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallOnTouch : MonoBehaviour
{
	[SerializeField] float timeToWait = 0.1f;

	public bool shouldWarnWithColor = false;

	public Material DangerousMaterial;

	void OnCollisionEnter()
	{
		StartCoroutine(WaitAndFall());
	}

	IEnumerator WaitAndFall()
	{
		if (shouldWarnWithColor)
		{
			yield return new WaitForSeconds(timeToWait * 0.8f);
			GetComponent<MeshRenderer>().material = DangerousMaterial;
			yield return new WaitForSeconds(timeToWait * 0.2f);
		}
		else
		{
			yield return new WaitForSeconds(timeToWait);
		}
		gameObject.AddComponent<Rigidbody>();
	}
}
