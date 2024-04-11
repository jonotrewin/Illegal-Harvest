using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
   
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(this.transform.position + this.transform.forward * 1.5f, 1.5f);

    }
    void Update()
    {
      


        if (Input.GetKeyDown(KeyCode.E))
        {
           CheckInteractables();
           
          
        }


        if (Input.GetKeyDown(KeyCode.F))
        {
   
            CheckSelectables();
           
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            PlayerStats._player.CarriedItem?.Drop();
        }



    }

   
    private void CheckSelectables()
    {
        Collider[] interactables = Physics.OverlapSphere(this.transform.position + this.transform.forward * 1.5f, 1.5f);

        foreach (Collider col in interactables)
        {
            if(col.TryGetComponent<IInteractable>(out IInteractable interactable))
            {
                interactable.Select();
                
            }

        }
    }

    private void CheckInteractables()
    {
        Collider[] interactables = Physics.OverlapSphere(this.transform.position + this.transform.forward * 1.5f, 1.5f);

        //IInteractable closestInteractable = null;
        bool objectFound = false;
        //float bestDistance = 10000;

        objectFound = CheckForType(interactables, typeof(Pest));
        if (objectFound) return;
        objectFound = CheckForType(interactables,  typeof(VeggieStats));
        if (objectFound) return;
        objectFound = CheckForType(interactables, typeof(VeggieContainer));
        if (objectFound) return;
        objectFound = CheckForType(interactables, typeof(Carryable));
        if (objectFound) return;
        objectFound = CheckForType(interactables, typeof(BoatInteract));
        if (objectFound) return;
        objectFound = CheckForType(interactables,  typeof(VeggiePlanter));
        if (objectFound) return;
        objectFound = CheckForType(interactables, typeof(IInteractable));
       

        


        //return closestInteractable;
    }

    private bool CheckForType(Collider[] interactables, Type type)
    {
        foreach (Collider interactable in interactables)
        {
            if (interactable.TryGetComponent<Carryable>(out Carryable carried) && PlayerStats._player.CarriedItem == carried)
            {
                return false;
            }

            
            if (interactable.TryGetComponent<VeggiePlanter>(out VeggiePlanter planter) && planter._isOccupied) continue;

           

            //if (!(interactable.GetComponent<type.GetType()>() != null) return;

            if (interactable.GetComponent(type)== null) continue;
          
            if (interactable.TryGetComponent<IInteractable>(out IInteractable interact))
            {

                //if (Vector3.Distance(interactable.transform.position, this.transform.position) <
                //    bestDistance)
                //{
                //    bestDistance = Vector3.Distance(interactable.transform.position, this.transform.position);
              
                interact.Interact();
                //}
                
                return true;
               

            }
            
        }
        return false;
    }
}
