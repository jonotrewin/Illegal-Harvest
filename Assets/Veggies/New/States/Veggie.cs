using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(VeggieStats)) ]
public class Veggie : StateMachine
{
    public VeggieStats _veggieStats;

    public Growing _growState;
    public Harvestable _harvestableState;




    private void Awake()
    {
        _growState = new Growing(this);
        _harvestableState = new Harvestable(this);
    }
    private void Start()
    {
        base.Start();
        _veggieStats = GetComponent<VeggieStats>();
  
    }

    protected override BasicState GetInitialState()
    {
       
        return _growState;
    }
}
