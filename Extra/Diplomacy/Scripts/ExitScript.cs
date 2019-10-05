using UnityEngine;
using System.Collections;

using UnityEngine.SceneManagement;



public class ExitScript : MonoBehaviour {

    public GameObject gj2;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        GameObject[] gj = GameObject.FindGameObjectsWithTag("Empire");
        if (gj.Length == 1) gj2.SetActive(true);
        if (Input.GetButtonDown("Cancel"))
        {
      
            SceneManager.LoadScene("Menu");
            SceneManager.UnloadScene("Game");
        }
        if (Input.GetButtonDown("Jump"))
{
    SceneManager.UnloadScene("Game");
    SceneManager.LoadScene("Game");
    
}
	}
}
