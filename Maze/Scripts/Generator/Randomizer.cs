using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ValueChance
{

    public int value;

    public int chance;
}



public static class Randomizer
{

    public static int GetRandomValue(ValueChance[] values)
    {
        int sum = 0;
        foreach (ValueChance valueChance in values)
        {
            sum += valueChance.chance;
        }

        int random = Random.Range(1, sum);

        int i = values.Length;
        
        
        while (sum > random)
        {
            
            i--;
            sum -= values[i].chance;
        }

        return i;
    }


}