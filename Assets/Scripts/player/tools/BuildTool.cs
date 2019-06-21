using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class BuildTool : MonoBehaviour
{
    //Build Veiw Managing
    [Header("BuildVeiw Managing")]
    public bool isOpen;
    public Camera TopDownCamera;
    public GameObject[] buttons;
    public GameObject nocash;
    public EnemySpawning es;
    public bool changeCaniKillTheHuman;
    public GameObject[] UpgradeButtons;
    [Space(1)]
    //BVM Player
    [Header("Player")]
    public GameObject Player;
    //Economy
    [Space(1)]
    [Header("Money Manager")]
    public GameObject psgo;
    [Space(1)]
    //Buildable Managing
    [Header("Buildable Managing")]
    public LayerMask layermask;
    public float rotateSpeed;
    public bool canPlace;
    [Space(1)]
    //BM Building specs
    [Header("Building")]
    public Transform selected;
    private Buildable buildable;
    private Quaternion unroundedRotation;

    #region singleton
    public static BuildTool instance;
    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else
            instance = this;
    }
    #endregion
    void Start()
    {
    }
    void Update()
    {
        //if BuildVeiw is open
        if (isOpen)
        {
            //and selected is not null
            if (selected != null)
            {
                //Set the shot to come from the position of theCamera
                Ray rayOrigin = TopDownCamera.ScreenPointToRay(Input.mousePosition);
                //Initialize Raycast
                RaycastHit hit;
                //if we hit the ground
                if (Physics.Raycast(rayOrigin, out hit, layermask))
                {
                    //Move selected to our mouse
                    //Do not move while rotating
                    if (!Input.GetMouseButton(1))
                    {
                        selected.position = new Vector3(hit.point.x, 0, hit.point.z);
                    }
                    //if we click the left mouse button Place
                    if (Input.GetMouseButtonDown(0))
                    {
                        //check if we are allowed to place & we can afford the building
                        if (canPlace)
                        {
                            
                            if (PointSystem.instance.Charge(buildable.Price))
                            {
                                buildable.BuildStart();
                            }
                            //if we cant afford run building failed
                            else
                            {
                                BuildingFailed(WhyBuildingFailed.NoMoney);
                            }
                        }
                        else
                        {
                            BuildingFailed(WhyBuildingFailed.NoMoney);
                        }
                    }
                    //if we dont click to place but instead right click then rotate
                    else if (Input.GetMouseButton(1) && buildable.canRotate)
                    {
                        //While left shift is held, snap to 45 degree angles
                        if (Input.GetKey(KeyCode.LeftShift))
                        {
                            if (Input.GetKeyDown(KeyCode.LeftShift))
                            {
                                unroundedRotation = selected.rotation;
                            }

                            unroundedRotation = Quaternion.Euler(
                                unroundedRotation.eulerAngles + new Vector3(0, Input.GetAxis("Mouse X") * rotateSpeed));
                            print(unroundedRotation.eulerAngles);
                            Quaternion roundedRotation = unroundedRotation;
                            Vector3 eulerRoundedRotation = roundedRotation.eulerAngles;
                            print(eulerRoundedRotation);
                            eulerRoundedRotation.y = Mathf.Round(eulerRoundedRotation.y / 45) * 45;
                            roundedRotation = Quaternion.Euler(eulerRoundedRotation);
                            selected.rotation = roundedRotation;
                            
                        }

                        else if (Input.GetAxis("Mouse X") != 0)
                        {
                            selected.Rotate(0, Input.GetAxis("Mouse X") * rotateSpeed, 0);
                        }
                    }
                }
                //if we are placing a building but press escape then cancel placement
                if (Input.GetKeyDown(KeyCode.Escape))
                    BuildingFailed(WhyBuildingFailed.Cancelled);
                //if in upgrade mode then cancel upgrade
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    HideUpgrade();
                }
            }
            else
            {
                //allow us to pause
                DeSelect();
            }
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            PointSystem.instance.active = !PointSystem.instance.active;
            PointSystem.instance.gameObject.SetActive(PointSystem.instance.active);
        }
    }
    //This opens and closes Build Mode
    public void BuildVeiwOpen(bool Open)
    {
        //if we want to open
        if (Open)
        {
            isOpen = true;
            //enable top-down camera
            TopDownCamera.enabled = true;
            //Disable moving and MouseLook
            Player.GetComponent<FirstPersonController>().enabled = false;
            //unlock cursor
            Cursor.lockState = CursorLockMode.None;
            //make it visible
            Cursor.visible = true;
            //if we allow it to change pausing
            if (changeCaniKillTheHuman)
            {
                //then pause
                es.CanZombiesDoStuff = false;
            }
            //show buttons for things we can place
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].SetActive(true);
            }
            PointSystem.instance.gameObject.SetActive(true);
            ///foreach (GameObject g in dm.thingswithhealth) {
            //	PlayerHealth ph = g.GetComponent<PlayerHealth> ();
            //	if (ph.Whatisthis == PlayerHealth.objType.building) {
            //		ph.bc.enabled = true;
            //	}
            ///}
            //Leave Build Mode
        }
        else
        {
            isOpen = false;
            //Disable top-down Veiw
            TopDownCamera.enabled = false;
            //allow movement and Mouse Look
            Player.GetComponent<FirstPersonController>().enabled = true;
            //Lock cursor
            Cursor.lockState = CursorLockMode.Locked;
            //make it invisible
            Cursor.visible = false;
            //if we allow it to unpause
            if (changeCaniKillTheHuman)
            {
                //then unpause
                es.CanZombiesDoStuff = true;
            }
            //hide buttons for things we can place
            for (int i = buttons.Length - 1; i > -1; i--)
            {
                buttons[i].SetActive(false);
            }
            if (!PointSystem.instance.active) PointSystem.instance.gameObject.SetActive(false);
            DeSelect();
            ///foreach (GameObject g in dm.thingswithhealth) {
            //	PlayerHealth ph = g.GetComponent<PlayerHealth> ();
            //	if (ph.Whatisthis == PlayerHealth.objType.building) {
            //		ph.bc.enabled = false;
            //	}
            ///}
        }
    }
    /// <summary>
    /// Sets the building we want to spawn in
    /// </summary>
    /// <param name="newSelected"></param>
    public void SetSelected(GameObject newSelected)
    {
        //creates new building
        GameObject go = Instantiate(newSelected, this.gameObject.transform);
        selected = go.transform;
        buildable = go.GetComponent<Buildable>();
    }
    public void DeSelect()
    {
        if(selected != null)
        Destroy(selected.gameObject);
        selected = null;
        buildable = null;
    }
    public void BuildingFailed(WhyBuildingFailed reason)
    {
        if (reason == WhyBuildingFailed.NoMoney)
        {
            nocash.SetActive(true);
            nocash.GetComponent<InsufficientFunds>().timer = 1;
        }
        if (reason == WhyBuildingFailed.Cancelled)
        {
            DeSelect();
        }
        if (reason == WhyBuildingFailed.SomethingInTheWay)
        {

        }
    }
    public void HideUpgrade()
    {
        for (int i = 0; i < UpgradeButtons.Length; i++)
        {
            UpgradeButtons[i].SetActive(false);
        }
        selected = null;
    }
    public bool HasSelected()
    {
        if (selected == null)
        {
            return false;
        }
        return true;
    }
    public enum WhyBuildingFailed
    {
        NoMoney,
        Cancelled,
        SomethingInTheWay
    }
}