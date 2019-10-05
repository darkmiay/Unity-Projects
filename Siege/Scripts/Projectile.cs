using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float speed;
    public Rigidbody body;
    public Team team;
    public Character character;
    private int damage;

    void Start()
    {
        body.velocity = transform.up * speed;
    }

    public void SetDamage(int newDamage)
    {
        damage = newDamage;
    }

    public void SetTeam(Team newTeam)
    {
        team = newTeam;
    }

    private void OnTriggerEnter(Collider other)
    {
        Character enemy = other.GetComponent<Character>();
        if (enemy!=null && enemy.team!=this.team)
        {
            enemy.TakeDamage(damage,character);
        }
        Destroy(this.gameObject);
    }

}
