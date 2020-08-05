using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Player : Character
{
    public FirstPersonController PlayerController;
    public Camera FirstPersonCamera;
    public InputDictionary InputManager;
    public LayerMask LayerMaskToIgnore;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(InputManager.Interact))
        {
            if (Physics.Raycast(FirstPersonCamera.transform.position, FirstPersonCamera.transform.forward, out RaycastHit hit, 10f, ~LayerMaskToIgnore))
            {
                Debug.Log($"hit: {hit.collider.name}");
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable)
                {
                    interactable.Invoke(this);
                }
            }
        }
        else if (Input.GetKeyDown(InputManager.Use))
        {
            if (Physics.Raycast(FirstPersonCamera.transform.position, FirstPersonCamera.transform.forward, out RaycastHit hit, 10f, ~LayerMaskToIgnore))
            {
                Debug.Log($"hit: {hit.collider.name}");
                Resource resource = hit.collider.GetComponent<Resource>();
                if (resource)
                {
                    Debug.Log("Hit a resource");
                    resource.Mine(10);
                }
            }
        }
    }
}
