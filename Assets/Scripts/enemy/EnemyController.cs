using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;
using ZombieCiv.Construction;
using ZombieCiv.UI;

namespace ZombieCiv.Controllers.AI
{
    public class EnemyController : MonoBehaviour, IPausable
    {
        [Header("Enemy Statistics")]
        public float currentHealth = 1;
        public int amountOfCashToAdd;
        public float maximumHealth = 100;
        public float rotatespeed = 1f;
        public float movementspeed = 1f;
        public float AttackSpeed;
        private float coolDowntimer;
        public float range = 0;
        public int damage = -10;
        public bool atTarget;
        [Space(1)]
        public EnemySpawning enemySpawner;
        [Space(1)]
        [Header("Targeting System")]
        public Transform target;
        public GameObject targetconfirmed;
        public PlayerHealth targetHealth;
        public float AttackDistance;
        private float healTime = 1;
        private bool navmeshset = false;
        public NavMeshAgent agent;           // the navmesh agent required for the path finding
        public ThirdPersonCharacter character;
        // Update is called once per frame
        private void Start()
        {
            GameObject EnemyManagerGameObject = GameObject.FindGameObjectWithTag("enemymanager");
            enemySpawner = EnemyManagerGameObject.GetComponent<EnemySpawning>();

            currentHealth = maximumHealth;
            coolDowntimer = AttackSpeed;
            agent.updateRotation = false;
            agent.updatePosition = true;
        }
        void Update()
        {
            heal(1, false);
            if (target != null)
                agent.SetDestination(target.position);
            Target();
            Move();
            Attack();
        }
        //Changes health also handles death
        public void ChangeHealth(float health)
        {
            currentHealth += health;
            if (currentHealth <= 0)
            {
                PointSystem.instance.AddCash(amountOfCashToAdd, Resource.Type.Credits);
                enemySpawner.RemoveEnemy(gameObject);
                Destroy(gameObject);
            }
            if (currentHealth > maximumHealth)
            {
                currentHealth = maximumHealth;
            }
        }
        /// <summary>
        /// Move me. but first check if i can move.
        /// </summary>
        private void Move()
        {
            //if we have a target and zombies are allowed to do stuff and our target has health then
            if (target != null && enemySpawner.CanZombiesDoStuff && targetHealth.currentHealth > 0)
            {
                //first calculate attack distance
                AttackDistance = targetHealth.AttackableDistance;
                if (AttackDistance > range)
                    AttackDistance = range;
                //if we are to far to attack then move
                if (agent.remainingDistance > AttackDistance)
                    character.Move(agent.desiredVelocity, false, false);
                else
                    character.Move(Vector3.zero, false, false);
            }
        }
        /// <summary>
        /// First see if we can reach a citizen if we cant then Sort through things with health to find closest thing to attack.
        /// </summary>
        public void Target()
        {
            GameObject[] citizens = CityManager.instance.Citizens.ToArray();
            float lastdistance = float.MaxValue;
            foreach (GameObject citizen in citizens)
            {
                agent.SetDestination(citizen.transform.position);
                if (agent.hasPath && agent.remainingDistance < lastdistance)
                {
                    lastdistance = agent.remainingDistance;
                    target = citizen.transform;
                    targetconfirmed = citizen;
                    targetHealth = citizen.GetComponent<PlayerHealth>();
                    AttackDistance = targetHealth.AttackableDistance;
                }
                if (target != null)
                    agent.SetDestination(target.position);
            }
            if (agent.hasPath == false)
            {
                GameObject[] buildings = CityManager.instance.getAllBuildingsAsGameObjects();
                lastdistance = float.MaxValue;
                foreach (GameObject building in buildings)
                {
                    agent.SetDestination(building.transform.position);
                    if (agent.hasPath && agent.remainingDistance < lastdistance)
                    {
                        lastdistance = agent.remainingDistance;
                        target = building.transform;
                        targetconfirmed = building;
                        targetHealth = building.GetComponent<PlayerHealth>();
                        AttackDistance = targetHealth.AttackableDistance;
                    }
                    if (target != null)
                        agent.SetDestination(target.position);
                }
            }
        }
        /// <summary>
        /// Attack the target. but first check if i can.
        /// </summary>
        void Attack()
        {
            if (CanAttack())
            {
                targetHealth.ChangeHealth(damage);
            }
        }
        /// <summary>
        /// checks if i can attack.
        /// </summary>
        /// <returns></returns>
        bool CanAttack()
        {
            if (target != null)
            {
                if (coolDowntimer <= 0)
                {
                    coolDowntimer = AttackSpeed;
                    if (enemySpawner.CanZombiesDoStuff && Vector3.Distance(transform.position, target.position) <= AttackDistance)
                    {
                        Debug.Log("Attack");
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    //Debug.Log(coolDowntimer + "seconds left till attack");
                    coolDowntimer -= Time.deltaTime;
                    return false;
                }
            }
            return false;
        }
        /// <summary>
        /// return my currentHealth variable
        /// </summary>
        /// <returns></returns>
        public float getHealth()
        {
            return currentHealth;
        }
        /// <summary>
        /// heal function. ignoreTime = true then heal instantly
        /// </summary>
        /// <param name="amt"></param>
        /// <param name="ignoreTime"></param>
        void heal(float amt, bool ignoreTime)
        {
            if (ignoreTime)
            {
                ChangeHealth(amt);
            }
            else
            {
                if (healTime > 0)
                {
                    healTime -= Time.deltaTime;
                }
                if (healTime <= 0)
                {
                    healTime = 1;
                    ChangeHealth(amt);
                }
            }
        }

        public void Pause()
        {
            throw new System.NotImplementedException();
        }
    }
}