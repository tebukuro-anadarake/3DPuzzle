using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundObjectsSetter : MonoBehaviour 
{
	GameData		game_data;
	int				confirmed_status;
	[SerializeField]
	int				objects_number;
	[SerializeField]
	GameObject[]	objects;
	GameObject		instanced_object;

	void Start () 
	{
		game_data = GameObject.Find ("DontDestroyObject").GetComponent<GameData> ();
		confirmed_status = game_data.DataGetterInt (GameData.DataType.BackGroundObjects);
		PutObjects ();
	}
	
	void Update () 
	{
		if (game_data.DataGetterInt (GameData.DataType.BackGroundObjects) != confirmed_status) {
			confirmed_status = game_data.DataGetterInt (GameData.DataType.BackGroundObjects);
			PutDestroyObjects ();
		}
	}

	/**
	 * BackGroundObjectsを置く
	 */
	void PutObjects()
	{
		if (confirmed_status == 1) {
			instanced_object = GameObject.Instantiate (objects [objects_number]);
		}
	}

	/**
	 * BackGroundObjectsが置かれていたら消して、置かれていなかったら置く
	 */
	void PutDestroyObjects()
	{
		if (confirmed_status == 1) {
			instanced_object = GameObject.Instantiate (objects [objects_number]);
		} else if (confirmed_status == 0) {
			GameObject.Destroy (instanced_object);
		}
	}
}
