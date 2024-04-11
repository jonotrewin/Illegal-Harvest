using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class EventManager
{
    // Start is called before the first frame update
    public delegate void GameEvent();

    public static event GameEvent OnLeftClick;
    public static event GameEvent OnRightClick;

    public static event GameEvent OnArrest;

    

    public static void LeftClick()
    {
        OnLeftClick?.Invoke();
    }
    public static void RightClick()
    {
        OnRightClick?.Invoke();
    }

}
