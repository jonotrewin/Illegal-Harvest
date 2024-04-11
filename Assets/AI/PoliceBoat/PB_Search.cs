using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PB_Search : BasicState
{


    public PB_Search(PoliceBoatSM policeBoatSM) : base("Search", policeBoatSM)
    {
        pbSM = policeBoatSM;
    }

    

    PoliceBoatSM pbSM;

    public override void Enter()
    {
        pbSM.stats._target = null;
        pbSM.stats._targetSeen = false;
        base.Enter();
    }

    public override void UpdatePhysics()
    {
        Search();
    }

    public override void UpdateLogic()
    {
        pbSM.stats._currentSearchTimer++;

        if(pbSM.stats._currentSearchTimer>=pbSM.stats._searchTimerThreshold)
        {
            pbSM.ChangeStates(pbSM.pb_Patrol);
        }
        
        pbSM.stats.CheckForEnemies();

        if (pbSM.stats._targetSeen)
        {
            
            pbSM.ChangeStates(pbSM.pb_Chase);
        }

        base.UpdateLogic();
    }
    public override void Exit()
    {
        pbSM.stats._currentSearchTimer = 0;
        base.Exit();
    }

    private void Search()
    {
        if (Vector3.Distance(pbSM.transform.position,pbSM.agent.destination) <= pbSM.agent.stoppingDistance)
        {

            Vector3 randomDirection = Random.insideUnitSphere * 100f; // 50f is the patrol radius
            randomDirection += pbSM.transform.position;
            NavMeshHit hit;
            NavMesh.SamplePosition(randomDirection, out hit, 100f, 1);
            Vector3 finalPosition = hit.position;


            pbSM.agent.destination = finalPosition;
            
        }

    }
}

