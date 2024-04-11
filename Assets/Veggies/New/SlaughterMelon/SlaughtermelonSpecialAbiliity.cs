using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlaughtermelonSpecialAbiliity : SpecialAbility
{
    [SerializeField]float attackRadius = 1.5f;

    [SerializeField]int damage = 5;
    [SerializeField]Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
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
            SlaughterMelonAttack();
            _currentTimer = 0;

        }
    }

    private void SlaughterMelonAttack()
    {
        animator.speed = 1;
        _specialActive = true;
        animator.Play("Base Layer.SlaughtermelonShake");

    }

    public void DamageSurrounding()
    {
        Collider[] hits = Physics.OverlapSphere(this.transform.position, attackRadius);

        bool hasHitSlug=false;

        foreach(Collider hit in hits)
        {
            if(hit.TryGetComponent<VeggieStats>(out VeggieStats veg))
            {
                veg.Damage(damage);
            }
            if (hasHitSlug) return;
            if (hit.TryGetComponent<Pest>(out Pest pest) )
            {
                hasHitSlug = true;
                pest.gameObject.SetActive(false);
            }
        }

        _specialActive=false;
        


   
    }

    public void StopAnim()
    {
        _specialActive = false;
        animator.speed = 0;
        Debug.Log("Okido");
       
    }
}
