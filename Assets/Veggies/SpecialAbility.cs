using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAbility : MonoBehaviour
{
    public bool _specialActive = false;

    protected VeggieStats stats;
    protected Veggie vSM;
    protected VeggieUI ui;


    protected int _timerThreshold = 5000;
    protected int _currentTimer = 0;

    

    protected void Start()
    {
        stats = GetComponent<VeggieStats>();
        vSM = GetComponent<Veggie>();
        ui = GetComponent<VeggieUI>();
        
    }

    private void Update()
    {
        
    }

    
}
