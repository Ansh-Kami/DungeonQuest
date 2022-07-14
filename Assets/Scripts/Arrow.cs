using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    private float lifetime;



    void Start()
    {
        rb.velocity = transform.right * speed;
        
    }

    void Update()
    {
        lifetime += Time.deltaTime;
        if (lifetime > 5) gameObject.SetActive(false);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<Health>().TakeDamage(1);
            gameObject.SetActive(false);
        }
    }

    
}
