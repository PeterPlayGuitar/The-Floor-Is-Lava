using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class PoliceLights : MonoBehaviour
{
	private Light light;
	private float startIntensity;
	[SerializeField] float startFi = 0;

	void Start()
	{
		light = GetComponent<Light>();
		startIntensity = light.intensity;
		fi = startFi;
	}

	float fi;
	[SerializeField] float fiSpeed = 0.01f;

	void Update()
	{
		light.intensity = startIntensity * Mathf.Sin(fi) * 0.5f + 0.5f;
		fi += fiSpeed;
	}
}
