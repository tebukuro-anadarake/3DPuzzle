using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * このクラスでは、UIなどの3D位置に関係ない効果音を鳴らすことができます。
 * 3Dで鳴らすべきものや、複雑な操作で鳴らすものはここでは鳴らさないでください。
 */
public class SEPlayer : MonoBehaviour 
{
	public enum 					SEType{Click, Decide, Back, Destroy};
	GameData						game_data;
	AudioSource						audio_source;
	Dictionary<SEType, AudioClip>	audio_clips;
	[SerializeField]
	AudioClip						click_clip;
	[SerializeField]
	AudioClip						decide_clip;
	[SerializeField]
	AudioClip						back_clip;
	[SerializeField]
	AudioClip						destroy_clip;


	void Start () 
	{
		game_data = gameObject.GetComponent<GameData> ();
		audio_source = gameObject.GetComponent<AudioSource> ();

		audio_clips = new Dictionary<SEType, AudioClip> () {
			{ SEType.Click, click_clip },
			{ SEType.Decide, decide_clip },
			{ SEType.Back, back_clip },
			{ SEType.Destroy, destroy_clip }
		};
	}

	void Update()
	{
		audio_source.volume = (float)game_data.DataGetterInt (GameData.DataType.SEVolume) / (float)game_data.MaxGetterInt (GameData.DataType.SEVolume) / 2;
	}

	/**
	 * 指定したSETypeの効果音を鳴らします
	 */
	public void PlaySE(SEType se_type)
	{
		audio_source.clip = audio_clips [se_type];
		audio_source.Play ();
	}
}
