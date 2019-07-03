using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour
{
    public int Damage;
    public int Durability;
    public float Speed;
    private float O_Speed;
    public int Range;
    public Camera cam;
    public Resource.Type EffectiveResource;
    private PointSystem ps;
    public Animator anim;
    public AnimationClip anima;
    private WaitForSeconds wfs;
    public GameObject inventory;
    // Use this for initialization
    void Start()
    {
        cam = this.transform.parent.GetComponent<Camera>();
        Speed = anima.length;
        O_Speed = Speed;
        ps = GameObject.FindGameObjectWithTag("pointcounter").GetComponent<PointSystem>();
        wfs = new WaitForSeconds(anima.length);
    }

    // Update is called once per frame
    void Update()
    {
        if (Speed > 0)
        {
            Speed -= Time.deltaTime;
        }
        if (Speed <= 0)
        {
            if (!anim.GetBool("Choppin"))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Vector3 origin = cam.ViewportToWorldPoint(new Vector3(.5f, .5f, .5f));
                    if (Physics.Raycast(origin, cam.transform.forward, out RaycastHit hit, Range))
                    {
                        Resource resource = hit.collider.GetComponent<Resource>();
                        if (resource != null)
                        {
                            resource.Mine(Damage, ps, true);
                        }
                    }
                    StartCoroutine(Chop());
                    Speed = O_Speed;
                }
                else Speed = 0;
            }


        }
    }
    private IEnumerator Chop()
    {
        anim.SetBool("Choppin", true);
        yield return wfs;
        anim.SetBool("Choppin", false);
    }
}
