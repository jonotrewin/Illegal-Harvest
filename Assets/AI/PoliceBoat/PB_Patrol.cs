using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PB_Patrol : BasicState
{
    public PB_Patrol(PoliceBoatSM policeBoatSM ) : base("Patrol", policeBoatSM)
    {
        pbSM = policeBoatSM;
    }
    
    PoliceBoatSM pbSM;

    int currentWaypoint = 0;

    
    public override void Enter()
    {
        pbSM.agent.speed = pbSM.stats._patrolSpeed;
        base.Enter();
        
    }


    // Update is called once per frame
    public override void UpdatePhysics()
    {
        PatrolMovement();

        base.UpdatePhysics();
    }


    private void PatrolMovement()
    {
        pbSM.agent.destination = pbSM.stats.patrolWaypoints[currentWaypoint].transform.position;

       // Debug.Log(pbSM.agent.destination);

        if (pbSM.agent.destination == null)
        {
            pbSM.agent.destination = pbSM.stats.patrolWaypoints[0].transform.position;
        }

        if (Vector3.Distance(pbSM.transform.position, pbSM.agent.destination) <= pbSM.agent.stoppingDistance)
        {

            currentWaypoint++;
            if (currentWaypoint >= pbSM.stats.patrolWaypoints.Count)
            {
                currentWaypoint = 0;
            }
        }
    }

    public override void UpdateLogic()
    {
        pbSM.stats.CheckForEnemies();

        if(pbSM.stats._targetSeen)
        {
           
            pbSM.ChangeStates(pbSM.pb_Chase);
        }

        base.UpdateLogic();
    }

    
}
