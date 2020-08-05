using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZombieCiv.Tools;
using ZombieCiv.UI;

namespace ZombieCiv.Construction
{
    public class Building : MonoBehaviour, IPausable
    {

        [Header("Building Info..")]
        //Building Specs
        public PlayerHealth ph;
        public BuildingType WhatIsThis;

        /// <summary>
        /// Starts Construction
        /// </summary>
        public IEnumerator BuildStart(float buildTime)
        {
            //construction.SetActive (true);
            //if BuildTime is Greater than zero Start the countDown
            while (buildTime > 0)
            {
                buildTime -= Time.deltaTime;
                //fake animation of building
                gameObject.transform.Translate(0, buildTime, 0);
                yield return null;
            }
            //add to list of objects with health
            AfterConstruction();
            CityManager.instance.OnBuild(gameObject, WhatIsThis);
        }
        //When we loose our health Destroy us
        public void destroyed()
        {
            DestructableManager.instance.removeMe(gameObject);
            CityManager.instance.onDestroy(this.gameObject, WhatIsThis);
            Destroy(gameObject);
        }
        public virtual void AfterConstruction()
        {

        }

        public void Pause()
        {
            throw new System.NotImplementedException();
        }

        public enum BuildingType
        {
            Nothing,
            Post,
            Wall,
            House,
            TownCenter,
            Farm,
            Factory,
            ResourceStorage
        }
    }
}
