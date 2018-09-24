using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pitch_camera : GameCamera {

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
		float pitch = Input.GetAxis("Mouse Y");
		transform.Rotate(pitch * movement_speed * Vector3.right, Space.Self);
	}
}
