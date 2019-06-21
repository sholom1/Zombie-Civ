using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PointSystem : MonoBehaviour
{
    [Header("Pointcounter Text")]
    public TextMeshProUGUI CreditsCounter;
    public TextMeshProUGUI WoodCounter;
    public TextMeshProUGUI StoneCounter;
    public TextMeshProUGUI ScrapCounter;
    public bool active = false;

    [Header("Starting cash")]
    public int startingcash;
    [Header("Current Cash")]
    private int CurrentCash = 0;
    private int Wood = 0;
    private int Stone = 0;
    private int ScrapMetal = 0;
    public Inventory playerInventory;
    #region singleton
    public static PointSystem instance;
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
        //adds starting cash
        AddCash(startingcash, Resource.Type.Credits);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            active = !active;
            gameObject.SetActive(active);
        }
    }
    public void AddCash(int amount, Resource.Type type)
    {
        switch (type)
        {
            case Resource.Type.Credits:
                CurrentCash += amount;
                CreditsCounter.text = "Points: " + CurrentCash.ToString();
                break;
            case Resource.Type.Wood:
                Wood += amount;
                break;
            case Resource.Type.Stone:
                Stone += amount;
                break;
            case Resource.Type.ScrapMetal:
                ScrapMetal += amount;
                break;
        }
        UpdateUI();
    }
    public bool Charge(Item item, int amount)
    {
        if (CanCharge(item, amount))
        {
            foreach (Inventory inventory in CityManager.instance.GetAllInventories())
            {
                if (inventory.TryGetValue(item, out int value) && value - amount > 0 && inventory.Remove(item, value))
                {
                    amount -= value;
                }
                if (amount > 0) return true;
            }
        }
        return false;
    }
    public bool Charge(Cost cost)
    {
        if (CanCharge(cost.CreditsCost, cost.WoodCost, cost.StoneCost, cost.ScrapCost))
        {
            CurrentCash -= cost.CreditsCost;
            Wood -= cost.WoodCost;
            Stone -= cost.StoneCost;
            ScrapMetal -= cost.ScrapCost;
            UpdateUI();
            return true;
        }
        return false;
    }
    public bool CanCharge(Item item, int amount)
    {
        foreach (Inventory inventory in CityManager.instance.GetAllInventories())
        {
            if (inventory.TryGetValue(item, out int value) && value - amount > 0 && inventory.Remove(item, value))
            {
                amount -= value;
            }
            if (amount > 0) return true;
        }
        return false;
    }
    public bool CanCharge(int credits, int wood, int stone, int scrap)
    {
        if (
            (credits == 0 || CurrentCash - credits > 0) &&
            (wood == 0 || Wood - wood > 0) &&
            (stone == 0 || Stone - stone > 0) &&
            (scrap == 0 || ScrapMetal - scrap > 0)
            ) return true;
        return false;
    }
    private void UpdateUI()
    {
        CreditsCounter.text = "Credits: " + CurrentCash;
        WoodCounter.text = "Wood: " + Wood;
        StoneCounter.text = "Stone: " + Stone;
        ScrapCounter.text = "Scrap Metal: " + ScrapMetal;
    }

    //public Invoice GetPayment(int amount, Item.ResourceType RT)
    //{
    //    List<Item> items = new List<Item>();
    //    List<Inventory> providingInventorys = new List<Inventory>();
    //    List<Receipt> receipts = new List<Receipt>();
    //    bool allitems = false;
    //    int AmountNeeded = amount;
    //    Item.ResourceType rt = RT;
    //    if (RT == Item.ResourceType.Credits && CurrentCash >= amount)
    //        return new Invoice(true);
    //    foreach (StorageArea storageArea in CityManager.instance.GetStorageAreas())
    //    {
    //        Inventory SGinventory = storageArea.inventory;
    //        foreach (Item item in SGinventory.StoredItems)
    //        {
    //            if (AmountNeeded > 0 && item.resourceType == rt)
    //            {
    //                AmountNeeded--;
    //                items.Add(item);
    //                receipts.Add(new Receipt(item, SGinventory));
    //                if (providingInventorys.IndexOf(SGinventory) == -1)
    //                {
    //                    providingInventorys.Add(SGinventory);
    //                }
    //            }
    //            else if (AmountNeeded == 0)
    //            {
    //                allitems = true;
    //                return new Invoice(items, allitems, providingInventorys, receipts);
    //            }
    //        }
    //    }
    //    if (AmountNeeded > 0)
    //    {
    //        foreach (Item item in playerInventory.StoredItems)
    //        {
    //            if (AmountNeeded > 0 && item.resourceType == rt)
    //            {
    //                AmountNeeded--;
    //                Debug.Log("how much more we need: " + AmountNeeded);
    //                items.Add(item);
    //                receipts.Add(new Receipt(item, playerInventory));
    //                if (providingInventorys.IndexOf(playerInventory) == -1)
    //                {
    //                    providingInventorys.Add(playerInventory);
    //                }
    //            }
    //            if(AmountNeeded == 0)
    //            {
    //                allitems = true;
    //                return new Invoice(items, allitems, providingInventorys, receipts);
    //            }
    //        }
    //        Debug.Log("Not enough items");
    //        allitems = false;
    //        return new Invoice(items, allitems, providingInventorys, receipts);
    //    }
    //    allitems = true;
    //    return new Invoice(items, allitems, providingInventorys, receipts);
    //}
}
