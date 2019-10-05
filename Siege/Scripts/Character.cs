using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{

    public List<Equipment> equipmentList;

    public enum CharacterType {Player, Warrior, Archer}

    public CharacterType characterType;
    public int MaxHealth = 10;
    private int Health;
    public int damage;
    public Image healthBar;
    public Text moneyUI;
    public Team team;
    public SkinnedMeshRenderer meshRenderer;
    public int money;

    public void TakeMoney(int money)
    {
        this.money += money;
        if(moneyUI!=null)
        {
            moneyUI.text = this.money.ToString();
        }  
    }

    void Start()
    {
        Health = MaxHealth;
        if(meshRenderer!=null)
        meshRenderer.material = team.material;
    }

    public void TakeDamage(int amount, Character damageDealer)
    {   
        Health -= amount;
        if (Health<=0)
        {
            if (damageDealer.characterType == CharacterType.Player)
            {
                damageDealer.TakeMoney(this.money);
            }
            else damageDealer.team.AddMoney(this.money);
            Die();
        }
        if (healthBar!=null)
        {
            healthBar.fillAmount = ((float)Health) / ((float)MaxHealth);
        }
    }

    void Die()
    {
        if(characterType!=CharacterType.Player)
        Destroy(this.gameObject);
        else
        {
            this.transform.position = team.waypoints[0].GetPoint();
            Health = MaxHealth;
        }
    }

    public void Equip(Item item)
    {
        foreach(Equipment equip in equipmentList)
        if(equip.type==item.type)
            {
           //     Transform[] oldEquip = equip.GetComponentsInChildren<Transform>();
           //     if(oldEquip.Length>=1)
           //     {
           //         foreach(Transform t in oldEquip)
           //         {
           //             Destroy(t.gameObject);
            //        }
            //    }
                Instantiate(item.prefab, equip.transform);             
            }
    }
}
