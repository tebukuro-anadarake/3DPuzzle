using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionMatrixSystem : MonoBehaviour 
{
	/**
	 * 16×12 Position行列作成。引数は(max_position, square_width, high_position)
	 */
	public Vector3[,] PositionMatrix(float a, float w, float h)
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
	 */
	Vector3 P(float x, float y, float z)
	{
		return new Vector3 (x, y, z);
	}
}
