using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StationManager : MonoBehaviour
{
    [Header("UI")]
    public Slider droneCountSlider;
    public TMP_Text droneCountText;
    public int minCountValue = 1;
    public int maxCountValue = 5;

    [Header("Settings")]
    [SerializeField] private StationConfig stationConfig;

    [Header("Drones")]
    [SerializeField] private GameObject dronePrefab;
    public float lineSpacing = 1.5f;
    public float margin = 1.5f;
    
    private List<GameObject> drones = new List<GameObject>();

    void Awake()
    {
        droneCountSlider.minValue = minCountValue;
        droneCountSlider.maxValue = maxCountValue;
        droneCountSlider.onValueChanged.AddListener(OnSliderChanged);
    }

    void Start()
    {
        OnSliderChanged(droneCountSlider.value);
    }

    public void OnSliderChanged(float value)
    {
        int count = Mathf.RoundToInt(value);
        droneCountText.text = count.ToString();
        ChangeDroneCount(count);
    }

    private void ChangeDroneCount(int targetCount)
    {
        while (drones.Count > targetCount)
        {
            var d = drones[drones.Count - 1];
            drones.RemoveAt(drones.Count - 1);
            Destroy(d);
        }

        while (drones.Count < targetCount)
        {
            var drone = Instantiate(dronePrefab);
            drone.AddComponent<Drone>();
            drone.GetComponent<Drone>().SetStation(this);
            SetDroneColor(drone);
            drones.Add(drone);
        }
        Arrange(drones, lineSpacing);
    }

    private void Arrange(List<GameObject> drones, float spacing)
    {
        Vector3 basePos = transform.position;
        float totalWidth = spacing * (drones.Count - 1);
        Vector3 start = basePos - transform.forward * (totalWidth / 2f);

        for (int i = 0; i < drones.Count; i++)
        {
            Vector3 targetPos = start + transform.forward * (i * spacing);
            drones[i].transform.position = targetPos + new Vector3(margin,0,0);
            drones[i].transform.rotation = transform.rotation;
        }
    }
    private void SetDroneColor(GameObject drone)
    {
        Renderer render = drone.GetComponent<Renderer>();
        if (render != null)
        {
            Material mat = stationConfig.droneMaterial != null
            ? stationConfig.droneMaterial
            : new Material(render.sharedMaterial);
            mat.color = stationConfig.droneColor;
            render.material = mat;
        }
    }

    public void DepositResource()
    {
        // Визуальный эффект выгрузки ресурса
        Debug.Log("Resource deposited at base!");
    }
}
