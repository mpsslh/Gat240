using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the Find Room by ensuring only one Waldorf is visible at a time
/// </summary>
public class FindRoomController : MonoBehaviour
{
    public const string ACTIVE_TAG = "Waldorf"; // the tag that denotes the visible Waldorf

    public List<GameObject> waldorf; // all the waldorfs in the room
    public List<float> cameraFOV; // the field of view to use to best view each waldorf (must match the correct game object index)
    public float switchInSeconds = 5.0f; // how long between switching to a new waldorf

    private GameObject activeWaldorf; // currently active waldorf
    private float fov = 60; // currently active fov

    /// <summary>
    /// Returns access to the active waldorf in the scene
    /// Can also use a search for the "Waldorf" tag
    /// </summary>
    public GameObject ActiveWaldorf
    {
        get { return activeWaldorf; }
    }

    /// <summary>
    /// Returns access to the best field of view to use for the current Waldorf
    /// </summary>
    public float FOV
    {
        get { return fov; }
    }

	// Use this for initialization
	void Start ()
    {
        // This starts the coroutine SwitchWaldorf
        // Coroutines run in a seperate thread and allow code to be called independent of the normal code flow
        StartCoroutine(SwitchWaldorf());
	}

    /// <summary>
    /// This is the actual coroutine ran in the StartCoroutine function
    /// It will keep running indefinitely see: while(true)
    /// It will call the PickWaldorf function
    /// Then it will wait an amount of time before proceeding in the loop
    /// </summary>
    /// <returns>An IEnumerator is returned by all coroutines</returns>
    IEnumerator SwitchWaldorf()
    {
        while(true)
        {
            PickWaldorf();
            yield return new WaitForSeconds(switchInSeconds);
        }
    }

    /// <summary>
    /// Picks a random Waldorf to make visible
    /// </summary>
    private void PickWaldorf()
    {
        // Ensure all Waldorfs are hidden
        foreach(GameObject w in waldorf)
        {
            w.SetActive(false);
            w.tag = "Untagged"; // Also be sure to make them all "Untagged" so they don't come up in a search
        }

        // Find a random index between 0 and the total waldorfs in our list
        int index = Random.Range(0, waldorf.Count);

        // make the random waldorf active
        activeWaldorf = waldorf[index];
        activeWaldorf.SetActive(true);
        activeWaldorf.tag = ACTIVE_TAG; // be sure to set the "Waldorf" tag for proper searches

        // set the appropriate field of view to use for the camera
        fov = cameraFOV[index];
    }
}
