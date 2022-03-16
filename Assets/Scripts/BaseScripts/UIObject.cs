using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIObject : MonoBehaviour 
{
	/**
	 * テクスチャを指定したサイズで表示してその上に文字を表示したボタンを表示
	 */
	protected bool TextureButton(Rect rect, Texture2D texture, string name, GUIStyle style)
	{
		GUI.DrawTexture (rect, texture);
		bool	button = GUI.Button (rect, name, style);
		return button;
	}

	/**
	 * テクスチャを指定したサイズで表示してその上に文字を表示したボタンを表示
	 */
	protected bool TextureButton(Rect rect, Texture2D texture)
	{
		GUI.DrawTexture (rect, texture);
		bool	button = GUI.Button (rect, "");
		return button;
	}

	/**
	 * テクスチャを指定したサイズで表示してその上に文字を表示したボタンを表示
	 */
	protected bool TextureRepeatButton(Rect rect, Texture2D texture, string name, GUIStyle style)
	{
		GUI.DrawTexture (rect, texture);
		bool	button = GUI.RepeatButton (rect, name, style);
		return button;
	}

	/**
	 * テクスチャを指定したサイズで表示してその上に文字を表示したボタンを表示
	 */
	protected bool TextureRepeatButton(Rect rect, Texture2D texture)
	{
		GUI.DrawTexture (rect, texture);
		bool	button = GUI.RepeatButton (rect, "");
		return button;
	}


	/**
	 * RectからそのRectの枠の中心に位置する正方形を返す
	 */
	protected Rect CenterRectSquare(Rect rect)
	{
		float	x = rect.x;
		float	y = rect.y;
		float	w = rect.width;
		float	h = rect.height;
		if (rect.height > rect.width) {
			h = rect.width;
			y += (rect.height - rect.width) / 2;
		} else {
			w = rect.height;
			x += (rect.width - rect.height) / 2;
		}
		return new Rect (x, y, w, h);
	}

	/**
	 * RectからそのRectの上端の中心に位置する正方形を返す
	 */
	protected Rect UpperRectSquare(Rect rect)
	{
		float	x = rect.x;
		float	y = rect.y;
		float	w = rect.width;
		float	h = rect.height;
		if (rect.height > rect.width) {
			h = rect.width;
		} else {
			w = rect.height;
			x += (rect.width - rect.height) / 2;
		}
		return new Rect (x, y, w, h);
	}

	/**
	 * RectからそのRectの上端の右端に位置する正方形を返す
	 */
	protected Rect UpperRightRectSquare(Rect rect)
	{
		float	x = rect.x;
		float	y = rect.y;
		float	w = rect.width;
		float	h = rect.height;
		if (rect.height > rect.width) {
			h = rect.width;
		} else {
			w = rect.height;
			x += (rect.width - rect.height);
		}
		return new Rect (x, y, w, h);
	}

	/**
	 * RectからそのRectの上端の左端に位置する正方形を返す
	 */
	protected Rect UpperLeftRectSquare(Rect rect)
	{
		float	x = rect.x;
		float	y = rect.y;
		float	w = rect.width;
		float	h = rect.height;
		if (rect.height > rect.width) {
			h = rect.width;
		} else {
			w = rect.height;
		}
		return new Rect (x, y, w, h);
	}

	/**
	 * RectからそのRectの下端の右端に位置する正方形を返す
	 */
	protected Rect DownerRightRectSquare(Rect rect)
	{
		float	x = rect.x;
		float	y = rect.y;
		float	w = rect.width;
		float	h = rect.height;
		if (rect.height > rect.width) {
			h = rect.width;
			y += (rect.height - rect.width);
		} else {
			w = rect.height;
			x += (rect.width - rect.height);
		}
		return new Rect (x, y, w, h);
	}

	/**
	 * 画面の縦と横の差の半分を返す
	 */
	protected Vector2 Margin()
	{
		if (Screen.height > Screen.width) {
			return new Vector2 (0, (Screen.height - Screen.width) / 2);
		} else {
			return new Vector2 ((Screen.width - Screen.height) / 2, 0);
		}
	}

	/**
	 * 画面の中央に入る最大の正方形の辺の長さ
	 */
	protected float MaxSquareSide()
	{
		if (Screen.height > Screen.width) {
			return Screen.width;
		} else {
			return Screen.height;
		}
	}

	/**
	 * 画面中央に切り取られた最大の正方形の中でRectを作成する方法です。
	 */
	protected Rect MarginedRect(float x, float y, float width, float height)
	{
		return new Rect (Margin ().x + MaxSquareSide () * x, Margin ().y + MaxSquareSide () * y, MaxSquareSide () * width, MaxSquareSide () * height);
	}

	/**
	 * 小さい方を返す
	 */
	protected float MinFloat(float a, float b)
	{
		if (a <= b) {
			return a;
		} else {
			return b;
		}
	}

	/**
	 * widthとheight小さい方を返す
	 */
	protected float MinWH()
	{
		return MinFloat (Screen.width, Screen.height);
	}
}
