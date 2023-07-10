using System.Collections;
using UnityEngine;

public class BulletControllerDirected : MonoBehaviour
{
    private float speed = 10f;
    private float lifeDuration = 2f;
    private float life = 2f;
    [SerializeField] private GameObject blastEffect;
    private Transform playerTransform;
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        life = 2f;
        Destroy(gameObject, lifeDuration);
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        life -= Time.deltaTime; 

        if (life <= 0f)
        {
            Destroy(gameObject); 
            return;
        }

        Vector3 direction = (playerTransform.position - transform.position).normalized;
        transform.Translate(direction * (speed * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _audioSource.Play();
            Instantiate(blastEffect, collision.gameObject.transform.position, Quaternion.identity);
            CameraShaker.instance.MoveCamera(2, 2, 2f);
            StartCoroutine(DestroyDelayed(gameObject));
            int playerlife = PlayerPrefs.GetInt("life");
            PlayerPrefs.SetInt("life", playerlife - 1);
        }
    }
    
    private IEnumerator DestroyDelayed(GameObject obj)
    {
        yield return new WaitForSeconds(0.5f); 
        Destroy(obj);
    }
}