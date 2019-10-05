using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ConnectorType
{
    Corridor,
    Room,
    None
}

public class Tile : MonoBehaviour {

    public ConnectorType connectorType;
    public Room room;
    public Vector2Int coordinates;
    public GameObject furniture;

    public Tile(int x, int y)
        {
        coordinates = new Vector2Int(x,y);
    }

    public void SetCoordinates(int x, int y)
    {
        coordinates.x = x;
        coordinates.y = y;
    }
}
