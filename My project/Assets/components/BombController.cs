using UnityEngine;

public class BombController : MonoBehaviour
{
    private float speed = 0.2f;
    [SerializeField] private GameObject blastEffect;
    
    private void Update()
    {
        transform.Translate(Vector3.down * (speed * Time.deltaTime));
    }
        
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Instantiate(blastEffect, collision.gameObject.transform.position, Quaternion.identity);
            CameraShaker.instance.MoveCamera(2,2,2f);
            Explode();
        }
    }

    private void Explode()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 10f);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Enemy") || collider.CompareTag("Obstacle"))
            {
                Destroy(collider.gameObject);
            }
        }
        Destroy(gameObject);
    }


}
