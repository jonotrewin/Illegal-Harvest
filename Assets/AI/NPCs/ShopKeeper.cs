using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopKeeper : MonoBehaviour, IInteractable
{
    [SerializeField]GameObject _UI;
    void IInteractable.Interact()
    {
        UIManager.EnableDisable(_UI);
        
        
    }

   
}
