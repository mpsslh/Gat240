using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera1 : GameCamera {

    private Vector3 originalPos;
    private Quaternion originalRot;

    private void Start()
    {
        originalPos = this.gameObject.transform.localPosition;
        originalRot = this.gameObject.transform.localRotation;
    }

    public override void ActivateCamera()
    {
        MonoBehaviour.print("ActivateCamera");
        base.ActivateCamera();
        this.gameObject.transform.localPosition = originalPos;
        this.gameObject.transform.localRotation = originalRot;
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1) || Input.GetKeyUp(KeyCode.Keypad1))
        {
            MonoBehaviour.print("1 pressed");
            ActivateCamera();
        }
        //pan left
        if (Input.GetKeyDown(KeyCode.A)){
            MonoBehaviour.print("A pressed");

        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            MonoBehaviour.print("D pressed");

        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            MonoBehaviour.print("W pressed");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            MonoBehaviour.print("S pressed");
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            MonoBehaviour.print("Q pressed");
        }

    }
}
