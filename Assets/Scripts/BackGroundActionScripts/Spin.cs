using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour 
{
	[SerializeField]
	float	x_speed;
	[SerializeField]
	float	y_speed;
	[SerializeField]
	float	z_speed;

	void Update () 
	{
		transform.Rotate (new Vector3(x_speed, y_speed, z_speed) * Time.deltaTime);
	}
}
