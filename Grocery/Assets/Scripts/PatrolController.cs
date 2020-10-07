using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolController : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public float distance;
    private bool movingUp = true;
    public Transform groundDetection;
    

    private void Update()
    {


        if (movingUp == true)
        {
            transform.Translate(Vector2.up * speed);
        }
        else
        {
            transform.Translate(Vector2.down * speed);
        }
        
        

        RaycastHit2D patrolInfo = Physics2D.Raycast(groundDetection.position, Vector2.right, distance);


        if(patrolInfo.collider == false)
        {
            if(movingUp == true)
            {
                movingUp = false;

            }
            else
            {
                movingUp = true;
            }

        }
        
   
    }
}
