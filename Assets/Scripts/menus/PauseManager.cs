using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class PauseManager : MonoBehaviour
{
    [Header("Pause Visuals")]
    public GameObject PauseGameObject;
    [Header("Player")]
    public GameObject PlayerGameObject;
    [Header("Build Tool")]
    public BuildTool bt;
    // Use this for initialization
    void Start()
    {
        PlayerGameObject = GameObject.Find("FPSController 1");
    }

    // Update is called once per frame
    void Update()
    {
        //only if i have nothing selected are we allowed to pause
        if (bt.HasSelected() == false)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Pause();
            }
        }
    }
    public void Pause()
    {
        //if unpaused then pause
        if (PauseGameObject.activeInHierarchy == false)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            PauseGameObject.SetActive(true);
            Time.timeScale = 0;
            //only if build mode is not open then disable movement
            PlayerGameObject.GetComponent<FirstPersonController>().enabled = false;
            Debug.Log("Succesfully paused");
            //if paused then unpause
        }
        else if (PauseGameObject.activeInHierarchy == true)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;
            //only if build mode is not open then enable movement
            if (bt.isOpen)
            {
                Debug.Log("Build Mode is open");
                PlayerGameObject.GetComponent<FirstPersonController>().enabled = false;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Debug.Log("Build Mode is not open");
                Cursor.visible = false;
                PlayerGameObject.GetComponent<FirstPersonController>().enabled = true;
            }
            PauseGameObject.SetActive(false);
        }
        else
        {
            Debug.Log("WTF where is the pause menu i cant have a pause script without a pause menu");
        }
    }
}
