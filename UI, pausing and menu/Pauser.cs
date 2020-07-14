using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Pausable
{
	void SetPause(bool pause);
}


public class Pauser : MonoBehaviour
{
	public Pausable[] entities;
}
