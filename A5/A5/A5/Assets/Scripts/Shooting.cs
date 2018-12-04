using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour {

	public GameObject projectile;
	public Transform player;
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
			GameObject bullet = Instantiate(projectile, player.position + offset*player.forward, Quaternion.identity) as GameObject;
			bullet.GetComponent<Rigidbody>().AddForce(transform.forward * bullet_speed);
			bullet.AddComponent<TimedDeath>();
		}

		if (Input.GetMouseButtonDown(1))
		{
			//right right
			GameObject bullet = Instantiate(projectile, player.position + offset*player.forward, Quaternion.identity) as GameObject;
			bullet.GetComponent<Transform>().localScale = new Vector3(4, 4, 4);
			bullet.GetComponent<Rigidbody>().mass = bullet.GetComponent<Rigidbody>().mass*2;
			bullet.GetComponent<Rigidbody>().AddForce(transform.forward * grenade_speed);
			bullet.AddComponent<TimedDeath>();
		}
	}
}
