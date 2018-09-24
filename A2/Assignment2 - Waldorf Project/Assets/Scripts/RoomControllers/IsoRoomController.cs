using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the Iso Room, showing/hiding the "sliding_wall" depending on the cameras projection
/// </summary>
public class IsoRoomController : MonoBehaviour
{
    public GameObject hiddenWall; // the object to show/hide "sliding_wall"

	// Use this for initialization
	void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!Camera.current) return; // safety check to make sure a camera exist (camera's in Unity are kinda weird)

        // If the current camera is perspective, show the wall, else hide it
        hiddenWall.SetActive(!Camera.current.orthographic);
	}
}
