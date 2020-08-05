using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZombieCiv.UI
{
    public class UI_Inventory_full : MonoBehaviour, IPausable
    {

        public GameObject invFullUIGO;
        public static UI_Inventory_full instance;
        private void Awake()
        {
            if (instance != null)
            {
                Destroy(this);
            }
            instance = this;
        }
        public IEnumerator DisplayWarning()
        {
            float timer = 1;
            invFullUIGO.SetActive(true);
            while (timer > 0)
            {
                timer -= Time.deltaTime;
                yield return null;
            }
            invFullUIGO.SetActive(false);
        }

        public void Pause()
        {
            throw new System.NotImplementedException();
        }
    }
}
