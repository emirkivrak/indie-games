using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PatrolFollow : MonoBehaviour
{
    public float speed;
    public Transform Target;
    public TextMeshProUGUI losetext;
    // Start is called before the first frame update
    void Start()
    {
        Target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        losetext.gameObject.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, Target.position, speed * Time.deltaTime);
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Destroy(collision.gameObject);
            losetext.gameObject.SetActive(true);
        }
        
    }
}
