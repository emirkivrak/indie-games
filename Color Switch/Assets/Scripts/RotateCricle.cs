using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCricle : MonoBehaviour
{
    public float Speed = 80f;
    private Transform Player; // todo
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if((Player.transform.position.y - transform.position.y  ) > 10)
        {
            Destroy(gameObject);
        }
        transform.Rotate(0.0f, 0.0f, Speed * Time.deltaTime);
    }
}
