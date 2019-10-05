using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSetup : MonoBehaviour
{
        public Character.CharacterType type;
        public List<Item> setup;

    public Item GetItem(ItemType type)
    {
        foreach(Item item in setup)
        {
            if (item.type == type) return item;
        }

        return null;
    }

    public bool ChangeItem(Item newItem)
    {

        for (int i = 0; i < setup.Count; i++)
        {
            if (setup[i].type == newItem.type)
            {
                setup[i] = newItem;
                return true;
            }
        }


        return false;
    }
}
