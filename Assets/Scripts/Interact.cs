using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
using ZombieCiv.Construction;

namespace ZombieCiv.Tools
{
    public class Interact : MonoBehaviour
    {
        public Camera cam;
        public int Range;
        public GameObject target;
        public GameObject TButtons;
        public Inventory PlayerInv;
        public FirstPersonController FPC;
        // Update is called once per frame
        #region singleton
        public static Interact instance;
        private void Awake()
        {
            if (instance != null)
                Destroy(this);
            else
                instance = this;
        }
        #endregion
        void Update()
        {
            if (!BuildTool.instance.isOpen)
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    Vector3 origin = cam.ViewportToWorldPoint(new Vector3(.5f, .5f, .5f));
                    if (Physics.Raycast(origin, cam.transform.forward, out RaycastHit hit, Range))
                    {
                        target = hit.collider.gameObject;
                        Interactable targetItem = target.GetComponent<Interactable>();
                        if (targetItem != null) targetItem.Use(this);
                    }
                }
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    TButtons.SetActive(false);
                    FPC.enabled = true;
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }
            }
        }
        //public void doTransfer(){
        //	int selected = TButtons.GetComponentInChildren<Dropdown> ().value;
        //	int amount = int.Parse(TButtons.GetComponentInChildren<InputField> ().text);
        //	Item.Type type1;
        //	Item.ResourceType material;
        //	if (selected == 0) {
        //		type1 = Item.Type.Resource;
        //		material = Item.ResourceType.Wood;
        //		PlayerInv.TransferItems (amount, target, type1, material);
        //	} else if (selected == 1) {
        //		type1 = Item.Type.Resource;
        //		material = Item.ResourceType.Stone;
        //		PlayerInv.TransferItems (amount, target, type1, material);
        //	} else if (selected == 2) {
        //		type1 = Item.Type.Resource;
        //		material = Item.ResourceType.Credits;
        //		PlayerInv.TransferItems (amount, target, type1, material);
        //	} else if (selected == 3) {
        //		type1 = Item.Type.Resource;
        //		material = Item.ResourceType.ScrapMetal;
        //		PlayerInv.TransferItems (amount, target, type1, material);
        //	} else if (selected == 4) {
        //		type1 = Item.Type.Weapon;
        //		material = Item.ResourceType.NONE;
        //		PlayerInv.TransferItems (amount, target, type1, material);
        //	}
        //	TButtons.SetActive (false);
        //       FPC.enabled = true;
        //       Cursor.lockState = CursorLockMode.Locked;
        //       Cursor.visible = false;
        //   }
    }
}
