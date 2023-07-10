using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWeaponsController : MonoBehaviour
{
    private float speed = 1f;
    private float lifeDuration = 12f;
    private PlayerController playerController; 

    private void Start()
    {
        Destroy(gameObject, lifeDuration);
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

    }

    private void Update()
    {
        transform.Translate(Vector3.down * (speed * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerController.ChangeBulletType(5f); 
            Destroy(gameObject);
        }
    }
}
