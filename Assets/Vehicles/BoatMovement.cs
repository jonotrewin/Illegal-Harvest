using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoatMovement : MonoBehaviour
{

    Rigidbody _rb;

    [SerializeField]float _movementSpeed = 10f;
    [SerializeField] float _rotationSpeed = 10f;
    [SerializeField] int _maxSpeed = 25;

    [SerializeField] HoseAndPump _hoseAndPump;
    public float waterRefillIncrement = 0.01f;

    private void OnEnable()
    {
        WaterUI.instance.gameObject.SetActive(true);
    }
    private void OnDisable()
    {
        WaterUI.instance.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _hoseAndPump = FindObjectOfType<HoseAndPump>();
    }

    private void Update()
    {
        if(WaterUI.instance.GetComponentInChildren<Slider>().value>=0.98f)
        {
            WaterUI.instance.gameObject.SetActive(false);
        }
        else
        {
            WaterUI.instance.enabled = true; //unnecessary atm
        }
       if(_rb.velocity.magnitude > 0)
        {
            _hoseAndPump.RefillWaterLevel(_rb.velocity.magnitude * waterRefillIncrement);

        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {


       
        if (Input.GetAxis("Vertical") != 0)
        {
            _rb.AddForce(this.transform.forward * _movementSpeed * Time.deltaTime* Input.GetAxis("Vertical"), ForceMode.Force);
        }

       

         if (Input.GetAxis("Horizontal") != 0)
        {
            transform.Rotate(this.transform.rotation.x, Input.GetAxis("Horizontal") * Time.deltaTime * _rotationSpeed, this.transform.rotation.z);
           
        }

        if (_rb.velocity.magnitude > _maxSpeed)
        {
            _rb.velocity = _maxSpeed * transform.forward;
        }
        else
        {
            _rb.velocity = _rb.velocity.magnitude * transform.forward;
        }
    }


}
