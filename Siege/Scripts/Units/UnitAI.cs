using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitAI : MonoBehaviour
{

    const float locomationAnimationSmoothTime = .1f;

    public NavMeshAgent agent;
    public Animator animator;
    public Character character;
    public float agressionRadius = 20f;
    private GameObject nearestEnemy;
    public LayerMask layerMask;
    public float attackCooldown = 1f;
    public float currentCooldown;
    public AttackType attackType;

    public Transform projectileSpawn;
    public GameObject projectilePrefab;
    
    public WayPoint currentWaypoint;
    private Vector3 currentTarget;

    public GameObject hitBox;

    private void Start()
    {
        currentTarget = currentWaypoint.GetPoint();
        agent.SetDestination(currentTarget);
        currentCooldown = attackCooldown;
    }

    private void Update()
    {
        currentCooldown -= Time.deltaTime;
        currentCooldown = Mathf.Clamp(currentCooldown, 0f, attackCooldown);
        //calculate speed
        float speedPercent = agent.velocity.magnitude / agent.speed;
        animator.SetFloat("SpeedPercent", speedPercent, locomationAnimationSmoothTime, Time.deltaTime);

        if(AttackType.melee==attackType)
        {
            MeleeAttack();
        }
        else
        {
            ProjectileAttack();
        }

    }

    void MeleeAttack()
    {
        Collider[] nearestUnits = Physics.OverlapSphere(this.transform.position, agressionRadius, layerMask);
        foreach (Collider col in nearestUnits)
        {
            if (col.GetComponent<Character>() == null) continue;
            if (col.GetComponent<Character>().team != this.character.team)
            {
                if (nearestEnemy != null)
                {
                    if (Vector3.Distance(nearestEnemy.transform.position, this.transform.position) > Vector3.Distance(col.transform.position, this.transform.position))
                    {
                        nearestEnemy = col.gameObject;                     
                    }
                }
                else
                {
                    nearestEnemy = col.gameObject;
                }
            }
        }
        if (nearestEnemy != null)
        {
            currentTarget = nearestEnemy.transform.position;
            if (Vector3.Distance(nearestEnemy.transform.position, this.transform.position) <= agent.stoppingDistance)
            {
                FaceTarget();
                if (currentCooldown <= 0)
                {
                  
                    currentCooldown = attackCooldown;
                    //attack
                    animator.SetTrigger("Attack");
                    hitBox.SetActive(true);
                }
            }

            agent.SetDestination(nearestEnemy.transform.position);

        }
        else if (currentWaypoint!=null)
        {

            if (Vector3.Distance(agent.destination, transform.position) <= currentWaypoint.radius)
            {
                if(currentWaypoint.type==WaypointType.Control)
                {
                    if(currentWaypoint.controlPoint.contest<1f || currentWaypoint.controlPoint.team!=character.team)
                    {

                    }
                    else
                    {
                        currentWaypoint = currentWaypoint.nextPoint();
                    }
                }
                else
                {
                    currentWaypoint = currentWaypoint.nextPoint();
                }
          
                currentTarget = currentWaypoint.GetPoint();

            }
            agent.SetDestination(currentTarget);

        }
        //Attack nearest enemy or contest next point if there's no enemys

    }

        void ProjectileAttack()
        {
        //Find nearest enemy in shoot radius
        if (nearestEnemy!=null)
        {
            if(Vector3.Distance(this.transform.position, nearestEnemy.transform.position) > agressionRadius)
            {
                nearestEnemy = null;
            }
        }
    
        if (nearestEnemy == null)
        {
            GameObject[] units = GameObject.FindGameObjectsWithTag("Unit");
      //      GameObject[]
            foreach (GameObject unit in units)
        {
            if(Vector3.Distance(this.transform.position,unit.transform.position)<=agressionRadius && unit.GetComponent<Character>().team != this.character.team)
            {
                    nearestEnemy = unit;
                    break;
            }
        }
        }
        
        if (nearestEnemy!=null && currentCooldown <= 0)
        {
                currentCooldown = attackCooldown;
                Shoot();
        }
        else if (currentWaypoint!=null)
        {
            if (Vector3.Distance(agent.destination, transform.position) <= currentWaypoint.radius)
            {
                if (currentWaypoint.type == WaypointType.Control)
                {
                    if (currentWaypoint.controlPoint.contest < 1f || currentWaypoint.controlPoint.team != character.team)
                    {

                    }
                    else
                    {
                        currentWaypoint = currentWaypoint.nextPoint();
                    }
                }
                else
                {
                    currentWaypoint = currentWaypoint.nextPoint();
                }

                currentTarget = currentWaypoint.GetPoint();

            }
            agent.SetDestination(currentTarget);

        }
    }

    private void Shoot()
    {
        transform.LookAt(nearestEnemy.transform);
        Projectile projectileScript = Instantiate(projectilePrefab, projectileSpawn.position, projectileSpawn.rotation).GetComponent<Projectile>();
        projectileScript.SetDamage(character.damage);
        projectileScript.SetTeam(character.team);
        projectileScript.character = this.character;

    }

    void FaceTarget()
    {
        Vector3 direction = (currentTarget - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

}
