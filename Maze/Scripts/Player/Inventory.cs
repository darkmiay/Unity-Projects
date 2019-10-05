using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

      List<Item> heavyItems;
     List<Item> mediumItems;
    public List<Item> lightItems;

    InventoryUI invUI;

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public void Start()
    {
        lightItems = new List<Item>();
        invUI = this.GetComponent<InventoryUI>();
    }

    //false if item not added
    public bool AddItem(Item item)
    {
        lightItems.Add(item);
        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
        return true ;
    }

    public bool RemoveItem(Item item)
    {
        lightItems.Remove(item);
        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
        return true;
    }

    public Item FindItem(ItemType itemType)
    {
        foreach (Item i in lightItems)
        {
            if (i.type==itemType)
            {
                return i;
            }
        }
        return null;
    }
}
