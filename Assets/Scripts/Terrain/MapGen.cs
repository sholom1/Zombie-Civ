using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGen : MonoBehaviour {
	public List<GameObject> Tiles;
	public List<GameObject> Resources;
	public List<GameObject> resources;
	public int X;
	public int Y;
	public string seedString;
	public int seed;
	public GameObject map;
	public bool genmap;
	/*For every factor(f) in map size a biome will be spawned
	 * Each Biome will:
	 * Consist completely of one Material
	 * Be square
	 */
	void Start () {
		if (genmap) {
			computeSeed ();
			GenerateLand ();
			GenerateResources ();
		}
		GameObject[] trees = GameObject.FindGameObjectsWithTag ("Tree");
		for (int i = 0; i < trees.Length; i++) {
			resources.Add (trees [i]);
		}
	}
	void computeSeed (){
		seed = seedString.GetHashCode ();
		if (seed < 0) {
			seed*=-1;
		}
		Debug.Log (seed);
	}
	void GenerateLand(){
		for (float x = .5f; x < X++; x+=.5f) {
			for (float y = .5f; y < Y++; y+=.5f) {
				Instantiate (Tiles [0], new Vector3(x, 0, y), new Quaternion(0,0,0,0), map.transform);
			}
		}
	}
	void GenerateResources(){
	}
}
