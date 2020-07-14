using UnityEngine;
using UnityEngine.UI;

public class PlayerSpawner : MonoBehaviour
{
	public GameManager gameManager;
	public CameraFollow cam;
	[SerializeField] GameObject[] players;

	public Slider[] healthBars;

	void Start()
	{
		foreach (var p in players)
		{
			p.SetActive(false);
		}

		GameObject[] playersTrue = new GameObject[Globals.Settings.numberOfPlayers];

		for (int i = 0; i < Globals.Settings.numberOfPlayers; i++)
		{
			playersTrue[i] = players[i];

			playersTrue[i].SetActive(true);
			Player tmp = playersTrue[i].GetComponent<Player>();
			tmp.SetHealthBar(healthBars[i]);

			//settings apply
			if (!Globals.Settings.weapons[i].hasGun)
				playersTrue[i].GetComponent<Hunter>().enabled = false;
			if (!Globals.Settings.weapons[i].hasSword)
				playersTrue[i].GetComponent<Sworder>().enabled = false;

			playersTrue[i].GetComponent<Bazooker>().SetNumberOfRockets(Globals.Settings.numberOfRockets);

			healthBars[i].gameObject.SetActive(true);
		}

		gameManager.SetPlayers(playersTrue);
		cam.SetPlayers(playersTrue);
	}
}
