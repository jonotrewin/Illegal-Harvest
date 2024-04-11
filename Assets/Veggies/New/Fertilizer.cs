using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fertilizer : Carryable
{
    int _uses = 5;

    int _nutritionIncrease =50;

    public void Use(VeggieStats veg)
    {
        _uses--;
     
        veg._fertilizerUsed = true;

    }
}
