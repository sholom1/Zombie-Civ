using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZombieCiv.Controllers.AI;

namespace ZombieCiv.Tools
{
    public class GunScript : MonoBehaviour
    {

        //Gun Specs
        [Header("Gun Specs..")]
        public int gundamage = 1;
        public float firerate = .25f;
        public float weaponrange = 50f;
        [Space(1)]
        //Gun Parts
        [Header("Gun parts..")]
        public Transform gunend;
        public float zoffset = 1f;
        public Camera fpscam;
        [Space(1)]
        //Effects
        [Header("Effects")]
        private WaitForSeconds shotduration = new WaitForSeconds(.07f);
        public AudioSource gunaudio;
        public LineRenderer laserline;


        //Timer
        private float nextfire;



        void Start()
        {
        }

        void Update()
        {
            //if Trigger is pressed and enough time has pased since last fire
            if (Input.GetButton("Fire1") && Time.time > nextfire)
            {
                //reset add time passed since timer
                nextfire = Time.time + firerate;

                //Play weapon effects
                StartCoroutine(shoteffect());

                //Bullet comes from center of camera
                Vector3 rayorigin = fpscam.ViewportToWorldPoint(new Vector3(.5f, .5f, zoffset));
                RaycastHit hit;

                laserline.SetPosition(0, gunend.position);

                //Fire weapon from:
                //Center of camera
                //in the direction camera is facing
                //using the raycast of name hit
                //but only go the range of the weapon
                if (Physics.Raycast(rayorigin, fpscam.transform.forward, out hit, weaponrange))
                {
                    //what have i shot
                    Debug.Log(hit.collider.gameObject.name.ToString());
                    //show bullet path
                    laserline.SetPosition(1, hit.point);
                    //check if ive hit a player
                    if (hit.collider.gameObject.tag == "Player")
                    {
                        //if i have hit a player tell me i did (Maybe play no friendly fire sound)
                        Debug.Log(hit.collider.gameObject);
                    }
                    //create health variable
                    EnemyController health = hit.collider.GetComponent<EnemyController>();

                    //if its an enemy with health
                    if (health != null)
                    {
                        health.ChangeHealth(gundamage);
                    }
                    //if i missed then shoot forward only as long as the weapon range
                }
                else
                {
                    laserline.SetPosition(1, rayorigin + (fpscam.transform.forward * weaponrange));
                }
            }
        }
        //weapon effects
        private IEnumerator shoteffect()
        {
            gunaudio.Play();
            laserline.enabled = true;
            yield return shotduration;
            laserline.enabled = false;
        }
    }
}
