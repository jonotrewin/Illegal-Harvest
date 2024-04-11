using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Zumeanie_SpecialAbility : SpecialAbility
{
   
   //the need to be replanted = specialActive;

    public float _healthLossPerFrame = 0.1f;

    bool _pickedUp;
    bool _replanted;

 

    float _originalGrowthSpeed;

    private void Start()
    {
        base.Start();
        _originalGrowthSpeed = stats.growthSpeed*10000;
    }

    private void Update()
    {
        stats.CanPickUp(_specialActive);

        if (vSM.CurrentState == vSM._harvestableState)
        {
            stats.CanPickUp(true);
            return;
        };

        if(_currentTimer>_timerThreshold)
        {
            _specialActive=true;
            stats.growthSpeed=0;
            
        }

        if (!_specialActive)
        {
            _currentTimer++;
            
            return;
           
        }

        TakeDamage();

        PlantReplantLogic();
    }

    private void PlantReplantLogic()
    {


        if (stats._isHarvested == true)
        {
            _pickedUp = true;
            Debug.Log("Thanks for picking me up, Bub!");
        }

        if (stats._isHarvested == false && _pickedUp == true)
        {
            _replanted = true;
            Debug.Log("thanks for planting me");
        }

        if (_pickedUp && _replanted)
        {
            _specialActive = false;
            stats.growthSpeed = _originalGrowthSpeed;
            _currentTimer = 0;
        }
    }

    private void TakeDamage()
    {
        if(_specialActive)
        { stats.Damage(_healthLossPerFrame); }

        


    }

}
