using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoseFire : Carryable
{
    ParticleSystem _ps;

    HoseAndPump _hoseAndPump;

    [SerializeField]float _maxPressure = 8f;
    [SerializeField]float _pressureIncrement = 0.05f;
    void Start()
    {
        _ps= GetComponentInChildren<ParticleSystem>();
        _ps.Pause();
        _ps.startSpeed = 0.1f;

        _hoseAndPump = GetComponentInParent<HoseAndPump>();

       

    }

    // Update is called once per frame
    void Update()
    {
        //if (PlayerStats._player.CarriedItem != this.GetComponent<Carryable>()) return;
       

        _ps.startSpeed = Mathf.Clamp(_ps.startSpeed, 0, _maxPressure);

        if (Input.GetMouseButtonDown(0) && PlayerStats._player.CarriedItem == this)
        {
            _ps.Stop();
            _ps.startSpeed = 1;
            _ps.Play();
        }
        if (Input.GetMouseButton(0) && PlayerStats._player.CarriedItem == this && _hoseAndPump._currentWaterLevel >= 0.01f)
        {
            
           
            
            PlayerStats._player.GetComponent<PlayerMovement>()._lockCamera = true;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
            {
                Vector3 mousePoint = new Vector3(hit.point.x, PlayerStats._player.transform.position.y, hit.point.z);
                PlayerStats._player.transform.LookAt(mousePoint);
            }
           
            _ps.startSpeed += _pressureIncrement;

            _hoseAndPump.ReduceWaterLevel();

            //PlayerStats._player.transform.Rotate(PlayerStats._player.transform.rotation.x, Input.GetAxis("Mouse X"), PlayerStats._player.transform.rotation.x);

        }
        else
        {
            _ps.startSpeed -= _pressureIncrement;
            PlayerStats._player.GetComponent<PlayerMovement>()._lockCamera = false;
        }
        if(_ps.startSpeed <= 0.1f)
        {
            _ps.Stop();
        }
    }

    public override void PickUp(Transform carryPoint)
    {
        WaterUI.instance.gameObject.SetActive(true);
        base.PickUp(PlayerStats._player._hoseCarryPoint);
    }

    public override void Drop()
    {
        _ps.Stop();
        WaterUI.instance.gameObject.SetActive(false);
        base.Drop();
       
    }
}
