using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;



public class BoatInteract : MonoBehaviour, IInteractable
{
    BoatMovement _bm;
    Rigidbody _rb;

    CancellationTokenSource cts;


    PlayerStats _player;

    [SerializeField] CinemachineVirtualCamera _cam;

    [SerializeField]int _interactRadius = 3;

    [SerializeField] GameObject _box1;
    [SerializeField] GameObject _box2;
    [SerializeField] GameObject _box3;

    List<GameObject> boxModels = new List<GameObject>();

    [SerializeField]List<GameObject> loadedBoxes = new List<GameObject>();
    public List<GameObject> CrateCount { get { return loadedBoxes; } }

    [SerializeField] GameObject _soil1;
    [SerializeField] GameObject _soil2;
    [SerializeField] GameObject _soil3;

    List<GameObject> soilModels = new List<GameObject>();

    [SerializeField] List<GameObject> loadedSoil = new List<GameObject>();

    bool _wasBumped = false;
    [SerializeField]int bumpDelay = 5000;

    bool _beingDriven = false;


    // Start is called before the first frame update

    private void OnDrawGizmos()
    {
        //
        //Gizmos.DrawSphere(this.transform.position, _interactRadius);
    }
    void Start()
    {
        _cam.enabled = false;
        _bm = GetComponent<BoatMovement>();
        _rb = GetComponent<Rigidbody>();
        

        boxModels.Add(_box1);
        boxModels.Add(_box2);
        boxModels.Add(_box3); 
        
        soilModels.Add(_soil1);
        soilModels.Add(_soil2);
        soilModels.Add(_soil3);

        _player = PlayerStats._player;


        _rb.isKinematic = true;

    }
    void Update()
    {
        MakeBoatItemsVisible(boxModels,loadedBoxes);
        MakeBoatItemsVisible(soilModels,loadedSoil);

        if(_player == null)
        {
            _player = PlayerStats._player;
        }

        if (_beingDriven)
        {

            Collider[] interactables = Physics.OverlapSphere(this.transform.position, _interactRadius);

            if (Input.GetKeyDown(KeyCode.E))
            {
                foreach (Collider collider in interactables)
                {
                    if (collider.TryGetComponent<Disembark>(out Disembark disembark))
                    {
                        GetOffBoat(disembark);
                        return;
                    }
                }
            }
        }
    }
    private void MakeBoatItemsVisible(List<GameObject> models, List<GameObject> loadedItems)
    {
        for(int i = 0; i<models.Count; i++)
        {
            if (loadedItems.Count-1>=i)
            {
                models[i].SetActive(true);
            }
            else
            {
                models[i].SetActive(false);
            }
        }
    }

    private void GetOffBoat(Disembark disembark)
    {
        _player.transform.position = disembark._disembarkPointPlayer.transform.position;
        _player.GetComponent<PlayerMovement>().enabled = true;
        _player.GetComponent<PlayerInteract>().enabled = true;
        _player.GetComponent<CapsuleCollider>().enabled = true;
        _player.GetComponent<CharacterController>().enabled = true;

        
        _player.transform.parent = null;

       
        this.transform.rotation = Quaternion.identity;
        this.transform.position = disembark._disembarkPointBoat.transform.position;
        

        _rb.isKinematic = true;
        _cam.Priority = 9;
        _cam.enabled = false;
        _bm.enabled = false;

        _beingDriven = false;
       
    }

    void IInteractable.Interact()
    {
        
        if(PlayerStats._player.CarriedItem == null)
        GetOnBoat();

        else
        {
            

           if(PlayerStats._player.CarriedItem is Container)
            {
                if (loadedBoxes.Count >= boxModels.Count) return;
                loadedBoxes.Add(PlayerStats._player.CarriedItem.gameObject);
                PlayerStats._player.CarriedItem.gameObject.SetActive(false);
                PlayerStats._player.CarriedItem.Drop();
                
            }

            if (PlayerStats._player.CarriedItem is Soil)
            {
                if (loadedSoil.Count >= soilModels.Count) return;
                loadedSoil.Add(PlayerStats._player.CarriedItem.gameObject);
                PlayerStats._player.CarriedItem.gameObject.SetActive(false);
                PlayerStats._player.CarriedItem.Drop();

            }

        }
    }

    void IInteractable.Select()
    {
        Debug.Log("Tot hier!");
        if (loadedBoxes.Count > 0)
        {
            loadedBoxes[loadedBoxes.Count - 1].SetActive(true);
            loadedBoxes[loadedBoxes.Count - 1].GetComponent<Container>().PickUp(PlayerStats._player._carryPoint);
            loadedBoxes.RemoveAt(loadedBoxes.Count - 1);
            
        }
        else if(loadedSoil.Count > 0)
        {
            Debug.Log("Running");
            loadedSoil[loadedSoil.Count - 1].SetActive(true);
            loadedSoil[loadedSoil.Count - 1].GetComponent<Soil>().PickUp(PlayerStats._player._carryPoint);
            loadedSoil.RemoveAt(loadedSoil.Count-1);

        }

    }

    public void GetOnBoat()
    {
        
        _player.GetComponent<PlayerMovement>().enabled = false;
        _player.GetComponent<PlayerInteract>().enabled = false;
        _player.GetComponent<CapsuleCollider>().enabled = false;
        _player.GetComponent<CharacterController>().enabled = false;

        _player.transform.position = this.transform.position;
        _player.transform.parent = this.transform;


        _cam.enabled = true;
        _cam.Priority = 11;
        _rb.isKinematic = false;
        _bm.enabled = true;
        _beingDriven = true;
    }


    public void Bump() 
    {
        if (_wasBumped) return;
        cts = new CancellationTokenSource();
        _wasBumped = true;

        if(loadedBoxes.Count > 0)
        {
            GameObject lostBox = loadedBoxes[loadedBoxes.Count - 1];
            lostBox.transform.position = (-transform.forward * 4) + transform.up;
            lostBox.SetActive(true);
            lostBox.GetComponent<Rigidbody>().isKinematic = false;
            loadedBoxes.RemoveAt(loadedBoxes.Count-1);
            
        }
        
        if(loadedSoil.Count >0 &&loadedBoxes.Count <=0)
        {
            GameObject lostSoil = loadedSoil[loadedSoil.Count - 1];
            lostSoil.transform.position = (-transform.forward) + transform.up;
            lostSoil.SetActive(true);
            lostSoil.GetComponent<Rigidbody>().isKinematic = false;
            loadedSoil.RemoveAt(loadedSoil.Count-1);
        }
       


        ResetBump();
        cts.Cancel();
        cts = null;
    }

    async void ResetBump()
    {
        await Task.Delay(bumpDelay);
        _wasBumped = false;
    
    }
}
