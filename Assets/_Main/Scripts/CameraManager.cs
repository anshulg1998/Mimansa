using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
	private Transform target;
	[SerializeField] private float smoothSpeed = 0.125f;
	private Vector3 offset;


	
	void LateUpdate()
	{
		if (target == null) return;

		Vector3 desiredPosition = target.position - offset;
		Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
		transform.position = smoothedPosition;
	}

	public void SetTarget(Transform newTarget)
	{
		target = newTarget;
		offset = target.position - transform.position;
	}
}
