using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolManager : MonoBehaviour
{
    public GameObject[] tools;
    public List<GameObject> Tools = new List<GameObject>();
    #region singleton
    public static ToolManager instance;
    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else
            instance = this;
    }
    #endregion
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!Interact.instance.TButtons.activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
            {
                if (Tools[0].activeInHierarchy)
                {
                    Tools[0].SetActive(false);
                }
                else
                {
                    Tools[0].SetActive(true);
                    Tools[1].GetComponent<BuildTool>().BuildVeiwOpen(false);
                    Tools[2].SetActive(false);
                }

            }
            if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
            {
                if (Tools[1].GetComponent<BuildTool>().isOpen)
                {
                    Tools[1].GetComponent<BuildTool>().BuildVeiwOpen(false);
                }
                else
                {
                    Tools[0].SetActive(false);
                    Tools[2].SetActive(false);
                    Tools[1].GetComponent<BuildTool>().BuildVeiwOpen(true);
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
            {
                if (Tools[2].activeInHierarchy)
                {
                    Tools[2].SetActive(false);
                }
                else
                {
                    Tools[0].SetActive(false);
                    Tools[1].GetComponent<BuildTool>().BuildVeiwOpen(false);
                    Tools[2].SetActive(true);
                }
            }
        }
    }
    public void Deselect()
    {
        Tools[0].SetActive(false);
        Tools[1].GetComponent<BuildTool>().BuildVeiwOpen(false);
        Tools[2].SetActive(false);
    }
}
