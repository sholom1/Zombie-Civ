using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour {
	public Type ResourceType;
	public int Amount;
	public float Health = 100f;
	public GameObject Prefab;
	public MapGen map;
	public List<GameObject> Debris;
	void Start(){
        map.resources.Add(gameObject);
	}
	public void Mine(float damage, PointSystem ps, bool isPlayer){
		Health -= damage;
		if (Health <= 0) {
			for (int i = 0; i < Amount; i++) {
				Instantiate (Prefab, new Vector3 (this.transform.position.x, this.transform.position.y + 2, this.transform.position.z), Prefab.transform.rotation);
			}
			foreach (var debris in Debris) {
				Dissolve thingtodissolve = debris.AddComponent<Dissolve> ();
				thingtodissolve.dissolve (20);
			}
			map.resources.Remove (this.gameObject);
			Destroy (this.gameObject);
		}
	}
	
	public enum Type{
		Stone,
		ScrapMetal,
		Wood,
		Credits
	}
}
