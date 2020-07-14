using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinnerPlayer : MonoBehaviour
{
	public float angularSpeed = 90;

	void Start()
	{
		transform.Rotate(Vector3.forward, 30);
	}

	public void StartWinningProcess(GameObject cam, GameObject winnerLight)
	{
		StartCoroutine(Spinning(cam, winnerLight));
	}

	IEnumerator Spinning(GameObject cam, GameObject winnerLight)
	{
		Vector3 shiftingFromCamera = cam.transform.rotation * Vector3.forward * 2;

		winnerLight.transform.position = cam.transform.position + shiftingFromCamera + Vector3.up * 1;
		winnerLight.SetActive(true);

		while (true)
		{
			transform.position = Vector3.Lerp(transform.position, cam.transform.position + shiftingFromCamera, 0.1f);

			transform.Rotate(Vector3.up, angularSpeed * Time.deltaTime, Space.World);

			yield return null;
		}
	}
}
