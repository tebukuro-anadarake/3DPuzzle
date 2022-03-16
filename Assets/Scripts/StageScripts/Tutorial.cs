using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : UIObject 
{
	GameManager		game_manager;
	PauseMenu		pause_menu;
	GameData		game_data;
	int				tutorial_page;
	public bool		is_tutorial{ get; set; }
	[SerializeField]
	Texture2D		screen_black;
	GUIStyle		style;
	GUIStyle		back_style;
	Rect			rect;
	SEPlayer		se_player;
	[SerializeField]
	Texture2D		right_button;
	[SerializeField]
	Texture2D		left_button;
	[SerializeField]
	Texture2D		back_button;
	[SerializeField]
	Texture2D		tutorial_button;
	const int		max_page = 7;
	const int		min_page = 1;

	void Start () 
	{
		GameObject	dont_destroy_object = GameObject.Find ("DontDestroyObject");
		game_data = dont_destroy_object.GetComponent<GameData> ();
		game_manager = GetComponent<GameManager> ();
		pause_menu = GetComponent<PauseMenu> ();

		//チュートリアルかどうかの設定
		if (game_data.DataGetterInt (GameData.DataType.NumberOfPlays) == 0) {
			is_tutorial = true;
		} else {
			is_tutorial = false;
		}
		//プレイ回数を+1する
		game_data.DataSetterInt(game_data.DataGetterInt (GameData.DataType.NumberOfPlays) + 1, GameData.DataType.NumberOfPlays);
		//チュートリアルのページを1にする
		tutorial_page = 1;

		//GUIStyleの設定
		style = new GUIStyle();
		style.fontSize = (int)(MinWH() / 21);
		style.fontStyle = FontStyle.Normal;
		style.normal.textColor = Color.white;
		style.alignment = TextAnchor.UpperLeft;
		//GUIStyleの設定
		back_style = new GUIStyle();
		back_style.fontSize = (int)(MinWH() / 20);
		back_style.fontStyle = FontStyle.Normal;
		back_style.normal.textColor = Color.black;
		back_style.alignment = TextAnchor.MiddleCenter;
		//Rectの設定
		rect = new Rect( 0, 0, Screen.width, Screen.height / 2 );
		//SEPlayerの設定
		se_player = dont_destroy_object.GetComponent<SEPlayer> ();
	}
	
	/**
	 * ページを引数分めくる関数
	 */
	void PagesCounter(int move_pages)
	{
		if (min_page <= tutorial_page + move_pages && tutorial_page + move_pages <= max_page) {
			se_player.PlaySE (SEPlayer.SEType.Click);
			tutorial_page += move_pages;
		}
	}

	void OnGUI()
	{
		//ボタンの透明化
		GUI.backgroundColor = Color.clear;

		//チュートリアルなうかどうか
		if (is_tutorial) {
			//画面ブラック化
			GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), screen_black);

			//ページに応じてチュートリアルメッセージを出す
			switch (tutorial_page) {
			case 1:
				Msg (
					"今からチュートリアルを始めます。" + System.Environment.NewLine +
					"右のボタンで次のページに進み、" + System.Environment.NewLine +
					"左のボタンで前のページに戻れます。"
				);
				break;
			case 2:
				Msg (
					"まず初めに、ゲームのルールについて。" + System.Environment.NewLine +
					"このゲームは、画面上にある赤色や青色の" + System.Environment.NewLine +
					"全 て の 「回路の欠片」を" + System.Environment.NewLine +
					"一つの輪になるようにつなげるゲームです。" + System.Environment.NewLine +
					"この「回路の欠片」を一つの回路に" + System.Environment.NewLine +
					"繋ぐことができればクリアとなります。" + System.Environment.NewLine +
					"また、緑色の部分は「回路の欠片」として" + System.Environment.NewLine +
					"使うこともできますが、" + System.Environment.NewLine +
					"必要でない場合、使わなくても結構です。"
				);
				break;
			case 3:
				Msg (
					"次に操作についてです。" + System.Environment.NewLine +
					"赤色や青色の「回路の欠片」は、" + System.Environment.NewLine +
					"タッチすると回転させることができます。" + System.Environment.NewLine +
					"緑色のものは回転させることはできません。"
				);
				break;
			case 4:
				Msg (
					"また、地球儀を回すようなイメージで" + System.Environment.NewLine +
					"カメラを動かすことができます。" + System.Environment.NewLine +
					"ゲームプレイ時には、右下にカメラを" + System.Environment.NewLine +
					"回転させるボタンもついているので、" + System.Environment.NewLine +
					"そのボタンも有効に活用するといいでしょう。" + System.Environment.NewLine +
					"また、画面をつまむようにすると、" + System.Environment.NewLine +
					"カメラをズームすることができます"
				);
				break;
			case 5:
				Msg (
					"またこのチュートリアルは、" + System.Environment.NewLine +
					"右上のボタンで、" + System.Environment.NewLine +
					"PlayGame中にいつでも開けます。"
				);
				break;
			case 6:
				Msg (
					"もし処理が重かった場合は" + System.Environment.NewLine +
					"メインメニューのOptionから" + System.Environment.NewLine +
					"背景オブジェクト切替で背景オブジェクトを" + System.Environment.NewLine +
					"消してください。"
				);
				break;
			case 7:
				Msg (
					"それでは、ゲームを始めてみましょう。" + System.Environment.NewLine +
					"(「閉じる」でチュートリアルを閉じれます。)"
				);
				break;
			}

			//進む、戻るボタン
			if (TextureButton (MarginedRect(0, (float)5/8, (float)1/4, (float)1/4), left_button)) {
				PagesCounter (-1);
			} else if (TextureButton (MarginedRect((float)3/4, (float)5/8, (float)1/4, (float)1/4), right_button)) {
				PagesCounter (1);
			}
			if (tutorial_page == max_page) {
				if (TextureButton (MarginedRect((float)1/4, (float)1/2, (float)1/2, (float)1/2), back_button, "閉じる", back_style)) {
					se_player.PlaySE (SEPlayer.SEType.Back);
					is_tutorial = false;
				}
			}
		} else {
			if (!game_manager.clear && !pause_menu.pause_mode) {
				if (TextureButton (UpperRightRectSquare(new Rect (Screen.width * 5 / 6, 0, Screen.width / 6, Screen.height / 6)), tutorial_button)) {
					se_player.PlaySE (SEPlayer.SEType.Click);
					is_tutorial = true;
				}
			}
		}
	}

	/**
	 * チュートリアルメッセージを表示する関数
	 */ 
	void Msg(string msg)
	{
		GUI.Label (rect, msg, style);
	}
}
