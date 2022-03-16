﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage27Data : StageData 
{
	public override void StageDataSetter ()
	{
		circuits = new List<int>[,] {
			{ P(0, 0), P(0, 0), P(0, 0), P(0, 0),   P(2, 1), P(2, 0), P(2, 1), P(2, 0),   P(0, 0), P(0, 0), P(0, 0), P(0, 0) },
			{ P(0, 0), P(0, 0), P(0, 0), P(0, 0),   P(1, 1), P(2, 3), P(2, 1), P(1, 1),   P(0, 0), P(0, 0), P(0, 0), P(0, 0) },
			{ P(0, 0), P(0, 0), P(0, 0), P(0, 0),   P(1, 1), P(1, 3), P(1, 1), P(1, 1),   P(0, 0), P(0, 0), P(0, 0), P(0, 0) },
			{ P(0, 0), P(0, 0), P(0, 0), P(0, 0),   P(2, 1), P(2, 0), P(2, 1), P(2, 1),   P(0, 0), P(0, 0), P(0, 0), P(0, 0) },

			{ P(0, 0), P(0, 0), P(0, 0), P(0, 0),   P(1, 0), P(2, 3), P(2, 1), P(1, 1),   P(0, 0), P(0, 0), P(0, 0), P(0, 0) },
			{ P(0, 0), P(0, 0), P(0, 0), P(0, 0),   P(1, 1), P(2, 3), P(2, 1), P(1, 2),   P(0, 0), P(0, 0), P(0, 0), P(0, 0) },
			{ P(0, 0), P(0, 0), P(0, 0), P(0, 0),   P(1, 1), P(2, 3), P(2, 1), P(1, 1),   P(0, 0), P(0, 0), P(0, 0), P(0, 0) },
			{ P(0, 0), P(0, 0), P(0, 0), P(0, 0),   P(1, 0), P(2, 2), P(2, 1), P(1, 0),   P(0, 0), P(0, 0), P(0, 0), P(0, 0) },

			{ P(1, 1), P(1, 0), P(1, 1), P(1, 0),   P(2, 1), P(2, 1), P(2, 2), P(2, 3),   P(1, 1), P(1, 0), P(1, 1), P(1, 0) },
			{ P(1, 1), P(1, 0), P(1, 1), P(1, 0),   P(1, 1), P(1, 1), P(1, 2), P(1, 1),   P(1, 1), P(1, 0), P(1, 1), P(1, 0) },
			{ P(1, 1), P(1, 0), P(1, 1), P(1, 0),   P(1, 0), P(2, 0), P(2, 0), P(1, 1),   P(1, 1), P(1, 0), P(1, 1), P(1, 0) },
			{ P(1, 1), P(1, 0), P(1, 1), P(1, 0),   P(2, 0), P(2, 0), P(2, 3), P(2, 0),   P(1, 1), P(1, 0), P(1, 1), P(1, 0) },

			{ P(0, 0), P(0, 0), P(0, 0), P(0, 0),   P(1, 1), P(2, 3), P(2, 3), P(1, 0),   P(0, 0), P(0, 0), P(0, 0), P(0, 0) },
			{ P(0, 0), P(0, 0), P(0, 0), P(0, 0),   P(1, 0), P(2, 3), P(2, 3), P(1, 1),   P(0, 0), P(0, 0), P(0, 0), P(0, 0) },
			{ P(0, 0), P(0, 0), P(0, 0), P(0, 0),   P(1, 0), P(2, 3), P(2, 2), P(1, 0),   P(0, 0), P(0, 0), P(0, 0), P(0, 0) },
			{ P(0, 0), P(0, 0), P(0, 0), P(0, 0),   P(1, 1), P(2, 0), P(2, 2), P(1, 1),   P(0, 0), P(0, 0), P(0, 0), P(0, 0) }
		};
	}
}
