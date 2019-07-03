using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Despawnable : MonoBehaviour {
	private float timer;
	private bool despawn = false;
	public void Despawn(float time){
		timer = time;
		despawn = true;
		if (gameObject.tag != "Tree") {
			MeshCollider col = gameObject.AddComponent<MeshCollider> ();
			col.convex = true;
			gameObject.AddComponent<Rigidbody> ();
		}
	}
	void Update(){
		if (despawn) {
			timer -= Time.deltaTime;
			if (timer <= 0) {
				Destroy (gameObject);
			}
		}
	}
}
