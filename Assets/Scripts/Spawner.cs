using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject objectToSpawn;

    public float spawnInterval = 5f;
    private Vector3 areaMin;
    private Vector3 areaMax;



    void Start()
    {
        CalculateBoundsFromPlane();
        StartCoroutine(WaitAndSpawn(spawnInterval));
    }

    void CalculateBoundsFromPlane()
    {
        Renderer renderer = GetComponent<Renderer>();
        Bounds bounds = renderer.bounds;
        areaMin = bounds.min;
        areaMax = bounds.max;
    }

    private IEnumerator WaitAndSpawn(float waitTime)
    {
        while (true)
        {
            Spawn();
            yield return new WaitForSeconds(waitTime);  
        }
    }

    private void Spawn()
    {
        float x = Random.Range(areaMin.x, areaMax.x);
        float z = Random.Range(areaMin.z, areaMax.z);
        Vector3 spawnPosition = new Vector3(x, areaMin.y, z);
        Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
    }
}
