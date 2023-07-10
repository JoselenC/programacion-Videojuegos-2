
using UnityEngine;
using UnityEngine.UI;

public class LifeController : MonoBehaviour
{
    private float speed = 3f;
    private Rigidbody2D rb;
    [SerializeField] private Image barraVida;
    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    private void Update()
    {
        if (rb != null)
        {
            Vector2 movement = transform.up * (speed * Time.deltaTime);
            rb.MovePosition(rb.position - movement);
        }
        int actualLife = PlayerPrefs.GetInt("life");
        if(barraVida != null)
            barraVida.fillAmount = (float)actualLife/10;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if  (collision.CompareTag("Player"))
        {
            Destroy(this.gameObject);
            int life = PlayerPrefs.GetInt("life");
            PlayerPrefs.SetInt("life", life+1);
        }
    }
}
