using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using ZombieCiv.Items;
using ZombieCiv.UI;

namespace ZombieCiv.Controllers.AI
{
    public class AI : MonoBehaviour
    {
        [Header("Stats")]
        public int Strength;
        public int Agility;
        public int Intellegence;
        public int Dexterity;
        public int Wisdom;
        public int Healthiness;
        public int Observationalism;
        [Space(1)]
        public int Health = 0;
        public int MaximumHealth;
        [Space()]
        [Header("Pathfinding")]
        public Transform target;
        public NavMeshAgent agent;
        [Space()]
        [Header("Job")]
        public WorkerType givenJob;
        public float AbilityToDoJob;
        private bool initialized = false;
        public bool HomeLess;
        public WorkerTask task;
        public Animator anim;
        public AnimationClip[] animas;
        private float actiontime;
        private float O_actiontime;
        private WaitForSeconds wfs;
        private int animIndicator;
        public GameObject Face;
        private float Distance = float.MaxValue;
        private float DistanceToTarget;
        public EnemySpawning es;
        public GameObject enemy;
        public LineRenderer lazer;
        public AudioSource gunshot;
        public Transform gunend;
        public int gunDamage;
        private WaitForSeconds wfs1 = new WaitForSeconds(0.7f);
        public MapGen map;
        // Use this for initialization
        void Start()
        {
            actiontime = 5;
            O_actiontime = actiontime;
            //wfs = new WaitForSeconds (animas[0].length);
            map = GameObject.FindGameObjectWithTag("Scripts").GetComponent<MapGen>();
            es = GameObject.FindGameObjectWithTag("enemymanager").GetComponent<EnemySpawning>();
        }

