using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team : MonoBehaviour
{

    public List<ItemSetup> itemSetup;

    public List<WayPoint> waypoints;

    public float spawnDelay=120f;
    private float spawnCooldown=0f;

    public float giveAwayDelay=1f;
    private float giveAwayCooldown = 0f;
    public int giveAwayMoney = 1;

    public List<Character> players;
    public int money = 0;

    public int unitSpawn = 20;
    public GameObject unitPrefab;

    public Color color;
    public Material material;

    private void Start()
    {
        material.color = color;
    }

    private void Update()
    {
        if (spawnCooldown <= 0f)
        {
            spawnCooldown = spawnDelay;

            Item bodyPrefab = itemSetup[0].GetItem(ItemType.Armor);
            foreach (WayPoint point in waypoints)
            {
                for (int i = 0; i < unitSpawn; i++)
                {
                    GameObject unit = Instantiate(bodyPrefab.prefab, point.GetPoint(), Quaternion.identity);
                    Character ch = unit.GetComponent<Character>();
                    ch.team = this;
                    unit.GetComponent<UnitAI>().currentWaypoint = point.nextPoint();
                 
                    foreach (Item item in itemSetup[0].setup)
                    {

                                ch.Equip(item);
                      
                    }
                }
            }

            if(players.Count>0)
            {
                int playerMoney = money / players.Count;
                Debug.Log(money);
                Debug.Log(playerMoney);
                foreach (Character player in players)
                {
                    player.TakeMoney(playerMoney);
                    money -= playerMoney;
                }
            }       
           
        }
        else spawnCooldown -= Time.deltaTime;

        if (giveAwayCooldown <= 0f)
        {
            giveAwayCooldown = giveAwayDelay;
            foreach (Character player in players)
            {
                player.TakeMoney(giveAwayMoney);
            }
        }
        else giveAwayCooldown -= Time.deltaTime;
        }

    public void AddMoney(int money)
    {
        this.money += money;
    }

    public ItemSetup GetItemSetup(Character.CharacterType characterType)
    {
        foreach(ItemSetup setup in itemSetup)
        {
            if (setup.type == characterType) return setup;
        }

        return null;
    }


}
