using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using ZombieCiv.Construction;

namespace ZombieCiv.Tools
{
    public class CityManager : MonoBehaviour
    {
        public int HouseNumber;
        public int SmithyNumber;
        public int FarmNumber;
        public int Population;
        public int MaxPopulation;
        public float SpawnTime;
        private float O_SpawnTime;
        public GameObject NPC;
        public List<GameObject> Citizens = new List<GameObject>();
        public List<Building> buildings = new List<Building>();
        public List<GameObject> houses = new List<GameObject>();
        public List<GameObject> Smithys = new List<GameObject>();
        public List<GameObject> Farms = new List<GameObject>();
        public List<GameObject> Towers = new List<GameObject>();
        public List<GameObject> ResourceStorage = new List<GameObject>();
        public GameObject TownCenter;
        public EnemySpawning es;

        #region singleton
        public static CityManager instance;
        private void Awake()
        {
            if (instance != null)
                Destroy(this);
            else
                instance = this;
        }
        #endregion

        // Use this for initialization
        void Start()
        {
            O_SpawnTime = SpawnTime;
        }

        // Update is called once per frame
        void Update()
        {
            if (Timer())
            {
                SpawnPeople();
            }
        }
        public void OnBuild(GameObject building, Building.BuildingType buildingtype)
        {
            Debug.Log(buildingtype.ToString());
            if (buildingtype == Building.BuildingType.House)
            {
                House house = building.GetComponent<House>();
                MaxPopulation += house.Room;
                es.MaxEnemies++;
                buildings.Add(house);
                HouseNumber++;
            }
            if (buildingtype == Building.BuildingType.Post)
            {
                Debug.Log("WTF4");
                Towers.Add(building);
            }
            if (buildingtype == Building.BuildingType.TownCenter)
            {
                TownCenter = building;
            }
            if (buildingtype == Building.BuildingType.ResourceStorage)
            {
                buildings.Add(building.GetComponent<StorageArea>());
            }
        }
        public void onDestroy(GameObject building, Building.BuildingType buildingtype)
        {
            if (buildingtype == Building.BuildingType.House)
            {
                MaxPopulation -= building.GetComponent<House>().Room;
                es.MaxEnemies--;
                houses.Remove(building);
            }
            if (buildingtype == Building.BuildingType.Post)
            {
                Towers.Remove(building);
            }
        }
        public bool Timer()
        {
            if (SpawnTime <= 0)
            {
                SpawnTime = O_SpawnTime;
                return true;
            }
            if (SpawnTime > 0)
            {
                SpawnTime -= Time.deltaTime;
                return false;
            }
            return false;
        }
        public void SpawnPeople()
        {
            if (TownCenter != null)
            {
                foreach (House house in buildings)
                {
                    if (house.UsedRoom < house.Room)
                    {
                        Debug.Log(house.position.ToString());
                        Debug.Log(house.SpawnPoint.localPosition.ToString());
                        GameObject NewGuy = Instantiate(NPC, house.SpawnPoint.position, house.SpawnPoint.rotation);
                        Debug.Log(NewGuy.transform.position.ToString());
                        //NewGuy.transform.Translate (TownCenter.transform.position);
                        Debug.Log(NewGuy.transform.position.ToString());
                        NewGuy.GetComponent<AI>().init(10, 10, 10, 10, 10, 10, 10, false);
                        NewGuy.GetComponent<AI>().setJob(AI.WorkerType.LumberJack, AI.WorkerTask.ChoppingWood);
                        Population++;
                        house.MoveIn(NewGuy);
                        Citizens.Add(NewGuy);
                        return;
                    }
                }
            }
        }
        public GameObject[] getAllBuildingsAsGameObjects()
        {
            List<GameObject> GameObjectsToReturn = new List<GameObject>();
            foreach (Building building in buildings)
            {
                GameObjectsToReturn.Add(building.gameObject);
            }
            return GameObjectsToReturn.ToArray();
        }
        public House[] GetHouses()
        {
            List<House> buildings_to_return = new List<House>();
            foreach (House house in buildings)
            {
                buildings_to_return.Add(house);
            }
            return buildings_to_return.ToArray();
        }
        public StorageArea[] GetStorageAreas()
        {
            List<StorageArea> buildings_to_return = new List<StorageArea>();
            foreach (Building storageArea in buildings)
            {
                if (storageArea is StorageArea)
                {
                    StorageArea Storagearea = storageArea as StorageArea;
                    Debug.Log(Storagearea.inventory.Space);
                    buildings_to_return.Add(Storagearea);
                }
            }
            return buildings_to_return.ToArray();
        }
        public Inventory[] GetAllInventories()
        {
            Queue<Inventory> inventories = new Queue<Inventory>();
            foreach (StorageArea storageArea in GetStorageAreas())
                inventories.Enqueue(storageArea.inventory);
            return inventories.ToArray();
        }
    }
}
