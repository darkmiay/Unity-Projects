using UnityEngine;
using System.Collections;

public class CaravanScript : MonoBehaviour {

   public int gold = 0;
   public int recources = 0;
   public int population = 0;
   public int progress = 0;
   public GameObject Destination;
  public CityScript City;
  public Color color;

	// Use this for initialization
	void Start () {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.color = color;
	}
	
	// Update is called once per frame
	void Update () {
        if (Vector3.Distance(this.transform.position, Destination.transform.position) > 0.01f)
        {
            Move();
        }
        else 
        {
            City.gold += gold;
            City.population += population;
            City.recources += recources;
            City.progress += progress;
            DestroyObject(this.gameObject);
        }
	}

    void Move()
    {
        this.transform.position = Vector3.MoveTowards(transform.position, Destination.transform.position, 0.001f);

    }
}
