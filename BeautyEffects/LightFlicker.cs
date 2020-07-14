using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class LightFlicker : MonoBehaviour
{
	private Light light;
	private float startIntencity;

	void Start()
	{
		light = GetComponent<Light>();
		startIntencity = light.intensity;
	}

	void Update()
	{
		light.intensity = startIntencity * Random.Range(0.5f, 1.5f);
	}
}
