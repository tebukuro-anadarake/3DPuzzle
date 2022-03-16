using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchAbleObject : MonoBehaviour 
{
	bool			touch_down;

	/**
	 * そのオブジェクトがタッチされた瞬間のみtrue(マルチタップ対応)
	 */
	protected bool OnTouchDown() 
	{
		// タッチされているとき
		if (Input.touchCount > 0) {
			//タッチされている指の数だけ処理
			for (int i = 0; i < Input.touchCount; i++) {
				// タッチ情報をコピー
				Touch		t = Input.GetTouch (i);
				// タッチしたときかどうか
				if (t.phase == TouchPhase.Began) {
					//タッチした位置からRayを飛ばす
					Ray			ray = Camera.main.ScreenPointToRay (t.position);
					RaycastHit	hit = new RaycastHit ();
					if (Physics.Raycast (ray, out hit)) {
						//Rayを飛ばしてあたったオブジェクトが自分自身だったら
						if (hit.collider.gameObject == this.gameObject) {
							return true;
						}
					}
				}
			}
		}
		return false; //タッチされてなかったらfalse
	}

	/**
	 * そのオブジェクトがタッチを離されたらtrue(マルチタップ対応)
	 */
	protected bool OnTouchUp()
	{
		// タッチされているとき
		if (Input.touchCount > 0) {
			//タッチされている指の数だけ処理
			for (int i = 0; i < Input.touchCount; i++) {
				// タッチ情報をコピー
				Touch		t = Input.GetTouch (i);
				// タッチ離したときかどうか
				if (t.phase == TouchPhase.Ended) {
					//タッチした位置からRayを飛ばす
					Ray			ray = Camera.main.ScreenPointToRay (t.position);
					RaycastHit	hit = new RaycastHit ();
					if (Physics.Raycast (ray, out hit)) {
						//Rayを飛ばしてあたったオブジェクトが自分自身だったら
						if (hit.collider.gameObject == this.gameObject) {
							return true;
						}
					}
				}
			}
		}
		return false; //タッチされてなかったらfalse
	}

	/**
	 * そのオブジェクトがタッチされていたらtrue(マルチタップ対応)
	 */
	protected bool OnTouch()
	{
		// タッチされているとき
		if (Input.touchCount > 0) {
			//タッチされている指の数だけ処理
			for (int i = 0; i < Input.touchCount; i++) {
				// タッチ情報をコピー
				Touch		t = Input.GetTouch (i);
				//タッチした位置からRayを飛ばす
				Ray			ray = Camera.main.ScreenPointToRay (t.position);
				RaycastHit	hit = new RaycastHit ();
				if (Physics.Raycast (ray, out hit)) {
					//Rayを飛ばしてあたったオブジェクトが自分自身だったら
					if (hit.collider.gameObject == this.gameObject) {
						return true;
					}
				}
			}
		}
		return false; //タッチされてなかったらfalse
	}

	/**
	 *　そのオブジェクトがタップされたらtrue(マルチタップ対応)(Updateで呼び出すこと)
	 */
	protected bool OnTouchTap()
	{
		if (OnTouchDown ()) {
			touch_down = true;
			return false;
		} else if (!OnTouch ()) {
			touch_down = false;
			return false;
		} else if (OnTouchUp () && touch_down) {
			return true;
		} else {
			return false;
		}
	}
}
