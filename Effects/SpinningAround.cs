using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningAround : MonoBehaviour
{
	[SerializeField] private float angularSpeed = 90;

	void Update()
	{
		transform.Rotate(angularSpeed * Time.deltaTime * Vector3.up, Space.World);
	}
}
