using UnityEngine;
using System;

public class Generator : MonoBehaviour {

	[SerializeField]
	int Width = 512;
	[SerializeField]
	int Height = 512;
    [SerializeField]
    int TerrainsCount=3;
    [SerializeField]
    int EmprieCount = 3;

	MapData MapData;
	Tile[,] Tiles;
	MeshRenderer GameMap;
    public GameObject terrainPrefab;
    public GameObject empirePrefab;
    public Sprite[] sprites;

    public BiomeType GetTileBiome(float X, float Y)
    {

        X += 0.5f;

        X *= Width;

        Y *= 1;

        Y += 0.5f;
 
        Y *= Height;


        return Tiles[Convert.ToInt32(X), Convert.ToInt32(Y)].biomeType;
      

    }

	void Start()
	{
		GameMap = transform.Find ("GameMapTexture").GetComponent<MeshRenderer> ();
		GetData (ref MapData);
		LoadTiles ();
		GameMap.materials[0].mainTexture = TextureGenerator.GetTexture (Width, Height, Tiles);
     SpawnCitys(EmprieCount);
      SpawnTerrains(TerrainsCount);
      
	}

    void SpawnTerrains(int terrainCount)
    {
        GameObject[] allterrains;
        Vector3 newVector;
       for (int i = 0; i < terrainCount; i++)
			{
                newVector = new Vector3(UnityEngine.Random.Range(-0.45f, 0.45f),
                           UnityEngine.Random.Range(-0.45f, 0.45f), 0);
               allterrains = GameObject.FindGameObjectsWithTag("Terrain");
               if (allterrains.Length != 0) 
               {

                   while (!NoObjectsNear(allterrains, newVector))                        
                       newVector = new Vector3(UnityEngine.Random.Range(-0.45f, 0.45f),
                      UnityEngine.Random.Range(-0.45f, 0.45f), 0);
               } 
         Instantiate(terrainPrefab, newVector, Quaternion.identity);
    }
       }
	
   public bool NoObjectsNear(GameObject[] allterrains,Vector3 newVector,float dimension=0.2f)
   {
       if (allterrains.Length != 0)
       foreach(GameObject g in allterrains)
       {
           if (Vector3.Distance(g.transform.position, newVector) < dimension) return false; 
       }
      
       return true;
   }

  

   public void SpawnCitys(int startCityCount)
   {

       for (int i = 0; i < startCityCount; i++)
       {
           Instantiate(empirePrefab, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);

       }
   }



	private void GetData(ref MapData mapData)
	{

		mapData = new MapData (Width, Height);
        int[,] map = MapGenerator.GenerateStartPoints(Height, Width, 3);
        MapGenerator.GenerateMap(ref map, 3);
        mapData.Data = map;
	}

	private void LoadTiles()
	{
        
		Tiles = new Tile[Width, Height];
		 int[,] map = MapGenerator.GenerateStartPoints(Height, Width, 3);
                MapGenerator.GenerateMap(ref map,3);
		for (var x = 0; x < Width; x++)
		{
			for (var y = 0; y < Height; y++)
			{
				Tile t = new Tile();
				t.X = x;
				t.Y = y;
                			
				float value = MapData.Data[x, y];           
			if (value==4) 
					t.biomeType = BiomeType.sand;
            else if (value==5)
					t.biomeType = BiomeType.grass;
            else
					t.biomeType = BiomeType.snow;
				
				Tiles[x,y] = t;

               
			}
		}
	}

}
