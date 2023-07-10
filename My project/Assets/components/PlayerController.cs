using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float verticalSpeed = 10f;
    private float horizontalSpeed = 10f;

    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private Transform bulletSpawn;
    [SerializeField] private ParticleSystem propulsorParticles;
    private Rigidbody2D _rb;
    private bool isChangingBullet;
    private AudioSource _audioSource;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        transform.position = new Vector3(0, -4, 0);
        isChangingBullet = false;
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = 1f +Input.GetAxis("Vertical");
        if (verticalInput > 1.5f)
        {
            propulsorParticles.Play();
        }
        else
        {
            propulsorParticles.Stop();
        }
        
        Vector2 movement = new Vector2(horizontalInput * horizontalSpeed * Time.deltaTime, verticalInput * verticalSpeed * Time.deltaTime);
        float moveHorizontal = Input.GetAxis("Horizontal");
        if (_rb.position.x + movement.x <1.3f && _rb.position.x + movement.x>-1.3f )
        {
            _rb.MovePosition(_rb.position + movement);
        }
        else{
            movement = new Vector2(0, verticalInput * verticalSpeed * Time.deltaTime);
             _rb.MovePosition(_rb.position + movement);
        }

        int rotationDirection = Mathf.Clamp(Mathf.RoundToInt(moveHorizontal), -1, 1);

        float rotationAngle = rotationDirection * 20f;

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, rotationAngle);



        if (Input.GetKeyDown(KeyCode.Space))
        {
            _audioSource.Play();
            FireBullet();
        }
    }

    private void FireBullet()
    {
        if (!isChangingBullet)
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position + new Vector3(0, 1, 0), Quaternion.identity);
            bullet.transform.rotation = transform.rotation;
        }
        else
        {
            // bullet hacia adelante
            GameObject bulletForward = Instantiate(bulletPrefab, bulletSpawn.position + new Vector3(0, 1, 0), Quaternion.identity);
            bulletForward.transform.rotation = transform.rotation;

            // bullet en diagonal
            GameObject bulletDiagonal1 = Instantiate(bulletPrefab, bulletSpawn.position + new Vector3(-0.5f, 1, 0), Quaternion.identity);
            bulletDiagonal1.transform.rotation = Quaternion.Euler(0f, 0f, 45f);

            GameObject bulletDiagonal2 = Instantiate(bulletPrefab, bulletSpawn.position + new Vector3(0.5f, 1, 0), Quaternion.identity);
            bulletDiagonal2.transform.rotation = Quaternion.Euler(0f, 0f, -45f);
        }
    }

    public void ChangeBulletType(float duration)
    {
        if (!isChangingBullet)
        {
            StartCoroutine(ChangeBulletCoroutine(duration));
        }
    }

    private IEnumerator ChangeBulletCoroutine(float duration)
    {
        isChangingBullet = true;

        yield return new WaitForSeconds(duration);

        isChangingBullet = false;
    }

}