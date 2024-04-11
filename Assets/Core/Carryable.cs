using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Carryable : MonoBehaviour, IInteractable
{


    [SerializeField] protected bool _canPickUp = false;
   

    protected Rigidbody _rb;
   
    

    // Start is called before the first frame update
    private void Awake()
    {
        
        if(!TryGetComponent<Rigidbody>(out _rb))
        {
            this.AddComponent<Rigidbody>();
            _rb = GetComponent<Rigidbody>();
        }
        if(!_canPickUp)
        {
            _rb.isKinematic = true;
        }
        
    }

    protected void Update()
    {
       
    }

    void IInteractable.Interact()
    {
        Debug.Log("Attempted Pickup");
        if (_canPickUp)
        {
            PickUp(PlayerStats._player._carryPoint);
            
        }
        //else Debug.Log("Pickup Failed");

    }



    public virtual void PickUp(Transform carryPoint)
    {
        if (TryGetComponent<NavMeshAgent>(out NavMeshAgent nma)) nma.enabled = false;
        Debug.Log("Pick Up");
        //if (ControlManager._currentVehicle != null) { return; }

        if (PlayerStats._player.CarriedItem != null) PlayerStats._player.CarriedItem.Drop();

        _rb.isKinematic = true;

        PlayerStats._player.SetCarriedItem(this);
        this.gameObject.transform.SetParent(PlayerStats._player.transform);
        this.gameObject.transform.position = carryPoint.position;
        this.gameObject.transform.rotation = carryPoint.rotation;

        EventManager.OnRightClick += Drop;

    }

    public virtual void Drop()
    {
        if (TryGetComponent<NavMeshAgent>(out NavMeshAgent nma)) nma.enabled = true;
        _rb.isKinematic = false;
        this.transform.parent = null;
        PlayerStats._player.SetCarriedItem(null);
        EventManager.OnRightClick -= Drop;
    }

    public void BaseDrop()
    {
        _rb.isKinematic = false;
        this.transform.parent = null;
        PlayerStats._player.SetCarriedItem(null);
        EventManager.OnRightClick -= Drop;
    }
    public void SwitchPickupable(bool value)
    {
        _canPickUp = value;
    }

    public void CanPickUp(bool value)
    {
        _canPickUp = value;
    }


}
