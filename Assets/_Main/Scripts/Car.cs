using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;

public class Car : MonoBehaviour, IResettable
{
	[Header("Movement Settings")]
	[SerializeField ] float accelerationstep = 8f;
	[SerializeField ] float maxSpeed = 20f;
	[SerializeField ] float maxAcceleration = 20f;
	[SerializeField] float drag = 50f;
	[SerializeField] UnityEngine.UI.Image speedNeedle;
	private float currentSpeed = 0f;
	private Vector3 startPosition;
	private float acceleration;
	private void Start()
	{
		startPosition = transform.position;
		ServiceLocator.Get<RaceManager>().SubscribeResettableObjects((IResettable)this);
	}

	private bool initalised = false;
	public void InitialiseAction(bool action)

	{
		initalised = action;
	}

	void Update()
	{
		if (initalised)
		{
			currentSpeed += acceleration * Time.deltaTime;
			currentSpeed = Mathf.Clamp(currentSpeed, 0f, maxSpeed);
			transform.Translate(Vector3.right * currentSpeed * Time.deltaTime);
			acceleration -= drag * Time.deltaTime;
			acceleration = Mathf.Clamp(acceleration, -drag, maxAcceleration);
			speedNeedle.transform.rotation = Quaternion.Lerp(Quaternion.Euler(0, 0, 0), Quaternion.Euler(0, 0, 180), currentSpeed / maxSpeed);
		}
	}
	public void Accelerate()
	{
		acceleration = Mathf.Clamp(acceleration, 0f, acceleration);
		acceleration += accelerationstep;
	}

	public void Reset()
	{
		transform.position = startPosition;
		currentSpeed = 0;
		acceleration = 0;
		initalised = false;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		ServiceLocator.Get<RaceManager>().EndRaceWin();
	}

}
