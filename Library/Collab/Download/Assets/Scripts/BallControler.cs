using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.Design.Serialization;
using System.Security.Cryptography;

using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class BallControler : MonoBehaviour
{
    public static BallControler instance;

    [SerializeField] private LineRenderer aimLineRenderer;
    [SerializeField] private GameObject aimArea;
    [SerializeField] private float maxForce;
    [SerializeField] private float forceModifier;
    [SerializeField] private LayerMask rayLayer;

    private float force;
    private Rigidbody rb;

    private Vector3 linePointA;
    private Vector3 linePointB;
    private Boolean shootReady;
    private Boolean aimReady;
    private Vector3 direction;

    private void Awake()
    {
        if (instance == null) 
        { 
            instance = this;
        }
        else 
        { 
            Destroy(gameObject);
        }
        rb = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
       // CameraFollow.instance.SetTarget(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(rb.velocity.magnitude <= 0.1f)
        {
            aimReady = true;
            aimArea.SetActive(true);
        }
        else
        {
            aimReady = false;
        }
        if (Input.GetMouseButtonDown(0) && !shootReady && aimReady)
        {
            linePointA = ClickPoint();
            aimLineRenderer.gameObject.SetActive(true);
            aimLineRenderer.SetPosition(0, aimLineRenderer.transform.localPosition);
        }
        if (Input.GetMouseButton(0) && aimReady)
        {
            linePointB = ClickPoint();
            linePointB.y = aimLineRenderer.transform.position.y;
            force = Mathf.Clamp(Vector3.Distance(linePointB, linePointA) * forceModifier, 0, maxForce);
            aimLineRenderer.SetPosition(1, transform.InverseTransformPoint(linePointB));
        }
        if (Input.GetMouseButtonUp(0) && aimReady)
        {
            shootReady = true;
            aimLineRenderer.gameObject.SetActive(false);
        }
        
    }

    private void FixedUpdate()
    {
        if (shootReady)
        {
            shootReady = false;
            direction = linePointA - linePointB;
            rb.AddForce(direction * force, ForceMode.Impulse);
            aimArea.SetActive(false);
            force = 0;
            linePointA = linePointB = Vector3.zero;
        }
    }

    Vector3 ClickPoint()
    {
        Vector3 position = Vector3.zero;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();
        if(Physics.Raycast(ray, out hit, Mathf.Infinity, rayLayer))
        {
            position = hit.point;
        }
        return position;
    }
}
