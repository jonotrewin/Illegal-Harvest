using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Minimap : MonoBehaviour
{

    List<GameObject> _mapMarkers = new List<GameObject>();

    [SerializeField] GameObject TraffickerDotPrefab;
    [SerializeField] GameObject StoreDotPrefab;

    [SerializeField]Trafficker trafficker;
    [SerializeField] ShopKeeper shopKeeper;

    [SerializeField] public static Animator animator;

    GameObject dotForTrafficker;
    GameObject dotForStore;

    RectTransform _minimapBounds;

    [SerializeField]Camera _miniMapCam;

    // Start is called before the first frame update
    void Start()
    {
        _minimapBounds = GetComponent<RectTransform>();
        _miniMapCam = GetComponentInChildren<Camera>();
        trafficker = FindAnyObjectByType<Trafficker>();
        shopKeeper = FindAnyObjectByType<ShopKeeper>();
        animator = GetComponent<Animator>();

        InstantiateMapMarkers(TraffickerDotPrefab);
        InstantiateMapMarkers(StoreDotPrefab);

    }



    private void InstantiateMapMarkers(GameObject dotPrefab)
    {
        GameObject newDot = Instantiate(dotPrefab);
        newDot.transform.parent = this.gameObject.transform;
        newDot.transform.position = this.transform.position;
        _mapMarkers.Add(newDot);
        
    }



    // Update is called once per frame
    void Update()
    {
        //if(animator.enabled == false)
        //{
        //    TryGetComponent<RawImage>(out RawImage mapImage);
        //    mapImage.color = Color.white;

        //}

        UpdateCameraPosition();
        UpdateDotPosition(trafficker.gameObject, _mapMarkers[0]);
        UpdateDotPosition(shopKeeper.gameObject, _mapMarkers[1]);

    }

    private void UpdateCameraPosition()
    {
            _miniMapCam.transform.position = new Vector3(PlayerStats._player.transform.position.x,
            _miniMapCam.transform.position.y, PlayerStats._player.transform.position.z);
    }

    private void UpdateDotPosition(GameObject inWorldObject, GameObject dotToMove)
    {
        dotToMove.SetActive(inWorldObject.activeSelf);
        float dotYPosition = 0; //reset position each frame
        Vector3 vectorToObject = inWorldObject.transform.position - PlayerStats._player.transform.position; //get vector from player to objects
        Vector3 VTOwithoutHeight = new Vector3(vectorToObject.x, 1, vectorToObject.z); //put the height to 1
       

        float distanceToObject = Vector3.Distance(inWorldObject.transform.position, PlayerStats._player.transform.position);
        //getting the distance to the object as a float


        //this will control when it snaps to the border of the map
        if(distanceToObject < _miniMapCam.orthographicSize)
        dotYPosition = distanceToObject*2; //no idea why it's x2???
        else
        {
            dotYPosition = (Screen.height) / 7.5f; // Don't ask me why..
        }


     

        //this moves the dotPrefab down or up according to how far I am.
        dotToMove.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + dotYPosition, this.transform.position.z);

        //this gets the angle between the forward of the world (which is the forward of the map) and the vector between player and object
        float AngleBetweenObject = Vector3.SignedAngle(Vector3.forward*100, VTOwithoutHeight,
            PlayerStats._player.transform.up);

        //this rotates the circle around the map so the direction is correct
        dotToMove.transform.RotateAround(this.transform.position,
            this.transform.forward, -AngleBetweenObject);
        dotToMove.transform.rotation = Quaternion.identity;

     
    }

    public static void PoliceChaser(bool value)
    {
        animator.enabled = value;
    }

    //private void OnDrawGizmos()
    //{
    //    Vector3 vectorToObject = inWorldObject.transform.position - PlayerStats._player.transform.position;
    //    Vector3 playerPositionWithoutHeight =
    //       new Vector3(PlayerStats._player.transform.position.x, 1, PlayerStats._player.transform.position.z);
    //    Vector3 VTOwithoutHeight = new Vector3(vectorToObject.x, 1, vectorToObject.z);

    //    //Gizmos.DrawLine(playerPositionWithoutHeight, inWorldObject.transform.position);
    //    Gizmos.DrawRay(playerPositionWithoutHeight, vectorToObject);
    //    Gizmos.DrawRay(playerPositionWithoutHeight,  Vector3.forward*100);

    //}
}
