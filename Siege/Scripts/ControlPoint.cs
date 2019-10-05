using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlPoint : MonoBehaviour
{

    public Team team;
    public Light lightColor;
    public MeshRenderer meshRenderer;
    public float contestRadius;
    public LayerMask layerMask;
    public float contest;
    public float contestValue;
    public Image contestBar;

    // Start is called before the first frame update
    void Start()
    {
        if (team!=null)
        {
            meshRenderer.material = team.material;
            lightColor.color = team.color;
            contestBar.color = team.color;               
        }
    }

    // Update is called once per frame
    void Update()
    {
        Team contestTeam;
        Collider[] nearestUnits = Physics.OverlapSphere(this.transform.position, contestRadius, layerMask);
        if(nearestUnits.GetLength(0)>0)
        {
            contestTeam = nearestUnits[0].GetComponent<Character>().team;
            foreach (Collider col in nearestUnits)
            {
                Team unitTeam = col.GetComponent<Character>().team;
                //If there's units from more than one team
                if (contestTeam != unitTeam) return;                                         
            }
            
            if (contestTeam == team)
            {
                contest += contestValue;
            }
   
            else
            {
                contest -= contestValue;
            }
            contest = Mathf.Clamp(contest, 0f, 1f);
            ChangeFill(contest);
            if (contest <= 0f && contestTeam != team)
            {
                team = contestTeam;
                meshRenderer.material = team.material;
                lightColor.color = team.color;
                contestBar.color = team.color;
            }
        }

      

     }

    void ChangeFill(float value)
    {
        if (contestBar != null)
        {
            contestBar.fillAmount = contest;
            // contestBar.fillAmount = Mathf.Clamp(contestBar.fillAmount, 0f, 1f);
        }
    }
}
