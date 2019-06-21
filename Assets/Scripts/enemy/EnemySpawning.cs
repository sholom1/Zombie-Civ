using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawning : MonoBehaviour {

	public GameObject enemy;
	public PlayerHealth playerHealth;
	public float spawnTimes = 3f;
	public Transform[] spawnPoints;
	public static int EnemyCount;
	public int MaxEnemies;
	public bool CanZombiesDoStuff = true;
	public List<GameObject> Zombies = new List<GameObject> ();

	// Use this for initialization
	void Update(){
		if (CanZombiesDoStuff) {
			Invoke ("spawn", spawnTimes);
		}
	}
	void spawn (){
		//if there are less enemys than maximum enemys
		if (EnemyCount < MaxEnemies) {
			//then spawn one
			int spawnPointIndex = Random.Range (0, spawnPoints.Length);
			Vector3 randomNearSpawn = new Vector3 (spawnPoints [spawnPointIndex].position.x + Random.Range (-5, 5), spawnPoints [spawnPointIndex].position.y, spawnPoints [spawnPointIndex].position.z + Random.Range (-5, 5));
			GameObject NewEnemy = GameObject.Instantiate (enemy, randomNearSpawn, spawnPoints[spawnPointIndex].transform.rotation, transform);
            AddEnemy(NewEnemy);
		} else {
			return;
		}
	}
    public void AddEnemy(GameObject EnemyToAdd)
    {
        Zombies.Add(EnemyToAdd);
        EnemyCount++;
    }
    public void RemoveEnemy(GameObject EnemyToAdd)
    {
        Zombies.Remove(EnemyToAdd);
        EnemyCount--;
    }
}
