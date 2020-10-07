using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mermi : MonoBehaviour
    
{
    
    public float speed = 10;
    public Rigidbody2D rb;
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Start()
    {

        rb.velocity = transform.right * speed;

    }

    private void Update()
    {
        if(transform.position.x > 30 || transform.position.x < -15
            || transform.position.y > 12 || transform.position.y < -12)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("patrol"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        else if(collision.transform.CompareTag("duvar"))
        {
            Destroy(gameObject);
        }
            
    }
}
