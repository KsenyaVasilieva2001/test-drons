using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceProvider : MonoBehaviour
{
    public static ResourceProvider Instance;
    public List<Resour�e> resources = new List<Resour�e>();

    void Awake()
    {
        Instance = this;
    }

    public Resour�e FindNearestAvailableResource(Vector3 position)
    {
        Resour�e nearestResource = null;
        float minDistance = float.MaxValue;

        foreach (Resour�e resource in resources)
        {
            if (resource.IsAvailable())
            {
                float distance = Vector3.Distance(position, resource.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestResource = resource;
                }
            }
        }
        return nearestResource;
    }
}
