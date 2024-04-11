using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoseAndPump : MonoBehaviour, IInteractable
{
    // Start is called before the first frame update

    [SerializeField]GameObject hose;
    [SerializeField]GameObject pump;
    [SerializeField]Transform hoseRestingPosition;
    Vector3 hoseReturnStartingPosition;




    [SerializeField]int _maxWaterLevel = 1000;
    public int MaxWaterLevel { get { return _maxWaterLevel; }}
    [SerializeField] public float _currentWaterLevel = 100;
    [SerializeField]float _waterLossIncrement = 0.1f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ReturnDroppedHose();

        _currentWaterLevel = Mathf.Clamp(_currentWaterLevel, 0, _maxWaterLevel);

        
    }

    

    private void ReturnDroppedHose()
    {
        if (PlayerStats._player.CarriedItem != hose.GetComponent<Carryable>())
        {
            hoseReturnStartingPosition = hose.transform.position;
            hose.transform.position = Vector3.Lerp(hoseReturnStartingPosition, hoseRestingPosition.position, Time.time * 0.0001f);
            hose.transform.rotation = hoseRestingPosition.rotation;
        }

        if (hose.TryGetComponent<Rigidbody>(out Rigidbody rb) && Vector3.Distance(hose.transform.position, pump.transform.position) < 3f)
        {
            rb.isKinematic = true;
        }
    }

    public void ReduceWaterLevel()
    {
        _currentWaterLevel -= _waterLossIncrement;
    }

    public void RefillWaterLevel(float amount)
    {
        _currentWaterLevel += amount;
    }

    void IInteractable.Interact()
    {
        hose.GetComponentInChildren<IInteractable>().Interact();
    }
}
