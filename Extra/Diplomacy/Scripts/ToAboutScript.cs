using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ToAboutScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        SceneManager.LoadScene("About");
        SceneManager.UnloadScene("Menu");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
