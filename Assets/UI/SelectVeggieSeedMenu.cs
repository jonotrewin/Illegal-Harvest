using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SelectVeggieSeedMenu : MonoBehaviour
{
    public static SelectVeggieSeedMenu instance;
   
    [SerializeField] VeggiePlanter _currentPlanter;

    [SerializeField] TextMeshProUGUI _noPlantsText;

    [SerializeField]bool _menuOpened;

    [SerializeField] Button[] _seedChoices;

    private void Start()
    {
        instance = this;
        _seedChoices = GetComponentsInChildren<Button>();
       
        DisableAtStart();
       

    }

    private void DisableAtStart()
    {
        

        EnableOrDisableMenu(false);
        _noPlantsText.gameObject.SetActive(false);
    }

    private void EnableOrDisableMenu(bool value)
    {
        bool seedAvailable = false;
        

        //Debug.Log("Opening Menu");
        for(int i=0; i<_seedChoices.Length; i++)
        {
        
            if (value == true && Seed.AreSeedsAvailable((Seed.SeedType)i))
            {


                _seedChoices[i].gameObject.SetActive(value);
                seedAvailable = true;
                Debug.Log("Item Checked");
            }

            else _seedChoices[i].gameObject.SetActive(false);
        }


        if (!seedAvailable)
        {
            _noPlantsText.gameObject.SetActive(value);
        }
        

    }

    private void Update()
    {
        if (instance == null) instance = this;


        if (Input.GetKey(KeyCode.E) && _menuOpened)
        {
            EnableOrDisableMenu(true);
            
        }
        else if (Input.GetKeyUp(KeyCode.E))
        {
            _noPlantsText.gameObject.SetActive(false);
            EnableOrDisableMenu(false);
            if (_menuOpened)
            {
                _currentPlanter.Plant();
                



            }
            _currentPlanter = null;

            _menuOpened = false;
        }


        else _menuOpened = false;

           
    }

    public void EnableMenu(VeggiePlanter planter)
    {
        Debug.Log("Call to Open received");
        _menuOpened = true;
      
        _currentPlanter = planter;
    }

}
