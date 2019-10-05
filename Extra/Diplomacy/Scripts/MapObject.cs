using UnityEngine;
using System.Collections;
using System;

public enum TerrainType
{
    mountain = 1,
    tree = 2,
    water = 3,
}

public class MapObject : MonoBehaviour {


    TerrainType terrainType;
    BiomeType biomeType;
    Generator script;
    SpriteRenderer spriteRenderer;
    public int resources=0;
    public int gold=0;
    public int progress=0;
    public int population=0;
    public GameObject flag=null;



	// Use this for initialization
	void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        script = GameObject.Find("Generator").GetComponent<Generator>();
      
        biomeType = script.GetTileBiome(this.transform.position.x, this.transform.position.y);
     
        int tp = UnityEngine.Random.Range(1, 4);
        if (tp == 1)
        {

            terrainType = TerrainType.mountain;

        }
        else if (tp == 2)
        {
            terrainType = TerrainType.tree;
        }
        else terrainType = TerrainType.water;
        string name = biomeType + "_" + terrainType;
            foreach(Sprite s in script.sprites)
            {
                if (s.name == name) spriteRenderer.sprite = s; 
            }
        if (terrainType==TerrainType.mountain)
        {
            if (biomeType==BiomeType.grass)
            {
                gold = 2 + UnityEngine.Random.Range(0, 2);
                resources = 1 + UnityEngine.Random.Range(0, 1);
            }
            else if (biomeType == BiomeType.sand)
            {
                gold = 3 + UnityEngine.Random.Range(-1, 3);
            }
            else
            {
                gold = 1 + UnityEngine.Random.Range(0, 1);
                resources = 1 + UnityEngine.Random.Range(0, 1);
                progress = 1 + UnityEngine.Random.Range(0, 1);
            }
        }
        else if (terrainType == TerrainType.tree)
        {
            if (biomeType == BiomeType.grass)
            {
                resources = 2 + UnityEngine.Random.Range(0, 2);
                population = 1 + UnityEngine.Random.Range(0, 1);
            }
            else if (biomeType == BiomeType.sand)
            {
                progress = 3 + UnityEngine.Random.Range(-1, 3);
            }
            else
            {
                resources = 3 + UnityEngine.Random.Range(-1, 3);
            }
        }
        else
        {
            if (biomeType == BiomeType.grass)
            {
                population = 2 + UnityEngine.Random.Range(0, 2);
                gold = 1 + UnityEngine.Random.Range(0, 1);
            }
            else if (biomeType == BiomeType.sand)
            {
                population = 3 + UnityEngine.Random.Range(-1, 3);
            }
            else
            {
                progress = 1 + UnityEngine.Random.Range(0, 1);
                resources = 1 + UnityEngine.Random.Range(0, 1);
                gold = 1 + UnityEngine.Random.Range(0, 1);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
