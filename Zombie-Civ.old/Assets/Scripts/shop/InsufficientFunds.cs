using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsufficientFunds : MonoBehaviour {
	public float timer;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (timer > 0) {
			timer -= Time.deltaTime;
		}
		if (timer < 0) {
			timer = 0;
		}
		if (timer == 0)
			timeEnd ();
	}
	void timeEnd(){
		gameObject.SetActive (false);
	}
}
