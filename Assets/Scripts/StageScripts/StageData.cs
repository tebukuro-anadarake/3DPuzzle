﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageData : MonoBehaviour 
{
	public List<int>[,] circuits{ get; protected set; }

	/**
	 * ある回路の様子を返す(0:void, 1:straight, 2:curve)
	 */
	protected List<int> P(int circuit_type, int spin_number)
	{
		return new List<int> (){ circuit_type, spin_number };
	}

	/**
	 * ステージの回路の配置をここで指定
	 */
	public virtual void StageDataSetter(){}

	/*
	こういう風に書く
	public override void StageDataSetter ()
	{
		circuits = new List<int>[,] {
			{ P(0, 0), P(0, 0), P(0, 0), P(0, 0),   P(0, 0), P(0, 0), P(0, 0), P(0, 0),   P(0, 0), P(0, 0), P(0, 0), P(0, 0) },
			{ P(0, 0), P(0, 0), P(0, 0), P(0, 0),   P(0, 0), P(0, 0), P(0, 0), P(0, 0),   P(0, 0), P(0, 0), P(0, 0), P(0, 0) },
			{ P(0, 0), P(0, 0), P(0, 0), P(0, 0),   P(0, 0), P(0, 0), P(0, 0), P(0, 0),   P(0, 0), P(0, 0), P(0, 0), P(0, 0) },
			{ P(0, 0), P(0, 0), P(0, 0), P(0, 0),   P(0, 0), P(0, 0), P(0, 0), P(0, 0),   P(0, 0), P(0, 0), P(0, 0), P(0, 0) },

			{ P(0, 0), P(0, 0), P(0, 0), P(0, 0),   P(0, 0), P(0, 0), P(0, 0), P(0, 0),   P(0, 0), P(0, 0), P(0, 0), P(0, 0) },
			{ P(0, 0), P(0, 0), P(0, 0), P(0, 0),   P(0, 0), P(0, 0), P(0, 0), P(0, 0),   P(0, 0), P(0, 0), P(0, 0), P(0, 0) },
			{ P(0, 0), P(0, 0), P(0, 0), P(0, 0),   P(0, 0), P(0, 0), P(0, 0), P(0, 0),   P(0, 0), P(0, 0), P(0, 0), P(0, 0) },
			{ P(0, 0), P(0, 0), P(0, 0), P(0, 0),   P(0, 0), P(0, 0), P(0, 0), P(0, 0),   P(0, 0), P(0, 0), P(0, 0), P(0, 0) },

			{ P(0, 0), P(0, 0), P(0, 0), P(0, 0),   P(0, 0), P(0, 0), P(0, 0), P(0, 0),   P(0, 0), P(0, 0), P(0, 0), P(0, 0) },
			{ P(0, 0), P(0, 0), P(0, 0), P(0, 0),   P(0, 0), P(0, 0), P(0, 0), P(0, 0),   P(0, 0), P(0, 0), P(0, 0), P(0, 0) },
			{ P(0, 0), P(0, 0), P(0, 0), P(0, 0),   P(0, 0), P(0, 0), P(0, 0), P(0, 0),   P(0, 0), P(0, 0), P(0, 0), P(0, 0) },
			{ P(0, 0), P(0, 0), P(0, 0), P(0, 0),   P(0, 0), P(0, 0), P(0, 0), P(0, 0),   P(0, 0), P(0, 0), P(0, 0), P(0, 0) },

			{ P(0, 0), P(0, 0), P(0, 0), P(0, 0),   P(0, 0), P(0, 0), P(0, 0), P(0, 0),   P(0, 0), P(0, 0), P(0, 0), P(0, 0) },
			{ P(0, 0), P(0, 0), P(0, 0), P(0, 0),   P(0, 0), P(0, 0), P(0, 0), P(0, 0),   P(0, 0), P(0, 0), P(0, 0), P(0, 0) },
			{ P(0, 0), P(0, 0), P(0, 0), P(0, 0),   P(0, 0), P(0, 0), P(0, 0), P(0, 0),   P(0, 0), P(0, 0), P(0, 0), P(0, 0) },
			{ P(0, 0), P(0, 0), P(0, 0), P(0, 0),   P(0, 0), P(0, 0), P(0, 0), P(0, 0),   P(0, 0), P(0, 0), P(0, 0), P(0, 0) }
		};
	}
	 */
}
