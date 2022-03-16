using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCameraAbleSetter : MonoBehaviour 
{
	MenuManager		menu_manager;
	CameraController	camera_controller;

	void Start () 
	{
		menu_manager = GameObject.Find ("MenuManager").GetComponent<MenuManager> ();
		camera_controller = GameObject.Find ("CameraStand").GetComponent<CameraController> ();
	}
	
	void Update () 
	{
		if (menu_manager.status == MenuManager.MenuStatus.Top) {
			camera_controller.move_able = true;
		} else {
			camera_controller.move_able = false;
		}
	}
}
