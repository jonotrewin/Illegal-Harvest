using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PoliceBoatStats : MonoBehaviour
{
    [ExecuteInEditMode]
    [SerializeField] public List<GameObject> patrolWaypoints = new List<GameObject>();
    [SerializeField] public int _patrolSpeed = 5;
    [SerializeField] public int _chaseSpeed = 7;

    public float _sightRadius = 30;

    public bool _targetSeen = false;
    public GameObject _target;

    [SerializeField]float _fieldOfView = 0.75f;

    public int _searchTimerThreshold = 5000;
    public int _currentSearchTimer = 0;

    public Rigidbody _rb;
    Vector3 draw;

    private void OnDrawGizmos()
    {
        DrawRoute();

        //Gizmos.DrawSphere(transform.position, _sightRadius);
        Gizmos.DrawRay(this.transform.position,draw);
       
     
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void DrawRoute()
    {
        Gizmos.color = Color.white;
        for (int i = 0; i < patrolWaypoints.Count; i++)
        {
            Vector3 pointB;
            Vector3 pointA = patrolWaypoints[i].transform.position;

            if (i + 1 >= patrolWaypoints.Count)
            {
                pointB = patrolWaypoints[0].transform.position;
            }
            else pointB = patrolWaypoints[i + 1].transform.position;

            Gizmos.DrawLine(pointA, pointB);
        }
    }

    public void CheckForEnemies()
    {
        
        Collider[] colliders = Physics.OverlapSphere(this.transform.position, _sightRadius);

        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent<BoatMovement>(out BoatMovement playerBoat))
            {
                
                if (playerBoat.isActiveAndEnabled == false) return;
                if (CheckInView(this.gameObject, collider.gameObject))
                {
                    
                    _target = playerBoat.gameObject;
                    _targetSeen = true;
                    return;
                }
                //return;
            }
        }

        _targetSeen = false;
        _target = null;
    }

    public bool CheckInView(GameObject viewer1,GameObject enemy)
    {
        //this finds whether I'm in front or behind//
        Vector3 vectorBetweenAgentAndTarget = enemy.transform.position+transform.up - viewer1.transform.position;
        
        draw = vectorBetweenAgentAndTarget;

        float dotProductOfView = Vector3.Dot(Vector3.Normalize(vectorBetweenAgentAndTarget), viewer1.transform.forward);
        
        Debug.Log(dotProductOfView);

        //I then find out whether it's within view//
        if (!(dotProductOfView >= _fieldOfView)) return false;
     

        //Now I check if there's anything blocking the view//
        Ray rayToPlayer = new Ray(viewer1.transform.position, vectorBetweenAgentAndTarget);
        Physics.Raycast(rayToPlayer, out RaycastHit hit);
        if (hit.collider.GetComponent<BoatMovement>()) return true;
        else return false;


    }
}

