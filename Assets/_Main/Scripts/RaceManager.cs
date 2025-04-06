using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor;
using UnityEngine;

public class RaceManager : MonoBehaviour, IResettable
{
	[SerializeField] Car _car;
	[SerializeField] Timer _timer;
	[SerializeField] RaceUI _raceUI;
	[SerializeField] RaceMap _raceMap;
	[SerializeField] int _raceTotalTime;
	[SerializeField] CameraManager _cameraManager;
	List<IResettable> resettableList;


	private void Awake()
	{
		resettableList = new List<IResettable>();
	}

	public void Initialise()
	{
		_raceUI.ShowButtonPanel();
	}
	public void SubscribeResettableObjects(IResettable resettable)
	{
		resettableList.Add(resettable);
	}


	public void StartRace()
	{
		CallResettableOjects();
		_cameraManager.SetTarget(_car.transform);
		_timer.StartTime(_raceTotalTime);
		_car.InitialiseAction(true);
		_raceMap.Initalise();
		GameObject g = new GameObject("Speech Handler");
		SpeechHandler sh =  g.AddComponent<SpeechHandler>();
		sh.Initialise(OnSpeechRegistered);
	}

	public void EndRaceLose()
	{
		_raceUI.RaceEndPanel("Lost the race!");
		Invoke(nameof(EndRace), 1f);
	}

	public void EndRaceWin()
	{
		_raceUI.RaceEndPanel("Wooo!! Victory");
		Invoke(nameof(EndRace), 1f);
	}

	private void EndRace()
	{
		Reset();
	}	

	public void OnSpeechRegistered(string text)
	{
		string cleanText = CleanText(text);
		if(Findstring(cleanText, "go"))
		{
			_car.Accelerate();
		}
			
	}

	public string CleanText(string input)
	{
		char[] charsToRemove = { '!', '.', ',', '?'};

		foreach (char c in charsToRemove)
		{
			input = input.Replace(c.ToString(), "");
		}

		return input.ToLower();
	}

	public bool Findstring(string A, string B)
	{
			int windowSize = B.Length;
			int maxIndex = A.Length - windowSize;

			for (int i = 0; i <= maxIndex; i++)
			{
				string window = A.Substring(i, windowSize);
				if (window == B)
				{
					return true;
				}
			}

		return false;
	}

	public void Reset()
	{
		CallResettableOjects();
		Initialise();
	}


	private void CallResettableOjects()
	{
		foreach (var item in resettableList)
		{
			item.Reset();
		}
	}

}
