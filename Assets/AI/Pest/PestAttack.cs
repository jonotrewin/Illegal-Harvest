using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PestAttack : BasicState
{
    public PestAttack(Pest pestSM) : base("Attack", pestSM) { }

    Pest pSM;
    VeggieStats target;
    bool isEating = false;

    public override void Enter()
    {

        
        pSM = stateMachine.GetComponent<Pest>();
        target = pSM._target;
        pSM._target.slugs.Add(pSM);
        pSM.NavMeshAgent.Resume();
        pSM.currentAttackTimer = pSM.AttackTimerThreshold;




    }

    private Vector3 CalculatePositionAroundVeggie(float angle, VeggieStats specificTarget)
    {
        float radiansAngle = Mathf.Deg2Rad * angle;
        float x = specificTarget.transform.position.x + pSM.NavMeshAgent.stoppingDistance * Mathf.Cos(radiansAngle);
        float z = specificTarget.transform.position.z + pSM.NavMeshAgent.stoppingDistance * Mathf.Sin(radiansAngle);

        return new Vector3(x, stateMachine.transform.position.y, z);
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if (PlayerStats._player.CarriedItem == stateMachine.GetComponent<Carryable>())
        {

            pSM.ChangeStates(pSM._pestPickedUp);
        }
            float angle = pSM._target.slugs.IndexOf(pSM) * (360f / pSM._target.slugs.Count);
        pSM.NavMeshAgent.destination = CalculatePositionAroundVeggie(angle, pSM._target);

        pSM.transform.LookAt(pSM._target.transform.position);

        if (!pSM.addedToSlugCount)
        {
            
            pSM.addedToSlugCount = true;
        }

        if (pSM.currentAttackTimer<pSM.AttackTimerThreshold)
        {
            pSM.currentAttackTimer++;
        }
        else if(pSM.currentAttackTimer>=pSM.AttackTimerThreshold && !isEating)
        {
            pSM.StartCoroutine(Attack());
            
        }


        if(pSM._target == null || pSM._target.gameObject.activeSelf == false || pSM._target == PlayerStats._player.CarriedItem)
        {
            pSM._target = null;
            stateMachine.ChangeStates(pSM._pestSearch);
        }

        if (Vector3.Distance(stateMachine.transform.position, pSM._target.transform.position) >= pSM.NavMeshAgent.stoppingDistance)
        {
            stateMachine.ChangeStates(pSM._pestSearch);
            Debug.Log("wtf");
        }



    }

    private IEnumerator Attack()
    {
        isEating=true;
        pSM.animator.SetBool("IsEating", true);
        pSM.animator.SetBool("IsIdle", false);
        pSM.animator.SetBool("IsMoving", false);
        target.Damage(pSM.damageAmount);
        yield return new WaitForSeconds(3);
        pSM.animator.SetBool("IsEating", false);
        pSM.animator.SetBool("IsIdle", true);
        pSM.currentAttackTimer = 0;
        isEating = false;

    }

    public override void Exit()
    {
        pSM.animator.SetBool("IsEating", false);
        base.Exit();
    }
}
