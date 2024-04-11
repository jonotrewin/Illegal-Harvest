using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats _player;

    private Carryable _carriedItem;

    public Carryable CarriedItem { get { return _carriedItem; } }

    [SerializeField] public Transform _carryPoint;
    [SerializeField] public Transform _hoseCarryPoint;

    [SerializeField] private int _money = 0;
    public int Money { get { return _money; } }

    [SerializeField]private int _maxArrestAttempts = 3;
    private int _currentArrestAttempts = 0;
    bool _arrestAttemptCooldownActive = false;
    public bool ArrestAttemptCooldownActive { get { return _arrestAttemptCooldownActive; } }
    
    public bool HasCarriedObject { get { return _carriedItem != null;  } }
    public int ArrestAttempts { get { return _maxArrestAttempts; } }

    int _arrestTimer = 0;
    [SerializeField]int _arrestTimerThreshold = 1000;


    bool _isArrested;


    // Start is called before the first frame update
    void Start()
    {
        

        InitalizeSeeds();
        _player = this;

    }

    private void InitalizeSeeds()
    {
        Seed.seeds.Add(Seed.SeedType.Aubermean, 0);
        Seed.seeds.Add(Seed.SeedType.Zuccmeanie, 0);
        Seed.seeds.Add(Seed.SeedType.Slaughtermelon, 0);
        Seed.seeds.Add(Seed.SeedType.NotSoSweetPotatoe, 0);


    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(ControlManager._currentVehicle);
        if(_player==null)
        {
            _player = this;
        }

        ArrestAttemptCooldown();

        Arrested();

    }

    private void Arrested()
    {
        if(_currentArrestAttempts>=_maxArrestAttempts && !_isArrested)
        {
            _isArrested = true;
            Debug.Log("Arrested!");

            //Put arrest logic here!
        }
    }

    private void ArrestAttemptCooldown()
    {
        if(_arrestAttemptCooldownActive)
        {
            _arrestTimer++;
        }
        if(_arrestTimer>_arrestTimerThreshold)
        {
            _arrestAttemptCooldownActive = false;
            _arrestTimer = 0;
        }
    }

    public void SetCarriedItem(Carryable carryable)
    {
        _carriedItem = carryable;
    }

    public void Pay(int amount)
    {
        _money += amount;
    }

    public void IncreaseArrestAttempts()
    {
        if (!_arrestAttemptCooldownActive && !GetComponent<PlayerMovement>().HasDodged)
        {
            _currentArrestAttempts++;
            _arrestAttemptCooldownActive = true;
        }
    }


}
