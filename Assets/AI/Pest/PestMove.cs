using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PestMove : BasicState
{
    Pest pSM;

    


    public PestMove(Pest pestSM) : base("Move", pestSM) {
        pSM = pestSM;
    }

    public override void Enter()
    {
        base.Enter();
        pSM.NavMeshAgent.Resume();
        pSM.animator.SetBool("IsWalking", true);

        

        pSM.NavMeshAgent.destination = pSM._target.transform.position;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if (PlayerStats._player.CarriedItem == stateMachine.GetComponent<Carryable>())
        {

            pSM.ChangeStates(pSM._pestPickedUp);
        }
        if (Vector3.Distance(stateMachine.transform.position, pSM._target.transform.position) <= pSM.NavMeshAgent.stoppingDistance)
        {
            stateMachine.ChangeStates(pSM._pestAttack);
            Debug.Log("wtf");
        }
       
        if (pSM._target == null || pSM._target.gameObject.activeSelf == false || pSM._target == PlayerStats._player.CarriedItem)
        {
            pSM._target = null;
            stateMachine.ChangeStates(pSM._pestSearch);
        }
    }

    public override void Exit()
    {
        pSM.animator.SetBool("IsWalking", false);
      
        base.Exit();

    }
}
