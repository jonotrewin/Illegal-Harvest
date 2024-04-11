using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOESphere : MonoBehaviour
{
    Vector3 _maxSize;
    // Start is called before the first frame update
    void Start()
    {
        FighterStats fighter = this.GetComponentInParent<FighterStats>();
        float r = fighter.Range;
        _maxSize = new Vector3(r, r, r);
        this.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        this.transform.position = fighter.transform.position;
       
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.localScale.magnitude <= _maxSize.magnitude)
        {
            transform.localScale += new Vector3(0.05f, 0, 0.05f);
        }
        if(transform.localScale.magnitude >= _maxSize.magnitude)
        {
            Destroy(this.gameObject);
        }
    }
}
