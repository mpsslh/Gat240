using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedDeath : MonoBehaviour {

	float time_till_death = 4.0f;
	// Use this for initialization
	void Start () {
		Object.Destroy(gameObject, time_till_death);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
