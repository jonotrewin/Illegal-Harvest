using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Growing : BasicState
{
    public Growing(Veggie veggieStateMachine) : base("Growing", veggieStateMachine) 
    {

    }
    Veggie veggie;
    VeggieStats veggieStats;

    public override void Enter()
    {
        base.Enter();

        veggie = stateMachine.GetComponent<Veggie>();
        veggieStats = stateMachine.GetComponent<VeggieStats>();
        stateMachine.gameObject.transform.localScale = Vector3.zero;

       


    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        Dehydrate();
        Grow();
       
        
      
    }

    public override void Exit()
    {
        CalculateValue();
        base.Exit();

    }

    private void CalculateValue()
    {
        float healthModifier = veggieStats._currentHealth / 100;
        veggieStats.value = (int)(stateMachine.gameObject.transform.localScale.x * veggieStats.baseValue * healthModifier);
    }

  

    private void Dehydrate()
    {
        //dehydrate each second
        veggieStats._waterPercentage -= veggieStats.waterReductionRate;

        float waterDamage = 0;
        Mathf.Clamp(veggieStats._waterPercentage, 0, veggieStats._maxWaterPercentage);
        //waterAmount when under- or overwatered
        if(veggieStats._waterPercentage>100)
        {
            waterDamage = veggieStats._waterPercentage - 100;
        }
        if (veggieStats._waterPercentage < 25)
        {
            waterDamage = 25 - veggieStats._waterPercentage;
        }
        //apply waterAmount
        float correctedDamage = waterDamage * 0.0001f * veggieStats._waterDamageMultiplier;
        veggieStats._currentHealth -= correctedDamage;


        //the longer without water, the smaller it will be
        //veggieStats._finalGrowthSize = new Vector3(veggieStats._finalGrowthSize.x + 0.5f - correctedDamage, veggieStats._finalGrowthSize.x +0.5f - correctedDamage, veggieStats._finalGrowthSize.x + 0.5f - correctedDamage);
        

    }

    private void Grow()
    {

        SetGrowthSize();

        //grows
        veggie.gameObject.transform.localScale += Vector3.one * Time.deltaTime * veggieStats.growthSpeed * (1 + Convert.ToInt32(veggieStats._hasCompost));

        if (veggieStats.gameObject.transform.localScale.x >= veggieStats._finalGrowthSize.x)
        {
            stateMachine.ChangeStates(veggie._harvestableState);
            Debug.Log("Ready!");
        }

    }

    private void SetGrowthSize()
    {
        veggieStats._finalGrowthSize = Vector3.one * ((veggieStats._currentHealth) * 0.01f);
    }
}
