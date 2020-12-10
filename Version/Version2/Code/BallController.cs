using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.EventSystems;

//AUTHORS: Charlie Christakos, Edgar Centeno
public class BallController : MonoBehaviour
{

    [SerializeField] private LineRenderer LineRenderer;
    [SerializeField] private GameObject areaAffector;
    [SerializeField] private float maxForce;
    [SerializeField] private float forceModifier;
    [SerializeField] private LayerMask rayLayer; 

    private float force;
    private Rigidbody rb;
    
    private Vector3 startPosition;
    private Vector3 endPosition;
    private bool canShoot = false;
    private bool aimReady;
    private Vector3 direction;
    public static BallController instance;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Application.targetFrameRate = 60; //on start max out at 60 fps
    }

    Vector3 ClickedPoint()
    {
        Vector3 position = Vector3.zero;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, rayLayer))
        {
            position = hit.point;
        }
        

        return position;
    }

    // Update is called once per frame
    void Update()
    {

        if (rb.velocity.magnitude <= 0.1f) //aiming only possible when ball is at rest
        {
            
            aimReady = true;
            areaAffector.SetActive(true);

        }
        else
        {
            aimReady = false;
        }
        if (Input.GetMouseButtonDown(0) && !canShoot && aimReady ) //if the mouse button is down and the player has not shot yet
        {
            startPosition = ClickedPoint();
            LineRenderer.gameObject.SetActive(true);
            LineRenderer.SetPosition(0,LineRenderer.transform.localPosition);
        }

        if (Input.GetMouseButton(0) && aimReady ) //if the mouse button is doing nothing
        {
            endPosition = ClickedPoint();
            endPosition.y = LineRenderer.transform.position.y;
            force = Mathf.Clamp(Vector3.Distance(endPosition, startPosition) * forceModifier, 0, maxForce);
            
            //this converts the mouse position into the local position for the ball in unity. 
            LineRenderer.SetPosition(1, transform.InverseTransformPoint(endPosition));
        }

        if (Input.GetMouseButtonUp(0) && aimReady )// if the mouse button is up
        {
            //Increments the score
            if (force > 0.5) //The force when the screen is clicked and the ball does not move comes out as 0.29. We only want to increment when the ball moves.
            {
                Scoring.currentScore++; //Increments the score of the current hole you are playing at
                Scoring.totalScore++; //Increments the score of the entire course while playing.
                UnityEngine.Debug.Log(Scoring.currentScore + "  " + Scoring.totalScore); //Prints out both of the total and current scores in the console while the game is playing
            }

            //gives player the ability to shoot
            canShoot = true;
            
            //line renderer does not show up when player is not aiming. 
            LineRenderer.gameObject.SetActive(false);
        }
    }



    private void FixedUpdate()
    {
        if (canShoot)
        {

            canShoot = false;
            direction = startPosition - endPosition;
            rb.AddForce(direction*force,ForceMode.Impulse);
            areaAffector.SetActive(false);
            force = 0;
            startPosition = endPosition = Vector3.zero;
        }
        
        //if the ball falls below the desired height it will restart the game
        if (rb.position.y < -2f)
        {
            //go to the GameRestart script to execute restart.
            FindObjectOfType<GameRestart>().EndGame();
        }
    }
}
