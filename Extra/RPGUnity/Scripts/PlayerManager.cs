using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour {

    #region Singleton

    public static PlayerManager instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    public GameObject player;

    public void Update()
    {
        if (Input.GetButtonDown("AbilitySlot1"))
            player.GetComponent<AbilityManager>().UseAbility(player.GetComponent<AbilityManager>().abilities[0]);
        if (Input.GetButtonDown("AbilitySlot2"))
            player.GetComponent<AbilityManager>().UseAbility(player.GetComponent<AbilityManager>().abilities[1]);
    }

    public void KillPlayer()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
