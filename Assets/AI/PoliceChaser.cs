using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.AI;

public class PoliceChaser : MonoBehaviour
{
    bool _hasSpottedPlayer = false;
    public bool HasSpottedPlayer { get { return _hasSpottedPlayer; } }
    PoliceFighter _fighterAI;

    [SerializeField]public float _perceptionRadius = 3f;

    NavMeshAgent _agent;

    PlayerStats _playerStats;

    Animator _anim;

    [SerializeField][Range(0,1)] float _fieldOfView;

    bool _isTackling = false;
    int _tackleCooldownThreshold = 1000
        ;
    int _currentTackleCooldown = 0;
    [SerializeField]float _tackleDistance = 5;


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(this.transform.position + transform.forward*_perceptionRadius, _perceptionRadius);
        Gizmos.DrawRay(this.transform.position, new Vector3(PlayerStats._player.transform.position.x, 1.5f, PlayerStats._player.transform.position.z) - this.transform.position);
        
        
    }

    private void Start()
    {
        _fighterAI = GetComponent<PoliceFighter>(); 
        _agent = GetComponent<NavMeshAgent>();
        _anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if(_agent.hasPath)
        {
            _anim.SetBool("IsMoving", true);
        }
        else _anim.SetBool("IsMoving", false);

        if (_hasSpottedPlayer)
        {
            
            _agent.destination = _playerStats.transform.position;
        }
        else
        {
            _fighterAI.enabled = true;


        }

       

        _anim.SetBool("IsAttacking", _isTackling);
    
        if (!_hasSpottedPlayer)
        CheckForCriminal();
       

        AttemptArrest();
        //Debug.Log(Vector3.Distance(this.transform.position, PlayerStats._player.transform.position));
        if (Vector3.Distance(this.transform.position, PlayerStats._player.transform.position) > _perceptionRadius)
        {
            //Debug.Log("Too Far");
            _hasSpottedPlayer = false;
        }


    }

    private void AttemptArrest()
    {
        if(Vector3.Distance(PlayerStats._player.transform.position, this.transform.position)<8f && 
            !_isTackling && _currentTackleCooldown>= _tackleCooldownThreshold && _hasSpottedPlayer)
        {
            StartCoroutine(Tackle());
            _currentTackleCooldown = 0;
        }
        else if(_currentTackleCooldown< _tackleCooldownThreshold)
        {
            _currentTackleCooldown++;
        }

        if(_isTackling)
        {
            //Vector3 distanceToPlayer = ( _playerStats.transform.position - transform.position).normalized;
            
            transform.position += this.transform.forward*Time.deltaTime*_tackleDistance;

        }
    }

    private IEnumerator Tackle()
    {
        _agent.Stop();
        
        _isTackling = true;
        _anim.SetBool("IsAttacking", true);
        yield return new WaitForSeconds(_anim.GetCurrentAnimatorClipInfo(0).Length);
        _isTackling = false;
        _agent.Resume();
    
     

    }

    private void CheckForCriminal()
    {
        

        Collider[] inView = Physics.OverlapSphere(this.transform.position + transform.forward * _perceptionRadius, _perceptionRadius);
        foreach (Collider thing in inView)
        {
            if (thing.transform.TryGetComponent<PlayerVisibility>(out PlayerVisibility vis))
            {
               
                
                if (!CheckInView())
                {
                    
                    return;
                }
                _hasSpottedPlayer = true;
               ;
                _playerStats = vis.transform.GetComponentInParent<PlayerStats>();
                 


                return;

            }

        }
        _hasSpottedPlayer = false;
    }

    private bool CheckInView()
    {
        //this finds whether I'm in front or behind//
        Vector3 playerTF = PlayerStats._player.transform.position;
        Vector3 vectorBetweenAgentAndTarget =   new Vector3(playerTF.x, 1.5f, playerTF.z) - this.transform.position ;
        float dotProductOfView = Vector3.Dot(Vector3.Normalize(vectorBetweenAgentAndTarget), this.transform.forward);
        //Debug.Log(dotProductOfView);
        
        //I then find out whether it's within view//
        if (!(dotProductOfView>=_fieldOfView)) return false;

        //Now I check if there's anything blocking the view//
        Ray rayToPlayer = new Ray(this.transform.position, vectorBetweenAgentAndTarget);
        Physics.Raycast(rayToPlayer, out RaycastHit hit);
        if (hit.collider.TryGetComponent<PlayerStats>(out _)) return true;
        else return false;
       

    }

    private void OnTriggerEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent<PlayerStats>(out PlayerStats player) && _isTackling)
        {
            player.IncreaseArrestAttempts();
            
        }
    }


}
