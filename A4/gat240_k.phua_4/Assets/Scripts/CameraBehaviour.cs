using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Camera behaviour is used to control the movement of the camera
/// Finds the first tag of "waldorf" and follows the objects movement
/// NOTE: students do NOT need to touch this code
///       however, you will need to set the initial position and rotation from data
/// </summary>
public class CameraBehaviour : MonoBehaviour
{
    private GameObject waldorf; // the object to follow

	// Use this for initialization
	void Start ()
    {
        waldorf = null;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (waldorf == null) // if we don't have a Waldorf
            waldorf = GameObject.FindGameObjectWithTag("waldorf"); // find a Waldorf
        if (waldorf == null) return; // if no Waldorf is available... bail!

        // move the x & z of the camera to be the same as Waldorf
        this.gameObject.transform.position = new Vector3(   waldorf.transform.position.x, 
                                                            gameObject.transform.position.y, 
                                                            waldorf.transform.position.z);
	}
}
