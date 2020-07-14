using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseButtonEvents : MonoBehaviour
{
	public GameManager gm;

	// Start is called before the first frame update
	public void GoToMainMenu()
	{
		SceneManager.LoadScene(0);
	}

	public void OnRestartPressed()
	{
		gm.LaunchNextMap();
	}

	public void OnExitPressed()
	{
		Application.Quit();
	}
}
