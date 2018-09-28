using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

	public Camera[] cameras;
	private int selected_camera;
	// Use this for initialization
	void Start()
	{
		selected_camera = 0;

		//Turn all cameras off, except the first default one
		for (int i = 1; i < cameras.Length; i++)
		{
			cameras[i].GetComponent<GameCamera>().DeactivateCamera();
		}

	}

	// Update is called once per frame
	void Update()
	{
		int tmp = selected_camera;
		if (Input.GetKeyUp(KeyCode.Alpha0) || Input.GetKeyUp(KeyCode.Keypad0))
		{
			tmp = 0;
		}
		if (Input.GetKeyUp(KeyCode.Alpha1) || Input.GetKeyUp(KeyCode.Keypad1))
		{
			tmp = 1;
		}
		if (Input.GetKeyUp(KeyCode.Alpha2) || Input.GetKeyUp(KeyCode.Keypad2))
		{
			tmp = 2;
		}
		if (Input.GetKeyUp(KeyCode.Alpha3) || Input.GetKeyUp(KeyCode.Keypad3))
		{
			tmp = 3;
		}

		if (Input.GetKeyUp(KeyCode.Alpha4) || Input.GetKeyUp(KeyCode.Keypad4))
		{
			tmp = 4;
		}

		if (Input.GetKeyUp(KeyCode.Alpha5) || Input.GetKeyUp(KeyCode.Keypad5))
		{
			tmp = 5;
		}
		if (Input.GetKeyUp(KeyCode.Alpha6) || Input.GetKeyUp(KeyCode.Keypad6))
		{
			tmp = 6;
		}
		if (Input.GetKeyUp(KeyCode.Alpha7) || Input.GetKeyUp(KeyCode.Keypad7))
		{
			tmp = 7;
		}
		if (Input.GetKeyUp(KeyCode.Alpha8) || Input.GetKeyUp(KeyCode.Keypad8))
		{
			tmp = 8;
		}
		if (tmp != selected_camera)
		{
			Debug.Log("Switched to" + tmp);
			cameras[selected_camera].GetComponent<GameCamera>().DeactivateCamera();
			cameras[tmp].GetComponent<GameCamera>().ActivateCamera();
			selected_camera = tmp;
		}
	}
}
