using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Pest : StateMachine
{
    public Animator animator;

    public PestSearch _pestSearch;
    public PestMove _pestMove;
    public PestAttack _pestAttack;
    public PestPickedUp _pestPickedUp;
    public NavMeshAgent NavMeshAgent;

    [SerializeField]public int AttackTimerThreshold = 500;
    [SerializeField] public int currentAttackTimer = 0;

    [SerializeField] public int damageAmount = 1;

    public VeggieStats _target;

    public bool addedToSlugCount = false;



    private void Update()
    {
        base.Update();
        GetComponent<Rigidbody>().isKinematic = true;

    }

    private void Awake()
    {
        _pestSearch = new PestSearch(this);
        _pestMove = new PestMove(this);
        _pestAttack = new PestAttack(this);
        _pestPickedUp = new PestPickedUp(this);
        
        animator = GetComponent<Animator>();

        NavMeshAgent = GetComponent<NavMeshAgent>();  
        
    }
    protected override BasicState GetInitialState()
    {

        return _pestSearch;
    }
}
