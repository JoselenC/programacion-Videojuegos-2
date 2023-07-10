using UnityEngine;

public class Background2 : MonoBehaviour

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
        float tempY = (cam.transform.position.y * (1 - parallaxEffect));
        float distY = (cam.transform.position.y * parallaxEffect);
        
        float tempX = (cam.transform.position.x * (1 - parallaxEffect));
        float distX = (cam.transform.position.x * parallaxEffect);


        transform.position = new Vector3(startPosX + distX, startPosY + distY, transform.position.z);

        if (tempY > startPosY + lengthY)
            startPosY += lengthY;
        
        if (tempX > startPosX + lengthX)
            startPosX += lengthX;
       
    }
}