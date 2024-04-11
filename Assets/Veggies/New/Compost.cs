using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compost : Carryable
{
    int _uses = 1;
    private float _healthIncreaseIncrement = 25;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = Vector3.one*(0.5f*_uses);

        if(_uses <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void IncreaseCompostCarried()
    {
        _uses++;   
    }

    public void UseCompost(VeggieStats veg)
    {
        
        veg._currentHealth += _healthIncreaseIncrement;
        _uses--;
    }
}
