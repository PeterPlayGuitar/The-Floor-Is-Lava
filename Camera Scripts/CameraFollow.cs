using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	public GameObject[] players;

	private Vector3 startPosition;

	void Awake()
	{
		cam = GetComponent<Camera>();
	}

	void Start()
	{
		startPosition = GetStartPosition();
	}

	private Vector3 sum = Vector3.zero;
	private Vector3 averagePoint = Vector3.zero;

	[SerializeField] private Vector3 customShifting = Vector3.zero;

	public void SetPlayers(GameObject[] _players)
	{
		players = _players;

		foreach (var player in players)
			averagePoint += player.transform.position;
		averagePoint /= players.Length;

		cam.transform.position = averagePoint - cam.transform.forward * 10;

		StartCoroutine(Following());
	}

	public void StopFollowing()
	{
		StopAllCoroutines();
		StartCoroutine(LastSecondsOFRound());
	}

	IEnumerator LastSecondsOFRound()
	{
		while (true)
		{
			transform.position = Vector3.Lerp(transform.position, startPosition, 0.1f);

			yield return null;
		}
	}

	[SerializeField] float minFarthering = 0;
	[SerializeField] float maxFarthering = 10;
	[SerializeField] float fartheringCoe = 13;

	private Camera cam;

	IEnumerator Following()
	{
		while (true)
		{
			Vector3 newCameraPos = new Vector3();

			averagePoint = Vector3.zero;
			sum = Vector3.zero;
			int counter = 0;
			foreach (var player in players)
				if (player != null)
				{
					sum += player.transform.position;
					counter++;
				}

			averagePoint = sum / (counter);

			newCameraPos = averagePoint;

			//filed of view calc
			float distanceBetween = 0; // also (when its 3 players) it's called greatest radius of center between players
			foreach (var player in players)
				if (player != null)
				{
					float tmp = (player.transform.position - averagePoint).magnitude;
					if (tmp > distanceBetween)
					{
						distanceBetween = tmp;
					}
				}

			// farthering camera
			newCameraPos -= cam.transform.forward * Mathf.Lerp(minFarthering, maxFarthering, distanceBetween / fartheringCoe);

			// little bit shift camera 
			newCameraPos += customShifting;

			cam.transform.position = Vector3.Lerp(cam.transform.position, newCameraPos, 0.1f);

			yield return new WaitForFixedUpdate();
		}
	}

	Vector3 GetStartPosition()
	{
		Vector3 startCamPos = new Vector3();

		averagePoint = Vector3.zero;
		sum = Vector3.zero;
		int counter = 0;
		foreach (var player in players)
			if (player != null)
			{
				sum += player.transform.position;
				counter++;
			}

		averagePoint = sum / (counter);

		startCamPos = averagePoint;

		//filed of view calc
		float distanceBetween = 0; // also (when its 3 players) it's called greatest radius of center between players
		foreach (var player in players)
			if (player != null)
			{
				float tmp = (player.transform.position - averagePoint).magnitude;
				if (tmp > distanceBetween)
				{
					distanceBetween = tmp;
				}
			}

		// farthering camera
		startCamPos -= cam.transform.forward * Mathf.Lerp(minFarthering, maxFarthering, distanceBetween / fartheringCoe);

		// little bit shift camera 
		startCamPos += customShifting;

		return startCamPos;
	}

}
