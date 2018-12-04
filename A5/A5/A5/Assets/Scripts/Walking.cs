using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walking : MonoBehaviour {

	public float speed;
	public Rigidbody rb;
	public Transform player;
	public Transform free_camera;
	void Start()
	{

	}

	void FixedUpdate()
	{
		free_camera.position = player.position;
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");

		Vector3 movement = moveVertical * player.forward + moveHorizontal * player.right;
		player.rotation = free_camera.rotation;
		
		rb.AddForce(movement * speed);
	}
}
