using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blink : MonoBehaviour
{
    Image img;

    private void OnEnable()
    {
        img = GetComponent<Image>();

        InvokeRepeating("DisableEnable", 0, 1);

    }

    public void DisableEnable()
    {
        img.enabled = !img.enabled;
    }

    private void OnDisable()
    {
        img.enabled = true;
        CancelInvoke("DisableEnable");
    }
}
