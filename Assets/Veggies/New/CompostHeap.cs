using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CompostHeap : MonoBehaviour, IInteractable
{

    [SerializeField]GameObject _compostHeap;

    [SerializeField]GameObject _compostPrefab;

    [SerializeField] int _compostCount = 0;

    [SerializeField]int _maxCompostCount = 10;

   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _compostHeap.transform.localScale = new Vector3(_compostHeap.transform.localScale.x, 0.01f + 0.1f * _compostCount, _compostHeap.transform.localScale.z);

        if(_compostCount > _maxCompostCount &&  (!this.gameObject.TryGetComponent<SpawnPests>(out SpawnPests _)))
        {
            this.gameObject.AddComponent<SpawnPests>();
        }
        else if(_compostCount < _maxCompostCount)
        {
            if (this.gameObject.TryGetComponent<SpawnPests>(out SpawnPests sp))
                Destroy(sp);
        }
    }

    void IInteractable.Interact()
    {
        if(PlayerStats._player.CarriedItem == null)
        {
            if (_compostCount <= 0) return;
            
            PickUpCompost();
            _compostCount--;    
            return;
        }

        Carryable carried = PlayerStats._player.CarriedItem;

        if(carried.TryGetComponent<Pest>(out Pest slug))
        {
            _compostCount++;
            slug.GetComponent<Carryable>().Drop();
            slug.gameObject.SetActive(false);

        }
        else if (carried.TryGetComponent<Veggie>(out Veggie veg))
        {
            _compostCount+=2;
            Destroy(veg.gameObject);

        }
        else if (carried.TryGetComponent<Compost>(out Compost compost))
        {
            _compostCount++;
            Destroy(compost.gameObject);

        }
        else return;

        PlayerStats._player.SetCarriedItem(null);
        

    }

    private void PickUpCompost()
    {
        GameObject newCompostObject = Instantiate(_compostPrefab);

        Compost compost = newCompostObject.GetComponent<Compost>();

        compost.PickUp(PlayerStats._player._carryPoint);

    }
}
