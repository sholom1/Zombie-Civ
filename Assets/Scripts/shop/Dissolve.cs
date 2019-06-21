using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissolve : MonoBehaviour {
	private float Timer;
	private bool dissolving;
	public void dissolve(float time){
		Timer = time;
		dissolving = true;
		if (gameObject.tag != "Tree") {
			MeshCollider col = gameObject.AddComponent<MeshCollider> ();
			col.convex = true;
			gameObject.AddComponent<Rigidbody> ();
		}
	}
	void Update(){
		if (dissolving) {
			Timer -= Time.deltaTime;
			if (Timer <= 0) {
				Destroy (gameObject);
			}
		}
	}
}
