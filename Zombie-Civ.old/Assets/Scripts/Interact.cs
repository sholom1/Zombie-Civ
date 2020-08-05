using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
using ZombieCiv.Construction;
using TMPro;

namespace ZombieCiv.Tools
{
    public class Interact : MonoBehaviour
    {
        public Camera cam;
        public int Range;
        public GameObject Target;
        public GameObject TransferButtons;
        public Inventory PlayerInv;
        public FirstPersonController Controller;
        public TextMeshProUGUI toolTip;
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
                Vector3 origin = cam.ViewportToWorldPoint(new Vector3(.5f, .5f, .5f));
                if (Physics.Raycast(origin, cam.transform.forward, out RaycastHit hit, Range))
                {
                    Target = hit.collider.gameObject;
                    Interactable targetItem = Target.GetComponent<Interactable>();
                    if (targetItem != null)
                    {
                        toolTip.gameObject.SetActive(true);
                        toolTip.text = "(F): " + targetItem.actionInfo;
                        if (Input.GetKeyDown(KeyCode.F))
                        {
                            targetItem.Use(this);
                        }
                    }
                }
                else { toolTip.text = "(F): "; toolTip.gameObject.SetActive(false); }

                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    TransferButtons.SetActive(false);
                    Controller.enabled = true;
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
