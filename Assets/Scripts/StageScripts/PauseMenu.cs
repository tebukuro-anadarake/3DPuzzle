using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : UIObject 
{
	[SerializeField]
	Texture2D	pause_button;
	[SerializeField]
	Texture2D	screen_black;
	[SerializeField]
	Texture2D	back_button;
	[SerializeField]
	Texture2D	resumption_button;
	[SerializeField]
	Texture2D	normal_button;
	[SerializeField]
	Texture2D	name_plate;

	public bool	pause_mode{ get; private set; }
	GameManager	game_manager;
	SEPlayer	se_player;
	Tutorial	tutorial;
	GUIStyle	normal_style;
	bool		back_mainmenu;

	void Start () 
	{
		GameObject	dont_destroy_object = GameObject.Find ("DontDestroyObject");
		se_player = dont_destroy_object.GetComponent<SEPlayer> ();
		pause_mode = false;
		back_mainmenu = false;
		game_manager = GetComponent<GameManager> ();
		tutorial = GetComponent<Tutorial> ();

		normal_style = new GUIStyle();
		normal_style.fontSize = (int)(MinWH() / 20);
		normal_style.fontStyle = FontStyle.Normal;
		normal_style.normal.textColor = Color.black;
		normal_style.alignment = TextAnchor.MiddleCenter;
	}
	
	void OnGUI () 
	{
		//ボタンの透明化
		GUI.backgroundColor = Color.clear;

		if (pause_mode && !back_mainmenu) {
			GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), screen_black);
			if (TextureButton (MarginedRect((float)1/4, (float)1/2, (float)1/2, (float)1/2), back_button, "MainMenuに戻る", normal_style)) {
				se_player.PlaySE (SEPlayer.SEType.Click);
				back_mainmenu = true;
			} else if (TextureButton (MarginedRect((float)1/4, 0, (float)1/2, (float)1/2), resumption_button, "ゲームを再開", normal_style)) {
				se_player.PlaySE (SEPlayer.SEType.Back);
				pause_mode = false;
			}
		} else if(back_mainmenu){
			GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), screen_black);
			if (TextureButton (new Rect (Screen.width / 4, Screen.height * 3 / 8, Screen.width / 2, Screen.height / 16), normal_button, "はい", normal_style)) {
				se_player.PlaySE (SEPlayer.SEType.Destroy);
				SceneManager.LoadScene ("MainMenu");
			} else if (TextureButton (new Rect (Screen.width / 4, Screen.height * 5 / 8, Screen.width / 2, Screen.height / 16), normal_button, "いいえ", normal_style)) {
				se_player.PlaySE (SEPlayer.SEType.Back);
				back_mainmenu = false;
			}
			GUI.DrawTexture (new Rect (0, Screen.height / 16, Screen.width * 3 / 4, Screen.height / 16), name_plate);
			GUI.Label (new Rect (0, Screen.height / 16, Screen.width * 3 / 4, Screen.height / 16), "本当によろしいですか？", normal_style);
		} else {
			if (!game_manager.clear && !tutorial.is_tutorial) {
				if (TextureButton (UpperLeftRectSquare(new Rect (0, 0, Screen.width / 6, Screen.height / 6)), pause_button)) {
					se_player.PlaySE (SEPlayer.SEType.Click);
					pause_mode = true;
				}
			}
		}
	}
}
