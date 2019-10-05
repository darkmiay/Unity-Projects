using UnityEngine;
using System.Collections;

public class EmpireScript : MonoBehaviour {

    public Color EmpireColor;
	// Use this for initialization
	void Start () {
        EmpireColor = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), 1.0f);

	}
	
	// Update is called once per frame
	void Update () {
        if (this.transform.childCount == 0)
            DestroyObject(this.gameObject);

	}
    
    void FindAllArmyS()
    {
        GameObject[] Armys = GameObject.FindGameObjectsWithTag("Army");
        foreach(GameObject g in Armys)
        {
            ArmyScript ass = g.GetComponent<ArmyScript>();
            if (ass.color == this.EmpireColor) DestroyObject(g);
        }
    }
}
