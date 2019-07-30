using UnityEngine;
using ZombieCiv.UI;

namespace ZombieCiv
{
    public class Despawnable : MonoBehaviour, IPausable
    {
        private float timer;
        private bool despawn = false;
        public void Despawn(float time)
        {
            timer = time;
            despawn = true;
            if (gameObject.tag != "Tree")
            {
                MeshCollider col = gameObject.AddComponent<MeshCollider>();
                col.convex = true;
                gameObject.AddComponent<Rigidbody>();
            }
        }

        public void Pause()
        {
            throw new System.NotImplementedException();
        }

        void Update()
        {
            if (despawn)
            {
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
