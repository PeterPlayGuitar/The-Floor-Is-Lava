using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hunter : MonoBehaviour
{
	public GameObject bullet;

	private KeyCode shoutKey;

	private Player player;

	private bool isLimitedBullets;
	private int numberOfBullets;
	public Text numberOfBulletsText;

	public Bazooker thisBazooker;

	void Awake()
	{
		player = GetComponent<Player>();
		thisBazooker = GetComponent<Bazooker>();

		if (player.ID != -1)
			shoutKey = Globals.Keys.playerKeys[player.ID].hunt;

		numberOfBullets = Globals.Settings.numberOfBullets;
		if (numberOfBullets == -1)
			isLimitedBullets = false;
		else
			isLimitedBullets = true;

		if (isLimitedBullets)
			numberOfBulletsText.text = numberOfBullets.ToString();
		else
			numberOfBulletsText.gameObject.SetActive(false);
	}

	void Update()
	{
		if (Input.GetKeyDown(shoutKey))
		{
			if (numberOfBullets != 0 && thisBazooker.NumberOfRockets == 0)
			{
				Quaternion tmp = Quaternion.LookRotation(player.lastInputNotZero, Vector3.up);

				Instantiate(bullet, transform.position + 0.8f * player.lastInputNotZero, tmp);

				if (isLimitedBullets)
				{
					numberOfBullets -= 1;
					numberOfBulletsText.text = numberOfBullets.ToString();
				}
			}
		}
	}
}
