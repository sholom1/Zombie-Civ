using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : Building{
	public int Room;
	public int UsedRoom;
	public List<GameObject> Tenants = new List<GameObject> ();
	public Transform SpawnPoint;
	public Vector3 position;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		position = transform.position;
	}
	public void MoveIn(GameObject NewTenant){
		Tenants.Add (NewTenant);
	}
	public GameObject Spawn(GameObject what){
		return GameObject.Instantiate (what, position, Quaternion.Euler (0, 0, 0));
	}
}
