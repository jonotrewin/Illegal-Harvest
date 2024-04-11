using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoseCollision : MonoBehaviour
{
    [SerializeField]float waterPercentageIncrease = 1.5f;
    List<ParticleCollisionEvent> collisions = new List<ParticleCollisionEvent>();

    float wateringRadius = 4;
    private void OnParticleCollision(GameObject other)
    {
      
        ParticlePhysicsExtensions.GetCollisionEvents(this.GetComponent<ParticleSystem>(), other, collisions);


        Vector3 pointWhereHits = collisions[0].intersection;

        Collider[] colliders = Physics.OverlapSphere(pointWhereHits, wateringRadius);
        foreach(Collider collider in colliders)
        {
            Debug.Log("hit collider");
            if(collider.TryGetComponent<VeggieStats>(out VeggieStats veggie))
            {
                Debug.Log("watered!");
                veggie._waterPercentage += waterPercentageIncrease;
            }
        }
    }

   

}
