using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIAnimator : MonoBehaviour
{
	public SpriteRenderer cortain;

	public float closingDuration = 1;

	private bool closingScene = false;

	public GameObject cam;

	public void CloseScene(string nextSceneName)
	{
		StartCoroutine(ClosingScene(nextSceneName));
	}

	IEnumerator ClosingScene(string nextSceneName)
	{
		closingScene = true;
		float timeCounter = closingDuration;
		while ((timeCounter -= Time.deltaTime) > 0)
		{
			cortain.color = Color.Lerp(Color.black, Color.clear, timeCounter / closingDuration);
			yield return null;
		}

		SceneManager.LoadScene(nextSceneName);
	}

	public Color pausingColor = new Color(0, 0, 0, 0.2f);

	public void PauseScene(bool pause)
	{
		if (!closingScene)
		{
			StopAllCoroutines();
			if (pause)
				StartCoroutine(PausingSceneEffect());
			else
				StartCoroutine(UnpausingSceneEffect());
		}
	}

	IEnumerator UnpausingSceneEffect()
	{
		float timeCounter = closingDuration;
		while ((timeCounter -= Time.deltaTime) > 0)
		{
			cortain.color = Color.Lerp(cortain.color, Color.clear, 0.1f);
			yield return null;
		}
		cortain.color = Color.clear;
	}

	IEnumerator PausingSceneEffect()
	{
		cortain.transform.position = cam.transform.position + cortain.transform.forward * 4;
		cortain.transform.rotation = cam.transform.rotation;

		float timeCounter = closingDuration;

		while ((timeCounter -= Time.deltaTime) > 0)
		{
			cortain.color = Color.Lerp(cortain.color, pausingColor, 0.1f);
			yield return null;
		}
		cortain.color = pausingColor;
	}
}
