using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AvoidCollisions : MonoBehaviour
{
    [SerializeField] private float minDistance = 10f;
    [SerializeField] private float force = 10f;
    private float smoothTime = 0.3f;
    private Vector3 velocity = Vector3.zero;

    private float lastAvoidanceTime = -1f;
    private float delay = 0.1f;

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
        if (Time.time - lastAvoidanceTime < delay)
        {
            return;
        }
        for (int i = 0; i < DroneManager.Instance.activeDrones.Count; i++)
        {
            var other = DroneManager.Instance.activeDrones[i];
            if(other.gameObject != gameObject)
            {
                Vector3 dir = transform.position - other.transform.position;

                if (dir.magnitude < minDistance)
                {
                    Debug.Log("MIN");
                    Vector3 targetPosition = transform.position + newDir.normalized * force * Time.deltaTime;
                    transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
                    lastAvoidanceTime = Time.time;
                }
            }
        }
    }
}
