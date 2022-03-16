using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : UIObject 
{
	List<GameObject>	circuit_list = new List<GameObject>();
	public bool			clear{ get; private set; }
	GameData			game_data;
	SEPlayer			se_player;
	[SerializeField]
	int					stage_number;
	[SerializeField]
	Texture2D			stage_button;
	[SerializeField]
	Texture2D			back_button;
	[SerializeField]
	Texture2D			screen_black;
	[SerializeField]
	Texture2D			name_plate;
	GUIStyle			normal_style;
	Tutorial			tutorial;
	PauseMenu			pause_menu;

	void Start () 
	{
		GameObject	dont_destroy_object = GameObject.Find ("DontDestroyObject");
		game_data = dont_destroy_object.GetComponent<GameData>();
		se_player = dont_destroy_object.GetComponent<SEPlayer> ();
		clear = false;

		//CircuitObjectが存在する個数分circuit_obj_listにぶちこむ
		bool			circuit_check = true;
		int				circuit_count = 0;
		while (circuit_check) {
			GameObject	circuit_obj = GameObject.Find ("Circuit" + circuit_count + "/Circuit");
			if (circuit_obj != null) {
				circuit_list.Add (circuit_obj);
			} else {
				circuit_check = false;
			}
			circuit_count++;
		}


		normal_style = new GUIStyle();
		normal_style.fontSize = (int)(MinWH() / 20);
		normal_style.fontStyle = FontStyle.Normal;
		normal_style.normal.textColor = Color.black;
		normal_style.alignment = TextAnchor.MiddleCenter;

		tutorial = GetComponent<Tutorial> ();
		pause_menu = GetComponent<PauseMenu> ();
	}
	
	void Update () 
	{
		//クリア判定
		List<GameObject>	chained_list = new List<GameObject> ();
		chained_list.Add (circuit_list[0]);
		bool	game_clear = true;

		for (int n = 0; n < circuit_list.Count - 1; n++) {
			List<GameObject>	n_chained_obj = chained_list [n].GetComponent<CircuitStatus> ().chained_object;

			switch (n_chained_obj.Count) {
			case 0:
				game_clear = false;
			break;
			case 1:
				game_clear = false;
			break;
			case 2:
				if (chained_list.Contains(n_chained_obj[0])) {
					if (chained_list.Contains (n_chained_obj [1])) {
						game_clear = false;
					} else {
						chained_list.Add (n_chained_obj [1]);
					}
				} else {
					chained_list.Add (n_chained_obj [0]);
				}
			break;
			}
			if (!game_clear) {
				break;
			}
		}
		clear = game_clear;

		if (clear) {
			if (game_data.DataGetterInt (GameData.DataType.ClearStage) < stage_number) {
				game_data.DataSetterInt (stage_number, GameData.DataType.ClearStage);
			}
		}

		//ここから、どのオブジェクトが光ることができるかの判定
		for (int i = 0; i < circuit_list.Count; i++) {
			circuit_list [i].GetComponent<CircuitStatus> ().isZeroNumberChained = false;
		}
		CircuitStatus	zero_status = circuit_list [0].GetComponent<CircuitStatus> ();
		zero_status.isZeroNumberChained = true;
		for (int n = 0; n < zero_status.chained_object.Count; n++) {
			List<GameObject>	n_checked_obj = new List<GameObject> ();
			n_checked_obj.Add (circuit_list [0]);
			n_checked_obj.Add (zero_status.chained_object[n]);
			bool n_count_continue = true;
			int k = 1;
			while (n_count_continue) {
				CircuitStatus	k_status = n_checked_obj [k].GetComponent<CircuitStatus> ();
				if (k_status.chained_object.Count == 2) {
					k_status.isZeroNumberChained = true;
					if (n_checked_obj.Contains (k_status.chained_object [0])) {
						if (n_checked_obj.Contains (k_status.chained_object [1])) {
							n_count_continue = false;
						} else {
							n_checked_obj.Add (k_status.chained_object [1]);
						}
					} else {
						n_checked_obj.Add (k_status.chained_object [0]);
					}
				} else {
					n_count_continue = false;
				}
				k++;
			}
		}
	}

	void OnGUI()
	{
		//ボタンの透明化
		GUI.backgroundColor = Color.clear;

		if (clear) {
			//画面ブラック化
			GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), screen_black);

			if (TextureButton (MarginedRect((float)1/4, (float)1/2, (float)1/2, (float)1/2), back_button, "MainMenu", normal_style)) {
				se_player.PlaySE (SEPlayer.SEType.Back);
				SceneManager.LoadScene ("MainMenu");
			} 
			if (stage_number < game_data.MaxGetterInt (GameData.DataType.ClearStage)) {
				if (TextureButton (MarginedRect((float)1/4, 0, (float)1/2, (float)1/2), stage_button, "NextStage", normal_style)) {
					se_player.PlaySE (SEPlayer.SEType.Decide);
					SceneManager.LoadScene ("Stage" + (stage_number + 1));
				}
			}
		} else if(!pause_menu.pause_mode && !tutorial.is_tutorial){
			GUI.DrawTexture (new Rect (Screen.width / 4, 0, Screen.width / 2, Screen.height / 16), name_plate);
			GUI.Label (new Rect (Screen.width / 4, 0, Screen.width / 2, Screen.height / 16), "Stage" + stage_number, normal_style);
		}
	}
}
