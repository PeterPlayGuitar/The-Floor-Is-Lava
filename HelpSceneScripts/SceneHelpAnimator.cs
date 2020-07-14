using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneHelpAnimator : MonoBehaviour
{
	public GameObject content;

	public Image cortain;

	public GameObject secondPlayerButtonsHelpImageGameObject;
	public GameObject thirdPlayerButtonsHelpImageGameObject;

	void Start()
	{
		content.transform.localScale = Vector3.one * Screen.width / 1920.0f;
		StartCoroutine(AppearingScene(true));

		if (Globals.Settings.numberOfPlayers != 3)
			secondPlayerButtonsHelpImageGameObject.transform.position = thirdPlayerButtonsHelpImageGameObject.transform.position;
		thirdPlayerButtonsHelpImageGameObject.SetActive(Globals.Settings.numberOfPlayers == 3);
	}

	IEnumerator AppearingScene(bool isIn)
	{
		float time = 0.5f;
		while ((time -= Time.deltaTime) > 0)
		{
			if (isIn)
				cortain.color = Color.Lerp(Color.clear, Color.black, time / 0.5f);
			else
				cortain.color = Color.Lerp(Color.black, Color.clear, time / 0.5f);

			yield return null;
		}

		if (isIn)
			cortain.color = Color.clear;
		else
		{
			SceneManager.LoadScene(Globals.Settings.choosenScene);
		}
	}

	void Update()
	{
		if (Input.anyKey)
		{
			StartCoroutine(AppearingScene(false));
		}
	}
}
