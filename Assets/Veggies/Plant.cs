using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Plant : Carryable, IInteractable
{
    [SerializeField] public Seed.SeedType seedType;

    private float _randomScale;
    private float _growthSpeed = 0.001f;
    private Vector3 _growthVector;
    private Vector3 _targetScale;

    private int _value = 100;
    public int Value { get { return _value; } }

    [SerializeField]bool _readyToHarvest = false;
    [SerializeField] bool _isHarvested = false;
    public bool IsReadyToHarvest { get { return _readyToHarvest; } }
    public bool IsHarvested { get { return _isHarvested; }}

    Planter _planter;
    // Start is called before the first frame update
    private void Awake()
    {
        this.transform.localScale = new Vector3(0, 0, 0);
        _randomScale = Random.Range(1.0f, 2.0f);
        _targetScale = new Vector3(_randomScale, _randomScale, _randomScale);
        _growthVector = new Vector3(_growthSpeed, _growthSpeed, _growthSpeed);

        _rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        if(_rb == null)
        {
            _rb = this.AddComponent<Rigidbody>();
        }
    }

    protected void Update()
    {

        base.Update(); 

        if(this.transform.localScale.magnitude < _targetScale.magnitude && !_readyToHarvest)
        {
            this.transform.localScale += _growthVector;
        }
        else
        {
            _readyToHarvest = true;
        }

        //Debug.Log(PlayerStats._player.CarriedItem);



    }

    void IInteractable.Interact()
    {
        if (!_readyToHarvest) return;


        Harvest();

        base.PickUp(PlayerStats._player._carryPoint);

    }

    public void Harvest()
    {
        _isHarvested = true;
    }

    public override void Drop()
    {
        _rb.isKinematic = false;
        this.transform.parent = null;
        PlayerStats._player.SetCarriedItem(null);
        EventManager.OnRightClick -= Drop;
    }

    public void Replant(Planter planter)
    {
        
        this.transform.position = planter.transform.position;
        this.transform.parent = planter.transform;
        this.transform.rotation = Quaternion.identity;
        _isHarvested=false;
        _rb.isKinematic = true;
        this.transform.parent = null;
        PlayerStats._player.SetCarriedItem(null);
       
    }

    private void OnDisable()
    {
        Harvest();
        
    }



}
