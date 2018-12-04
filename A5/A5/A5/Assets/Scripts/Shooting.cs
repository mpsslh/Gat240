using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour {

	public GameObject projectile;
	public Transform firing_position;
	public float offset;
	public float bullet_speed;
	public float grenade_speed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0))
		{
			//left click
			GameObject bullet = Instantiate(projectile, firing_position.position + offset*firing_position.forward, firing_position.rotation) as GameObject;
			bullet.GetComponent<Rigidbody>().AddForce(firing_position.forward * bullet_speed);
			bullet.AddComponent<TimedDeath>();
		}

		if (Input.GetMouseButtonDown(1))
		{
			//right right
			GameObject bullet = Instantiate(projectile, firing_position.position + offset*firing_position.forward, firing_position.rotation) as GameObject;
			bullet.GetComponent<Transform>().localScale = new Vector3(4, 4, 4);
			bullet.GetComponent<Rigidbody>().mass = bullet.GetComponent<Rigidbody>().mass*2;
			bullet.GetComponent<Rigidbody>().AddForce(firing_position.forward * grenade_speed);
			bullet.AddComponent<TimedDeath>();
		}
	}
}
