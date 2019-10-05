using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    //pool for each object (items, units)

    public int roundTime=20;

    private int currentMin;
    private int currentSec;

    public Text timeText;

    private const float Second = 1f;
    private const int OneMinuteSeconds=60;
    private float timePercent;

    // Start is called before the first frame update
    void Start()
    {
        currentMin = roundTime;
        currentSec = 0;
        timePercent = Second;
        UpdateTimerText();
    }

    // Update is called once per frame
    void Update()
    {
        timePercent -= Time.deltaTime;
        if(timePercent<=0)
        {
            timePercent = Second;
            if (currentSec <= 0)
            {
                if (currentMin <= 0)
                {
                    GameEnd();
                }
                else currentMin--;
                currentSec = OneMinuteSeconds - 1;
                UpdateTimerText();
            }
            else
            {
                currentSec--;
                UpdateTimerText();
            }

        }
    }

    void GameEnd()
    {

    }

    void UpdateTimerText()
    {
        if (timeText != null)
        timeText.text = currentMin + ":" + currentSec;
    }
}
