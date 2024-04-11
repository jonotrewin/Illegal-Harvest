using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisibility : MonoBehaviour
{
   [SerializeField]float _baseVisibilityRadius = 5;
    [SerializeField] float _idleVisibilityRadius = 2;
    [SerializeField] float _sneakVisibilityRadius = 3;
    [SerializeField] float _dodgeVisibilityRadius = 7;

    float _currentVisibilityRadius;
    FighterStats _fs;
    PlayerMovement _playerMovement;

    [SerializeField]GameObject _visibilitySphere;


    private void Start()
    {
        _fs = GetComponentInParent<FighterStats>();

        _playerMovement = GetComponentInParent<PlayerMovement>();
        
        _currentVisibilityRadius = _baseVisibilityRadius;   
    }

    private void Update()
    {
        _visibilitySphere.transform.localScale = new Vector3(_currentVisibilityRadius, _visibilitySphere.transform.localScale.y, _currentVisibilityRadius);

      
        if(!_playerMovement.IsMoving)
        {
            _currentVisibilityRadius = _idleVisibilityRadius;
        }
        if (_playerMovement.IsMoving)
        {
            _currentVisibilityRadius = _baseVisibilityRadius;
        }
        if (_playerMovement.IsSneaking)
        {
            _currentVisibilityRadius = _sneakVisibilityRadius;
        }
        if (_playerMovement.HasDodged)
        {
            _currentVisibilityRadius = _dodgeVisibilityRadius;
        }

        _visibilitySphere.transform.localScale = new Vector3(_currentVisibilityRadius, _visibilitySphere.transform.localScale.y, _currentVisibilityRadius);
    }





}
