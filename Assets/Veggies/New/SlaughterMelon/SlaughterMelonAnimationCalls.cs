using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlaughterMelonAnimationCalls : MonoBehaviour
{
    SlaughtermelonSpecialAbiliity smSA;

    private void Start()
    {
        smSA = GetComponentInParent<SlaughtermelonSpecialAbiliity>();

    }

    public void Stop()
    {
        smSA.StopAnim();
    }

    public void DamageSurrounding()
    {
        smSA.DamageSurrounding();
    }

}
