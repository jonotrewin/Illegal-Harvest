using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WaterUI : MonoBehaviour
{
    Slider slider;

   [SerializeField] HoseAndPump _currentHoseAndPump;

    public static WaterUI instance;
    
    void Start()
    {
        instance = this;
        slider = GetComponent<Slider>();
        this.gameObject.SetActive(false);
        
    }

    private void OnEnable()
    {
        _currentHoseAndPump = FindObjectOfType<HoseAndPump>();
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = (1f / _currentHoseAndPump.MaxWaterLevel) * _currentHoseAndPump._currentWaterLevel;
    }

  
}
