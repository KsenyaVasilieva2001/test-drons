using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AvoidCollisions : MonoBehaviour
{
    [SerializeField] private float minDistance = 10f;
    [SerializeField] private float pushCoefficient = 0.5f;

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
        float speed = agent.velocity.magnitude;
        if (speed <= 0f) return;
        Vector3 totalPush = Vector3.zero;
        for (int i = 0; i < DroneManager.Instance.activeDrones.Count; i++)
        {
            var other = DroneManager.Instance.activeDrones[i];
            if(other.gameObject != gameObject)
            {
                Vector3 delta = transform.position - other.transform.position;
                float dist = delta.magnitude;
                if (dist < minDistance && dist > 0f)
                {
                    Vector3 dir = delta.normalized;
                    float pushMag = speed * pushCoefficient * Time.deltaTime;
                    totalPush += dir * pushMag;
                }
            }
        }
        if (totalPush != Vector3.zero)
        {
            agent.Move(totalPush);
        }
    }
}
