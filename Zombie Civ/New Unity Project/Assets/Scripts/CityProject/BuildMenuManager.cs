using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildMenuManager : MonoBehaviour
{
    public GameObject BuildingButtonsContainer;
    public Button BuyBuildingPrefab;
    public List<Button> InstantiatedButtons = new List<Button>();
    // Start is called before the first frame update
    void Start()
    {
        foreach(BuildingPrefab building in TileManager.instance.buildingDictionary.GetAllLoadedBuildings())
        {
            Button newButton = Instantiate(BuyBuildingPrefab, BuildingButtonsContainer.transform);
            newButton.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = building.name;
            newButton.interactable = false;
            InstantiatedButtons.Add(newButton);
        }
    }
    public void UpdateButtonsForSelectedTile(LandTile selected)
    {
        foreach (Button button in InstantiatedButtons)
        {
            TMPro.TextMeshProUGUI buttonText = button.GetComponentInChildren<TMPro.TextMeshProUGUI>();
            if (buttonText.text == selected.CurrentBuilding) {
                button.interactable = false;
                continue;
            }
            button.onClick.RemoveAllListeners();
            if (TileManager.instance.buildingDictionary.TryGetBuilding(buttonText.text, out BuildingPrefab build))
            {
                button.onClick.AddListener(() => selected.BuildBuilding(build));
                button.onClick.AddListener(() => button.interactable = false);
                button.interactable = true;
                continue;
            }
            button.interactable = false;
        }
    }
}
