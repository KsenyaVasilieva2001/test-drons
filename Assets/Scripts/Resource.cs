using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resour�e : MonoBehaviour
{
    public bool isAvailable = true;
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
