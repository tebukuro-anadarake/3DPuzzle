using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugOnAndroid : MonoBehaviour 
{
	private static List<string> logMsg = new List<string>();

	public static void log(string msg) 
	{
		logMsg.Add(msg);
		// 直近の30件のみ保存する
		if (logMsg.Count > 30) {
			logMsg.RemoveAt (0);
		}
	}

	Rect		rect;
	GUIStyle	style;

	void Start()
	{
		rect = new Rect( 0, 0, Screen.width, Screen.height );
		style = new GUIStyle();
		style.fontSize = 30;
		style.fontStyle = FontStyle.Normal;
		style.normal.textColor = Color.white;
	}

	void OnGUI ()
	{
		string outMessage = "";
		foreach (string msg in logMsg) {
			outMessage += msg + System.Environment.NewLine;
		}

		GUI.Label(rect, outMessage, style);
	}
}
