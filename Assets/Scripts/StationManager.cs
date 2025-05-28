using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
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
    public float margin = 4f;

    [Header("Visual Effects")]
    public ParticleSystem unloadParticles;
    public float scalePunchAmount = 1.3f;
    public float scalePunchDuration = 0.2f;

    private List<Drone> drones = new List<Drone>();

    private int resourceCount = 0;
    public event Action<int> OnResourceDeposited;

    void Start()
    {
        ChangeDroneCount(1);
    }

    public int GetDronesCount()
    {
        return drones.Count;
    }


    public void SetSpeed(float speed)
    {
        foreach (var drone in drones)
            drone.GetComponent<NavMeshAgent>().speed = speed;
    }
   

    public void ChangeDroneCount(int targetCount)
    {
        while (drones.Count > targetCount)
        {
            var d = drones[drones.Count - 1];
            drones.RemoveAt(drones.Count - 1);
            Destroy(d.gameObject);
            DroneManager.Instance.RemoveDrone(d);
        }

        while (drones.Count < targetCount)
        {
            var drone = Instantiate(dronePrefab);
            drone.AddComponent<Drone>();
            drone.GetComponent<Drone>().SetStation(this);
            SetDroneColor(drone);
            drones.Add(drone.GetComponent<Drone>());
            DroneManager.Instance.AddDrone(drone.GetComponent<Drone>());
        }
        Arrange(drones, lineSpacing);
    }

    private void Arrange(List<Drone> drones, float spacing)
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
        resourceCount++;
        OnResourceDeposited?.Invoke(resourceCount);
        PlayUnloadVFX();
    }
    private void PlayUnloadVFX()
    {
        if (unloadParticles != null)
        {
            ParticleSystem ps = Instantiate(unloadParticles, transform.position, Quaternion.identity);
            ps.Play();
            Destroy(ps.gameObject, ps.main.duration + ps.main.startLifetime.constantMax);
        }
        StartCoroutine(PunchScale());
    }

    private IEnumerator PunchScale()
    {
        Vector3 original = transform.localScale;
        Vector3 target = original * scalePunchAmount;
        float half = scalePunchDuration * 0.5f;
        float t = 0f;

        while (t < half)
        {
            transform.localScale = Vector3.Lerp(original, target, t / half);
            t += Time.deltaTime;
            yield return null;
        }
        transform.localScale = target;
        t = 0f;
        while (t < half)
        {
            transform.localScale = Vector3.Lerp(target, original, t / half);
            t += Time.deltaTime;
            yield return null;
        }
        transform.localScale = original;
    }
    public int GetDepositCount()
    {
        return resourceCount;
    }
}
