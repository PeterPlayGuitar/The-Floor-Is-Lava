using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantMoving : MonoBehaviour
{
	public Vector3 speed = new Vector3(0, 1, 0);

	// Update is called once per frame
	void Update()
	{
		transform.Translate(speed * Time.deltaTime);
	}
}
