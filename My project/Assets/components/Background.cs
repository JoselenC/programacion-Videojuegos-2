using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour

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
        float tempY = (position.y * (1 - parallaxEffect));
        float distY = (position.y * parallaxEffect);
        
        float tempX = (position.x * (1 - parallaxEffect));
        float distX = (position.x * parallaxEffect);


        transform.position = new Vector3(startPosX + distX, startPosY + distY, transform.position.z);

        if (tempY > startPosY + lengthY/4)
            startPosY += lengthY;
        
        if (tempX > startPosX + lengthX/4)
            startPosX += lengthX;
    }
}
