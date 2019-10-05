using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connection
{
    public Tile from;
    public Tile to;
    public ConnectorType type;
    public Door door;
public Connection(Tile from,Tile to,ConnectorType type)
    {
        this.from = from;
        this.to = to;
        this.type = type;
    }
}


public class Room : MonoBehaviour {

    public Vector2Int startPos;
    public Vector2Int endPos;
    public List<Room> connectedRooms;
    public bool connectedToCorridor = false;
    public List<Connection> connections;
    public RoomType roomType;

    public Room(Vector2Int start, Vector2Int end)
    {
        startPos = start;
        endPos = end;
        connections = new List<Connection>();
    }
}
