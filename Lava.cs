using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour, Pausable, SettingsAffectable
{
	public bool shouldItLift = true;
	public bool isWater = false;

	private float speed = 1;

	private bool isOnPause = false;

	public GameObject LavaSplash;
	public GameObject WaterSplash;

	public Material lavaMat;
	public Material waterMat;

	public ParticleSystem lavaSplashing;
	public ParticleSystem waterSplashing;

	void Awake()
	{
		GameManager.pausables.Add(this);
		GameManager.settingsAffectables.Add(this);
	}

	void Start()
	{
		if (isWater)
		{
			waterSplashing.Play();
			GetComponent<MeshRenderer>().material = waterMat;
		}
		else
		{
			lavaSplashing.Play();
			GetComponent<MeshRenderer>().material = lavaMat;
		}

		if (shouldItLift)
			StartCoroutine(Lifting());
	}

	IEnumerator Lifting()
	{
		while (true)
		{
			if (!isOnPause)
			{
				transform.Translate(Vector3.up * speed * Time.deltaTime);
			}

			yield return null;
		}
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			GameObject splash = Instantiate(isWater ? WaterSplash : LavaSplash);
			splash.transform.position = collision.gameObject.transform.position;

			collision.gameObject.GetComponent<Player>().Die(this);
		}
	}

	public void SetPause(bool pause)
	{
		isOnPause = pause;
	}

	public void ApplySettingsValues()
	{
		speed = Globals.Settings.lavaSpeed;
	}
}
