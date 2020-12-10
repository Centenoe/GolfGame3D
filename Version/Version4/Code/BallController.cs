using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using Debug = UnityEngine.Debug;

//AUTHORS: Charlie Christakos, Edgar Centeno
public class BallController : MonoBehaviour
{

    [SerializeField] private LineRenderer LineRenderer;
    [SerializeField] private GameObject areaAffector;
    [SerializeField] private float maxForce;
    [SerializeField] private float forceModifier;
    [SerializeField] private LayerMask rayLayer;

    public float powerupDuration = 10;
    public static float force;
    public float defaultPlayerSpeed = 1;
    public float defaultJumpForce = 8;

    public float speedPowerupTime;
    public float jumpPowerupTime;
    public float playerSpeed;
    public float jumpForce = 80;
    private Rigidbody rb;
    
    private Vector3 startPosition;
    private Vector3 endPosition;
    private bool canShoot = false;
    private bool aimReady;
    private Vector3 direction;
    public static BallController instance;

    public static float outputForce;


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
        if (Input.GetMouseButtonDown(0) && !canShoot && aimReady) //if the mouse button is down and the player has not shot yet
        {
            startPosition = ClickedPoint();
            LineRenderer.gameObject.SetActive(true);
            LineRenderer.SetPosition(0,LineRenderer.transform.localPosition);
        }

        if (Input.GetMouseButton(0) && aimReady) //if the mouse button is doing nothing
        {
            endPosition = ClickedPoint();
            endPosition.y = LineRenderer.transform.position.y;
            force = Mathf.Clamp(Vector3.Distance(endPosition, startPosition) * forceModifier, 0, maxForce);
            outputForce = force * 25;
            
            //this converts the mouse position into the local position for the ball in unity. 
            LineRenderer.SetPosition(1, transform.InverseTransformPoint(endPosition));
        }

        if (Input.GetMouseButtonUp(0) && aimReady )// if the mouse button is up
        {

            UnityEngine.Debug.Log(force);
            //Increments the score (Jacob)
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

        //this is a timer that will count down the time until it reaches zero
        if (speedPowerupTime > 0)
        {
            speedPowerupTime -= Time.deltaTime;
            UnityEngine.Debug.Log("maxforce is: " +maxForce);
            
        }
        else
        {
            speedPowerupTime = 0;
            playerSpeed = defaultPlayerSpeed;
        }

        if (jumpPowerupTime > 0)
        {
            jumpPowerupTime -= Time.deltaTime;
        }
        else
        {
            jumpPowerupTime = 0;
            jumpForce = defaultJumpForce;
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

    public void PickupSpeed(float multiplier)
    {
        maxForce *= multiplier;
        force *= multiplier;
        outputForce *= multiplier;
        speedPowerupTime = powerupDuration;

    }

    public void PickupJump(float multiplier)
    {
        jumpForce *= multiplier;
        jumpPowerupTime = powerupDuration;
        
    }
}
