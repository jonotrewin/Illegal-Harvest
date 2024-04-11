using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    CharacterController _cc;
    [SerializeField]float _baseMovementSpeed = 6f;
    [SerializeField] private float _currentMovementSpeed = 4f;
    [SerializeField] private float _rotationSpeed = 20f;



    bool _hasDodged = false;

    
    public bool HasDodged {get{ return _hasDodged; }}

    [SerializeField]int _dodgeTimerThreshold = 3;
    int _dodgeTimer = 0;
    [SerializeField]Animator _animator;

    bool _isSneaking = false;
    public bool IsSneaking { get{ return _isSneaking; } }

    bool _isMoving = false;
    public bool IsMoving { get { return _isMoving; } }

    Vector3 _dodgeLocation;
    [SerializeField]int _dodgeDistance = 10;
    private float _sneakSpeed = 0.3f;

    [SerializeField]int _maxDodges = 3;
    int _dodgesLeft;

    public bool _lockCamera;

    public float _jumpHeight = 30f;

    bool _canJump;

    private void OnGUI()
    {
        GUILayout.Label($"{_dodgesLeft}");
    }

    private void Start()
    {
        _dodgesLeft = _maxDodges;
        _cc = GetComponent<CharacterController>();
   
    }

    private void FixedUpdate()
    {
        Vector3 relativeHorizontal = Input.GetAxis("Horizontal") * Camera.main.transform.right;
        Vector3 relativeVertical = Input.GetAxis("Vertical") * Camera.main.transform.forward;
        Vector3 relativeMovement = relativeHorizontal + relativeVertical;
        float velocityY = ApplyGravity();
        Vector3 relativeMovementVector = new Vector3(relativeMovement.x, velocityY, relativeMovement.z);

        if(!_lockCamera)RotateCharacter(relativeMovementVector);

        _animator.SetBool("IsDodging", _hasDodged);
        _animator.SetBool("IsSneaking", _isSneaking);
        _animator.SetBool("IsMoving", _isMoving);
        _animator.SetBool("IsShooting", _lockCamera);

        Sprint();
        _cc.Move(relativeMovementVector * _currentMovementSpeed * Time.deltaTime);

      
    }

    private void Sprint()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _currentMovementSpeed = _baseMovementSpeed * 1.5f;
        }
  
            
   
        else
        {
           _currentMovementSpeed = _baseMovementSpeed;
        }
    }


    

    private void RotateCharacter(Vector3 direction)
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            _isMoving = true;
            
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z)),
                Time.deltaTime * _rotationSpeed
            );
        }
        else _isMoving = false;
    }


    private float ApplyGravity()
    {

        



        if (_canJump && Input.GetKeyDown(KeyCode.Space))
        {
            _canJump = false;
            return _jumpHeight;

        }
        else
        {
          
            return -1f;
                
        };
    }


}
