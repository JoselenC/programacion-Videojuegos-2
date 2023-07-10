using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeTime : MonoBehaviour
{
    private float lifetime = 1f; // Duración deseada del prefab en segundos

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

}
