using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircuitShiner : MonoBehaviour 
{
	CircuitStatus	circuit_status;
	Material		circuit_material;
	Material		base_material;

	void Start () 
	{
		circuit_status = transform.parent.transform.parent.gameObject.GetComponent<CircuitStatus> ();
		circuit_material = gameObject.GetComponent<Renderer> ().material;
		base_material = transform.parent.gameObject.GetComponent<Renderer> ().material;
	}
	
	void Update () 
	{
		if (circuit_status.isZeroNumberChained) {
			circuit_material.EnableKeyword ("_EMISSION");
			circuit_material.SetColor ("_EmissionColor", new Color (0.3f, 0.7f, 1));
			base_material.EnableKeyword ("_EMISSION");
			base_material.SetColor ("_EmissionColor", new Color (0.3f, 0.7f, 1));
		} else {
			circuit_material.EnableKeyword ("_EMISSION");
			circuit_material.SetColor ("_EmissionColor", new Color (1, 0.3f, 0.3f));
			base_material.EnableKeyword ("_EMISSION");
			base_material.SetColor ("_EmissionColor", new Color (1, 0.3f, 0.3f));
		}
	}
}
