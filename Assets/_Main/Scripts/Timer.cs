using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour, IResettable
{
	[SerializeField] TimerUI _timerUI;

    private int _time;

	private int time
	{
		get
		{
			return _time;
		}
		set
		{
			_time = value;
			_timerUI.UpdateTime(_time);
		}
	}

    private Coroutine _coroutine;


    public void StartTime(int time)
    {
		this.time = time;
       _coroutine = StartCoroutine(IE_StartTimer());

    }

	public void StopTimer()
	{
		if(_coroutine != null) 
		StopCoroutine(_coroutine);
	}

	//Using Coroutine instead of c# stopwatch for considering future game mechanic possibilities for slow mo time effects and etc.
	IEnumerator IE_StartTimer()
	{
		while (time > 0)
		{
			time--;
			yield return new WaitForSeconds(1);
		}
		_timerUI.Reset();
		ServiceLocator.Get<RaceManager>().EndRaceLose();

	}
	private void Start()
	{
		ServiceLocator.Get<RaceManager>().SubscribeResettableObjects((IResettable)this);
	}
	public void Reset()
	{
		StopTimer();
	}
}
