using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayGameMenu : TouchAbleObject 
{
	MenuManager		menu_manager;
	SEPlayer		se_player;

	void Start () 
	{
		menu_manager = GameObject.Find ("MenuManager").GetComponent<MenuManager> ();
		se_player = GameObject.Find ("DontDestroyObject").GetComponent<SEPlayer> ();
	}
	
	void Update () 
	{
		if (menu_manager.status == MenuManager.MenuStatus.Top) {
			if (OnTouchTap ()) {
				se_player.PlaySE (SEPlayer.SEType.Click);
				menu_manager.status = MenuManager.MenuStatus.StageSelect;
			}
		}
	}
}
