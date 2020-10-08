using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEditor;

public class Player : MonoBehaviour
{
    public float jumpForce = 10f;
    public Rigidbody2D rb;
    public SpriteRenderer sR;
    public Color[] Colors;
    public TextMeshProUGUI score_text;
    public GameObject smallCircle;
    public GameObject colorChanger;
    public GameObject double_circle;
    
    string last_circle;

    private int score;


    private string Color;
    // Start is called before the first frame update


    private void Start()
    {
        GetRandomColor();
        rb.constraints = RigidbodyConstraints2D.FreezePosition;
        last_circle = "small_circle";
        FindObjectOfType<AudioManager>().Play("backgroundsong");
    }
    // Update is called once per frame      
    void Update()
    {
        
        
        if(Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(0))
        {
            rb.constraints = RigidbodyConstraints2D.None;
            rb.velocity = Vector2.up * jumpForce;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "ColorChanger")
        {
            FindObjectOfType<AudioManager>().Play("positive_1");
            GetRandomColor();
            Destroy(col.gameObject);
            updateScoreText(getLastCircle()) ;
            InstantiateNewCircle();
            return;
        }
        if (col.tag != Color)
        {
            Debug.Log("Game Over");
            AudioManager audio = FindObjectOfType<AudioManager>();
            audio.Play("gameover");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        
    }

    private void GetRandomColor()
    {
        int index = Random.Range(0, 4);

        switch (index)
        {
            case 0:
                Color = "Cyan";
                sR.color = Colors[0];
                score_text.color = Colors[0];
                break;
            case 1:
                Color = "Yellow";
                sR.color = Colors[1];
                score_text.color = Colors[1];
                break;
            case 2:
                Color = "Magenta";
                sR.color = Colors[2];
                score_text.color = Colors[2];
                break;
            case 3:
                Color = "Pink";
                sR.color = Colors[3];
                score_text.color = Colors[3];
                break;
        }
    }

    private void InstantiateNewCircle()
    {
        int rand_circle = Random.Range(0, 2);
        int circle_speed = calculateCircleRotationSmall();
        if(score < 30)
        {
            smallCircle.GetComponent<RotateCricle>().Speed = circle_speed;
            Instantiate(smallCircle, new Vector3(transform.position.x, transform.position.y + 8, transform.position.z), Quaternion.identity);
            Instantiate(colorChanger, new Vector3(transform.position.x, transform.position.y + 11, transform.position.z), Quaternion.identity);
        }
        else
        {
            if(rand_circle == 0)
            {
                smallCircle.GetComponent<RotateCricle>().Speed = circle_speed;
                Instantiate(colorChanger, new Vector3(transform.position.x, transform.position.y + 11, transform.position.z), Quaternion.identity);
                Instantiate(smallCircle, new Vector3(transform.position.x, transform.position.y + 8, transform.position.z), Quaternion.identity);
            }
            else
            {
                Instantiate(double_circle, new Vector3(0.0f, transform.position.y + 13, transform.position.z), Quaternion.identity);
                Instantiate(colorChanger, new Vector3(transform.position.x, transform.position.y + 20, transform.position.z), Quaternion.identity);
            }
        }
        
        

    }

    private void updateScoreText(int point)
    {
        score = score + point;
        score_text.text = "Skor:"  + score.ToString() ;
    }

    private int getLastCircle()
    {
        if(last_circle == "small_circle")
        {
            return 10;
        }
        else if(last_circle == "double_circle")
        {
            return 30;
        }

        else
        {
            return 0;
        }
        
    }

    int calculateCircleRotationSmall()
    {
        if (score < 20)
        {
            return 100;
        }
        else if(score < 30)
        {
            return 120;
        }

        else if (score < 40)
        {
            return 150;
        }

        else if(score < 70)
        {
            return 170;
        }

        else
        {
            return 200  ;
        }
    }
}
