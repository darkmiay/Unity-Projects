using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public Character character;
    public Animator animator;
    public List<Character> wasAttacked;
    public bool attackWas = false;

    private void Update()
    {

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            attackWas = true;
        }
        else if(attackWas)
        {
            attackWas = false;
            this.gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        wasAttacked = new List<Character>();
    }   

    private void OnTriggerEnter(Collider other)
    {
        //задели ли мы персонажа
        Character enemyCharacter = other.GetComponent<Character>();
        if (enemyCharacter == null) return;

        //есть ли он в списке тех кого мы уже задевали за эту атаку
        foreach (Character chr in wasAttacked)
        {
            if (chr == enemyCharacter) return;

        }

        //если персонаж из вражеской тимы - он получит урон
        if (character.team != enemyCharacter.team)
        {
            enemyCharacter.TakeDamage(this.character.damage, character);
            wasAttacked.Add(enemyCharacter);
        }

    }


}
