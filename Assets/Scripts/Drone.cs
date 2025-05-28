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
    public Resourñe currentTarget;
    public bool isCollecting = false;

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
        if (!isCollecting)
        {
            if (currentTarget != null && other.gameObject == currentTarget.gameObject)
            {
                isCollecting = true;
                StartCoroutine(CollectResource());
            }
        }
        else if (other.gameObject == station.gameObject)
        {
            Debug.Log("Find Station");
            station.DepositResource();
            currentTarget = null;
            isCollecting = false;
        }
    }

    IEnumerator CollectResource()
    {
        Debug.Log("Collect!");
        currentTarget.Collect();
        yield return new WaitForSeconds(collectionTime);
        agent.SetDestination(station.transform.position);
    }

    public void SetStation(StationManager station)
    {
        this.station = station;
    }
}
