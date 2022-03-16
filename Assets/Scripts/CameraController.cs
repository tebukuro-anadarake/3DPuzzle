using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : UIObject 
{
	[SerializeField]
	float		x_rotate_speed;
	[SerializeField]
	float		y_rotate_speed;
	[SerializeField]
	float		z_rotate_speed;
	[SerializeField]
	float		zoom_speed;
	[SerializeField]
	float		max_zoom_in;
	[SerializeField]
	float		max_zoom_out;
	public bool	move_able{ get; set; }
	bool		z_rotate_button_plus;
	bool		z_rotate_button_minus;
	GameObject	camera_obj;
	[SerializeField]
	Texture2D	right_spin_button;
	[SerializeField]
	Texture2D	left_spin_button;

	void Start () 
	{
		move_able = true;
		camera_obj = GameObject.Find("CameraStand/Main Camera");
	}
	
	void Update ()
	{
		if (move_able) {
			if (z_rotate_button_plus) {
				transform.Rotate (new Vector3 (0, 0, z_rotate_speed * Time.deltaTime));
			} else if (z_rotate_button_minus) {
				transform.Rotate (new Vector3 (0, 0, -1 * z_rotate_speed * Time.deltaTime));
			}
			//タッチ操作に対する反応
			if (Input.touchCount > 0) {
				//タッチ位置の重心を求める
				Vector2			center_of_gravity = new Vector2(0, 0);
				for (int i = 0; i < Input.touchCount; i++) {
					Touch	touch = Input.GetTouch (i);
					center_of_gravity += touch.position;
				}
				center_of_gravity = center_of_gravity / Input.touchCount;

				//タッチの移動の成分を加法。相殺分はZoomに重心からの方向に応じて追加。
				Vector3			camera_move = new Vector3(0, 0, 0);
				float			zoom = 0;
				for (int i = 0; i < Input.touchCount; i++) {
					Touch	touch = Input.GetTouch (i);
					//X方向について
					if (camera_move.x * touch.deltaPosition.x < 0) {
						float i_zoom = Mathf.Abs (touch.position.x - center_of_gravity.x);
						if ((touch.position.x - center_of_gravity.x) * touch.deltaPosition.x < 0) {
							zoom -= i_zoom / Screen.width;
						} else {
							zoom += i_zoom / Screen.width;
						}
					} 
					camera_move += new Vector3 (0, touch.deltaPosition.x / Screen.width, 0);
					//Y方向について
					if (camera_move.y * touch.deltaPosition.y < 0) {
						float i_zoom = Mathf.Abs (touch.position.y - center_of_gravity.y);
						if ((touch.position.y - center_of_gravity.y) * touch.deltaPosition.y < 0) {
							zoom -= i_zoom / Screen.height;
						} else {
							zoom += i_zoom / Screen.height;
						}
					} 
					camera_move += new Vector3 (-1 * touch.deltaPosition.y / Screen.height, 0, 0);
				}
				transform.Rotate ((new Vector3(camera_move.x * x_rotate_speed, camera_move.y * y_rotate_speed, 0)) * Time.deltaTime);
				if (zoom > 0) {
					if (CameraStandLong (camera_obj.transform.localPosition) > max_zoom_in) {
						camera_obj.transform.localPosition = CameraZoom (zoom * zoom_speed * CameraStandLong (camera_obj.transform.localPosition), camera_obj.transform.localPosition);
					}
				} else if (zoom < 0) {
					if (CameraStandLong (camera_obj.transform.localPosition) < max_zoom_out) {
						camera_obj.transform.localPosition = CameraZoom (zoom * zoom_speed * CameraStandLong (camera_obj.transform.localPosition), camera_obj.transform.localPosition);
					}
				}
			}
		}
	}

	void OnGUI ()
	{
		//ボタンの透明化
		GUI.backgroundColor = Color.clear;

		if (move_able) {
			z_rotate_button_plus = TextureRepeatButton (DownerRightRectSquare(new Rect (Screen.width * 3 / 4 , Screen.height * 5 / 6, Screen.width / 4, Screen.height / 6)), left_spin_button);
			z_rotate_button_minus = TextureRepeatButton (DownerRightRectSquare(new Rect (Screen.width / 2, Screen.height * 5 / 6, Screen.width / 4, Screen.height / 6)), right_spin_button);
		}
	}


	/**
	 * Zoom移動後のカメラの位置を返します
	 * (引数　ズームスピード、カメラの現在位置)
	 */
	Vector3 CameraZoom(float speed, Vector3 position)
	{
		Vector3 vector = new Vector3 (
			                 (position.x - (position.x / (Mathf.Abs (position.x) + Mathf.Abs (position.y) + Mathf.Abs (position.z))) * speed * Time.deltaTime),
			                 (position.y - (position.y / (Mathf.Abs (position.x) + Mathf.Abs (position.y) + Mathf.Abs (position.z))) * speed * Time.deltaTime),
			                 (position.z - (position.z / (Mathf.Abs (position.x) + Mathf.Abs (position.y) + Mathf.Abs (position.z))) * speed * Time.deltaTime)
		                 );
		return vector;
	}


	/**
	 * カメラとカメラスタンドの間の長さを返します
	 * (引数　カメラの現在位置)
	 */
	float CameraStandLong(Vector3 position)
	{
		return Mathf.Sqrt (Mathf.Pow (position.x, 2) + Mathf.Pow (position.y, 2) + Mathf.Pow (position.z, 2));
	}
}
