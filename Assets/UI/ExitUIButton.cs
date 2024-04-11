using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitUIButton : MonoBehaviour
{
    [SerializeField]GameObject UI;
    
    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(() => UIManager.EnableDisable(UI));
    }

 
}
