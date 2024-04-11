using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUILogic : MonoBehaviour
{
    int _price = 0;
    // Start is called before the first frame update

    private void Update()
    {
        if(_price >= 0)
        {
            //Debug.Log("Is at 0");
        }
    }

    public void SetPrice(int price)
    {
        _price = price;
    }
    
    public void BuyPhysicalObject(GameObject item)
    {
       
        if (!EnoughMoney(_price))
        {
            //Debug.Log("Not Enough Money!");
            return;
        }
        if (item.TryGetComponent<Carryable>(out Carryable carry))
        {

            GameObject product = Instantiate(carry.gameObject);
            if(PlayerStats._player.CarriedItem != null)
            {
                PlayerStats._player.CarriedItem.BaseDrop(); 
            }
            product.GetComponent<Carryable>().PickUp(PlayerStats._player._carryPoint);
        }
        PlayerStats._player.Pay(-_price);


    }

    public void BuySeed(int seedID)
    {
        if (!EnoughMoney(_price))
        {
            //Debug.Log("Not Enough Money!");
            return;
        }

        
        
         PlayerStats._player.Pay(-_price);
         Seed.ChangeSeedCount((Seed.SeedType)seedID, 1);
        
        
            
        
    }

    private bool EnoughMoney(int amount)
    {
       return PlayerStats._player.Money >= amount;
    }

    
}
