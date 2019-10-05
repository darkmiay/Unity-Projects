using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ToMenuScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        SceneManager.LoadScene("Menu");
        SceneManager.UnloadScene("About");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
