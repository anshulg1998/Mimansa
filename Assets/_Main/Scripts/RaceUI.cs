using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RaceUI : MonoBehaviour, IResettable
{
	[SerializeField] RectTransform buttonPanel;
	[SerializeField] Button _startRaceButton;
	[SerializeField] Button _restartRaceButton;

	[SerializeField] RectTransform raceEndPanel;
	[SerializeField] Button resetGameButton;
	[SerializeField] TMP_Text raceEndMessage;


	private void Start()
	{
		ServiceLocator.Get<RaceManager>().SubscribeResettableObjects((IResettable)this);
		_startRaceButton.onClick.AddListener(ButtonClicked);
		_restartRaceButton.onClick.AddListener(ButtonClicked);

		resetGameButton.onClick.AddListener(() => ServiceLocator.Get<RaceManager>().Reset());
	}

	private async void ButtonClicked()
	{
		HideButtonPanel();
		await Task.Delay(500);
		ServiceLocator.Get<RaceManager>().StartRace();
	}

	private void HideButtonPanel()
	{
		_startRaceButton.transform.DOScale(Vector3.zero, .15f);
		_restartRaceButton.transform.DOScale(Vector3.zero, .15f);

		buttonPanel.transform.DOScale(Vector3.one, .15f).OnComplete(() =>
		buttonPanel.gameObject.SetActive(false));

	}

	public void ShowButtonPanel()
	{
		_startRaceButton.transform.DOScale(Vector3.one, .1f);
		_restartRaceButton.transform.DOScale(Vector3.one, .1f);

		buttonPanel.transform.DOScale(Vector3.one, .1f).OnStart(() =>
		buttonPanel.gameObject.SetActive(true));
	}

	public void RaceEndPanel(string message)
	{
		raceEndMessage.text = message;
		raceEndPanel.gameObject.SetActive(true);
	}

	public void Reset()
	{
		buttonPanel.gameObject.SetActive(false);
		raceEndPanel.gameObject.SetActive(false);
	}

}
