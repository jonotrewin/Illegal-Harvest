using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PoliceBoatSM : StateMachine
{
    public PoliceBoatStats stats;
    public NavMeshAgent agent;

    public PB_Patrol pb_Patrol;
    public PB_Chase pb_Chase;
    public PB_Search pb_Search;


    
    void Start()
    {
        
        pb_Patrol = new PB_Patrol(this);
        pb_Chase = new PB_Chase(this);
        pb_Search = new PB_Search(this);


        stats = GetComponent<PoliceBoatStats>();
        agent = GetComponent<NavMeshAgent>();

        base.Start();

    }

    protected override BasicState GetInitialState()
    {
        return pb_Patrol;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<BoatInteract>(out BoatInteract boat))
        {
            boat.Bump();

            GetComponent<Rigidbody>().AddForce((boat.transform.position - this.transform.position) * 30f, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<BoatInteract>(out BoatInteract boat))
        {
            boat.Bump();

            GetComponent<Rigidbody>().AddForce(-(boat.transform.position - this.transform.position).normalized * 10f, ForceMode.Impulse);
            boat.gameObject.GetComponent<Rigidbody>().AddForce(this.transform.forward * 50f, ForceMode.Impulse);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.TryGetComponent<BoatMovement>(out BoatMovement boat))
        {
            other.gameObject.GetComponent<Rigidbody>().AddForce(this.transform.forward * 50f, ForceMode.Impulse);
            this.gameObject.GetComponent<Rigidbody>().AddForce(-this.transform.forward * 1000f, ForceMode.Impulse);
        }
    }
}
