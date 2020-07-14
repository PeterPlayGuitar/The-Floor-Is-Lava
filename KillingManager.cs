using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillingManager : MonoBehaviour
{
	public GameManager gm;

	public void IWasKilled(GameObject killedOne)
	{
		var players = gm.GetPlayers();

		int notKilledNumber = 0;
		GameObject survived = null;

		for (int i = 0; i < players.Length; i++)
			if (players[i] != null)
			{
				if (killedOne == players[i])
					players[i] = null;
				else
				{
					survived = players[i];
					++notKilledNumber;
				}
			}

		if (notKilledNumber < 2)
			gm.GameOver(survived);
	}
}
