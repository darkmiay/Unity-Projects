using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum AttackType { melee, projectile }

public class PlayerAttack : MonoBehaviour
{

    public Transform projectileSpawn;
    public GameObject projectilePrefab;

    public int damage;
    public Character character;

    public AttackType attackType;
    public Sprite meleeIcon;
    public Sprite projectileIcon;
    public Image attackTypeImage;

    public float attackCooldown = 1f;
    public float currentCooldown;
    public Text cooldownText;
    public Animator animator;

    public GameObject hitBox;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentCooldown -= Time.deltaTime;
        currentCooldown = Mathf.Clamp(currentCooldown, 0f, attackCooldown);
        cooldownText.text = currentCooldown.ToString();

        if (Input.GetButtonDown("Fire2"))
        {
            ChangeAttackType();
        }

        if (Input.GetButtonDown("Fire1") && currentCooldown<=0)
        {
            currentCooldown = attackCooldown;
            if (attackType == AttackType.melee)
            {
                Attack();
            }
            else Shoot();


        }
    }

    void ChangeAttackType()
    {
        if (attackType == AttackType.melee)
        {
            attackType = AttackType.projectile;
            attackTypeImage.sprite = projectileIcon;
        }
        else
        {
            attackType = AttackType.melee;
            attackTypeImage.sprite = meleeIcon;
        }
    }

    void Attack()
    {
        animator.SetTrigger("Attack");
        hitBox.SetActive(true);
    }

    void Shoot()
    {
        Projectile projectileScript = Instantiate(projectilePrefab, projectileSpawn.position, projectileSpawn.rotation).GetComponent<Projectile>();
        projectileScript.SetDamage(damage);
        projectileScript.SetTeam(character.team);
        projectileScript.character = this.character;
    }
}
