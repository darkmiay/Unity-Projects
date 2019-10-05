using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Furniture", menuName = "Generator/Furniture", order = 51)]
public class Furniture : ScriptableObject
{
    //length and width
    public Vector2Int size;

    //count per room
    public int maxCount;
    public int minCount;

    //blocks the way?
    public bool blocksOut;

    //should it be in line with other furniture of this type? (like storage racks)
    public bool lineUp;

    //should it be near the wall?
    public bool nearTheWall;

    public GameObject furniturePrefab;
}
