using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public enum EnemyType
    {
        Multiple,
        Circular,
        Directed,
        Common,
        Sinusoidal, 
        Multipart,
        Tower
    }

    [SerializeField] private EnemyType enemyType;
    [SerializeField] private GameObject blastEffect;
    private float speed = 1f;
    private float shootInterval = 2f;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawn;
    private float radius = 1f;
    private float angle = 1f;
    private float rotationSpeed = 180f; 
    private Rigidbody2D rb;
    private GameObject player; 
    private float amplitude = 2f; 
    private float frequency = 1f; 
    private float time = 0f;
    private Vector3 startPosition;
    private AudioSource _audioSource;
    private int currentLife; 
    private int maxLife; 
    [SerializeField] private Slider slider;

    public void Start()
    { 
        _audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        startPosition = transform.position;
        
        if (enemyType == EnemyType.Directed || enemyType == EnemyType.Common)
        {
            currentLife = 2;
            maxLife = currentLife;
            StartCoroutine(Shoot());
        }
        if (enemyType == EnemyType.Circular)
        {
            currentLife = 3;
            maxLife = currentLife;
            StartCoroutine(ShootCircular());
        }
        if (enemyType == EnemyType.Multiple)
        {
            currentLife = 1;
            maxLife = currentLife;
            StartCoroutine(ShootMultiple());
        }
        if (enemyType == EnemyType.Sinusoidal || enemyType == EnemyType.Multipart)
        {
            currentLife = 2;
            maxLife = currentLife;
            StartCoroutine(ShootLineal());
        }

        slider.value = maxLife;
    }

    private void Update()
    {
        if (enemyType == EnemyType.Sinusoidal)
        {
            time += Time.deltaTime;
            float y = startPosition.y;
            float x = startPosition.x + Mathf.Sin(time * frequency) * amplitude; 
            float horizontalOffset = speed * Time.deltaTime;
            transform.position = new Vector3(x + horizontalOffset, y, transform.position.z);
        }
        
        if (enemyType == EnemyType.Directed || enemyType == EnemyType.Common )
        {
            Vector2 movement = transform.up * (8 * Time.deltaTime);
            rb.MovePosition(rb.position - movement);
        }
        
        if (enemyType == EnemyType.Circular || enemyType == EnemyType.Multiple)
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
        if (collision.CompareTag("Player"))
        {
            Instantiate(blastEffect, collision.gameObject.transform.position, Quaternion.identity);
            StartCoroutine(DestroyDelayed(gameObject));
            CameraShaker.instance.MoveCamera(2,2,2f);
            int life = PlayerPrefs.GetInt("life");
            PlayerPrefs.SetInt("life", life-1);
        }
    }
    private IEnumerator DestroyDelayed(GameObject obj)
    {
        yield return new WaitForSeconds(0.7f); 
        Destroy(obj);
    }

    private IEnumerator Shoot()
    {
        while (true)
        {
            if (Vector3.Distance(transform.position, player.transform.position) <= 9f)
            {
                _audioSource.Play();
                Instantiate(bulletPrefab, bulletSpawn.position - new Vector3(0, 1, 0), Quaternion.identity);
            }
            yield return new WaitForSeconds(shootInterval);
        }
    }
    
    private IEnumerator ShootMultiple()
    {while (true)
        {
            if (Vector3.Distance(transform.position, player.transform.position) <= 9f)
            {
                _audioSource.Play();
                for (int i = 0; i < 6; i++)
                {
                    float angle = i * (360f / 6f); 
                    Vector3 offset = Quaternion.Euler(0f, 0f, angle) * Vector3.right * radius; // Cálculo del offset en el círculo
                    Instantiate(bulletPrefab, transform.position + offset, Quaternion.identity);
                }
            }
            yield return new WaitForSeconds(shootInterval);
        }

    }

    private IEnumerator ShootCircular()
    {
        while (true)
        {
            if (Vector3.Distance(transform.position, player.transform.position) <= 10f)
            {
                Vector3 bulletSpawnPosition = bulletSpawn.transform.position;
                Vector2 bulletDirection = (this.transform.position - bulletSpawnPosition).normalized;
                _audioSource.Play();
                GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPosition, Quaternion.identity);
                bullet.GetComponent<Rigidbody2D>().velocity = bulletDirection * speed;
            } 
            yield return new WaitForSeconds(shootInterval);
        }
    }
    
    private IEnumerator ShootLineal()
    {
        while (true)
        {
            if (Vector3.Distance(transform.position, player.transform.position) <= 9f)
            {
                _audioSource.Play();
                Instantiate(bulletPrefab, bulletSpawn.position - new Vector3(0, 1, 0), Quaternion.identity);
            } 
            yield return new WaitForSeconds(0.02f);
        }
    }

    public void TakeDamage()
    {
        currentLife -= 1;
        slider.value = (float)currentLife/maxLife;
    }
    
    public int GetCurrentLife()
    {
        return currentLife;
    }
}
