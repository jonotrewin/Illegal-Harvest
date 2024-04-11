using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedPacket : MonoBehaviour
{
    [SerializeField]Seed.SeedType type;
    [SerializeField] int _seedAmount = 3;
    private void OnTriggerEnter(Collider other)
    {
        if(!other.gameObject.TryGetComponent<PlayerStats>(out PlayerStats ps)) return;
        Seed.ChangeSeedCount(type, _seedAmount);
        GameObject.Destroy(this.gameObject);
    }

    private void Update()
    {
        transform.Rotate(0, 0.1f, 0);
    }
}
