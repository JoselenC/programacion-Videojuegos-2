using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPlayerControllerMultiple : MonoBehaviour
{
    private float speed = 10f;
    private float lifeDuration = 2f;
    [SerializeField] private GameObject blastEffect;
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        Destroy(gameObject, lifeDuration);
    }
    private void Update()
    {
        transform.Translate(Vector3.up * (speed * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            _audioSource.Play();
            Instantiate(blastEffect, collision.gameObject.transform.position, Quaternion.identity);
            StartCoroutine(DestroyDelayed(gameObject));
            CameraShaker.instance.MoveCamera(2,2,2f);
            EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();
            if (enemy != null)
            {
                if(enemy.GetCurrentLife()>1)
                    enemy.TakeDamage();
                else
                    Destroy(collision.gameObject);
            }
        }
        else if (collision.CompareTag("Obstacle"))
        {
            _audioSource.Play();
            Instantiate(blastEffect, collision.gameObject.transform.position, Quaternion.identity);
            StartCoroutine(DestroyDelayed(gameObject));
            CameraShaker.instance.MoveCamera(2,2,2f);
            Destroy(collision.gameObject);
        }
    }
    
    private IEnumerator DestroyDelayed(GameObject obj)
    {
        yield return new WaitForSeconds(0.5f); 
        Destroy(obj);
    }
}
