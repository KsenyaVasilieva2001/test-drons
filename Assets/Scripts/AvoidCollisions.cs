using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AvoidCollisions : MonoBehaviour
{
    [SerializeField] private float minDistance = 100f;
    [SerializeField] private float force = 10f;

    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        AvoidOtherDrones();
    }

    void AvoidOtherDrones()
    {
        Vector3 newDir = Vector3.zero;

        for (int i = 0; i < DroneManager.Instance.activeDrones.Count; i++)
        {
            var other = DroneManager.Instance.activeDrones[i];
            if(other.gameObject != gameObject)
            {
                Vector3 dir = transform.position - other.transform.position;

                if (dir.magnitude < minDistance)
                {
                    Debug.Log("MIN");
                    transform.position += dir.normalized * force * Time.deltaTime;
                }
            }
        }
    }
}
