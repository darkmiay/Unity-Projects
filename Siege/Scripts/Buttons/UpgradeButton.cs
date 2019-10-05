using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{

    public Item item;
    public ItemSetup itemSetup;
    public List<UpgradeButton> requirements;
    public List<UpgradeButton> nextItems;
    public int cost = 1000;
    public bool purchased = false;

    public void Upgrade(Character character)
    {
        if(character.money>=cost)
        {
            character.TakeMoney(-cost);
            purchased = true;
            itemSetup.ChangeItem(item);
            CheckNextItems();
        }
    }

    private void CheckNextItems()
    {
        foreach(UpgradeButton nextItem in nextItems)
        {
            bool available = true; 
            foreach(UpgradeButton req in nextItem.requirements)
            {
                if (req.purchased != true) available = false;
            }
            if (available) nextItem.Unlock();
        }
    }

    public void Unlock()
    {
        GetComponent<Button>().interactable = true;
    }
}
