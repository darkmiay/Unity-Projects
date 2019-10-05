using UnityEngine;
using System.Collections;
using System;

public class ArmyScript : MonoBehaviour {

    public int UnitsCount;
    public int progressLevel;
  public  GameObject Destination;
  public GameObject StartEmpire;
  public CityScript City;
  public Color color;
  bool OnPosition;
  public bool DestinationIsCity;
	// Use this for initialization
	void Start () {
        StartEmpire = this.transform.parent.parent.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        if (StartEmpire != this.transform.parent.parent.gameObject) {
             City.war = UnityEngine.Random.Range(0, 1) == 1 ? true : false;
                        City.CityIsBisy = false;
            DestroyObject(this.gameObject);
        }
        if (DestinationIsCity)
        {
            if (StartEmpire == Destination.transform.parent.gameObject)
            {
                City.war = UnityEngine.Random.Range(0, 1) == 1 ? true : false;
                City.CityIsBisy = false;
                City.army += this.UnitsCount;
                DestroyObject(this.gameObject);
            }
        }
        else if (StartEmpire == Destination.transform.parent.parent.gameObject)
        {
            City.war = UnityEngine.Random.Range(0, 1) == 1 ? true : false;
            City.CityIsBisy = false;
            City.army += this.UnitsCount;
            DestroyObject(this.gameObject);
        }
        if (!OnPosition)
            if (Vector3.Distance(this.transform.position, Destination.transform.position) > 0.01f)
            {
                Move();
            }
            else
            {
                OnPosition = true;
  
                if (DestinationIsCity)
                {
                    CityScript cs = Destination.GetComponent<CityScript>();
                    float thisPower = UnitsCount * progressLevel * 0.25f + UnitsCount;
                    float EnemyPower = (cs.army * cs.progressLevel * 0.25f + cs.army) + (cs.population * cs.progressLevel * 0.125f + cs.population * 0.5f);
                    if (thisPower > EnemyPower)
                    {
                        cs.transform.parent = this.transform.parent.parent;
                        cs.TakeColor(this.color);
                        cs.population = 0;
                        cs.EmpireColor = (this.color);
                        cs.army = Convert.ToInt32((thisPower - EnemyPower) / 1.25f);
                        City.war = UnityEngine.Random.Range(0, 1) == 1 ? true : false;
                        City.CityIsBisy = false;
                        DestroyObject(this.gameObject);
                    }
                    else
                    {
                        cs.population = Convert.ToInt32((EnemyPower - thisPower) / 1.625f);
                        cs.army = 0;
                        City.war = UnityEngine.Random.Range(0, 1) == 1 ? true : false;
                        City.CityIsBisy = false;
                        DestroyObject(this.gameObject);
                    }
                }
                else
                {

                        FlagScript cs = Destination.GetComponent<FlagScript>();
                        float thisPower = UnitsCount * progressLevel * 0.25f + UnitsCount;
                        float EnemyPower = (cs.army * cs.progressLevel * 0.25f + cs.army) + (cs.population * cs.progressLevel * 0.125f + cs.population * 0.5f);
                        if (thisPower > EnemyPower)
                        {
                            cs.transform.parent = this.transform.parent.parent;
                            cs.CityCor = this.transform.parent.gameObject;
                            cs.TakeColor(this.color);
                            cs.population = 0;
                            cs.color = this.color;
                            cs.army = Convert.ToInt32((thisPower - EnemyPower) / 1.25f);
                            City.war = UnityEngine.Random.Range(0, 1) == 1 ? true : false;
                            City.CityIsBisy = false;
                            DestroyObject(this.gameObject);
                        }
                        else
                        {
                            cs.population = Convert.ToInt32((EnemyPower - thisPower) / 1.625f);
                            cs.army = 0;
                            City.war = UnityEngine.Random.Range(0, 1) == 1 ? true : false;
                            City.CityIsBisy = false;

                            DestroyObject(this.gameObject);
                        }
                    }
                
            }
       
	}
    void Move()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, Destination.transform.position, 0.0005f);
    }
}
