using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Player Statistics")]
    public float currentHealth;
    public float maximumHealth = 100f;
    public ObjectType Whatisthis;
    public float AttackableDistance;
    public float HealthPerSecond = 0f;
    private float hbLength;
    [Space(1)]
    [Header("if Player things to disable on death")]
    public GameObject deathcam;
    public GameObject deathtext;
    public bool isDead = false;
    public BoxCollider bc;
    public EnemySpawning es;


    // Use this for initialization
    void Start()
    {
        GameObject st = GameObject.FindGameObjectWithTag("Scripts");
        currentHealth = maximumHealth;

        hbLength = Screen.width / 2;
        //Add to things with healthlist;
        DestructableManager.instance.addMe(gameObject);

    }

    // Update is called once per frame
    void Update()
    {
        //can be used for healing
        ChangeHealth(HealthPerSecond * Time.deltaTime);
    }

    
    //Harm or Heal int positive heal in negative damage
    public virtual void ChangeHealth(float health)
    {
        if (health < 0)
        {
            Debug.Log("Ive lost health my name is: " + gameObject.name);
        }
        //Change health
        currentHealth += health;
        if (Whatisthis == ObjectType.Player)
        {
            GameObject healthCounter = GameObject.FindGameObjectWithTag("Health Counter");
            healthCounter.GetComponentInChildren<TextMeshProUGUI>().text = "Health: " + currentHealth + "/" + maximumHealth;
            healthCounter.GetComponentInChildren<Slider>().maxValue = maximumHealth;
            healthCounter.GetComponentInChildren<Slider>().value = currentHealth;

        }
        //if Dead
        if (currentHealth <= 0)
        {
            Debug.Log("Im dead my name is: " + gameObject.name);
            //if player die
            if (Whatisthis == ObjectType.Player)
            {
                deathtext.SetActive(true);
                deathcam.SetActive(true);
                DestructableManager.instance.removeMe(gameObject);
                es.CanZombiesDoStuff = false;
                Destroy(gameObject);
            }
            //if building destroy
            if (Whatisthis == ObjectType.building)
            {
                gameObject.GetComponentInParent<Building>().destroyed();
            }

        }
        hbLength = (Screen.width / 2) * (currentHealth / (float)maximumHealth);
    }
    //Type of thing with health
    public enum ObjectType
    {
        Player,
        building,
        Guard
    }

}