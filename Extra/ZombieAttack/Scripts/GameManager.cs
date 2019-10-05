using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private bool dead = false;
    public PlayerController playerController;

    public GameObject deadUI;
    public int killCount = 0;
    public Text killCountText;

    public float spawnCooldown=2f;
    public float currentCooldown=1f;
    public float zombieSpeed = 1f;
    public float speedIncRate =1.01f;
    public float cooldownIncRate = 1.01f;


    public Transform[] spawns;
    public GameObject ZombiePrefab;
    public GameObject ZombieParent;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        currentCooldown -= Time.deltaTime;

        if(currentCooldown<=0)
        {
            currentCooldown = spawnCooldown;
            SpawnEnemy();
            spawnCooldown /= cooldownIncRate;            
        }

        if(dead && Input.GetKey(KeyCode.E))
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }

    public void KillPlayer()
    {
        playerController.BlockControlls();
        deadUI.SetActive(true);
        dead = true;
    }

    public void SpawnEnemy()
    {
        EnemyController enemy = Instantiate(ZombiePrefab, spawns[Random.Range(0, spawns.Length)].position,Quaternion.identity, ZombieParent.transform).GetComponent<EnemyController>();
        enemy.speed = zombieSpeed;
    }

    public void KillEnemy()
    {
        killCount++;
        killCountText.text = killCount.ToString();
        zombieSpeed *= speedIncRate;
    }
}
