using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Room Type", menuName = "Generator/RoomType", order = 51)]
public class RoomType : ScriptableObject
{

    //Furniture that 100% should be in this room type
    public List<Furniture> requiredFurnitures;

    public List<Furniture> optionalFurnitures;

    //this room type size
    public int minSize;
    public int maxSize;

    //furniture percentage in room of this type
    public float minFill;
    public float maxFill;

    //rooms count
    public int Count;

    //Chance that room will be this type
    public int roomChance;
}
