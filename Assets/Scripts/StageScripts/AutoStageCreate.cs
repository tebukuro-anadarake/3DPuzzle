using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoStageCreate : MonoBehaviour 
{
	StageData	stage_data;
	[SerializeField]
	GameObject	curve_object;
	[SerializeField]
	GameObject	straight_object;
	[SerializeField]
	GameObject	zero_number_object;

	void Awake () 
	{
		stage_data = GetComponent<StageData> ();
		stage_data.StageDataSetter ();

		const float			high_position = 2.95f;
		const float			max_position = 1.5f;
		const float			square_width = 1.0f;
		Vector3[,]			position_matrix = GameObject.Find("GameManager").GetComponent<PositionMatrixSystem>().PositionMatrix (max_position, square_width, high_position);	//Position割り振り行列

		//ここで全ての回路を自動生成
		const int		matrix_height = 16;
		const int		matrix_width = 12;
		int				circuit_number = 0;

		for (int n = 0; n < matrix_height; n++) {
			for (int m = 0; m < matrix_width; m++) {
				if (stage_data.circuits [n, m] [0] != 0) {
					GameObject	n_obj = new GameObject();
					switch (stage_data.circuits [n, m] [0]) {
					case 1:
						n_obj = Instantiate (straight_object);
						break;
					case 2:
						n_obj = Instantiate (curve_object);
						break;
					}
					n_obj.name = "Circuit" + circuit_number;
					n_obj.transform.position = new Vector3 (position_matrix [n, m] [0], position_matrix [n, m] [1], position_matrix [n, m] [2]);
					n_obj.transform.rotation = Quaternion.Euler (ObjectEuler(n, m));
					if (circuit_number == 0) {
						GameObject zero_obj = Instantiate (zero_number_object);
						zero_obj.transform.parent = n_obj.transform;
						zero_obj.transform.localPosition = new Vector3 (0, 0.85f, 0);
					}
					GameObject.Find (n_obj.name + "/Circuit").transform.localRotation = Quaternion.Euler (0, stage_data.circuits[n, m][1] * 90, 0);
					circuit_number++;
				}
			}
		}
	}

	/**
	 * [n, m]におけるオブジェクトの回転を返す
	 */
	Vector3 ObjectEuler(int n, int m)
	{
		if (0 <= n && n <= 3 && 4 <= m && m <= 7) {
			return new Vector3 (0, 0, 180);
		} else if (4 <= n && n <= 7 && 4 <= m && m <= 7) {
			return new Vector3 (0, 0, 90);
		} else if (8 <= n && n <= 11 && 0 <= m && m <= 3) {
			return new Vector3 (-90, 0, 0);
		} else if (8 <= n && n <= 11 && 4 <= m && m <= 7) {
			return new Vector3 (0, 0, 0);
		} else if (8 <= n && n <= 11 && 8 <= m && m <= 11) {
			return new Vector3 (90, 0, 0);
		} else if (12 <= n && n <= 15 && 4 <= m && m <= 7) {
			return new Vector3 (0, 0, -90);
		} else {
			return new Vector3 (0, 0, 0);
		}
	}
}
