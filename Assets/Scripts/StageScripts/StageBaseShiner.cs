using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageBaseShiner : MonoBehaviour 
{
	GameObject		gm_obj;
	GameManager		game_manager;
	Material		base_material;

	void Start () 
	{
		gm_obj = GameObject.Find ("GameManager");
		if (gm_obj != null) {
			game_manager = gm_obj.GetComponent<GameManager> ();
		}
		base_material = gameObject.GetComponent<Renderer> ().material;
	}
	
	void Update () 
	{
		if (gm_obj != null) {
			if (game_manager.clear) {
				base_material.EnableKeyword ("_EMISSION");
				base_material.SetColor ("_EmissionColor", new Color (0.3f, 0.7f, 1));
			} else {
				base_material.EnableKeyword ("_EMISSION");
				base_material.SetColor ("_EmissionColor", new Color (1, 0.3f, 0.3f));
			}
		} else {
			base_material.EnableKeyword ("_EMISSION");
			base_material.SetColor ("_EmissionColor", new Color (0.16f, 1, 0.16f));
		}
	}
}
