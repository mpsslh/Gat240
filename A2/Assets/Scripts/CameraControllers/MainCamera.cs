using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is used for the Main Camera object (accessed with "0" key)
/// It doesn't do much but it may help you as a base for other cameras
/// NOTE: This is the ONLY camera that does not start at 0,0,0 with an identity rotation
/// </summary>
public class MainCamera : GameCamera
{
    private Vector3 originalPos;
    private Quaternion originalRot;

    private void Start()
    {
        originalPos = this.gameObject.transform.localPosition;
        originalRot = this.gameObject.transform.localRotation;
    }

    public override void ActivateCamera()
    {
        base.ActivateCamera();
        this.gameObject.transform.localPosition = originalPos;
        this.gameObject.transform.localRotation = originalRot;
    }
}
