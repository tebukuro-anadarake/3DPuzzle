using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : UIObject 
{
	public enum MenuStatus {Top, StageSelect, Option};
	public MenuStatus	status;
	int					stage_select_page;
	int					stage_select_max;
	const int			stage_select_min = 1;
	GameData			game_data;
	SEPlayer			se_player;
	[SerializeField]
	Texture2D			plus_button;
	[SerializeField]
	Texture2D			minus_button;
	[SerializeField]
	Texture2D			volume_meter;
	[SerializeField]
	Texture2D			volume_gauge;
	[SerializeField]
	Texture2D			right_button;
	[SerializeField]
	Texture2D			left_button;
	[SerializeField]
	Texture2D			screen_black;
	[SerializeField]
	Texture2D			back_button;
	[SerializeField]
	Texture2D			stage_button;
	[SerializeField]
	Texture2D			volume_name_plate;
	[SerializeField]
	Texture2D			normal_button;

	GUIStyle			normal_style;
	bool				bgm_bar_touch;
	bool				se_bar_touch;
	Rect				bgm_gauge_rect;
	Rect				se_gauge_rect;
	Rect				bgm_meter_rect;
	Rect				se_meter_rect;
	void Start () 
	{
		GameObject		dont_destroy_object = GameObject.Find ("DontDestroyObject");
		game_data = dont_destroy_object.GetComponent<GameData> ();
		se_player = dont_destroy_object.GetComponent<SEPlayer> ();
		status = MenuStatus.Top;
		if (game_data.DataGetterInt (GameData.DataType.ClearStage) == game_data.MaxGetterInt (GameData.DataType.ClearStage)) {
			stage_select_page = game_data.DataGetterInt (GameData.DataType.ClearStage);
			stage_select_max = game_data.DataGetterInt (GameData.DataType.ClearStage);
		} else {
			stage_select_page = game_data.DataGetterInt (GameData.DataType.ClearStage) + 1;
			stage_select_max = game_data.DataGetterInt (GameData.DataType.ClearStage) + 1;
		}
		normal_style = new GUIStyle();
		normal_style.fontSize = (int)(MinWH() / 20);
		normal_style.fontStyle = FontStyle.Normal;
		normal_style.normal.textColor = Color.black;
		normal_style.alignment = TextAnchor.MiddleCenter;

		bgm_bar_touch = false;
		se_bar_touch = false;
		bgm_gauge_rect = new Rect (0, 0, 0, 0);
		se_gauge_rect = new Rect (0, 0, 0, 0);
		bgm_meter_rect = new Rect (0, 0, 0, 0);
		se_meter_rect = new Rect (0, 0, 0, 0);
	}
	
	void Update () 
	{
		if (status == MenuStatus.Option) {
			//タッチ判定
			if (Input.touchCount > 0) {
				//タッチ位置の重心を求める
				Vector2			center_of_gravity = new Vector2(0, 0);
				for (int i = 0; i < Input.touchCount; i++) {
					Touch	touch = Input.GetTouch (i);
					center_of_gravity += touch.position;
				}
				center_of_gravity = center_of_gravity / Input.touchCount;

				for (int i = 0; i < Input.touchCount; i++) {
					Touch	touch = Input.GetTouch (i);
					//TouchPhaseによる分岐 
					if (touch.phase == TouchPhase.Began) {
						float	forgive_width = 0;
						forgive_width = Screen.width / 16;
						//VolumeGaugeを触ったかどうか
						if (bgm_gauge_rect.xMin - forgive_width <= touch.position.x && touch.position.x <= bgm_gauge_rect.xMax + forgive_width &&
							bgm_gauge_rect.yMin <= (Screen.height - touch.position.y) && (Screen.height - touch.position.y) <= bgm_gauge_rect.yMax) {
							bgm_bar_touch = true;
						}
						if (se_gauge_rect.xMin - forgive_width <= touch.position.x && touch.position.x <= se_gauge_rect.xMax + forgive_width &&
							se_gauge_rect.yMin <= (Screen.height - touch.position.y) && (Screen.height - touch.position.y) <= se_gauge_rect.yMax) {
							se_bar_touch = true;
						}
					} else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled) {
						bgm_bar_touch = false;
						se_bar_touch = false;
					} else {
						//BGMバー操作をできる状態かどうか
						if (bgm_bar_touch) {
							//BarのrectのxMax,xMinからはみ出ていたらはみ出た側の最大値、はみ出ていなかったら, DataType.VolumeのMax * (position.x-xMin) / (xMax-xMin)
							if (center_of_gravity.x <= bgm_meter_rect.xMin) {
								game_data.DataSetterInt (game_data.MinGetterInt (GameData.DataType.BGMVolume), GameData.DataType.BGMVolume);
							} else if (center_of_gravity.x >= bgm_meter_rect.xMax) {
								game_data.DataSetterInt (game_data.MaxGetterInt (GameData.DataType.BGMVolume), GameData.DataType.BGMVolume);
							} else {
								game_data.DataSetterInt ((int)((float)game_data.MaxGetterInt (GameData.DataType.BGMVolume) * (center_of_gravity.x - bgm_meter_rect.xMin) / bgm_meter_rect.width), GameData.DataType.BGMVolume);
							}
						}
						//SEバー操作をできる状態かどうか
						if (se_bar_touch) {
							//BarのrectのxMax,xMinからはみ出ていたらはみ出た側の最大値、はみ出ていなかったら, DataType.VolumeのMax * (position.x-xMin) / (xMax-xMin)
							if (center_of_gravity.x <= se_meter_rect.xMin) {
								game_data.DataSetterInt (game_data.MinGetterInt (GameData.DataType.SEVolume), GameData.DataType.SEVolume);
							} else if (center_of_gravity.x >= se_meter_rect.xMax) {
								game_data.DataSetterInt (game_data.MaxGetterInt (GameData.DataType.SEVolume), GameData.DataType.SEVolume);
							} else {
								game_data.DataSetterInt ((int)((float)game_data.MaxGetterInt (GameData.DataType.SEVolume) * (center_of_gravity.x - se_meter_rect.xMin) / se_meter_rect.width), GameData.DataType.SEVolume);
							}

						}
					}
				}
			}
		} else {
			bgm_bar_touch = false;
			se_bar_touch = false;
		}
	}

	void OnGUI ()
	{
		//ボタンの透明化
		GUI.backgroundColor = Color.clear;

		if (status == MenuStatus.StageSelect) {
			//画面ブラック化
			GUI.DrawTexture(new Rect (0, 0, Screen.width, Screen.height), screen_black);
			//ボタン配置
			string stage_name;
			if (stage_select_page <= game_data.DataGetterInt (GameData.DataType.ClearStage)) {
				stage_name = "Stage" + stage_select_page + System.Environment.NewLine + "(Clear)";
			} else {
				stage_name = "Stage" + stage_select_page;
			}

			if (TextureButton( MarginedRect ((float)1/4, 0, (float)1/2, (float)1/2), stage_button, stage_name, normal_style)) {
				se_player.PlaySE (SEPlayer.SEType.Decide);
				SceneManager.LoadScene ("Stage" + stage_select_page);
			} else if (TextureButton (MarginedRect ((float)1/4, (float)1/2, (float)1/2, (float)1/2), back_button, "Back", normal_style)) {
				se_player.PlaySE (SEPlayer.SEType.Back);
				status = MenuStatus.Top;
			} else if (TextureButton (MarginedRect (0, (float)1/8, (float)1/4, (float)1/4), left_button)) {
				if (stage_select_page > stage_select_min) {
					se_player.PlaySE (SEPlayer.SEType.Click);
					stage_select_page--;
				}
			} else if (TextureButton (MarginedRect ((float)3/4, (float)1/8, (float)1/4, (float)1/4), right_button)) {
				if (stage_select_page < stage_select_max) {
					se_player.PlaySE (SEPlayer.SEType.Click);
					stage_select_page++;
				}
			}
		} else if (status == MenuStatus.Option) {
			//画面ブラック化
			GUI.DrawTexture(new Rect (0, 0, Screen.width, Screen.height), screen_black);
			//音量メーターを表示
			PutMeter ((float)3/16, (float)1/4, (float)5/8, (float)1/16, GameData.DataType.BGMVolume);
			PutMeter ((float)3/16, (float)1/2, (float)5/8, (float)1/16, GameData.DataType.SEVolume);
			//ボタン配置
			bool	option_back = TextureButton (CenterRectSquare(new Rect (Screen.width * 1 / 4, Screen.height * 3 / 4, Screen.width / 2, Screen.height / 4)), back_button, "Back", normal_style);
			bool	option_bgm_down = TextureButton (UpperRectSquare(new Rect (Screen.width * 1 / 16, Screen.height / 4, Screen.width / 8, Screen.height / 16)), minus_button);
			bool	option_bgm_up = TextureButton (UpperRectSquare(new Rect (Screen.width * 13 / 16, Screen.height / 4, Screen.width / 8, Screen.height / 16)), plus_button);
			bool	option_se_down = TextureButton (UpperRectSquare(new Rect (Screen.width * 1 / 16, Screen.height / 2, Screen.width / 8, Screen.height / 16)), minus_button);
			bool	option_se_up = TextureButton (UpperRectSquare(new Rect (Screen.width * 13 / 16, Screen.height / 2, Screen.width / 8, Screen.height / 16)), plus_button);
			bool	option_bg_objects = TextureButton (new Rect (Screen.width * 1 / 8, Screen.height * 5 / 8, Screen.width * 3 / 4, Screen.height / 12), normal_button, "背景オブジェクト表示切替", normal_style);
			//BGMとかSEのラベルを表示
			GUI.DrawTexture (new Rect (0, Screen.height * 5 / 32, Screen.width / 2, Screen.height / 16), volume_name_plate);
			GUI.Label (new Rect (0, Screen.height * 5 / 32, Screen.width / 2, Screen.height / 16), "BGM : " + game_data.DataGetterInt(GameData.DataType.BGMVolume), normal_style);
			GUI.DrawTexture (new Rect (0, Screen.height * 13 / 32, Screen.width / 2, Screen.height / 16), volume_name_plate);
			GUI.Label (new Rect (0, Screen.height * 13 / 32, Screen.width / 2, Screen.height / 16), "SE : " + game_data.DataGetterInt(GameData.DataType.SEVolume), normal_style);
			//ここから表示ではない
			if (option_back) {
				se_player.PlaySE (SEPlayer.SEType.Back);
				status = MenuStatus.Top;
			} else if (option_bgm_down) {
				se_player.PlaySE (SEPlayer.SEType.Click);
				game_data.DataSetterInt (game_data.DataGetterInt (GameData.DataType.BGMVolume) - 1, GameData.DataType.BGMVolume);
			} else if (option_bgm_up) {
				se_player.PlaySE (SEPlayer.SEType.Click);
				game_data.DataSetterInt (game_data.DataGetterInt (GameData.DataType.BGMVolume) + 1, GameData.DataType.BGMVolume);
			} else if (option_se_down) {
				se_player.PlaySE (SEPlayer.SEType.Click);
				game_data.DataSetterInt (game_data.DataGetterInt (GameData.DataType.SEVolume) - 1, GameData.DataType.SEVolume);
			} else if (option_se_up) {
				se_player.PlaySE (SEPlayer.SEType.Click);
				game_data.DataSetterInt (game_data.DataGetterInt (GameData.DataType.SEVolume) + 1, GameData.DataType.SEVolume);
			} else if (option_bg_objects) {
				se_player.PlaySE (SEPlayer.SEType.Click);
				if (game_data.DataGetterInt (GameData.DataType.BackGroundObjects) == game_data.MaxGetterInt (GameData.DataType.BackGroundObjects)) {
					game_data.DataSetterInt (game_data.MinGetterInt (GameData.DataType.BackGroundObjects), GameData.DataType.BackGroundObjects);
				} else {
					game_data.DataSetterInt (game_data.DataGetterInt (GameData.DataType.BackGroundObjects) + 1, GameData.DataType.BackGroundObjects);
				}
			}
		}
	}

	/**
	 * メーターを表示(ついでにメーター情報を改訂)
	 */
	void PutMeter(float mx, float my, float mw, float mh, GameData.DataType data_type)
	{
		//メーター、ゲージのレクトを設定
		Rect	meter_rect = new Rect (
			                  Screen.width * mx, 
			                  Screen.height * (my + mh / 2) - Screen.width * mw * volume_meter.height / volume_meter.width / 2, 
			                  Screen.width * mw, 
			                  Screen.width * mw * volume_meter.height / volume_meter.width
		                  );
		Rect	gauge_rect = new Rect (
			                  Screen.width * mx + (Screen.width * mw - Screen.height * mh * volume_gauge.width / volume_gauge.height) * DataFloat (data_type), 
			                  Screen.height * my, 
			                  Screen.height * mh * volume_gauge.width / volume_gauge.height, 
			                  Screen.height * mh
		                  );
		//メーター、ゲージを表示
		GUI.DrawTexture(meter_rect, volume_meter);
		GUI.DrawTexture(gauge_rect, volume_gauge);

		if (data_type == GameData.DataType.BGMVolume) {
			bgm_meter_rect = meter_rect;
			bgm_gauge_rect = gauge_rect;
		} else if (data_type == GameData.DataType.SEVolume) {
			se_meter_rect = meter_rect;
			se_gauge_rect = gauge_rect;
		}
	}

	/**
	 * GameDataの値のMax値と比較した倍率(<=1)を返す
	 */
	float DataFloat(GameData.DataType data_type)
	{
		return (float)game_data.DataGetterInt (data_type) / (float)game_data.MaxGetterInt (data_type);
	}
}
