using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PestPickedUp : BasicState
{
    public PestPickedUp(Pest pestSM) : base("PickedUp", pestSM) { }

    Pest pSM;
  

    public override void Enter()
    {

        pSM = stateMachine.GetComponent<Pest>();
        pSM.NavMeshAgent.enabled = false;
        pSM.animator.SetBool("IsCarried", true);
        pSM = stateMachine.GetComponent<Pest>();
        pSM._target?.slugs.Remove(pSM);
        pSM._target = null;
        stateMachine.gameObject.transform.Rotate(90, 0, 0);




    }

    public override void UpdateLogic()
    {
        if(PlayerStats._player.CarriedItem != stateMachine.GetComponent<Carryable>())
        {
            stateMachine.ChangeStates(pSM._pestSearch);
        }
    }

    public override void Exit()
    {
        pSM.animator.SetBool("IsCarried", false);
        pSM.NavMeshAgent.enabled = true;
        base.Exit();
    }


}
