using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
	public GameObject[] pages;

	public UIAnimator animator;
	public Camera mainCamera;

	public SettingsBehaviour settings;

	void Awake()
	{
		Screen.fullScreen = true;
	}

	void Start()
	{
		SetPage(0);
	}

	public void SetPage(int number)
	{
		foreach (var go in pages)
		{
			go.SetActive(false);
		}
		pages[number].SetActive(true);

		mainCamera.backgroundColor = Color.HSVToRGB(Random.value, 1, 0.75f);
	}

	public void GoToNextScene()
	{
		//optional settings:
		if (settings.GameTyepDropDown.value == 1)
		{
			if (settings.toggle.isOn)
				Globals.Settings.lavaSpeed = settings.LavaSlider.value / 100.0f * Globals.Settings.Originals.lavaSpeed;
			else
				Globals.Settings.lavaSpeed = 0;

			if (settings.NumberOfBulletsSlider.value == settings.NumberOfBulletsSlider.maxValue)
				Globals.Settings.numberOfBullets = -1;
			else
				Globals.Settings.numberOfBullets = (int)settings.NumberOfBulletsSlider.value;

			Globals.Settings.weapons[0] = new PlayerSettings(settings.gunsToggles[0].isOn, settings.swordsToggles[0].isOn);
			Globals.Settings.weapons[1] = new PlayerSettings(settings.gunsToggles[1].isOn, settings.swordsToggles[1].isOn);
			Globals.Settings.weapons[2] = new PlayerSettings(settings.gunsToggles[2].isOn, settings.swordsToggles[2].isOn);

			Globals.Settings.gunStrength = (int)settings.gunStrengthSlider.value;
			Globals.Settings.swordStrength = (int)settings.swordStrengthSlider.value;
		}
		else if (settings.GameTyepDropDown.value == 2)
		{
			//Globals.Settings.weapons
			Globals.Settings.numberOfBullets = 0;
			Globals.Settings.weapons[0] = new PlayerSettings(false, false);
			Globals.Settings.weapons[1] = new PlayerSettings(false, false);
			Globals.Settings.weapons[2] = new PlayerSettings(false, false);

			Globals.Settings.numberOfRockets = 9999999;
		}


		// required settings:

		Globals.Settings.numberOfPlayers = settings.numberOfPlayersDrowpdown.value + 2;

		Globals.GameStream.score = new int[3] { -1, -1, -1 };
		for (int i = 0; i < Globals.Settings.numberOfPlayers; i++)
			Globals.GameStream.score[i] = 0;

		if (settings.mapsListDropdown.value == 0) // choosen random maps
		{
			Globals.Settings.isRandomMap = true;
			Globals.Settings.choosenScene = Globals.mapNames[Random.Range(0, Globals.mapNames.Length)];
		}
		else
		{
			Globals.Settings.isRandomMap = false;
			Globals.Settings.choosenScene = Globals.mapNames[settings.mapsListDropdown.value - 1];
		}

		animator.CloseScene("Help");
	}

	public void ExitPressed()
	{
		Application.Quit();
	}
}
