using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ISOCamera : GameCamera
{
	public GameObject starting;
	private Vector3 originalPos;
	private Quaternion originalRot;
	private readonly float orthographic_size = 3.0f;
	// Use this for initialization
	void Start()
	{
		gameObject.transform.position = starting.transform.position;
		gameObject.transform.rotation = starting.transform.rotation;

		originalPos = this.gameObject.transform.localPosition;
		originalRot = this.gameObject.transform.localRotation;
		GetComponent<Camera>().orthographicSize = orthographic_size;
	}

	public override void ActivateCamera()
	{
		base.ActivateCamera();
		this.gameObject.transform.localPosition = originalPos;
		this.gameObject.transform.localRotation = originalRot;
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetMouseButtonUp(0))
		{
			Debug.Log("left clicked");
			GetComponent<Camera>().orthographic = false;
		}
		if (Input.GetMouseButtonUp(1))
		{
			Debug.Log("right clicked");
			GetComponent<Camera>().orthographic = true;
		}
	}
}
