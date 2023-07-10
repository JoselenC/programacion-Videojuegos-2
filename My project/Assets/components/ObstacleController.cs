using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    [SerializeField] private EnemyType enemyType;
    [SerializeField] private GameObject blastEffect;
    private int maxHitPoints = 1;
    private int currentHitPoints;
    private float radius = 1f;
    private float angle = 2f;
    private float rotationSpeed = 180f; 
    private float speed = 3f;
    private Rigidbody2D rb;
    private AudioSource _audioSource;

    
    public enum EnemyType
    {
        Asteroid,
        CircularAsteroid,
        Wall
    }

    public void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        currentHitPoints = maxHitPoints;
    }
    
    private void Update()
    {
        if (enemyType == EnemyType.Asteroid)
        {
            Vector2 movement = transform.up * (10 * Time.deltaTime);

            rb.MovePosition(rb.position - movement);
        }
        if (enemyType == EnemyType.CircularAsteroid)
        {
            angle += 2 * Time.deltaTime;
            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;
            Vector2 circularMotion = new Vector2(x, y);

            Vector2 targetPosition = rb.position + circularMotion;
            targetPosition.y -= 200 * Time.deltaTime; 
            rb.position = Vector2.Lerp(rb.position, targetPosition, speed * Time.deltaTime);

            Vector2 direction = circularMotion - rb.position;
            float targetRotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            float rotation = Mathf.MoveTowardsAngle(rb.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            rb.rotation = rotation;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            _audioSource.Play();
            Instantiate(blastEffect, transform.position, Quaternion.identity);
            StartCoroutine(DestroyDelayed(collision.gameObject));
            TakeDamage(1);
            int score = PlayerPrefs.GetInt("score");
            PlayerPrefs.SetInt("score", score+10);

        }
        else if (collision.CompareTag("Player"))
        {
            _audioSource.Play();
            Instantiate(blastEffect, collision.gameObject.transform.position, Quaternion.identity);
            StartCoroutine(DestroyDelayed(gameObject));
            CameraShaker.instance.MoveCamera(2,2,2f);
            int score = PlayerPrefs.GetInt("score");
            PlayerPrefs.SetInt("score", score-10);
        }
    }
    private IEnumerator DestroyDelayed(GameObject obj)
    {
        yield return new WaitForSeconds(0.7f); 
        Destroy(obj);
    }

    private void TakeDamage(int amount)
    {
        currentHitPoints -= amount;

        if (currentHitPoints <= 0)
        {
            Destroy(gameObject);
        }
    }
}
