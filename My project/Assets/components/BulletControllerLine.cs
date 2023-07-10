using System.Collections;
using UnityEngine;

public class BulletControllerLine : MonoBehaviour
{
    private float speed = 10f;
    private float lifeDuration = 2f;
    [SerializeField] private GameObject blastEffect;
    private AudioSource _audioSource;
    private bool bulletFired = false;
    private float timeSinceCollision = 0f; 
    
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        Destroy(gameObject, lifeDuration);
    }

    private void Update()
    {
        transform.Translate(Vector3.down * (speed * Time.deltaTime));
        if (bulletFired)
        {
            timeSinceCollision += Time.deltaTime;
            if (timeSinceCollision > 3f)
            {
                int life = PlayerPrefs.GetInt("life");
                PlayerPrefs.SetInt("life", life - 1);
                timeSinceCollision = 0f;
                bulletFired = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _audioSource.Play();
            Instantiate(blastEffect, collision.gameObject.transform.position, Quaternion.identity);
            StartCoroutine(DestroyDelayed(gameObject));
            CameraShaker.instance.MoveCamera(2,2,2f);
            bulletFired = true; 
        }
    }
    
    private IEnumerator DestroyDelayed(GameObject obj)
    {
        yield return new WaitForSeconds(0.5f); 
        Destroy(obj);
    }
}