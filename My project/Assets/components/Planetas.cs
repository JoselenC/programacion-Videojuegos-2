using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planetas : MonoBehaviour

{
    [SerializeField] private Transform cam;
    [SerializeField] private float parallaxEffect;

    private float startPosY;
    private float lengthY;
    private float startPosX;
    private float lengthX;


    private void Start()
    {
        startPosY = transform.position.y;
        lengthY = GetComponent<SpriteRenderer>().bounds.size.y;
        
        startPosX = transform.position.x;
        lengthX = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void Update()
    {
        var position = cam.transform.position;
        
        float temp = (position.y * (1 - parallaxEffect));
        float dist = (position.y * parallaxEffect);

        float tempx = (position.x * (1 - parallaxEffect));
        float distx = (position.x * parallaxEffect);

        transform.position = new Vector3(startPosX + distx,startPosY + dist, transform.position.z);

        if (temp > startPosY + lengthY*3)
            startPosY += lengthY*8;
        
        if (tempx > startPosX + lengthX*3)
            startPosX += lengthX*8;
    }
}
