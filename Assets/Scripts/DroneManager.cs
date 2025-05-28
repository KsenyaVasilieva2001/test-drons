using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DroneManager : MonoBehaviour
{
    public static DroneManager Instance { get; private set; }
    public List<Drone> activeDrones = new List<Drone>();


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddDrone(Drone drone)
    {
        if (!activeDrones.Contains(drone))
        {
            activeDrones.Add(drone);
        }
    }

    public void RemoveDrone(Drone drone)
    {
        if (activeDrones.Contains(drone))
        {
            activeDrones.Remove(drone);
        }
    }
}
