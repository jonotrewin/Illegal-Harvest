using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PestSearch : BasicState
{
    VeggieStats[] Veggies;

    Pest pSM;
    

    public PestSearch (Pest pestStateMachine) : base("Search", pestStateMachine) { }

    public override void Enter()
    {
        base.Enter();
       
        pSM = stateMachine.GetComponent<Pest>();
        pSM.animator.SetBool("isIdle", true);
    }

    public override void UpdateLogic()
    {
        if (PlayerStats._player.CarriedItem == stateMachine.GetComponent<Carryable>())
        {
           
            pSM.ChangeStates(pSM._pestPickedUp);
        }

        if (pSM._target ==null)
        {
            Debug.Log("no veggies");
            Veggies = GameObject.FindObjectsByType<VeggieStats>(FindObjectsSortMode.None);
            FindTarget();
            return;
        }
        base.UpdateLogic();
        if(pSM._target != null)
        {
            Debug.Log("made it");
            stateMachine.ChangeStates(pSM._pestMove);
        }

       
    }

    private void FindTarget()
    {
      
       

        VeggieStats closestVeggie = null;

        

        pSM._target = Veggies[Random.Range(0,Veggies.Length-1)];
        if(pSM._target._isHarvested)
        {
            pSM._target = null;
        }
        
    }

    public override void Exit()
    {
        pSM.animator.SetBool("IsIdle", false);
        base.Exit();
    }
}
