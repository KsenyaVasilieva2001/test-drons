using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resourñe : MonoBehaviour
{
    private bool isAvailable = true;
    public bool IsAvailable()
    {
        return isAvailable;
    }

    public void Collect()
    {
        isAvailable = false;
        Destroy(gameObject);
    }
}
