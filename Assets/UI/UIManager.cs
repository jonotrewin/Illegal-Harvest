using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UIManager
{
    public static void EnableDisable(GameObject gameObject)
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
