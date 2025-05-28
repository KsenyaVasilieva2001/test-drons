using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PathVisualizer : MonoBehaviour
{
    public static PathVisualizer Instance { get; private set; }
    public bool ShowPaths = false;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (ShowPaths)
        {
            for (int i = 0; i < DroneManager.Instance.activeDrones.Count; i++)
            {
                var drone = DroneManager.Instance.activeDrones[i];
                drone.GetComponent<LineRenderer>().enabled = true;
                var agent = DroneManager.Instance.activeDrones[i].GetComponent<NavMeshAgent>();
                VisualizePath(agent);
            }
        }
        else
        {
            for (int i = 0; i < DroneManager.Instance.activeDrones.Count; i++)
            {
                var drone = DroneManager.Instance.activeDrones[i];
                drone.GetComponent<LineRenderer>().enabled = false;
            }
            return;
        }
    }

    void VisualizePath(NavMeshAgent agent)
    {
        var path = agent.path;
        var lineRenderer = agent.GetComponent<LineRenderer>();
        lineRenderer.positionCount = path.corners.Length;
        lineRenderer.SetPositions(path.corners);
    }
}