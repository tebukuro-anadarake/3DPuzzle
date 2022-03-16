using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircuitStatus : MonoBehaviour 
{
	public int				spin_number{ get; set;}
	//回路の初期設定
	private bool			first_circuit_p_z;
	private bool			first_circuit_p_x;
	private bool			first_circuit_m_z;
	private bool			first_circuit_m_x;
	//回路の種類
	[SerializeField]
	private bool			straight_circuit;
	[SerializeField]
	private bool			curve_circuit;
	//現在の回路の向き
	public bool				circuit_p_z{get; private set;}
	public bool				circuit_p_x{get; private set;}
	public bool				circuit_m_z{get; private set;}
	public bool				circuit_m_x{get; private set;}
	//隣のオブジェクト
	private GameObject		obj_p_z;
	private GameObject		obj_p_z_child;
	public GameObject		ObjPZ{ get { return obj_p_z_child; }}

	private GameObject		obj_p_x;
	private GameObject		obj_p_x_child;
	public GameObject		ObjPX{ get { return obj_p_x_child; }}

	private GameObject		obj_m_z;
	private GameObject		obj_m_z_child;
	public GameObject		ObjMZ{ get { return obj_m_z_child; }}

	private GameObject		obj_m_x;
	private GameObject		obj_m_x_child;
	public GameObject		ObjMX{ get { return obj_m_x_child; }}
	//回路の両端が繋がっているか（繋がってるオブジェクトのみ入るリスト）
	public List<GameObject>	chained_object = new List<GameObject>();
	//private bool			isZeroNumber;
	public bool				isZeroNumberChained{ get; set; }
	//public List<GameObject>	zero_chained_object = new List<GameObject>();
	//回路のそれぞれの方向はどの方向に繋がっているか
	public enum 			CircuitChain{PX, MX, PZ, MZ};
	private CircuitChain	p_x_chain;
	private CircuitChain	m_x_chain;
	private CircuitChain	p_z_chain;
	private CircuitChain	m_z_chain;


	void Start () 
	{
		//あまりにも面倒なので一度座標設定の自動化に挑戦する
		const float			forgive_error = 0.2f;
		const float			high_position = 2.95f;
		const float			max_position = 1.5f;
		const float			min_position = -1.5f;
		const float			square_width = 1.0f;

		const int 			square_size = 4;

		//Vector3[,]			position_matrix = PositionMatrix (max_position, square_width, high_position);	//Position割り振り行列
		Vector3[,]			position_matrix = GameObject.Find("GameManager").GetComponent<PositionMatrixSystem>().PositionMatrix (max_position, square_width, high_position);	//Position割り振り行列

		//自分の座標がどのマスかを調べる
		int this_i = 0;
		int this_j = 0;
		for (int i = 0; i < square_size * 4; i++) {
			for (int j = 0; j < square_size * 3; j++) {
				if (ErrorForgiveEqual (position_matrix [i, j].x, transform.position.x, forgive_error) && ErrorForgiveEqual (position_matrix [i, j].y, transform.position.y, forgive_error) && ErrorForgiveEqual (position_matrix [i, j].z, transform.position.z, forgive_error)) {
					this_i = i;
					this_j = j;
				}
			}
		}

		bool				circuit_findable = true;
		for(int n = 0; circuit_findable; n++) {
			GameObject	finded_obj = GameObject.Find ("Circuit" + n);
			if (finded_obj != null) {
				if (		ErrorForgiveEqual (position_matrix [PXVersus(this_i, this_j)[0], PXVersus(this_i, this_j)[1]].x, finded_obj.transform.position.x, forgive_error) &&
							ErrorForgiveEqual (position_matrix [PXVersus(this_i, this_j)[0], PXVersus(this_i, this_j)[1]].y, finded_obj.transform.position.y, forgive_error) &&
							ErrorForgiveEqual (position_matrix [PXVersus(this_i, this_j)[0], PXVersus(this_i, this_j)[1]].z, finded_obj.transform.position.z, forgive_error)) {
					obj_p_x = finded_obj;
				} else if (	ErrorForgiveEqual (position_matrix [MXVersus(this_i, this_j)[0], MXVersus(this_i, this_j)[1]].x, finded_obj.transform.position.x, forgive_error) &&
							ErrorForgiveEqual (position_matrix [MXVersus(this_i, this_j)[0], MXVersus(this_i, this_j)[1]].y, finded_obj.transform.position.y, forgive_error) &&
							ErrorForgiveEqual (position_matrix [MXVersus(this_i, this_j)[0], MXVersus(this_i, this_j)[1]].z, finded_obj.transform.position.z, forgive_error)) {
					obj_m_x = finded_obj; 
				} else if (	ErrorForgiveEqual (position_matrix [PZVersus(this_i, this_j)[0], PZVersus(this_i, this_j)[1]].x, finded_obj.transform.position.x, forgive_error) &&
							ErrorForgiveEqual (position_matrix [PZVersus(this_i, this_j)[0], PZVersus(this_i, this_j)[1]].y, finded_obj.transform.position.y, forgive_error) &&
							ErrorForgiveEqual (position_matrix [PZVersus(this_i, this_j)[0], PZVersus(this_i, this_j)[1]].z, finded_obj.transform.position.z, forgive_error)) {
					obj_p_z = finded_obj; 
				} else if (	ErrorForgiveEqual (position_matrix [MZVersus(this_i, this_j)[0], MZVersus(this_i, this_j)[1]].x, finded_obj.transform.position.x, forgive_error) &&
							ErrorForgiveEqual (position_matrix [MZVersus(this_i, this_j)[0], MZVersus(this_i, this_j)[1]].y, finded_obj.transform.position.y, forgive_error) &&
							ErrorForgiveEqual (position_matrix [MZVersus(this_i, this_j)[0], MZVersus(this_i, this_j)[1]].z, finded_obj.transform.position.z, forgive_error)) {
					obj_m_z = finded_obj; 
				}
			} else {
				circuit_findable = false;
			}
		}

		if (obj_p_z != null) {
			obj_p_z_child = GameObject.Find (obj_p_z.name + "/Circuit");
		}
		if (obj_p_x != null) {
			obj_p_x_child = GameObject.Find (obj_p_x.name + "/Circuit");
		}
		if (obj_m_z != null) {
			obj_m_z_child = GameObject.Find (obj_m_z.name + "/Circuit");
		}
		if (obj_m_x != null) {
			obj_m_x_child = GameObject.Find (obj_m_x.name + "/Circuit");
		}

		//こっから回路の回転についての自動化
		if (straight_circuit) {
			first_circuit_p_z = true;
			first_circuit_p_x = false;
			first_circuit_m_z = true;
			first_circuit_m_x = false;
		} else if (curve_circuit) {
			first_circuit_p_x = false;
			first_circuit_p_z = false;
			first_circuit_m_z = true;
			first_circuit_m_x = true;
		}
		if (ErrorForgiveEqual (transform.localEulerAngles.y, 0, 1)) {
			spin_number = 0;
		} else if (ErrorForgiveEqual (transform.localEulerAngles.y, 90, 1)) {
			spin_number = 1;
		} else if (ErrorForgiveEqual (transform.localEulerAngles.y, 180, 1)) {
			spin_number = 2;
		} else if (ErrorForgiveEqual (transform.localEulerAngles.y, 270, 1)) {
			spin_number = 3;
		}

		//こっからisZeroNumberの自動化
		/*
		if (transform.parent.gameObject.name.Equals ("Circuit0")) {
			isZeroNumber = true;
		} else {
			isZeroNumber = false;
		}*/

		//回路がどの方向につながっているか
		//基本形
		p_x_chain = CircuitChain.MX;
		p_z_chain = CircuitChain.MZ;
		m_x_chain = CircuitChain.PX;
		m_z_chain = CircuitChain.PZ;
		//変化形
		if (0 <= this_i && this_i <= 3 && this_j == 4) {
			m_z_chain = CircuitChain.MZ;
		} 
		if (0 <= this_i && this_i <= 3 && this_j == 7) {
			p_z_chain = CircuitChain.PZ;
		}
		if (4 <= this_i && this_i <= 7 && this_j == 4) {
			m_z_chain = CircuitChain.MX;
		}
		if (4 <= this_i && this_i <= 7 && this_j == 7) {
			p_z_chain = CircuitChain.MX;
		}
		if (this_i == 8 && 0 <= this_j && this_j <= 3) {
			m_x_chain = CircuitChain.MZ;
		}
		if (this_i == 8 && 8 <= this_j && this_j <= 11) {
			m_x_chain = CircuitChain.PZ;
		}
		if (8 <= this_i && this_i <= 11 && this_j == 0) {
			m_z_chain = CircuitChain.MZ;
		}
		if (8 <= this_i && this_i <= 11 && this_j == 11) {
			p_z_chain = CircuitChain.PZ;
		}
		if (this_i == 11 && 0 <= this_j && this_j <= 3) {
			p_x_chain = CircuitChain.MZ;
		}
		if (this_i == 11 && 8 <= this_j && this_j <= 11) {
			p_x_chain = CircuitChain.PZ;
		}
		if (12 <= this_i && this_i <= 15 && this_j == 4) {
			m_z_chain = CircuitChain.PX;
		} 
		if (12 <= this_i && this_i <= 15 && this_j == 7) {
			p_z_chain = CircuitChain.PX;
		}
	}


	/**
	 * PX,MX,PZ,MZの方向を向いているかどうかを返す
	 */
	public bool ChainableCheck(CircuitChain circuit)
	{
		if (circuit == CircuitChain.PX) {
			return circuit_p_x;
		} else if (circuit == CircuitChain.MX) {
			return circuit_m_x;
		} else if (circuit == CircuitChain.PZ) {
			return circuit_p_z;
		} else if (circuit == CircuitChain.MZ) {
			return circuit_m_z;
		} else {
			return false;
		}
	}


	/**
	 * pz方向の対応(iは行、jは列)
	 */
	int[] PZVersus(int i, int j)
	{
		Dictionary<long, int[]>	versus = new Dictionary<long, int[]> (){ 
			{S(0, 7), V(11, 11)}, {S(1, 7), V(10, 11)}, {S(2, 7), V(9, 11)}, {S(3, 7), V(8, 11)},
			{S(4, 7), V(8, 11)}, {S(5, 7), V(8, 10)}, {S(6, 7), V(8, 9)}, {S(7, 7), V(8, 8)},
			{S(8, 11), V(3, 7)}, {S(9, 11), V(2, 7)}, {S(10, 11), V(1, 7)}, {S(11, 11), V(0, 7)},
			{S(12, 7), V(11, 8)}, {S(13, 7), V(11, 9)}, {S(14, 7), V(11, 10)}, {S(15, 7), V(11, 11)}
		};
		if (versus.ContainsKey (S (i, j))) {
			return versus [S (i, j)];
		} else {
			return V (i, j + 1);
		} 
	}

	/**
	 * mz方向の対応(iは行、jは列)
	 */
	int[] MZVersus(int i, int j)
	{
		Dictionary<long, int[]>	versus = new Dictionary<long, int[]> (){ 
			{S(0, 4), V(11, 0)}, {S(1, 4), V(10, 0)}, {S(2, 4), V(9, 0)}, {S(3, 4), V(8, 0)},
			{S(4, 4), V(8, 0)}, {S(5, 4), V(8, 1)}, {S(6, 4), V(8, 2)}, {S(7, 4), V(8, 3)},
			{S(8, 0), V(3, 4)}, {S(9, 0), V(2, 4)}, {S(10, 0), V(1, 4)}, {S(11, 0), V(0, 4)},
			{S(12, 4), V(11, 3)}, {S(13, 4), V(11, 2)}, {S(14, 4), V(11, 1)}, {S(15, 4), V(11, 0)}
		};
		if (versus.ContainsKey (S (i, j))) {
			return versus [S (i, j)];
		} else {
			return V (i, j - 1);
		} 
	}

	/**
	 * px方向の対応(iは行、jは列)
	 */
	int[] PXVersus(int i, int j)
	{
		Dictionary<long, int[]>	versus = new Dictionary<long, int[]> (){ 
			{S(11, 0), V(15, 4)}, {S(11, 1), V(14, 4)}, {S(11, 2), V(13, 4)}, {S(11, 3), V(12, 4)},
			{S(15, 4), V(0, 4)}, {S(15, 5), V(0, 5)}, {S(15, 6), V(0, 6)}, {S(15, 7), V(0, 7)},
			{S(11, 8), V(12, 7)}, {S(11, 9), V(13, 7)}, {S(11, 10), V(14, 7)}, {S(11, 11), V(15, 7)}
		};
		if (versus.ContainsKey (S (i, j))) {
			return versus [S (i, j)];
		} else {
			return V (i + 1, j);
		} 
	}

	/**
	 * mx方向の対応(iは行、jは列)
	 */
	int[] MXVersus(int i, int j)
	{
		Dictionary<long, int[]>	versus = new Dictionary<long, int[]> (){ 
			{S(8, 0), V(4, 4)}, {S(8, 1), V(5, 4)}, {S(8, 2), V(6, 4)}, {S(8, 3), V(7, 4)},
			{S(0, 4), V(15, 4)}, {S(0, 5), V(15, 5)}, {S(0, 6), V(15, 6)}, {S(0, 7), V(15, 7)},
			{S(8, 8), V(7, 7)}, {S(8, 9), V(6, 7)}, {S(8, 10), V(5, 7)}, {S(8, 11), V(4, 7)}
		};
		if (versus.ContainsKey (S (i, j))) {
			return versus [S (i, j)];
		} else {
			return V (i - 1, j);
		} 
	}



	int[] V(int i, int j)
	{
		return new int[]{ i, j };
	}

	long S(int i, int j)
	{
		return (i * 10000000000L + j);
	}


	/**
	 * 16×12 Position行列作成。引数は(max_position, square_width, high_position)
	 *
	Vector3[,] PositionMatrix(float a, float w, float h)
	{
		Vector3[,]	position_matrix = new Vector3[,]{
			//my
			{P(0, 0, 0), P(0, 0, 0), P(0, 0, 0), P(0, 0, 0), P(a, -1*h, a-w*3),     P(a, -1*h, a-w*2),     P(a, -1*h, a-w),     P(a, -1*h, a),     P(0, 0, 0), P(0, 0, 0), P(0, 0, 0), P(0, 0, 0)},
			{P(0, 0, 0), P(0, 0, 0), P(0, 0, 0), P(0, 0, 0), P(a-w, -1*h, a-w*3),   P(a-w, -1*h, a-w*2),   P(a-w, -1*h, a-w),   P(a-w, -1*h, a),   P(0, 0, 0), P(0, 0, 0), P(0, 0, 0), P(0, 0, 0)},
			{P(0, 0, 0), P(0, 0, 0), P(0, 0, 0), P(0, 0, 0), P(a-w*2, -1*h, a-w*3), P(a-w*2, -1*h, a-w*2), P(a-w*2, -1*h, a-w), P(a-w*2, -1*h, a), P(0, 0, 0), P(0, 0, 0), P(0, 0, 0), P(0, 0, 0)},
			{P(0, 0, 0), P(0, 0, 0), P(0, 0, 0), P(0, 0, 0), P(a-w*3, -1*h, a-w*3), P(a-w*3, -1*h, a-w*2), P(a-w*3, -1*h, a-w), P(a-w*3, -1*h, a), P(0, 0, 0), P(0, 0, 0), P(0, 0, 0), P(0, 0, 0)},
			//mx
			{P(0, 0, 0), P(0, 0, 0), P(0, 0, 0), P(0, 0, 0), P(-1*h, a-w*3, a-w*3), P(-1*h, a-w*3, a-w*2), P(-1*h, a-w*3, a-w), P(-1*h, a-w*3, a), P(0, 0, 0), P(0, 0, 0), P(0, 0, 0), P(0, 0, 0)},
			{P(0, 0, 0), P(0, 0, 0), P(0, 0, 0), P(0, 0, 0), P(-1*h, a-w*2, a-w*3), P(-1*h, a-w*2, a-w*2), P(-1*h, a-w*2, a-w), P(-1*h, a-w*2, a), P(0, 0, 0), P(0, 0, 0), P(0, 0, 0), P(0, 0, 0)},
			{P(0, 0, 0), P(0, 0, 0), P(0, 0, 0), P(0, 0, 0), P(-1*h, a-w, a-w*3),   P(-1*h, a-w, a-w*2),   P(-1*h, a-w, a-w),   P(-1*h, a-w, a),   P(0, 0, 0), P(0, 0, 0), P(0, 0, 0), P(0, 0, 0)},
			{P(0, 0, 0), P(0, 0, 0), P(0, 0, 0), P(0, 0, 0), P(-1*h, a, a-w*3),     P(-1*h, a, a-w*2),     P(-1*h, a, a-w),     P(-1*h, a, a),     P(0, 0, 0), P(0, 0, 0), P(0, 0, 0), P(0, 0, 0)},
			//mz, py, pz
			{P(a-w*3, a-w*3, -1*h), P(a-w*3, a-w*2, -1*h), P(a-w*3, a-w, -1*h),   P(a-w*3, a, -1*h),    P(a-w*3, h, a-w*3), P(a-w*3, h, a-w*2), P(a-w*3, h, a-w), P(a-w*3, h, a), P(a-w*3, a, h), P(a-w*3, a-w, h), P(a-w*3, a-w*2, h), P(a-w*3, a-w*3, h)},
			{P(a-w*2, a-w*3, -1*h), P(a-w*2, a-w*2, -1*h), P(a-w*2, a-w, -1*h),   P(a-w*2, a, -1*h),    P(a-w*2, h, a-w*3), P(a-w*2, h, a-w*2), P(a-w*2, h, a-w), P(a-w*2, h, a), P(a-w*2, a, h), P(a-w*2, a-w, h), P(a-w*2, a-w*2, h), P(a-w*2, a-w*3, h)},
			{P(a-w, a-w*3, -1*h),   P(a-w, a-w*2, -1*h),   P(a-w, a-w, -1*h),     P(a-w, a, -1*h),      P(a-w, h, a-w*3),   P(a-w, h, a-w*2),   P(a-w, h, a-w),   P(a-w, h, a),   P(a-w, a, h),   P(a-w, a-w, h),   P(a-w, a-w*2, h),   P(a-w, a-w*3, h)},
			{P(a, a-w*3, -1*h),     P(a, a-w*2, -1*h),     P(a, a-w, -1*h),       P(a, a, -1*h),        P(a, h, a-w*3),     P(a, h, a-w*2),     P(a, h, a-w),     P(a, h, a),     P(a, a, h),     P(a, a-w, h),     P(a, a-w*2, h),     P(a, a-w*3, h)},
			//px
			{P(0, 0, 0), P(0, 0, 0), P(0, 0, 0), P(0, 0, 0), P(h, a, a-w*3),     P(h, a, a-w*2),     P(h, a, a-w),     P(h, a, a),     P(0, 0, 0), P(0, 0, 0), P(0, 0, 0), P(0, 0, 0)},
			{P(0, 0, 0), P(0, 0, 0), P(0, 0, 0), P(0, 0, 0), P(h, a-w, a-w*3),   P(h, a-w, a-w*2),   P(h, a-w, a-w),   P(h, a-w, a),   P(0, 0, 0), P(0, 0, 0), P(0, 0, 0), P(0, 0, 0)},
			{P(0, 0, 0), P(0, 0, 0), P(0, 0, 0), P(0, 0, 0), P(h, a-w*2, a-w*3), P(h, a-w*2, a-w*2), P(h, a-w*2, a-w), P(h, a-w*2, a), P(0, 0, 0), P(0, 0, 0), P(0, 0, 0), P(0, 0, 0)},
			{P(0, 0, 0), P(0, 0, 0), P(0, 0, 0), P(0, 0, 0), P(h, a-w*3, a-w*3), P(h, a-w*3, a-w*2), P(h, a-w*3, a-w), P(h, a-w*3, a), P(0, 0, 0), P(0, 0, 0), P(0, 0, 0), P(0, 0, 0)}
		};

		return position_matrix;
	}


	/**
	 * PはPositionの略。Vector3を返す
	 *
	Vector3 P(float x, float y, float z)
	{
		return new Vector3 (x, y, z);
	}
	*/

	/**
	 * ある一定の誤差の範囲でfloat値が等しいかを比較する関数
	 */
	bool ErrorForgiveEqual(float sample1, float sample2, float forgive_error)
	{
		if (sample1 >= sample2 - forgive_error && sample1 <= sample2 + forgive_error) {
			return true;
		} else {
			return false;
		}
	}

	void Update () 
	{
		SpinCircuit ();
		JudgeCircuit ();
	}

	/**
	 * spin_numberの値を1上げます
	 */
	public void SpinUp()
	{
		if (spin_number == 3) {
			spin_number = 0;
		} else {
			spin_number++;
		}
	}

	/**
	 * 回転した状態でどの方向に回路が続いているかを変更する
	 */
	private void SpinCircuit()
	{
		List<bool>		circuit_list = new List<bool> () {first_circuit_m_x, first_circuit_m_z, first_circuit_p_x, first_circuit_p_z};
		for(int i = 0; i < spin_number; i++){
			circuit_list.Add (circuit_list [0]);
			circuit_list.RemoveAt (0);
		}
		circuit_p_z = circuit_list [3];
		circuit_p_x = circuit_list [2];
		circuit_m_z = circuit_list [1];
		circuit_m_x = circuit_list [0];
	}

	/**
	 * 回路の両端が繋がっているかを判断し、繋がっているとされるオブジェクトをリストに入れる
	 */
	private void JudgeCircuit()
	{
		chained_object.Clear ();

		if (circuit_m_x) {
			if (ObjMX != null) {
				CircuitStatus	obj_status = ObjMX.GetComponent<CircuitStatus> ();
				if (obj_status.ChainableCheck(m_x_chain)) {
					chained_object.Add (ObjMX);
				}
			}
		}


		if (circuit_m_z) {
			if (ObjMZ != null) {
				CircuitStatus	obj_status = ObjMZ.GetComponent<CircuitStatus> ();
				if (obj_status.ChainableCheck(m_z_chain)) {
					chained_object.Add (ObjMZ);
				} 
			}
		}


		if (circuit_p_x) {
			if (ObjPX != null) {
				CircuitStatus	obj_status = ObjPX.GetComponent<CircuitStatus> ();
				if (obj_status.ChainableCheck(p_x_chain)) {
					chained_object.Add (ObjPX);
				} 
			}
		}


		if (circuit_p_z) {
			if (ObjPZ != null) {
				CircuitStatus	obj_status = ObjPZ.GetComponent<CircuitStatus> ();
				if (obj_status.ChainableCheck(p_z_chain)) {
					chained_object.Add (ObjPZ);
				} 
			} 
		}
	}
}
