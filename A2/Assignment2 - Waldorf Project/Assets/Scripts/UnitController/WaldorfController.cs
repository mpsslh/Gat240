using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the behavior of Waldorf allowing him to walk along a path
/// NOTE: Waldorf has some issues with his movement because I setup his model weird.
///       Try your best to handle the rotation issues!
/// </summary>
public class WaldorfController : MonoBehaviour
{
    public const float MIN_DIST = 0.01f; // the minimum distance before changing directions
    public const float DAMPEN = 0.01f; // reduces the speed to allow our moveSpeed a better value range
    public GameObject room; // the room in which Walforf resides in
    public List<GameObject> waypoint; // a list of waypoints for Waldorf to follow
    public float moveSpeed = 1.0f; // the speed at which Waldorf moves

    private int targetIndex = 0; // the index of our current waypoint

	// Use this for initialization
	void Start ()
    {
        targetIndex = 0;
	}
	
    /// <summary>
    /// FixedUpdate is like update but smoother as it works based off of time elapsed
    /// </summary>
	void FixedUpdate ()
    {
        // Get the target waypoint based on our current index
        GameObject tar = waypoint[targetIndex];

        // Direction to move in (normalized for correct speed)
        Vector3 dir = (tar.gameObject.transform.localPosition - this.gameObject.transform.localPosition).normalized;

        // Move Waldorf in the direction by the movement speed
        this.gameObject.transform.Translate(dir * moveSpeed * DAMPEN, Space.World);

        // The target to look at (made this different than direction so I can ignore y easily)
        Vector3 tarPos = tar.transform.position;
        tarPos.y = this.gameObject.transform.position.y; // ignore y difference when looking

        // Look at the target, make sure to use the room's up to avoid issues
        this.gameObject.transform.LookAt(tarPos, room.transform.up);

        // Recalculate our direction vector to determine the distance between the objects
        dir = tar.gameObject.transform.localPosition - this.gameObject.transform.localPosition;
        dir.y = 0.0f; // ignore the y

        // if direction is less than our min, let's move to the next target
        if ( dir.magnitude <= MIN_DIST)
        {
            // set the current target's position, to avoid issues later
            this.gameObject.transform.localPosition = tar.gameObject.transform.localPosition;

            // increase our target index and wrap it if it exceeds our total waypoints
            if (++targetIndex >= waypoint.Count)
                targetIndex = 0;
        }
	}
}
