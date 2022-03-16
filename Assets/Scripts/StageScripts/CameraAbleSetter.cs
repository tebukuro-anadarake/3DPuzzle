using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAbleSetter : MonoBehaviour 
{
	GameManager			game_manager;
	CameraController	camera_controller;
	Tutorial			tutorial;
	PauseMenu			pause_menu;
	void Start () 
	{
		GameObject	game_manager_obj = GameObject.Find ("GameManager");
		game_manager = game_manager_obj.GetComponent<GameManager> ();
		pause_menu = game_manager_obj.GetComponent<PauseMenu> ();
		camera_controller = GameObject.Find ("CameraStand").GetComponent<CameraController> ();
		tutorial = game_manager_obj.GetComponent<Tutorial> ();
	}
	
	void Update () 
	{
		if (!game_manager.clear && !tutorial.is_tutorial && !pause_menu.pause_mode) {
			camera_controller.move_able = true;
		} else {
			camera_controller.move_able = false;
		}
	}
}
