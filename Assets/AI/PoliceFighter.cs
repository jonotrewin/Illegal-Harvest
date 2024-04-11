using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceFighter : FighterStats
{
    // Start is called before the first frame update
    public override void FindTarget()
    {
        Collider[] inRangeColliders = Physics.OverlapSphere(this.transform.position, _range);
        
        foreach(Collider collider in inRangeColliders)
        {
            if(collider.TryGetComponent<PlantFighter>(out PlantFighter plant))
            {
                _target=plant;
            }

        }
        


    }
}
