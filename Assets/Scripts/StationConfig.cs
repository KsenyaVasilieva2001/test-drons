using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StationConfig", menuName = "Configs/Stations")]
public class StationConfig : ScriptableObject
{
    public string stationName;
    public Color droneColor;
    public Material droneMaterial;
}
