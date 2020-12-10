using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Author: Edgar Centeno
public class MovingPlatforms : MonoBehaviour
{
    public Vector3[] points;
    private int pointNumber = 0;
    private Vector3 currentTarget;
    private float tolerance; //snaps cleanly to its final position
    public float speed; //how fast it will move between its points
    public float delayTime; //how long it will wait before it starts moving again
    private float delayStart;
    public bool automatic; 
    
    // Start is called before the first frame update
    void Start()
    {
        // initialize length and tolerance  
        if (points.Length > 0)
        {
            currentTarget = points[0];
        }
        tolerance = speed * Time.deltaTime;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //check if platform is at the position its suppose to be at else update the target set by user
        if (transform.position != currentTarget)
        {
            MovePlatform();
        }
        else
        {
            UpdateTarget();
        }
    }

    //moves the platform 
    void MovePlatform()
    {
        // heading is the points of the current target minus the actual position. 
        Vector3 heading = currentTarget - transform.position;
        //actual position calculated by speed time and the heading
        transform.position += (heading / heading.magnitude) * speed * Time.deltaTime;
        if (heading.magnitude < tolerance)
        {
            transform.position = currentTarget;
            delayStart = Time.time;
        }
    }

    void UpdateTarget()
    {
        //automatic follows the delay time
        if (automatic)
        {
            if (Time.time - delayStart > delayTime)
            {
                //go through each point the user has set 
                pointNumber++;
                if (pointNumber >= points.Length)
                {
                    pointNumber = 0;
                }
                //current target is the current point number 
                currentTarget = points[pointNumber];
            }
        }
    }
}
