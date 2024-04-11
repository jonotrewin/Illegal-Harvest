using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harvestable : BasicState
{
    Veggie veggie;
    VeggieStats veggieStats;

    float originalValue;
    public Harvestable(Veggie veggieStateMachine) : base("Harvestable", veggieStateMachine)
    {
        
    }

    public override void Enter()
    {
       
        veggie = stateMachine.GetComponent<Veggie>();
        veggieStats = stateMachine.GetComponent<VeggieStats>();
        stateMachine.GetComponent<Carryable>().CanPickUp(true);
        Debug.Log("harvest me");

        originalValue = veggieStats.value;
        base.Enter();
      
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        if(veggieStats._isHarvested) Rotting();
    }

    private void Rotting()
    {
        veggieStats._currentRot += veggieStats.rotIncreaseRate;

        if (veggieStats._currentRot >= 100)
        {
            veggieStats.value = 1;
        }
        else if (veggieStats._currentRot > 25)
        {
            veggieStats.value = (int)(originalValue / veggieStats._currentRot);
        }
    }
}
