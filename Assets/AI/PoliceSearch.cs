using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class PoliceSearch : MonoBehaviour
{
    NavMeshAgent _agent;
    PoliceChaser _chaser;
    PoliceFighter _fighter;


    int _currentMoveTimer;
    int _moveTimerThreshold = 4000;

    [SerializeField] int _patrolRadius;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _chaser = GetComponent<PoliceChaser>();
        _fighter = GetComponent<PoliceFighter>();
        

    }

    private void Update()
    {
        if (_chaser.HasSpottedPlayer || _fighter.HasTarget) return;

        Search();

    }

    private void Search()
    {
        if (_currentMoveTimer >= _moveTimerThreshold)
        {
            
            Vector3 randomDirection = Random.insideUnitSphere * _patrolRadius;
            randomDirection += transform.position;
            NavMeshHit hit;
            NavMesh.SamplePosition(randomDirection, out hit, _patrolRadius, 1);
            Vector3 finalPosition = hit.position;


            _agent.destination = finalPosition;
            _currentMoveTimer = 0;
        }
        else
        
            
            _currentMoveTimer++;

        }
    }

