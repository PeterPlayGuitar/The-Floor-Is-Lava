using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon : MonoBehaviour
{
	public GameObject rocket;
	float shift = 1;


	public void Activate()
	{
		Instantiate(rocket, transform.position + transform.forward * shift, transform.rotation);
	}
}
