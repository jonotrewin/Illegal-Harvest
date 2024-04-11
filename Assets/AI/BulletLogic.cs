using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLogic : MonoBehaviour
{

    int _damage = 1;
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent<PoliceFighter>(out PoliceFighter policeOfficer))
        {
            policeOfficer.Damage(_damage);
            Destroy(this.gameObject);
        }
       
    }

    public void SetDamage(int damage)
    {
        _damage=damage;
    }
}
