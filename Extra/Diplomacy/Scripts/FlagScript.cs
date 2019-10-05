using UnityEngine;
using System.Collections;

public class FlagScript : MonoBehaviour {

    GameObject Destionaton;
    public GameObject CityCor;
  CityScript City;
    MapObject DestScript;
    public SpriteRenderer sr;
    public GameObject CaravanPrefab;
    bool OnPosition=false;
  public Color color;

    int fullness=0;
    int gold = 0;
    int recources = 0;
   public int population = 0;
    public int army = 0;
    public int progress = 0;
   public int progressLevel=1;

    float startTime;

	// Use this for initialization
	void Start () {
        CityCor = NearestCity();
        City = CityCor.GetComponent<CityScript>();
        Destionaton = City.Destination;
        sr.color = City.GiveColor();
        color = sr.color;
        DestScript = Destionaton.GetComponent<MapObject>();
       
	}

    public void TakeColor(Color c)
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = c;
        color = c;
    }
	// Update is called once per frame
	void Update () {
        if (!OnPosition)
            if (Vector3.Distance(this.transform.position, Destionaton.transform.position) > 0.01f)
            {
                Move();
            }
            else
            {              
                OnPosition = true;
                City.CityIsBisy = false;
            }
        else
        {
            var deltaTime = Time.time - startTime;
            if (deltaTime >= 1)
            {
                GenerateRecources();
                startTime = Time.time;
            }
        }
        if (fullness > 10)
        {
            TakeColor(CityCor.GetComponent<SpriteRenderer>().color);
            GameObject caravan = Instantiate(CaravanPrefab, this.transform.position, Quaternion.identity) as GameObject;
            caravan.transform.parent = this.transform.parent;
            CaravanScript cs = caravan.GetComponent<CaravanScript>();
            cs.City = City;
            cs.Destination = CityCor;
            cs.color = color;
            cs.gold = gold;
            gold = 0;
            cs.population = population;
            population = 0;
            cs.recources = recources;
            recources = 0;
            cs.progress = progress;
            progress = 0;
            fullness = 0;

        }

	}

    void GenerateRecources()
    {
        gold+=DestScript.gold;
        recources += DestScript.resources;
        population += DestScript.population;
        progress += DestScript.progress;
        fullness = gold + recources + population + progress;
    }

    void Move()
    {
        this.transform.position= Vector3.MoveTowards(this.transform.position, Destionaton.transform.position, 0.0005f);
    }

    GameObject NearestCity()
    {
        float Dist= 1000.0f;
        GameObject Nearest = null ;
        foreach(GameObject g in GameObject.FindGameObjectsWithTag("City"))
        {
            if (Vector3.Distance(g.transform.position, this.transform.position) < Dist) { Dist = Vector3.Distance(g.transform.position, this.transform.position); Nearest = g; };
        }
        return Nearest;
    }
}
