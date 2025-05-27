using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceProvider : MonoBehaviour
{
    public static ResourceProvider Instance;
    public List<Resourñe> resources = new List<Resourñe>();

    void Awake()
    {
        Instance = this;
    }

    public Resourñe FindNearestAvailableResource(Vector3 position)
    {
        Resourñe nearestResource = null;
        float minDistance = float.MaxValue;

        foreach (Resourñe resource in resources)
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
