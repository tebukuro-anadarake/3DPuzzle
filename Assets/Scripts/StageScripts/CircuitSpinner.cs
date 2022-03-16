using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircuitSpinner : TouchAbleObject 
{
	CircuitStatus	circuit_status;
	GameObject		game_manager;
	AudioSource		audio_source;
	GameData		game_data;
	Tutorial		tutorial;
	public int		stand_by_frame{ get; private set; }
	const int		spin_frame = 6;
	PauseMenu		pause_menu;

	void Start () 
	{
		circuit_status = this.gameObject.GetComponent<CircuitStatus> ();
		game_manager = GameObject.Find ("GameManager");
		audio_source = gameObject.GetComponent<AudioSource> ();
		game_data = GameObject.Find ("DontDestroyObject").GetComponent<GameData> ();
		tutorial = game_manager.GetComponent<Tutorial> ();
		pause_menu = game_manager.GetComponent<PauseMenu> ();
		stand_by_frame = 0;
	}
	
	void Update () 
	{
		audio_source.volume = (float)game_data.DataGetterInt (GameData.DataType.SEVolume) / (float)game_data.MaxGetterInt (GameData.DataType.SEVolume);
		if (!game_manager.GetComponent<GameManager> ().clear && !tutorial.is_tutorial && !pause_menu.pause_mode) {
			if (OnTouchTap ()) {
				circuit_status.SpinUp ();
				this.transform.localRotation = Quaternion.Euler (0, circuit_status.spin_number * 90, 0);
				audio_source.Play ();
				stand_by_frame = spin_frame;
			}
		}
		if (stand_by_frame >= 1) {
			stand_by_frame--;
		}
	}
}