        // Update is called once per frame
        void Update()
        {
            if (actiontime > 0)
            {
                actiontime -= Time.deltaTime;
            }
            if (actiontime < 0)
            {
                actiontime = 0;
            }
            try
            {
                DistanceToTarget = Vector3.Distance(this.transform.position, target.position);
            }
            catch
            {
                target = this.gameObject.transform;
                DistanceToTarget = Vector3.Distance(this.transform.position, target.position);
            }
            //Debug.Log (DistanceToTarget);
            if (initialized && target != null && !(DistanceToTarget <= 5))
            {
                agent.speed = Agility;
                anim.SetFloat("Speed", 1);
                agent.destination = target.position;
            }
            else
            {
                anim.SetFloat("Speed", 0);
                agent.destination = this.transform.position;
            }
            if (givenJob == WorkerType.LumberJack && task == WorkerTask.ChoppingWood && !HomeLess)
            {
                //Debug.Log ("Set Chop completed");
                Chop();
            }
            if (givenJob == WorkerType.Soldier && task == WorkerTask.Attack && !HomeLess)
            {
                Shoot();
            }
        }
        public void setJob(WorkerType workerType, WorkerTask workerTask)
        {
            if (workerType == WorkerType.UNCHANGED)
            {
            }
            else if (workerType == WorkerType.UNASSIGNED)
            {
                anim.SetBool("Chopper", false);
                anim.SetBool("Soldier", false);
            }
            else
            {
                givenJob = workerType;
                if (givenJob == WorkerType.LumberJack)
                {
                    O_actiontime = animas[0].length;
                    wfs = new WaitForSeconds(animas[0].length);
                    anim.SetBool("Chopper", true);
                }
                else if (givenJob == WorkerType.Soldier)
                {
                    O_actiontime = animas[1].length;
                    wfs = new WaitForSeconds(animas[1].length);
                    anim.SetBool("Soldier", true);
                }
            }
            task = workerTask;
        }
        public void init(int strength, int agility, int intellegence, int dexteritiy, int wisdom, int healthiness, int observationalism, bool homeless)
        {
            if (strength > 20)
                strength = 20;
            Strength = strength;
            if (agility > 20)
                agility = 20;
            Agility = agility;
            if (intellegence > 20)
                intellegence = 20;
            Intellegence = intellegence;
            if (dexteritiy > 20)
                dexteritiy = 20;
            Dexterity = dexteritiy;
            if (wisdom > 20)
                wisdom = 20;
            Wisdom = wisdom;
            if (healthiness > 20)
                healthiness = 20;
            Healthiness = healthiness;
            if (observationalism > 20)
                observationalism = 20;
            Observationalism = observationalism;
            givenJob = WorkerType.UNASSIGNED;
            initialized = true;
            MaximumHealth = Healthiness * 10;
            ChangeHealth(MaximumHealth);
            HomeLess = homeless;
        }
        public void ChangeHealth(int healAMT)
        {
            Health += healAMT;
        }
        public void SetEnemy()
        {
            float range = 50;
            GameObject tempTarget = null;
            foreach (GameObject enemy1 in es.Zombies)
            {
                float Distance = Vector3.Distance(this.transform.position, enemy1.transform.position);
                if (Distance <= 50)
                {
                    if (Distance < range)
                    {
                        range = Distance;
                        tempTarget = enemy1;
                    }
                }
            }
            target = tempTarget.transform;
            enemy = tempTarget;
        }
        public void Shoot()
        {
            if (enemy != null)
            {
                if (actiontime <= 0)
                {
                    RaycastHit hit;
                    lazer.SetPosition(0, gunend.position);
                    StartCoroutine(Fire());
                    if (Physics.Raycast(Face.transform.position, Face.transform.forward, out hit, 50))
                    {
                        lazer.SetPosition(1, hit.point);
                        if (hit.collider.gameObject.tag == "enemy")
                        {
                            hit.collider.GetComponent<EnemyController>().ChangeHealth(gunDamage);
                        }
                    }
                    else
                    {
                        lazer.SetPosition(1, (Face.transform.forward * 50));
                    }
                }
            }
            else
            {
                SetEnemy();
            }
        }
        public IEnumerator Fire()
        {
            anim.SetTrigger("fire");
            gunshot.Play();
            lazer.enabled = true;
            yield return wfs1;
            lazer.enabled = false;
        }
        public void SetChop()
        {
            foreach (GameObject tree in map.resources)
            {
                float Distance1 = Vector3.Distance(this.transform.position, tree.transform.position);
                if (Distance1 < Distance)
                {
                    Distance = Distance1;
                    target = tree.transform;
                    //Debug.Log ("New Target: " + target.name);
                }
            }
        }
        public void Chop()
        {
            if (target == null)
            {
                Distance = float.MaxValue;
                SetChop();
            }
            Resource TreeResource = target.GetComponent<Resource>();
            if (TreeResource != null)
            {
                //SDebug.Log ("Chop is running " + TreeResource.gameObject.name);
                if (target != null && TreeResource != null && TreeResource.ResourceType == Resource.Type.Wood)
                {
                    float d = Vector3.Distance(this.gameObject.transform.position, target.position);
                    if (d < 5)
                    {
                        if (actiontime == 0)
                        {
                            anim.SetTrigger("Choppin");
                            if (TreeResource.Health - Strength * 10 <= 0)
                            {
                                TreeResource.Mine(Strength * 10, GameObject.FindGameObjectWithTag("pointcounter").GetComponent<PointSystem>(), false);
                                Distance = float.MaxValue;
                                gameObject.transform.LookAt(target);
                                SetChop();
                            }
                            else
                            {
                                TreeResource.Mine(Strength * 10, GameObject.FindGameObjectWithTag("pointcounter").GetComponent<PointSystem>(), false);
                            }
                            actiontime = O_actiontime;
                        }
                    }
                }
            }
            else
            {
                Distance = float.MaxValue;
                SetChop();
            }
        }
        public enum WorkerType
        {
            UNASSIGNED,
            UNCHANGED,
            Builder,
            Soldier,
            Farmer,
            Blacksmith,
            LumberJack,
            Miner,
            Scientist,
        }
        public enum WorkerTask
        {
            IDLE,
            ChoppingWood,
            CarryingWoodHome,
            CarryingWoodToConstruction,
            CarryingWoodToFactory,
            MiningStone,
            CarryingStoneHome,
            CarryingStoneToConstruction,
            CarryingStoneToFactory,
            Building,
            FollowPlayer,
            GetInPosition,
            Defend,
            Attack,
            AttackWithGroup,
            PlantCrops,
            HarvestCrops,
            DeliverCrops,
            SmeltMetal,
            MakeTool,
            DeliverMetal,
            DeliverTools,
            Research,
        }
    }
}
