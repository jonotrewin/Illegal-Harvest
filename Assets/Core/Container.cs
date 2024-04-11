using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Container : Carryable, IInteractable
{
    public List<GameObject> items = new List<GameObject>();

    [Header("Choose Restricted Access")]
    [SerializeField]bool _vegetables;

    int timeRun;

  

    void IInteractable.Interact()
    {
        timeRun++;

        if (!PlayerStats._player.HasCarriedObject)
        {
            TakeItem();
           
            
        }
        else if (PlayerStats._player.HasCarriedObject)
        {
            Debug.Log("placing");
            PlaceItem();
           

        }

        Debug.Log(timeRun);
    }

    void IInteractable.Select()
    {
        if (!_canPickUp) return; 
        PickUp(PlayerStats._player._carryPoint);
    }

    private void TakeItem()
    {
       
        GameObject itemToTake = items[items.Count - 1];
        itemToTake.SetActive(true);
        itemToTake.GetComponent<Carryable>().PickUp(PlayerStats._player._carryPoint);
        items.RemoveAt(items.Count - 1);
        

    }

    private void PlaceItem()
    {
        if (_vegetables && !PlayerStats._player.CarriedItem.TryGetComponent<Veggie>(out Veggie _)) return;

        

        items.Add(PlayerStats._player.CarriedItem.gameObject);
        PlayerStats._player.CarriedItem.gameObject.SetActive(false);
        PlayerStats._player.CarriedItem.Drop();
        PlayerStats._player.SetCarriedItem(null);
    }
}
