using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class TimerUI : MonoBehaviour, IResettable
{
    [SerializeField] TMP_Text _timerText;
	[SerializeField] int _effectThresholdTime = -1;


    public void UpdateTime(int time)
	{
		if(time == _effectThresholdTime)
		{
			StartEffect();	
		}
		_timerText.text = $"Timer : {time.ToString()}";
	}

	public void StartEffect()
	{
		_timerText.color = Color.red;
		_timerText.transform.DOScale(Vector3.one * 1.2f, .5f).SetLoops(-1, LoopType.Yoyo);
	}

	public void Reset()
	{
		_timerText.color = Color.white;
		_timerText.transform.DOKill();
		_timerText.transform.localScale = Vector3.one;
	}
}
