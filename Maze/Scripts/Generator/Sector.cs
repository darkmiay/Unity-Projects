using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Sector", menuName = "Generator/Sector", order = 51)]
public class Sector : ScriptableObject
{
    //Room types that 100% should be in this room type
    public List<RoomType> requiredRooms;

    public List<RoomType> optionalRooms;

}
