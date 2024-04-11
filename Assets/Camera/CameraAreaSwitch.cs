using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.Rendering;

public class CameraAreaSwitch : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera _areaCamera;

    private void Start()
    {
        _areaCamera.enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        _areaCamera.enabled = true;
        _areaCamera.Priority +=2;
    
       
    }

    private void OnTriggerExit(Collider other)
    {
        _areaCamera.enabled = false;
        _areaCamera.Priority -= 2;
    }
}
