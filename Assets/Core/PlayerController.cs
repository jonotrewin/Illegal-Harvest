using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    private PlayerStats _playerStats;
    
    NavMeshAgent _navMeshAgent;

    [SerializeField] float _interactableDistance =4f;
    public bool _controlsOverridden = false;
    // Start is called before the first frame update

    IInteractable _interactable = null;
    IInteractable _selectable = null;
    RaycastHit _hitInteractable;
    RaycastHit _hitSelectable;

    bool _isWalkingToInteractable = false;
    bool _isWalkingToSelectable = false;
    


    private void Start()
    {
        _navMeshAgent = this.GetComponent<NavMeshAgent>();
        _playerStats = GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        

        if (Input.GetMouseButtonDown(0))
        {

            if (!EventSystem.current.IsPointerOverGameObject())
            {
                EventManager.OnLeftClick += PlayerOnLeftClick;
                EventManager.LeftClick();
                EventManager.OnLeftClick -= PlayerOnLeftClick;
            }

        }

        if (Input.GetMouseButtonDown(1))
        {

            if (!EventSystem.current.IsPointerOverGameObject())
            {
                EventManager.OnRightClick += PlayerOnRightClick;
                EventManager.RightClick();
                EventManager.OnRightClick -= PlayerOnRightClick;
            }

        }
        OutOfRangeInteractions();
    }

    private void OutOfRangeInteractions()
    {
        //Debug.Log(_selectable);

        if (_isWalkingToInteractable)
        {
            if (IsPlayerInRange(_hitInteractable.point))
            {
                
                _interactable.Interact();
                _isWalkingToInteractable = false;
                
            }
        }
        if (_isWalkingToSelectable)
        {
            if (IsPlayerInRange(_hitSelectable.point))
            {
                MovePlayer(this.transform.position);
                _selectable.Select();
                _isWalkingToSelectable = false;
                
            }
        }
    }

    void PlayerOnLeftClick()
    {
        _isWalkingToInteractable = false;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
       
        Physics.Raycast(ray, out _hitInteractable);

        bool hitAnInteractable = _hitInteractable.collider.gameObject.TryGetComponent<IInteractable>(out _interactable);

        if (_controlsOverridden && hitAnInteractable && IsPlayerInRange(_hitInteractable.point))
        {
            _interactable.Interact();
            return;
        }

        if(hitAnInteractable)
        {
            MovePlayer(_hitInteractable.transform.position);
            _isWalkingToInteractable = true;
            return;

        }

        MovePlayer(_hitInteractable.point);
 



    }

    void PlayerOnRightClick()
    {
        _isWalkingToSelectable = false;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out _hitSelectable);

        bool hitASelectable = _hitSelectable.collider.gameObject.TryGetComponent<IInteractable>(out IInteractable selectable);
        if (selectable != _selectable)
        {
            _selectable = selectable;
        }




        if (hitASelectable && IsPlayerInRange(_hitSelectable.transform.position))  
        {
            _selectable.Select();
          
        }
        else if (_hitSelectable.collider.gameObject.TryGetComponent<Planter>(out Planter planter))
        {
            MovePlayer(_hitSelectable.transform.position);
            _selectable.Select();

        }
        if (hitASelectable && !IsPlayerInRange(_hitSelectable.transform.position))
        {
            MovePlayer(_hitSelectable.transform.position);
            _isWalkingToSelectable= true;

            _selectable = _hitSelectable.collider.gameObject.GetComponent<IInteractable>();
            
        }







    }

    private bool IsPlayerInRange(Vector3 position)
    {
        return Vector3.Distance(this.transform.position, position) <= _interactableDistance;
    }

    private void MovePlayer(Vector3 position)
    {
        
        _navMeshAgent.destination = position;
    }



}
