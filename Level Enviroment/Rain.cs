using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rain : MonoBehaviour
{
	public GameObject drop;

	[Range(0.1f, 1)]
	public float timeRateRangeFrom = 1;

	[Range(1.1f, 40)]
	public float timeRateRangeTo = 3;

	public float speedRange = 1; // if it is 1 then meteor start speed will be from -1 to 1 to random direction

	private Bounds bounds = new Bounds(Vector3.zero, new Vector3(20, 0, 20));

	void Start()
	{
		StartCoroutine(Raining());
	}

	IEnumerator Raining()
	{
		while (true)
		{
			float timeToWait = Random.Range(timeRateRangeFrom, timeRateRangeTo);

			yield return new WaitForSeconds(timeToWait);

			Vector3 pos = new Vector3(
				Random.Range(-bounds.size.x / 2, bounds.size.x / 2),
				0,
				Random.Range(-bounds.size.x / 2, bounds.size.x / 2));
			pos += bounds.center;
			pos += transform.position;

			Instantiate(drop, pos, new Quaternion()).GetComponent<Rigidbody>().velocity =
				Random.rotation * (Random.Range(-speedRange, speedRange) * Vector3.forward);
		}
	}
}
