using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHeadFollow : MonoBehaviour
{
    [SerializeField]GameObject cameraHead;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] hits = Physics.OverlapSphere(this.transform.position, 30);

        foreach(Collider hit in hits)
        {
            if(hit.TryGetComponent<PlayerStats>(out PlayerStats player))
            {
                cameraHead.transform.LookAt(player.transform);
                return;
            }
            if (hit.TryGetComponent<BoatMovement>(out BoatMovement boat))
            {
                cameraHead.transform.LookAt(boat.transform);
                return;
            }
        }

    }
}
