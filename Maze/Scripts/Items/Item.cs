using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum ItemType { Key, Torch };

[CreateAssetMenu(fileName = "New item", menuName = "Items/Item")]
public class Item : ScriptableObject {

  

    public string name;
    public Weight weight;
    public Sprite icon;
    public Mesh model;
    public ItemType type;

         public enum Weight
    { Light, Medium, Heavy };
}
