using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour 
{
	AudioSource[]	audio_sources = new AudioSource[2];
	float[]			bgms_volume = new float[2];
	GameData		game_data;
	[SerializeField]
	float			feed_time;
	[SerializeField]
	float			start_time;
	[SerializeField]
	float			end_time;
	[SerializeField]
	bool			is_feed_in;
	[SerializeField]
	AudioClip		bgm_clip;
	float			play_start_counter;
	float			play_start_time;
	int				audio_number;
	void Start () 
	{
		audio_sources = gameObject.GetComponents<AudioSource> ();
		game_data = GameObject.Find ("DontDestroyObject").GetComponent<GameData> ();
		audio_sources [0].clip = bgm_clip;
		audio_sources [1].clip = bgm_clip;
		if (is_feed_in) {
			bgms_volume [0] = 0;
			bgms_volume [1] = 0;
		} else {
			bgms_volume [0] = 1f;
			bgms_volume [1] = 1f;
		}
		play_start_counter = 0;
		play_start_time = 1;
		audio_number = 0;
		audio_sources [0].time = start_time;
		audio_sources [1].time = start_time;
		if (feed_time == 0) {
			feed_time = 0.000001f;
		}
	}
	
	void Update () 
	{
		if (audio_sources[0].isPlaying || audio_sources[1].isPlaying) {
			//フィードイン
			if (bgms_volume[audio_number] < 1f) {
				bgms_volume[audio_number] += Time.deltaTime / feed_time;
			}
			audio_sources[audio_number].volume = bgms_volume[audio_number] * (float)game_data.DataGetterInt (GameData.DataType.BGMVolume) / (float)game_data.MaxGetterInt (GameData.DataType.BGMVolume) / 2;

			//ループ機能
			if (audio_sources[audio_number].time >= end_time) {
				audio_number = ZeroOneChange (audio_number);
				if (is_feed_in) {
					bgms_volume [audio_number] = 0;
				} else {
					bgms_volume [audio_number] = 1f;
				}
				audio_sources[audio_number].volume = bgms_volume[audio_number] * (float)game_data.DataGetterInt (GameData.DataType.BGMVolume) / (float)game_data.MaxGetterInt (GameData.DataType.BGMVolume) / 2;
				audio_sources [audio_number].time = start_time;
				audio_sources[audio_number].Play ();
			}
		} else {
			//オーディオ再生までのカウント
			play_start_counter += Time.deltaTime;
			if (play_start_counter >= play_start_time) {
				audio_sources[audio_number].Play ();
				play_start_counter = 0;
			}
		}
	}

	/**
	 * 0なら1、1なら0を返す
	 */ 
	int ZeroOneChange(int number)
	{
		if (number == 0) {
			return 1;
		} else if (number == 1) {
			return 0;
		} else {
			return int.MinValue;
		}
	}
}
