using UnityEngine;
using System.Collections;

public class CityScript : MonoBehaviour {

   public int gold = 0;
   public int recources = 0;
   public int population = 0;
   public int army = 0;
   public int progress = 0;

    int goldGain = 1;
    int flagCount = 0;
    int resourcesGain = 1;
    int foodGain = 1;
    int progressGain = 1;
   public int progressLevel = 1;
    float startTime;

    public bool CityIsBisy = false;
   public bool war = false;

    SpriteRenderer spriteRenderer;
    Generator script;
    BiomeType biomeType;
    public EmpireScript Empire;
    public GameObject FlagPrefab;
    public GameObject ArmyPrefab;
    public GameObject Destination;
    public Color EmpireColor;

	// Use this for initialization
	void Start () {
        SpawnPoint();
        EmpireScript Empire = GetComponentInParent<EmpireScript>();
           script = GameObject.Find("Generator").GetComponent<Generator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        biomeType = script.GetTileBiome(this.transform.position.x, this.transform.position.y);
        string name = biomeType + "_house";
        foreach (Sprite s in script.sprites)
        {
            if (s.name == name) spriteRenderer.sprite = s;
        }
        EmpireColor = Empire.EmpireColor;
        spriteRenderer.color = Empire.EmpireColor;
	}

  public Color GiveColor()
    {
        return EmpireColor;
    }

    public void TakeColor(Color c)
  {
      spriteRenderer = GetComponent<SpriteRenderer>();
      spriteRenderer.color = c;
  }

  void SpawnPoint()
  {
      GameObject[] allterrains;
      GameObject[] allempires;
      Vector3 newVector;
      newVector = new Vector3(UnityEngine.Random.Range(-0.45f, 0.45f),
           UnityEngine.Random.Range(-0.45f, 0.45f), 0);
      allempires = GameObject.FindGameObjectsWithTag("City");
      allterrains = GameObject.FindGameObjectsWithTag("Terrain");


      while (!NoObjectsNear(allempires, newVector, 0.2f) && (!NoObjectsNear(allterrains, newVector, 0.1f)))
          newVector = new Vector3(UnityEngine.Random.Range(-0.45f, 0.45f),
         UnityEngine.Random.Range(-0.45f, 0.45f), 0);
      this.transform.position = newVector;
  }

  public bool NoObjectsNear(GameObject[] allterrains, Vector3 newVector, float dimension = 0.2f)
  {
      if (allterrains.Length != 0)
          foreach (GameObject g in allterrains)
          {
              if (Vector3.Distance(g.transform.position, newVector) < dimension) return false;
          }

      return true;
  }

	// Update is called once per frame
	void Update () {

        
       
        var deltaTime = Time.time - startTime;
        if (deltaTime >= 1)
        {
            gold += goldGain;
            recources += resourcesGain;
            population += foodGain;
            progress += progressGain;
            startTime = Time.time;
        }
      if (!CityIsBisy)
      {
          GameObject EmptyTerrain;
          if (NearestEmptyTerrain(out EmptyTerrain) && flagCount < progressLevel)
          {
              Destination = EmptyTerrain;
              flagCount++;
              StartExpidition(EmptyTerrain);

          }
          if(progressLevel>=2)
          {
              GameObject nearestEnemy;
              bool DestinationIsCity;
              if (NearestEnemy(out nearestEnemy, out DestinationIsCity))
              {
                  SendArmy(nearestEnemy, DestinationIsCity);
              }
          }
         
      }
      if (ReadyForLVLUP()) LVLUP();
	}

