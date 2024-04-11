using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SweetPotatoeAnimationCalls : MonoBehaviour
{

    SweetPotatoeSpecialAbility spSA;
    // Start is called before the first frame update
    void Start()
    {
        spSA = GetComponentInParent<SweetPotatoeSpecialAbility>();
    }

    public void Heal()
    {
        spSA.WaterSurrounding();
    }

    public void StopAnim()
    {
        spSA.StopAnim();
    }
}
