using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public static class Globals
{
	public static class Settings
	{
		public static string choosenScene = "Dragon Dangeon";

		public static class Originals
		{
			public static float lavaSpeed = 0.13f;
			public static bool isRandomMap = true;

			// player settings
			public static int numberOfPlayers = 2;
			public static int numberOfBullets = -1;
			public static int numberOfRockets = 0;
			public static PlayerSettings[] weapons = { new PlayerSettings(true, true),
													new PlayerSettings(true, true),
													new PlayerSettings(true, true) };
			public static int gunStrength = 25;
			public static int swordStrength = 50;
		}

		public static float lavaSpeed = Originals.lavaSpeed;
		public static bool isRandomMap = Originals.isRandomMap;

		public static int numberOfPlayers = Originals.numberOfPlayers;
		public static int numberOfBullets = Originals.numberOfBullets;
		public static int numberOfRockets = Originals.numberOfRockets;
		public static PlayerSettings[] weapons = Originals.weapons;
		public static int gunStrength = Originals.gunStrength;
		public static int swordStrength = Originals.swordStrength;
	}

	public static string[] mapNames = {
		"Dragon Dangeon",
		"Cactus Monster",
		"Crazy Boat",
		"Face Punching",
		"For Love!",
		"OMG",
		"Pirate Supercat",
		"V-U-lcano Base",
		"Virgin Island",
		"Vomit Stantion",
		"Crazy",
		"Exposed!",
		"God Bless Me",
		"Hallo Lady!",
		"Is This Real!",
		"Make It Stop!",
		"Space Question Mark",
		"The End",
		"Wow World",
		"Wow World Two",
		"Punnish Rope",
		"Castle",
		"Mech",
		"Hell",
		"Pirates GO!",
		"Apocalypse",
		"Hell Two",
		"Hi! Bye!",
		"Join My Web!",
		"New Orlean",
		"Nice",
		"Not Expected",
		"Mama",
		"Okay Now This Is Epic",
		"No Thank You",
		"Hasta La Vista!",
		"Vampire!"
	};

	public static class GameStream
	{
		public static int[] score = { 0, 0, -1 };
	}

	public static class Keys
	{
		public class PlayerKeys
		{
			public KeyCode up, down, left, right;
			public KeyCode hunt;
			public KeyCode jump;
			public KeyCode hit;
			public KeyCode bazooka;
		}

		public static PlayerKeys[] playerKeys =
		{
			new PlayerKeys{
				up = KeyCode.W,
				down = KeyCode.S,
				left = KeyCode.A,
				right = KeyCode.D,
				hunt = KeyCode.Alpha1,
				jump = KeyCode.Alpha2,
				hit = KeyCode.Alpha3,
				bazooka = KeyCode.Alpha4,
			},

			new PlayerKeys{
				up = KeyCode.UpArrow,
				down = KeyCode.DownArrow,
				left = KeyCode.LeftArrow,
				right = KeyCode.RightArrow,
				hunt = KeyCode.Comma,
				jump = KeyCode.Period,
				hit = KeyCode.Slash,
				bazooka = KeyCode.Quote,
			},

			new PlayerKeys{
				up = KeyCode.U,
				down = KeyCode.J,
				left = KeyCode.H,
				right = KeyCode.K,
				hunt = KeyCode.V,
				jump = KeyCode.B,
				hit = KeyCode.N,
				bazooka = KeyCode.M,
			}
		};
	}

}

public class GameManager : MonoBehaviour
{
	public KeyCode restart = KeyCode.R;

	public static List<Pausable> pausables = new List<Pausable>();
	public static List<SettingsAffectable> settingsAffectables = new List<SettingsAffectable>();

	bool onPause = false;

	public UIAnimator animator;

	public GameObject canvas;

	private GameObject[] players = null;

	public GameObject cam;
	public GameObject winnerLight;

	public CameraFollow cameraFolloScript;

	public GameObject gamePlayCanvas;

	void Start()
	{
		cantPressRestart = true;

		ApplySettings();
	}

	public void LaunchNextMap()
	{
		pausables.Clear();
		settingsAffectables.Clear();

		if (Globals.Settings.isRandomMap)
			SceneManager.LoadScene(Globals.mapNames[Random.Range(0, Globals.mapNames.Length)]);
		else
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	private bool cantPressRestart = false;
	void Update()
	{
		if (Input.GetKeyDown(restart))
		{
			if (!cantPressRestart)
			{
				LaunchNextMap();
			}
		}
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			onPause = !onPause;
			SetPause(onPause);
		}
	}

	public void GameOver(GameObject survived)
	{
		StartCoroutine(LastSecondsOfRound(survived));

		cameraFolloScript.StopFollowing();
	}

	public GameObject tieMesh;
	public GameObject scoreGameObject;
	public Text scoreTextBox;

	IEnumerator LastSecondsOfRound(GameObject survived)
	{
		yield return new WaitForSeconds(0.5f);

		cantPressRestart = false;

		if (survived != null)
		{
			//calc scores
			Globals.GameStream.score[survived.GetComponent<Player>().ID] += 1;

			survived.GetComponent<Player>().RemoveAllComponents();
			Destroy(survived.GetComponent<Player>().GetComponent<jump>());
			survived.AddComponent<WinnerPlayer>().StartWinningProcess(cam, winnerLight);
		}
		else
		{
			tieMesh.SetActive(true);
			tieMesh.AddComponent<WinnerPlayer>().StartWinningProcess(cam, winnerLight);
		}


		// hang scores
		if (Globals.Settings.numberOfPlayers == 2)
			scoreTextBox.text = Globals.GameStream.score[0].ToString() + " : " + Globals.GameStream.score[1].ToString();
		else
			scoreTextBox.text = Globals.GameStream.score[0].ToString() + " : " + Globals.GameStream.score[2].ToString() + " : " + Globals.GameStream.score[1].ToString();
		scoreGameObject.SetActive(true);
	}

	public void ApplySettings()
	{
		foreach (var obj in settingsAffectables)
			obj.ApplySettingsValues();
	}

	public GameObject thirdPlayerButtonsGameObject;
	public void SetPause(bool pause)
	{
		animator.PauseScene(pause);
		canvas.SetActive(pause);
		gamePlayCanvas.SetActive(!pause);

		thirdPlayerButtonsGameObject.SetActive(Globals.Settings.numberOfPlayers == 3);

		foreach (var p in pausables)
			try
			{
				p.SetPause(pause);
			}
			catch (MissingReferenceException exc) { }
	}

	public void SetPlayers(GameObject[] p)
	{
		players = p;
	}

	public GameObject[] GetPlayers()
	{
		return players;
	}
}
