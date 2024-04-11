using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Planter : MonoBehaviour, IInteractable
{
    public static GameObject _currentlySelectedPlant;
    // Start is called before the first frame update

    private Plant _lastGrownVeggie;

    public bool _isOccupied;

   

    private void Update()
    {
        //Debug.Log($"Zucc: {Seed.seeds[Seed.SeedType.Zuccmeanie]} Sweet: {Seed.seeds[Seed.SeedType.NotSoSweetPotatoe]} Aubermean: {Seed.seeds[Seed.SeedType.Aubermean]} Watermelon: {Seed.seeds[Seed.SeedType.Slaughtermelon]} ");
        
        if(_lastGrownVeggie != null && _lastGrownVeggie.IsHarvested)
        { _isOccupied = false; }
     
    }
    

    public void Plant()
    {
        Plant carriedPlant = null;

        if (!_isOccupied && _currentlySelectedPlant != null && Seed.AreSeedsAvailable(_currentlySelectedPlant.GetComponent<Plant>().seedType))
        {

            GameObject veggie = Instantiate(_currentlySelectedPlant);
            _lastGrownVeggie = veggie.GetComponent<Plant>();
            veggie.transform.position = this.transform.position;
            Seed.ChangeSeedCount(veggie.GetComponent<Plant>().seedType, -1);
            veggie.transform.parent = this.transform;


            _isOccupied = true;
        }
       

        if(PlayerStats._player.CarriedItem.TryGetComponent<Plant>(out carriedPlant))
        { Debug.Log("Mingmon"); }
    }

    void IInteractable.Interact()
    {
        if (_isOccupied) return;
        
        Planter._currentlySelectedPlant = null;

        //SelectVeggieSeedMenu.instance.EnableMenu(this);


      
        
        if (PlayerStats._player.CarriedItem.TryGetComponent<Plant>(out Plant carriedPlant))
        {
            carriedPlant.Replant(this);
            _isOccupied = true;

            
        }

     
       
    }



    public static void SetCurrentlySelectedPlant(GameObject plant)
    {
        _currentlySelectedPlant = plant;
    }

    
    public static void FillUp(int something)
    {
        Seed.ChangeSeedCount(_currentlySelectedPlant.GetComponent<Plant>().seedType, something);
    }
  
}
