using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-100)]
public class GameManager : MonoBehaviour
{
	[SerializeField] RaceManager _raceManager;

	public void Start()
	{
		Initialise();
	}

	public void Initialise()
	{
		ServiceLocator.Register(_raceManager);
		_raceManager.Initialise();
	}
}
