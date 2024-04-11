using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantFighter : FighterStats
{

    

    Plant _plant;

    
    // Start is called before the first frame update
    private void Start()
    {
        _plant = GetComponent<Plant>();

    }

    // Update is called once per frame
    void Update()
    {
        if(_plant.IsHarvested || !_plant.IsReadyToHarvest) return;

        base.Update();

    }

    public override void Die()
    {
        if(transform.parent.TryGetComponent<Planter>(out Planter planter))
        { planter._isOccupied = false; }
        
        base.Die();
        Destroy(gameObject);

    }

    public override void FindTarget()
    {
        Collider[] inRangeColliders = Physics.OverlapSphere(this.transform.position, _range);
        
        foreach(Collider collider in inRangeColliders)
        {
            if(collider.TryGetComponent<PoliceFighter>(out PoliceFighter policeOfficer))
            {
                _target=policeOfficer;
            }

        }
        


    }

    
}
