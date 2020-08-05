using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Receipt{
	public Item item;
	public Inventory inventory;
	public Receipt(Item i, Inventory inv){
		item = i;
		inventory = inv;
	}
}
