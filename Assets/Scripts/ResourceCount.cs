using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//œŒ—€À¿“‹ Œ“—ﬁƒ¿ »« RESOURCE
public class ResourceCount : MonoBehaviour
{
    public static ResourceCount Instance { get; private set; }
    public event Action<int> OnResourceCollected;
    private int total = 0;

    void Awake() => Instance = this;

    public void Collect()
    {
        total++;
        OnResourceCollected?.Invoke(total);
    }
}
