using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
public class MapNameAppearingOnSceneLoading : MonoBehaviour
{
	[SerializeField] float hangingDuration = 2;
	[SerializeField] float disapearingDuration = 1;

	public Text textBox;

	void Start()
	{
		StartCoroutine(Hanging());
		textBox.text = SceneManager.GetActiveScene().name;
	}

	IEnumerator Hanging()
	{
		yield return new WaitForSeconds(hangingDuration);
		StartCoroutine(Disapearing());
	}

	IEnumerator Disapearing()
	{
		float time = disapearingDuration;
		while ((time -= Time.deltaTime) > 0)
		{
			textBox.color = Color.Lerp(new Color(1, 1, 1, 0), Color.white, time / disapearingDuration);

			yield return null;
		}
		GetComponent<Canvas>().gameObject.SetActive(false);
	}
}
