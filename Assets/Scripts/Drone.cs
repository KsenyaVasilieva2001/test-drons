using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Drone : MonoBehaviour
{
    [SerializeField] private StationManager station;
    public float collectionTime = 2f;

    private NavMeshAgent agent;
    private Resourñe currentTarget;
    private bool isCollecting = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (!isCollecting && currentTarget == null)
        {
            FindAndMoveToResource();
        }
    }

    public void FindAndMoveToResource()
    {
        Resourñe resource = ResourceProvider.Instance.FindNearestAvailableResource(transform.position);
        if (resource != null)
        {
            currentTarget = resource;
            agent.SetDestination(resource.transform.position);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == currentTarget.gameObject)
        {
            StartCoroutine(CollectResource());
        }
        else if (other.gameObject == station.gameObject)
        {
            station.DepositResource();
            currentTarget = null;
        }
    }

    IEnumerator CollectResource()
    {
        isCollecting = true;
        yield return new WaitForSeconds(collectionTime);
        currentTarget.Collect();
        agent.SetDestination(station.transform.position);
        isCollecting = false;
    }

    public void SetStation(StationManager station)
    {
        this.station = station;
    }
}
