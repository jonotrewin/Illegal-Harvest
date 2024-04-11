using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Soil : Carryable, IInteractable
{
    

    [SerializeField] int _uses = 3;
    [SerializeField] private GameObject _planter;
   

    [SerializeField] private int _fadeThreshold = 10000;
    private float _currentFadeTimer =0f;
    private bool _fading = false;

    private int _dropTimerHoldThreshold = 100;
    private float _currentdropTimerHold = 0;

    private bool _justPickedUp = false;



    // Start is called before the first frame update
    void Start()
    {
        
      
        
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        PlaceSoil();
        
        CheckDepleted();
        
    }

    

    private void CheckDepleted()
    {
        if(_uses <= 0)
        {
            Fade();
            
        }
    }

    private void Fade()
    {
        if (!_fading)
        {
            base.Drop();
            this.SwitchPickupable(false);
            _fading = true;
        }

        _currentFadeTimer++;
        if(_currentFadeTimer>=_fadeThreshold)
        {
            Destroy(this.gameObject);
        }
       



    }

    
    public override void Drop()
    {
        base.Drop();
        _justPickedUp = true;
        
    }

    private void PlaceSoil()
    {
        if (PlayerStats._player.CarriedItem == this && Input.GetMouseButtonDown(0))
        {
            
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit,5000f))
            {
                if (hit.collider.gameObject.layer != LayerMask.NameToLayer("Ground")) return;
                GameObject newPlanter = Instantiate(_planter);

                newPlanter.transform.position = hit.point;
                _uses--;

            }







        }
        if(!_justPickedUp) _justPickedUp=true;
    }
}
