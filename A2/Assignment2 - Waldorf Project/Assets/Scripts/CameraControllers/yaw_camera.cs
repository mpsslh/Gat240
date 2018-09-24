using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//yam camera script
public class yaw_camera : GameCamera {

	public GameObject starting;
	private Vector3 originalPos;
	private Quaternion originalRot;
	public float movement_speed;
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
		float yaw = Input.GetAxis("Mouse X");
		transform.Rotate(yaw * movement_speed * Vector3.up, Space.Self);
	}
}