    void SendArmy(GameObject Destination2, bool DestinationIsCity)
    {
       int Sarmy = gold;
        gold = 0;
        if (population > recources)
        {
            Sarmy += recources;
            recources = 0;
            population -= recources;
        }
        else
        {
            Sarmy += population;
            population = 0;
            recources -= population;
        }
        CityIsBisy = true;
        GameObject army = Instantiate(ArmyPrefab, this.transform.position, Quaternion.identity) as GameObject;
        army.transform.parent = this.transform;
        ArmyScript armyScript = army.GetComponent<ArmyScript>();
        armyScript.City = this.gameObject.GetComponent<CityScript>();
        armyScript.Destination = Destination2;
        armyScript.color = GiveColor();
        armyScript.progressLevel = this.progressLevel;
        armyScript.UnitsCount = Sarmy;
        armyScript.DestinationIsCity = DestinationIsCity;
        SpriteRenderer sr = army.GetComponent<SpriteRenderer>();
        sr.color = GiveColor();
        this.army = 0;
        CityIsBisy = true;
    }
    void LVLUP()
    {
        war = Random.Range(0, 2)==1? true : false;
          goldGain ++;
          resourcesGain++;
    foodGain ++;
 progressGain ++;
   progressLevel ++;
        if (progressLevel==2)
        {     
               string name = biomeType + "_city";
        foreach (Sprite s in script.sprites)
        {
            if (s.name == name) spriteRenderer.sprite = s;
        }
               script = GameObject.Find("Generator").GetComponent<Generator>();
        }
      
    }

    bool ReadyForLVLUP()
    {
        if (this.gold >= (50 * this.progressLevel * this.progressLevel) &&
            this.recources >= (50 * this.progressLevel * this.progressLevel) &&
            this.population >= (50 * this.progressLevel * this.progressLevel) &&
            this.progress >= (100 * this.progressLevel * this.progressLevel)) return true;

        return false;

    }

    void StartExpidition(GameObject EmptyTerrain)
    {
       GameObject flag = Instantiate(FlagPrefab,this.transform.position,Quaternion.identity) as GameObject;
       flag.transform.parent = this.transform;
       FlagScript fs = flag.GetComponent<FlagScript>();
       fs.progressLevel = this.progressLevel;
       MapObject scr = EmptyTerrain.GetComponent<MapObject>();
       scr.flag = flag;
       CityIsBisy = true;
    }

    bool NearestEmptyTerrain(out GameObject EmptyTerrain,float dimension=0.5f)
    {
        float dist = 100f ;
        GameObject newTer=null;
        GameObject[] allterrains = GameObject.FindGameObjectsWithTag("Terrain");
       if (allterrains.Length != 0)
       foreach(GameObject g in allterrains)
       {
           MapObject scr = g.GetComponent<MapObject>();
           if (Vector3.Distance(g.transform.position, this.transform.position) < dimension) 
           {
               if (dist > Vector3.Distance(g.transform.position, this.transform.position) && scr.flag==null)
               {
                   dist =
                       Vector3.Distance(g.transform.position, this.transform.position);
                   newTer = g;
               }
           }
       }
       if (newTer==null)
       EmptyTerrain = null;
       else { 
           EmptyTerrain = newTer;

           return true; }
       return false;
  
    }
    bool NearestEnemy(out GameObject nearestEnemy, out bool DestinationIsCity)
    {
        DestinationIsCity = true;
        float dist = 100f;
        GameObject newTer = null;
        GameObject[] allCitys = GameObject.FindGameObjectsWithTag("City");
        GameObject[] allFlags = GameObject.FindGameObjectsWithTag("Flag");
        foreach (GameObject g in allCitys)
            {
                CityScript scr = g.GetComponent<CityScript>();
              
                    if (dist > Vector3.Distance(g.transform.position, this.transform.position) && g != this.gameObject && g.transform.parent!=this.transform.parent)
                    {
                        dist =
                            Vector3.Distance(g.transform.position, this.transform.position);
                        newTer = g;
                        DestinationIsCity = true;
                    }
                
            }
        foreach (GameObject g in allFlags)
        {
            FlagScript scr = g.GetComponent<FlagScript>();

            if (dist > Vector3.Distance(g.transform.position, this.transform.position) && scr.CityCor.transform.parent != this.transform.parent)
            {
                dist =
                    Vector3.Distance(g.transform.position, this.transform.position);
                newTer = g;
                DestinationIsCity = false;
            }

        }
        if (newTer == null)
            nearestEnemy = null;
        else
        {
            nearestEnemy = newTer;

            return true;
        }
        return false;
    }
}
