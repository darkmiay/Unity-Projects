using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Generator : MonoBehaviour
{

    enum Side { up, down, left, right };

    int sideCount = 4;

    Vector2Int SideToVector(Side side)
    {
        switch (side)
        {
            case Side.up:
                return new Vector2Int(0, 1);
            case Side.down:
                return new Vector2Int(0, -1);
            case Side.right:
                return new Vector2Int(1, 0);
            case Side.left:
                return new Vector2Int(-1, 0);
        }
        return Vector2Int.zero;
    }

    Side AdjacentSide(Side side)
    {
        switch (side)
        {
            case Side.up:
                return Side.down;
            case Side.down:
                return Side.up;
            case Side.right:
                return Side.left;
            case Side.left:
                return Side.right;
        }
        return Side.up;
    }

    void PlaceDoor(Transform doorTransform, Vector2Int position, Side side)
    {
        switch (side)
        {
            case Side.up:
                doorTransform.position = new Vector3(position.x + 0.5f, 0f, position.y + 0.45f);
                break;
            case Side.down:
                doorTransform.position = new Vector3(position.x + 0.5f, 0f, position.y - 0.45f);
                break;
            case Side.right:
                doorTransform.position = new Vector3(position.x + 0.45f, 0f, position.y - 0.5f);
                doorTransform.Rotate(new Vector3(0f, 90f, 0f));
                break;
            case Side.left:
                doorTransform.position = new Vector3(position.x - 0.45f, 0f, position.y - 0.5f);
                doorTransform.Rotate(new Vector3(0f, 90f, 0f));
                break;
        }
    }

    void PlaceWall(Transform wallTransform, Vector2Int position, int height, Side side)
    {
        switch (side)
        {
            case Side.up:
                wallTransform.position = new Vector3(position.x + 0.5f, height, position.y + 0.45f);
                break;
            case Side.down:
                wallTransform.position = new Vector3(position.x + 0.5f, height, position.y - 0.45f);
                break;
            case Side.right:
                wallTransform.position = new Vector3(position.x + 0.45f, height, position.y - 0.5f);
                wallTransform.Rotate(new Vector3(0f, 90f, 0f));
                break;
            case Side.left:
                wallTransform.position = new Vector3(position.x - 0.45f, height, position.y - 0.5f);
                wallTransform.Rotate(new Vector3(0f, 90f, 0f));
                break;
        }
    }

    void PlaceCorner(Transform cornerTransform, Vector2Int position, int height, Side sideOne, Side sideTwo)
    {
        cornerTransform.position = new Vector3(position.x, height, position.y);
        if ((sideOne == Side.up && sideTwo == Side.right)||(sideOne == Side.right && sideTwo == Side.up))
        {
            cornerTransform.Rotate(new Vector3(0f, -45f, 0f));
        }
        else if ((sideOne == Side.down && sideTwo == Side.right) || (sideOne == Side.right && sideTwo == Side.down))
        {
            cornerTransform.Rotate(new Vector3(0f, 45f, 0f));
        }
        else if ((sideOne == Side.down && sideTwo == Side.left) || (sideOne == Side.left && sideTwo == Side.down))
        {
            cornerTransform.Rotate(new Vector3(0f, 135f, 0f));
        }
        else 
        {
            cornerTransform.Rotate(new Vector3(0f, 225f, 0f));
        }
    }


    Side LeftSide(Side side)
    {
        switch (side)
        {
            case Side.up:
                return Side.left;
            case Side.down:
                return Side.right;
            case Side.right:
                return Side.up;
            case Side.left:
                return Side.down;
        }
        return Side.up;
    }

    Side RightSide(Side side)
    {
        switch (side)
        {
            case Side.up:
                return Side.right;
            case Side.down:
                return Side.left;
            case Side.right:
                return Side.down;
            case Side.left:
                return Side.up;
        }
        return Side.up;
    }

    int blockSize = 3;

    public Sector sector;

    public GameObject doorPrefab;
    public GameObject roomPrefab;
    public GameObject wallPrefab;
    public GameObject cornerPrefab;

    public Vector2Int blockMapSize;
     Vector2Int mapSize;

    public int roomMaxSize = 4;
    public int roomMinSize = 4;
    public int wallHeight = 2;

    Vector2Int currentBlock;
    Side currentSide;

    public int blocksCount = 100;

    public GameObject tilePrefab;

    public int connectorChanceRoom = 50;
    public int connectorChanceCorridor = 50;
    public bool limitRoomsCount = false;
    public int roomsLimit = 100;
    int roomsCount = 0;

    public int directionChangeChance;
    public int directionNotChangeChance;

    Tile[,] _tiles;
    List<Room> _rooms;
    bool[,] blockTiles;

    List<Tile> connectorTiles;
    List<Vector2Int> blockList;

    private void Start()
    {
        blockTiles = new bool[blockMapSize.x, blockMapSize.y];
        mapSize = new Vector2Int((blockMapSize.x * blockSize) + blockSize + (roomMaxSize*2),
                                 (blockMapSize.y * blockSize) + blockSize + (roomMaxSize * 2));
        _tiles = new Tile[mapSize.x, mapSize.y];
        connectorTiles = new List<Tile>();
        blockList = new List<Vector2Int>();
        _rooms = new List<Room>();

        Generate();
    }

    void Generate()
    {
               
        GenerateBlocks(); //ready

        BlocksToTiles(); //ready

        GenerateRooms(); //ready

        Debug.Log("Room count: " + roomsCount);

        GenerateWalls(); //ready

      //  GenerateFurniture();

      //    GenerateItems();

    }


    void GenerateBlocks()
    {

        //Choose random side 
        //Check for another block in this location 
        //if it is - create block in another location 
        //else create block 
        //repeat 

        ValueChance[] valueChances = new ValueChance[4];

        //Creating first block 
        CreateBlock(new Vector2Int(Random.Range(1, blockTiles.GetLength(0)-2), (Random.Range(1, blockTiles.GetLength(1) - 2))));

        
        for (int i = 0; i < blocksCount; i++)
        {
            RestoreValueChance(valueChances);

            currentSide = (Side)Randomizer.GetRandomValue(valueChances);
            int k = 0;
            //out of the map check
            while ((currentBlock.x + SideToVector(currentSide).x) >= blockTiles.GetLength(0)-1 ||
            (currentBlock.y + SideToVector(currentSide).y) >= blockTiles.GetLength(1)-1 ||
            (currentBlock.x + SideToVector(currentSide).x) <= 0 ||
            (currentBlock.y + SideToVector(currentSide).y) <= 0 ||
            blockTiles[currentBlock.x + SideToVector(currentSide).x, currentBlock.y + SideToVector(currentSide).y] == true ||

                        blockTiles[currentBlock.x + SideToVector(currentSide).x + SideToVector(RightSide(currentSide)).x,
            currentBlock.y + SideToVector(currentSide).y + SideToVector(RightSide(currentSide)).y] == true &&
                blockTiles[currentBlock.x + SideToVector(RightSide(currentSide)).x,
            currentBlock.y + SideToVector(RightSide(currentSide)).y] == true ||

            blockTiles[currentBlock.x + SideToVector(currentSide).x + SideToVector(LeftSide(currentSide)).x,
            currentBlock.y + SideToVector(currentSide).y + SideToVector(LeftSide(currentSide)).y] == true &&
                blockTiles[currentBlock.x + SideToVector(LeftSide(currentSide)).x,
            currentBlock.y + SideToVector(LeftSide(currentSide)).y] == true)                         
            {
                for (int j = 0; j < valueChances.Length; j++)
                {
                    if (valueChances[j].value == (int)currentSide)
                        valueChances[j].chance = 0;
                }
                currentSide = (Side)Randomizer.GetRandomValue(valueChances);
                k++;
                if (k >= valueChances.Length)
                {
                    int num = Random.Range(0, blockList.Count - 1);                   
                    currentBlock = blockList[num];
                    blockList.Remove(blockList[num]);
                    RestoreValueChance(valueChances);
                }
                
            }

            {             
                CreateBlock(currentBlock + SideToVector(currentSide));               
            }


        }

    }

    void RestoreValueChance(ValueChance[] valueChance)
    {
        for (int j = 0; j < valueChance.Length; j++)
        {

            valueChance[j].value = j;
            if (valueChance[j].value == (int)currentSide)
                valueChance[j].chance = directionNotChangeChance;
            else
                valueChance[j].chance = directionChangeChance;

        }
    }

    void BlocksToTiles()
    {
        for (int i = 0; i < blockTiles.GetLength(0); i++)
        {
            for (int j = 0; j < blockTiles.GetLength(1); j++)
            {
                if (blockTiles[i, j]== true)
                {
                    for (int k = 0; k < blockSize && (i*3) + k < mapSize.x-roomMaxSize; k++)
                    {
                        for (int l = 0; l < blockSize && l+(j*3)<mapSize.y- roomMaxSize; l++)
                        {
                            int x, y;
                            x = (i * 3) + k + roomMaxSize;
                            y = (j * 3) + l + roomMaxSize;
                            //     GameObject newTileObj = Instantiate(tilePrefab, new Vector3(x, 0f, y) , Quaternion.identity);
                            GameObject newTileObj = CreateTile(new Vector2Int(x,y));
                            GameObject newTileRoof = CreateRoofTile(new Vector3Int(x, y, wallHeight));
                            Tile newTile = newTileObj.GetComponent<Tile>();
                            newTile.SetCoordinates(x, y);
                            _tiles[x, y] = newTile;
                            if (((x % 3 == 1) && (y % 3 != 1)) || (y % 3 == 1) && (x % 3 != 1)) //if tile on block edge in center it'll become coonector
                            {                               
                                connectorTiles.Add(_tiles[x, y]);
                                _tiles[x, y].connectorType = ConnectorType.Corridor;
                            }
                        }
                    }                                               
                }
                        
            }
        }
    }

    GameObject CreateTile(Vector2Int position)
    {
        GameObject tile = Instantiate(tilePrefab, new Vector3(position.x, 0f, position.y),Quaternion.identity);
        return tile;
    }

    GameObject CreateRoofTile(Vector3Int position)
    {
        GameObject tile = Instantiate(tilePrefab, new Vector3(position.x, position.z+0.1f,position.y), Quaternion.identity);
        return tile;
    }

    void CreateBlock(Vector2Int coor)
    {
        blockTiles[coor.x, coor.y] = true;
        currentBlock.x = coor.x;
        currentBlock.y = coor.y;
        blockList.Add(new Vector2Int(coor.x,coor.y));
       
    }

    bool CheckBlock(int x, int y)
    {
        return blockTiles[x, y];
    }

    void GenerateRooms()
    {

        
//        foreach(Tile t in connectorTiles.ToArray())
        while(connectorTiles.Count>0)
        {
            if (limitRoomsCount)
            {
                if (roomsLimit <= roomsCount)
                {
                    break;
                }
            }

            ValueChance[] connectorValueChance = new ValueChance[2];
            connectorValueChance[0].value = (int)ConnectorType.Corridor;
            connectorValueChance[0].chance = connectorChanceCorridor;
            connectorValueChance[1].value = (int)ConnectorType.Room;
            connectorValueChance[1].chance = connectorChanceRoom;

            Tile t;

            ConnectorType connectorType = (ConnectorType)Randomizer.GetRandomValue(connectorValueChance);
            int c = 0;
            do
            {
                t = connectorTiles[Random.Range(0, connectorTiles.Count)];
                c++;
            }
            while (c < 10 && t.connectorType!= connectorType);
           
            Vector2Int connectPosition = Vector2Int.zero;
            Side heightSide=0;
            bool deadConnector = true;
            for (int i = 0; i < sideCount; i++)
            {
                connectPosition = t.coordinates + SideToVector((Side)i);
                if ((connectPosition.x < _tiles.GetLength(0) && connectPosition.y < _tiles.GetLength(1))&&(connectPosition.x > 0 && connectPosition.y > 0))
                if (_tiles[connectPosition.x, connectPosition.y] == null)
                {
                    deadConnector = false;
                    heightSide = (Side)i;
                    break;
                }                                 
            }
            if (!deadConnector)
            {
                Side adjacentSide = 0;
                bool roomCreated = false;
                int k = 0;

                while (!roomCreated)
                {
                    if (k > 1) break;
                    if (k==0)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            if ((Side)i != heightSide && (Side)i != AdjacentSide(heightSide))
                            {
                                adjacentSide = (Side)i;
                            }
                        }
                    }
                    else
                    {
                        adjacentSide = AdjacentSide(adjacentSide);
                    }
                    k++;

                List<Vector2Int> obstacles = new List<Vector2Int>();
                List<Vector2Int> roomEndList = new List<Vector2Int>();
                for (int i = 0; i < roomMaxSize; i++)
                {
                    for (int j = 0; j < roomMaxSize; j++)
                    {
                           
                            int x,y = 0;
                        //check all tiles in one random direction
                        //change height and adjacent due to direction
                        //Check each tile, can it be room edge or not
                        //(Check for enough length and no obstacles)
                        if (heightSide == Side.up || heightSide == Side.down)
                        {
                             x = connectPosition.x + i * SideToVector(adjacentSide).x;
                             y = connectPosition.y + j * SideToVector(heightSide).y;
                        }
                        else
                        {
                             x = connectPosition.x + i * SideToVector(heightSide).x;
                             y = connectPosition.y + j * SideToVector(adjacentSide).y;                           
                        }
                            if ((x < _tiles.GetLength(0) && y < _tiles.GetLength(1)) && (x > 0 && y > 0))
                            {
                           
                            if (_tiles[x, y] != null)
                            {
                                if (i <= roomMinSize || j <= roomMinSize)
                                    deadConnector = true;
                                else
                                    obstacles.Add(_tiles[x, y].coordinates);
                            }
                            else
                            {
                                if (i >= roomMinSize && j >= roomMinSize)
                                    roomEndList.Add(new Vector2Int(x,y));
                            }
                            }
                        }
                    if (deadConnector) break;
                }
                    //Check how big room can be
                    //If space is enough - create random size room
                    //choose EndPos randomly

                    if (roomEndList.Count > 0 && !deadConnector)
                    {
                        int opa = 0;
                        Vector2Int start = Vector2Int.zero, end = Vector2Int.zero;
                        Vector2Int roomEnd;
                        bool deadEnd;
                        do
                        {
                            opa++;
                            roomEnd = roomEndList[Random.Range(0, roomEndList.Count - 1)];
                            deadEnd = false;

                            //startpos should allways has less x and y and if it's not swap х or y or both
                            if (connectPosition.x > roomEnd.x)
                            {
                                start.x = roomEnd.x;
                                end.x = connectPosition.x;
                            }
                            else
                            {
                                start.x = connectPosition.x;
                                end.x = roomEnd.x;
                            }

                            if (connectPosition.y > roomEnd.y)
                            {
                                start.y = roomEnd.y;
                                end.y = connectPosition.y;
                            }
                            else
                            {
                                start.y = connectPosition.y;
                                end.y = roomEnd.y;
                            }
                            foreach (Vector2Int obstacle in obstacles)
                            {
                                if (obstacle.x>=start.x && obstacle.y>=start.y && obstacle.x <=end.x && obstacle.x <= obstacle.y)
                                {
                                    deadEnd = true;
                                    break;
                                }
                            }
                            if (opa > 30)
                                {
                                Debug.Log("Shit");
                                deadEnd = false;
                            }
                        }
                        while (deadEnd);                     

                        _rooms.Add(CreateRoom(start, end));
                    }
                    
            }
            }
            connectorTiles.Remove(t);
            //delete tile from connectors
            
        }
        //repeat until connectors isn't empty

    }

    Room CreateRoom(Vector2Int startPos, Vector2Int endPos)
    {
        //Fill with tiles
        //Find tiles that can be doors and make them connectors
        //Create room and add it to all rooms
        roomsCount++;
        Color roomColor = Random.ColorHSV(0f,1f,0f,1f,0.5f,1f);
        GameObject roomObject = Instantiate(roomPrefab);
        roomObject.name = "Room " + roomsCount;
        Room newRoom = roomObject.GetComponent<Room>();
        newRoom.startPos = startPos; newRoom.endPos = endPos;
        newRoom.connections = new List<Connection>();

        for (int i = startPos.x; i < endPos.x+1; i++)
        {
            for (int j = startPos.y; j < endPos.y+1; j++)
            {
             //   GameObject newTileObj = Instantiate(tilePrefab, new Vector3(i, 0f, j), Quaternion.identity);
                GameObject newTileObj = CreateTile(new Vector2Int(i, j));
                GameObject newTileRoof = CreateRoofTile(new Vector3Int(i, j, wallHeight));
                newTileObj.transform.parent = roomObject.transform;
                newTileRoof.transform.parent = roomObject.transform;
                newTileRoof.GetComponentInChildren<MeshRenderer>().material.color = roomColor;
                newTileObj.GetComponentInChildren<MeshRenderer>().material.color = roomColor;
                newTileObj.GetComponent<Tile>().coordinates = new Vector2Int(i, j);
                newTileObj.GetComponent<Tile>().room = newRoom;
                _tiles[i, j] = newTileObj.GetComponent<Tile>();
                //if tile not edge or inside, he'll be connector
                if (((i==startPos.x || i==endPos.x) && (j!=startPos.y || j!=endPos.y)) ||
                    ((i != startPos.x || i != endPos.x) && (j == startPos.y || j == endPos.y)))
                {
                    connectorTiles.Add(_tiles[i, j]);
                    _tiles[i, j].connectorType = ConnectorType.Room;
                }
            }
        }

        return newRoom;
    }    

    bool RoomAlreadyConnected(Room currentRoom, Room targetRoom)
    {
        bool RAC = false;
        foreach (Room room in currentRoom.connectedRooms)
        {
            if (targetRoom == room)
            {
                RAC = true; break;
            }
        }
        return RAC;
    }

    void GenerateWalls ()
    {
        //In each room     
        //Check all neighbor tles that not in room we are in, if there is another room, place door and add connected rooms
        //Rotate door
        foreach (Room room in _rooms)
        {
            for (int i = room.startPos.x; i <= room.endPos.x; i++)
            {
                for (int j = room.startPos.y; j <= room.endPos.y; j++)
                {
                    if (((i == room.startPos.x || i == room.endPos.x) && (j != room.startPos.y || j != room.endPos.y)) ||
                       ((i != room.startPos.x || i != room.endPos.x) && (j == room.startPos.y || j == room.endPos.y)))
                    {
                        for (int k = 0; k < sideCount; k++)
                        {
                            if ((i + SideToVector((Side)k).x >= _tiles.GetLength(0) || j + SideToVector((Side)k).y >= _tiles.GetLength(1)) || (i + SideToVector((Side)k).x < 0 || j + SideToVector((Side)k).y < 0)) continue;
                                Tile currentTile = _tiles[i + SideToVector((Side)k).x, j + SideToVector((Side)k).y];
                            if(currentTile != null)                       
                            {
                                ConnectorType conectorType = currentTile.room != null ? ConnectorType.Room : ConnectorType.Corridor;

                                if (conectorType == ConnectorType.Room)
                                {
                                    if (currentTile.room == _tiles[i, j].room || RoomAlreadyConnected(_tiles[i, j].room, currentTile.room) || RoomAlreadyConnected(currentTile.room,_tiles[i, j].room)) continue;
                                }

                                if (!(conectorType==ConnectorType.Corridor && _tiles[i,j].room.connectedToCorridor))
                                {
                                    Connection newConnection = new Connection(_tiles[i, j], currentTile, conectorType);
                                    if (conectorType == ConnectorType.Corridor)
                                    {
                                        _tiles[i, j].room.connectedToCorridor = true;
                                    }
                                    else
                                        {
                                            _tiles[i, j].room.connectedRooms.Add(currentTile.room);
                                        }
                                    GameObject door = Instantiate(doorPrefab);
                                    PlaceDoor(door.transform, new Vector2Int(i, j), (Side)k);
                                    _tiles[i, j].room.connections.Add(newConnection);
                                }                              
                                break;
                            }
                        }
                    }
                }
            }
        }

        //Place corner prefabs and rotate them 
        //Place wall prefabs on other tiles
        //Place walls in corridors

        for (int i = 0; i < _tiles.GetLength(0); i++)
        {
            for (int j = 0; j < _tiles.GetLength(1); j++)
            {
                bool blockingDoor = false;
                if (_tiles[i, j] != null)
                {
                    List<Side> nullNeighbors = new List<Side>();
                    for (int k = 0; k < sideCount; k++)
                    {
                        if ((i + SideToVector((Side)k).x >= _tiles.GetLength(0) || j + SideToVector((Side)k).y >= _tiles.GetLength(1)) || (i + SideToVector((Side)k).x < 0 || j + SideToVector((Side)k).y < 0))
                        {
                            nullNeighbors.Add((Side)k);
                            continue;
                        }                      
                        Tile currentTile = _tiles[i + SideToVector((Side)k).x, j + SideToVector((Side)k).y];
                        if (currentTile == null)
                        {
                            nullNeighbors.Add((Side)k);
                            continue;
                        }
                        if (currentTile.room == null)
                        {
                            if (_tiles[i, j].room != null) nullNeighbors.Add((Side)k); 
                        }
                        else
                        {
                            if(_tiles[i, j].room!=null)
                            foreach (Connection connection in _tiles[i, j].room.connections)
                            {
                                if (connection.from == _tiles[i, j]) blockingDoor = true;
                            }
                            if (_tiles[i, j].room == null) nullNeighbors.Add((Side)k);
                            else if (_tiles[i, j].room != currentTile.room) nullNeighbors.Add((Side)k);
                        }
                    }
                    if(nullNeighbors.Count>0 && !blockingDoor)
                    {
                        if(nullNeighbors.Count==1)
                        {
                            for (int w = 0; w < wallHeight; w++)
                            {
                                GameObject wall = Instantiate(wallPrefab);
                                PlaceWall(wall.transform, new Vector2Int(i, j), w, nullNeighbors[0]);
                            }
                        }
                        else
                        {
                            for (int w = 0; w < wallHeight; w++)
                            {
                                GameObject corner = Instantiate(cornerPrefab);
                                PlaceCorner(corner.transform, new Vector2Int(i, j), w, nullNeighbors[0], nullNeighbors[1]);
                            }
                        }
                    }
                }
            }
        }

    }

    void GenerateFurniture()
    {
        //Place required rooms first
        int requiredCount = 0;
        foreach (RoomType roomType in sector.requiredRooms)
        {
            _rooms[requiredCount].roomType = roomType;
        }

        //Randomly choose from the optional ones
        ValueChance[] valueChance = new ValueChance[sector.optionalRooms.Count];
        for (int i = 0; i < sector.optionalRooms.Count; i++)
        {
            valueChance[i].value = i;
            valueChance[i].chance = sector.optionalRooms[i].roomChance;
        }

        foreach (Room room in _rooms)
        {
            int tileCount = (room.endPos.x - room.startPos.x) * (room.endPos.y - room.startPos.y);
            float percentage = Random.Range(room.roomType.minFill, room.roomType.maxFill);
            List<Tile> doorTiles = new List<Tile>();
            List<Tile> tiles = new List<Tile>();
            List<Tile> furnitureTiles = new List<Tile>();
            for (int i = room.startPos.x; i <= room.endPos.x; i++)
            {
                for (int j = room.startPos.y; j <= room.endPos.y; j++)
                {

                    bool tileIsDoor = false;
                    foreach (Connection connection in room.connections)
                    {
                        if (connection.from == _tiles[i, j]) tileIsDoor = true;
                    }
                    if (tileIsDoor)
                        doorTiles.Add(_tiles[i, j]);

                    else
                        tiles.Add(_tiles[i, j]);
                }
            }
            if (room.roomType==null) 
            {
                room.roomType = sector.optionalRooms[Randomizer.GetRandomValue(valueChance)];
            }

            for (int i = 0; i < room.roomType.requiredFurnitures.Count; i++)
            {
                Furniture currentFurniture = room.roomType.requiredFurnitures[i];
                List<Tile> tilesToCheck = tiles;
                int k = Random.Range(currentFurniture.minCount, currentFurniture.maxCount);
                while (tiles.Count>0 && (percentage<=(furnitureTiles.Count/tileCount)) && k>0)
                   //while there is more created furniture than empty tiles (in percentage), or there's no more empty tiles
                {
                    //Form furniture conditions, and if there's no tiles that it can be placed on, we will place other furniture               
                    bool bad = true;
                    while (tilesToCheck.Count>0 && bad)
                    {
                        //Take random tile
                        Tile currentTile = tilesToCheck[Random.Range(0, tiles.Count - 1)];
                        tilesToCheck.Remove(currentTile);
                        //if furniture blocks way
                        if (currentFurniture.blocksOut)
                        {
                            //check path to doors
                            foreach (Connection connection in room.connections)
                            {
                                List<Tile> path = new List<Tile>();
                                IsBlockingPath(currentTile,path,connection);
                            }
                        }                                                                
                        //Check should it be near the wall or not
                        if (currentFurniture.nearTheWall)
                        {
                            isNearWall(currentTile.coordinates, room);
                        }
                        //Check should it be in line with other tiles of this type or not
                        if (currentFurniture.lineUp)
                            {
                                IsLineUp(new Vector2Int(currentTile.coordinates.x,currentTile.coordinates.y),currentFurniture);
                            }
                    }
                    // k--;                   
                    
                }
                //do the same for optional furniture
            }
        }

    }

    void GenerateItems()
    {
        //Items will be spawned inside furniture on it 
    }

    bool isNearWall(Vector2Int pos,Room room)
    {

        if ((pos.x == room.startPos.x || pos.x == room.endPos.x) &&
            pos.y == room.startPos.y || pos.y == room.endPos.y) return true;
        return false;
    }

    bool IsLineUp(Vector2Int pos,Furniture furniture)
    {
  //      if ()

            for(int i = 0;i<sideCount;i++)
            {
            Vector2Int sidePos = new Vector2Int(pos.x + SideToVector((Side)i).x, pos.y + SideToVector((Side)i).y);
                if (IsFurnitureOfTypeOnTile(furniture, sidePos))
                {
                Vector2Int currentPos;
                //Check tiles on right and left sides, than check that there's is no furniture of this type in front of this tile
                for (int k = 0; k < 3; k++)
                {
                    currentPos = new Vector2Int
                        ((sidePos.x) + (SideToVector(LeftSide((Side)i)).x) + (k * SideToVector(AdjacentSide((Side)i)).x),
                        (sidePos.y) + (SideToVector(LeftSide((Side)i)).y) + (k * SideToVector(AdjacentSide((Side)i)).y));
                    if (IsFurnitureOfTypeOnTile(furniture,currentPos))
                    {
                        return false;
                    }
                }

                for (int k = 0; k < 3; k++)
                {
                    currentPos = new Vector2Int
                        ((sidePos.x) + (SideToVector(RightSide((Side)i)).x) + (k * SideToVector(AdjacentSide((Side)i)).x),
                        (sidePos.y) + (SideToVector(RightSide((Side)i)).y) + (k * SideToVector(AdjacentSide((Side)i)).y));
                    if (IsFurnitureOfTypeOnTile(furniture, currentPos))
                    {
                        return false;
                    }
                }
            }
            }
        return true;
    }

    bool IsFurnitureOfTypeOnTile(Furniture furniture, Vector2Int pos)
    {
        if (pos.x >= mapSize.x || pos.y >= mapSize.y || pos.x < 0 || pos.y < 0) return false;

        if (_tiles[pos.x, pos.y].furniture.GetComponent<Furniture>() == furniture) return true;
        return false;
    }

    bool IsBlockingPath(Tile currentTile,List<Tile> path,Connection connection)
    {
        //Take tiles on all sides
        //Check if there's something
        //If there's nothing - do it again
        //If there's door tile return false
        List<Tile> newPath = new List<Tile>();
        newPath.AddRange(path);
        newPath.Add(currentTile);
        int k = 0;
        for (int i = 0; i < sideCount; i++)
        {
            if (currentTile.coordinates.x + SideToVector((Side)i).x>mapSize.x || currentTile.coordinates.y + SideToVector((Side)i).y>mapSize.y 
                || currentTile.coordinates.x + SideToVector((Side)i).x<0 || currentTile.coordinates.y + SideToVector((Side)i).y<0)
            {
                k++;
                continue;
            }

           Tile sideTile = _tiles[currentTile.coordinates.x + SideToVector((Side)i).x, currentTile.coordinates.y + SideToVector((Side)i).y];

            //ignore tiles wuth furniture
            if (sideTile.furniture !=null)
            {
                k++;
                continue;
            }

            //found tile with door
                if (connection.from == sideTile) return false;

            //repeat this func
            if (!IsBlockingPath(sideTile, newPath,connection))
            {
                return false;
            }


        }
       
        return true;
    }

}

