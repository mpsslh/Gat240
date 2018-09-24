using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the space room to make the hiding spot move in a constant speed/direction
/// </summary>
public class SpaceRoomController : MonoBehaviour
{
    public GameObject movingSpace; // the object we want to move "hiding_spot"
    public float moveSpeed = 1.0f; // the speed to move the object
    public Vector3 moveDir = Vector3.forward; // the direction to move the object in

	// Use this for initialization
	void Start () {
		
	}
	
	// Smooth update over consistent time
	void FixedUpdate ()
    {
        // Move our object in a direction over a speed
        movingSpace.transform.Translate(moveDir * moveSpeed);
	}
}
