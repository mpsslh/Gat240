using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindCamera : GameCamera
{
	public GameObject starting;
	private Vector3 originalPos;
	private Quaternion originalRot;
	public FindRoomController room_controller;
	// Use this for initialization
	void Start()
	{
		gameObject.transform.position = starting.transform.position;
		gameObject.transform.rotation = starting.transform.rotation;

		originalPos = this.gameObject.transform.localPosition;
		originalRot = this.gameObject.transform.localRotation;
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
			GetComponent<Camera>().fieldOfView = room_controller.FOV;
			this.transform.LookAt(room_controller.ActiveWaldorf.GetComponent<Transform>().position);
		}
	}
}
