using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SweetPotatoeSpecialAbility : SpecialAbility
{
    [SerializeField]float waterRadius = 1.5f;

    [SerializeField]float waterAmount = 2.5f;
    [SerializeField]Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        animator.speed = 0;
        animator = GetComponentInChildren<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_currentTimer< _timerThreshold)
        {
            _currentTimer++;
        }
        
        if (_currentTimer >= _timerThreshold)
        {
            PotatoeCry();
            _currentTimer = 0;

        }
    }

    private void PotatoeCry()
    {
        animator.enabled = true;
        _specialActive = true;
        animator.speed = 1;
        animator.Play("Base Layer.CryingAnimation");

    }

    public void WaterSurrounding()
    {
        Collider[] hits = Physics.OverlapSphere(this.transform.position, waterRadius);

        
        foreach (Collider hit in hits)
        {
            if(hit.TryGetComponent<VeggieStats>(out VeggieStats veggie))
            {
                veggie._waterPercentage += waterAmount;
            }
        }


   
    }

    public void StopAnim()
    {
        _specialActive = false;
        _currentTimer = 0;
        animator.speed = 0;
        animator.enabled = false;
        Debug.Log("Okido");
       
    }
}
