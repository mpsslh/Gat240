using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceCamera : GameCamera
{
	public GameObject starting;
	public Vector3 offset;
	public GameObject hiding_spot;
	public float speed;
	public float minimum_distance;
	public KeyCode custom;
	private Vector3 dir;

	private Color color1 = Color.red;
	private Color color2 = Color.blue;
	private Color default_color;
	private readonly float duration = 3.0F;

	// Use this for initialization
	void Start()
	{
		default_color = GetComponent<Camera>().backgroundColor;
		gameObject.transform.position = starting.transform.position;
		gameObject.transform.rotation = starting.transform.rotation;
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKey(custom))
		{
			Debug.Log(custom + "holding");
			Debug.Log(dir);
			if (Vector3.Distance(hiding_spot.GetComponent<Transform>().position, transform.position) > minimum_distance)
			{
				dir = Vector3.Normalize(hiding_spot.GetComponent<Transform>().position - transform.position);
				transform.position = transform.position + dir * speed * Time.deltaTime;
				float t = Mathf.PingPong(Time.time, duration) / duration;
				GetComponent<Camera>().backgroundColor = Color.Lerp(color1, color2, t);
			}//move towards hiding spot at movemet speed;
		}
		else
		{
			GetComponent<Camera>().backgroundColor = default_color;
		}
	}
}
