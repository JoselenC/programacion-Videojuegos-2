using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarController : MonoBehaviour
{
    private float speed = 3f;
    private Rigidbody2D rb;
    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    private void Update()
    {
        Vector2 movement = transform.up * (speed * Time.deltaTime);

        rb.MovePosition(rb.position - movement);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if  (collision.CompareTag("Player"))
        {
            Destroy(this.gameObject);
            int score = PlayerPrefs.GetInt("score");
            PlayerPrefs.SetInt("score", score+20);
        }
    }
}
