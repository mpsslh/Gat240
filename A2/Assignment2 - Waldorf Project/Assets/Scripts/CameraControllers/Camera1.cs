using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//pz_camera script
public class Camera1 : GameCamera {

	public GameObject starting;
	private Vector3 originalPos;
	private Quaternion originalRot;
	public float movement_speed;
	// Use this for initialization
	void Start () {
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
	void Update () {
		if (Input.GetKey(KeyCode.A)) {
			Vector3 move = Vector3.left*movement_speed;
			transform.Translate(move, Space.Self);
		}
		if (Input.GetKey(KeyCode.D)) {
			Vector3 move = Vector3.right * movement_speed;
			transform.Translate(move, Space.Self);
		}
		if (Input.GetKey(KeyCode.W))
		{
			Vector3 move = Vector3.forward * movement_speed;
			transform.Translate(move, Space.Self);
		}
		if (Input.GetKey(KeyCode.S)) {
			Vector3 move = Vector3.back * movement_speed;
			transform.Translate(move, Space.Self);
		}
		if (Input.GetKey(KeyCode.E)) {
			Vector3 move = Vector3.up * movement_speed;
			transform.Translate(move, Space.Self);
		}
		if (Input.GetKey(KeyCode.Q)) {
			Vector3 move = Vector3.down * movement_speed;
			transform.Translate(move, Space.Self);
		}
	}
}
