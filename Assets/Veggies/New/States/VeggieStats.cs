using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VeggieStats : Carryable, IInteractable
{

    public List<Pest> slugs = new List<Pest>();

    public Seed.SeedType SeedType;
    [SerializeField] public int _maxHealth = 100;
    [SerializeField] public float _currentHealth;

    [SerializeField] private float _growthSpeed = 1;
    public float growthSpeed { get { return _growthSpeed * 0.0001f; } set { _growthSpeed = value; } }


    public float _growthPercentage {get{ return 102 / _finalGrowthSize.x * transform.localScale.x; } }
    public bool _fertilizerUsed;

    [SerializeField] public float _waterPercentage = 100;
    [SerializeField] public float _maxWaterPercentage = 125;
    [SerializeField] private float _waterReductionRate = 1;
    [SerializeField] public float _waterDamageMultiplier = 1.5f;
    public float waterReductionRate { get { return _waterReductionRate * 0.0001f; }  set{ _waterReductionRate = value; }}


    public bool _hasCompost = false;

    public Vector3 _finalGrowthSize = Vector3.one*1.5f;

    [SerializeField] public float _currentRot = 0;
    [SerializeField] private float _rotIncreaseRate =0.1f;
    public float rotIncreaseRate { get { return _rotIncreaseRate * 0.0001f; } set {_rotIncreaseRate = value; } }

    public bool _isHarvested = false;

    //public bool _interactedWith = false;

    

    public int baseValue = 500;
    [SerializeField] public int value;
   


    private void Start()
    {
        _currentHealth = _maxHealth;
        _waterPercentage = 0;
    }

    private void Update()
    {
        if (_currentHealth <= 0)
        {
            Die();
        }

        if (!_isHarvested && Vector3.Distance(PlayerStats._player.transform.position, transform.position) < 10) 
        {
            Vector3 LookAtNotY = new Vector3(PlayerStats._player.transform.position.x, this.transform.position.y, PlayerStats._player.transform.position.z);
            transform.LookAt(LookAtNotY);
        }    

        _waterPercentage =Mathf.Clamp(_waterPercentage, 0, _maxWaterPercentage);
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);

        if(_fertilizerUsed)
        StartCoroutine(FertilizerUsedTimer());
    }

    private IEnumerator FertilizerUsedTimer()
    {
        yield return new WaitForSeconds(60);
        _fertilizerUsed=false;
    }

    public override void PickUp(Transform carryPoint)
    {
        _isHarvested = true;
        base.PickUp(carryPoint);
    }
    public void Damage(float damage)
    {
        _currentHealth -= damage;
       
    }

    private void Die()
    {

        _isHarvested = true;
        this.gameObject.SetActive(false);
        Destroy(this.gameObject);
    }

   void IInteractable.Interact()
    {
        //_interactedWith = true;
        if (_canPickUp)
        {
            PickUp(PlayerStats._player._carryPoint);
            return;

        }

        else if (PlayerStats._player.CarriedItem != null && PlayerStats._player.CarriedItem.TryGetComponent<Compost>(out Compost compost))
        {
            
                compost.UseCompost(this);
                return;
            
        }
        //_interactedWith = false;

    }
}
