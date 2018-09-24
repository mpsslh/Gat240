using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is used for the base Game Camera object to be inherited by all other cameras
/// This should help make your life easier...
/// </summary>
public class GameCamera : MonoBehaviour
{
    /// <summary>
    /// Says if a camera is active or not
    /// </summary>
    public bool IsActive
    {
        get
        {
            return this.gameObject.activeSelf;
        }
        private set
        {
            this.gameObject.SetActive(value);
        }
    }

    /// <summary>
    /// Function called when a camera is activated to turn it on
    /// This is a "virtual" function which means you can do an "override" with the inheriting class
    /// </summary>
    virtual public void ActivateCamera()
    {
        IsActive = true;
    }

    /// <summary>
    /// Function called when a camera is deactivated to turn it off
    /// This is a "virtual" function which means you can do an "override" with the inheriting class
    /// </summary>
    virtual public void DeactivateCamera()
    {
        IsActive = false;
    }

    /// <summary>
    /// Helper function to easily reset the position and rotation of your camera
    /// Call this function when you activate the camera to perform a reset
    /// </summary>
    protected void ResetTransform()
    {
        this.gameObject.transform.localPosition = Vector3.zero;
        this.gameObject.transform.localRotation = Quaternion.identity;
    }
}
