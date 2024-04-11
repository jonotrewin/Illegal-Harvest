using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnIfInWater : MonoBehaviour
{
    [SerializeField]BoatInteract boat;

    private void Update()
    {
        if(Physics.Raycast(this.transform.position, Vector3.down, out RaycastHit hit) && hit.collider.gameObject.tag == "Water")
        {
            boat.GetOnBoat();
        }
    }
}
