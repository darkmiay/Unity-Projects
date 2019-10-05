using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Sword,Shield,Armor
}

[CreateAssetMenu(fileName = "Item", menuName = "Equipment/Item", order = 51)]
public class Item : ScriptableObject
{
    public ItemType type;
    public GameObject prefab;  
}
