using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Trafficker : MonoBehaviour
{
    [SerializeField] public Container _veggieContainer;

    [SerializeField] int _saleCountdownThreshold = 500;
    [SerializeField] float _currentSaleCountdown = 0;
    public int CurrentSaleCountdown { get { return _saleCountdownThreshold - (int)_currentSaleCountdown; } }
    [SerializeField] float _countdownSpeed = 0.1f;
    GameObject _vegetableToDestroy = null;

    [SerializeField]Transform[] spawnLocations;
    Transform _currentSpawnpoint;

    [SerializeField] int _waitForMove = 5; 


    void Start()
    {
        _veggieContainer = GetComponent<Container>();
        if( _veggieContainer == null )
        {
            _veggieContainer = GetComponentInChildren<Container>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if( _veggieContainer.items.Count >0 )
        {
            _currentSaleCountdown += _countdownSpeed;
            if (_currentSaleCountdown >= _saleCountdownThreshold)
            {
                SellVeggies();
                _currentSaleCountdown = 0;
                StartCoroutine(MoveTrafficker());
            }
        }
    }



    public void SellVeggies()
    {
        int payment=0;
        

        foreach (GameObject veg in _veggieContainer.items)
        {
            VeggieStats veggieData = veg.GetComponent<VeggieStats>();
            payment += (int)(veggieData.value);
            _vegetableToDestroy = veg;

            
            


        }
        _veggieContainer.items.Clear();

        if(_vegetableToDestroy != null)Destroy(_vegetableToDestroy);
        PlayerStats._player.Pay(payment);
    }

    private IEnumerator MoveTrafficker()
    {
        yield return new WaitForSeconds(_waitForMove);
        
        Transform newSpawnLocation = spawnLocations[Random.Range(0, spawnLocations.Length - 1)];

       while(newSpawnLocation ==_currentSpawnpoint )
        {
            newSpawnLocation = spawnLocations[Random.Range(0, spawnLocations.Length - 1)];
        }

       if(newSpawnLocation != _currentSpawnpoint)
        {
            this.transform.position = newSpawnLocation.position;
            _currentSpawnpoint = newSpawnLocation;
            newSpawnLocation = null;
        }
    }
}
