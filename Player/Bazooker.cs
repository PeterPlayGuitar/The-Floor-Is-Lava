using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bazooker : MonoBehaviour
{
	Player player;

	private KeyCode shoutKey;
	int numberOfRockets = 0;
	float timeToReload = 0.3f;

	bool canShoot = true;

	public GameObject rocket;
	public GameObject RocketImageOnGamePlayCanvas;

	public int NumberOfRockets { get => numberOfRockets; }

	public void SetNumberOfRockets(int newNumberOfRockets)
	{
		numberOfRockets = newNumberOfRockets;
		UpdateRocketIcon();
	}

	void Start()
	{
		UpdateRocketIcon();
	}

	private void UpdateRocketIcon()
	{
		if (NumberOfRockets > 0)
			RocketImageOnGamePlayCanvas.SetActive(true);
		else
			RocketImageOnGamePlayCanvas.SetActive(false);
	}

	public void AddRocket(int numberOfRockets)
	{
		this.numberOfRockets += 1;
		UpdateRocketIcon();
	}

	void Awake()
	{
		player = GetComponent<Player>();

		if (player.ID != -1)
			shoutKey = Globals.Keys.playerKeys[player.ID].hunt;
	}

	void Update()
	{
		if (Input.GetKeyDown(shoutKey))
		{
			if (NumberOfRockets != 0)
			{
				if (canShoot)
				{
					Quaternion tmp = Quaternion.LookRotation(player.lastInputNotZero, Vector3.up);

					Instantiate(rocket, transform.position + 0.8f * player.lastInputNotZero, tmp);
					canShoot = false;
					StartCoroutine(Reloading());

					numberOfRockets -= 1;
					UpdateRocketIcon();
				}
			}
		}
	}

	IEnumerator Reloading()
	{
		yield return new WaitForSeconds(timeToReload);

		canShoot = true;
	}
}
