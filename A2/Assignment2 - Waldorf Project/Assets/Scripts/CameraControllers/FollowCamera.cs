using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : GameCamera
{
	public GameObject starting;
	public Vector3 offset;
	public GameObject waldorf;
	int state;

	// Use this for initialization
	void Start()
	{
		state = 0;
		gameObject.transform.position = starting.transform.position;
		gameObject.transform.rotation = starting.transform.rotation;
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetMouseButtonUp(0))
		{
			Debug.Log("left clicked");
			state = 1;

		}
		if (Input.GetMouseButtonUp(1))
		{
			Debug.Log("right clicked");
			state = 2;
		}
		switch (state)
		{
			case 1:
				gameObject.transform.position = waldorf.GetComponent<Transform>().position;
				gameObject.transform.rotation = waldorf.GetComponent<Transform>().rotation;
				break;
			case 2:
				Debug.Log(waldorf.GetComponent<Transform>().forward);
				gameObject.transform.position = waldorf.GetComponent<Transform>().position + waldorf.GetComponent<Transform>().forward * offset.z
					+ waldorf.GetComponent<Transform>().right * offset.x + waldorf.GetComponent<Transform>().up * offset.y;
				gameObject.transform.rotation = waldorf.GetComponent<Transform>().rotation;
				break;
			default: break;
		}
	}
}
