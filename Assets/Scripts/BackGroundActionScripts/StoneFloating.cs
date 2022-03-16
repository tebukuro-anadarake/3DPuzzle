using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneFloating : MonoBehaviour 
{
	Vector3		base_position;
	float		position_count;
	[SerializeField]
	float		move_max;
	[SerializeField]
	float		speed;
	void Start () 
	{
		base_position = transform.localPosition;
	}
	
	void Update () 
	{
		position_count += Time.deltaTime * speed;
		transform.localPosition = base_position + new Vector3(0, 1, 0) * move_max * Mathf.Sin (position_count);
	}
}
