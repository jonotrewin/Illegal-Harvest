using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VeggiePlanter : MonoBehaviour, IInteractable
{
    public static GameObject _currentlySelectedPlant;
    // Start is called before the first frame update

    private VeggieStats _lastGrownVeggie;

    public bool _isOccupied;


  
    private void Update()
    {
        //Debug.Log($"Zucc: {Seed.seeds[Seed.SeedType.Zuccmeanie]} Sweet: {Seed.seeds[Seed.SeedType.NotSoSweetPotatoe]} Aubermean: {Seed.seeds[Seed.SeedType.Aubermean]} Watermelon: {Seed.seeds[Seed.SeedType.Slaughtermelon]} ");
        

        if(_lastGrownVeggie == PlayerStats._player.CarriedItem)
        { _isOccupied = false; }
        

    }
    

    public void Plant()
    {
       

        if (!_isOccupied && _currentlySelectedPlant != null && Seed.AreSeedsAvailable(_currentlySelectedPlant.GetComponentInChildren<VeggieStats>().SeedType))
        {
            Debug.Log("Slaughtermelon should load");
            GameObject veggie = Instantiate(_currentlySelectedPlant);
            _lastGrownVeggie = veggie.GetComponentInChildren<VeggieStats>();
            veggie.transform.position = this.transform.position + transform.up * 0.3f;
            Seed.ChangeSeedCount(veggie.GetComponentInChildren<VeggieStats>().SeedType, -1);
            veggie.transform.parent = this.transform;


            _isOccupied = true;
        }

        _currentlySelectedPlant = null;
        
    }

    void IInteractable.Interact()
    {
        if (_lastGrownVeggie?._isHarvested == true)
        {
            _isOccupied = false;
        }
       
       

        if (_isOccupied) return;
        
        Planter._currentlySelectedPlant = null;

        SelectVeggieSeedMenu.instance.EnableMenu(this);

        

        

            if (PlayerStats._player.CarriedItem.TryGetComponent<VeggieStats>(out VeggieStats carriedVeggie))
        {
            Replant(carriedVeggie);
            _isOccupied = true;

            
        }

        



    }

    void Replant(VeggieStats veggie)
    {

        veggie.transform.position = this.transform.position;
        veggie.transform.parent = this.transform;
        this.transform.rotation = Quaternion.identity;
        veggie._isHarvested = false;
        veggie.GetComponent<Rigidbody>().isKinematic = true;
        veggie.transform.parent = null;
        _isOccupied = true;
        PlayerStats._player.SetCarriedItem(null);

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
