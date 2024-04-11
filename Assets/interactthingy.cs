using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactthingy : MonoBehaviour, IInteractable
{
     void IInteractable.Interact()
    {
        Debug.Log("Interacted with" + this.name);
    }
}
