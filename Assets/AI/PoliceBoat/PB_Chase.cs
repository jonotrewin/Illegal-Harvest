using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PB_Chase : BasicState
{
    public PB_Chase(PoliceBoatSM policeBoatSM) : base("Chase", policeBoatSM)
    {
        pbSM = policeBoatSM;
    }

    PoliceBoatSM pbSM;


    public override void Enter()
    {
        pbSM.stats._sightRadius *= 1.5f;
        pbSM.agent.speed = pbSM.stats._chaseSpeed;
        Minimap.animator.SetBool("BeingChased", true);
        base.Enter();
    }
    public override void UpdateLogic()
    {
       

        pbSM.agent.destination = pbSM.stats._target.transform.position;

        CheckForBoat();

         

        base.UpdateLogic();
    }

    private void CheckForBoat()
    {
        Collider[] colliders = Physics.OverlapSphere(pbSM.transform.position, pbSM.stats._sightRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent<BoatMovement>(out BoatMovement _)) return;

            
        }
        if(!pbSM.stats.CheckInView(pbSM.gameObject,pbSM.stats._target))
        {
            pbSM.ChangeStates(pbSM.pb_Search);
        }

        
        
    }
  

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }

    public override void Exit()
    {
        pbSM.stats._sightRadius /= 1.5f;
        Minimap.animator.SetBool("BeingChased", false);
        base.Exit();
    }


}
