using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rayveiwer : MonoBehaviour {

	public float weaponrange = 50f;

	private Camera fpscam;

	void Start () {
		fpscam = GetComponentInParent<Camera> ();
	}


	void Update () {
		Vector3 lineorigin = fpscam.ViewportToWorldPoint (new Vector3 (0.5f, 0.5f, 0));
		Debug.DrawRay (lineorigin, fpscam.transform.forward * weaponrange, Color.green);
	}
}
