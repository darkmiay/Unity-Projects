using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MyMath {

    public static void Swap(ref int a, ref int b)
    {
        int x = a;
        a = b;
        b = x;
    }

}
