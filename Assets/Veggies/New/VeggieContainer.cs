using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VeggieContainer : Carryable, IInteractable
{
    public List<GameObject> _stashedPlants;

    public void AddPlant()
    {
       
        if (PlayerStats._player.CarriedItem.GetComponent<VeggieStats>()!= null)
        {
            Debug.Log("Something");
            _stashedPlants.Add(PlayerStats._player.CarriedItem.gameObject);
            PlayerStats._player.CarriedItem.gameObject.SetActive(false);
            PlayerStats._player.SetCarriedItem(null);
            
        }
        
    }

    public void TakePlant()
    {
        if (_stashedPlants.Count <= 0) return;

        GameObject takenPlant = _stashedPlants[_stashedPlants.Count-1];
        takenPlant.SetActive(true);
        takenPlant.GetComponent<VeggieStats>().PickUp(PlayerStats._player._carryPoint);
        _stashedPlants.RemoveAt(_stashedPlants.Count - 1);



    }

    void IInteractable.Interact()
    {
        //if (ControlManager._currentVehicle == null)
        //{
            if (PlayerStats._player.CarriedItem != null)
            {
            
                AddPlant();
            }
            else
            {
                TakePlant();
            }
        //}
        
    }
}
