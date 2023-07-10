using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target; 
    private Vector3 offset; 

    private void LateUpdate()
    {
        offset = new Vector3(0,3,-10);
        if (target != null)
        {
            transform.position = target.position + offset;
        }
    }
}
