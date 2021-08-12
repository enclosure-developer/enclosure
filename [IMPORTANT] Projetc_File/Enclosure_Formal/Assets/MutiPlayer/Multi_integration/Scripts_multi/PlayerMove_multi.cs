using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
// using TreeEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerMove_multi : NetworkBehaviour
{
    public int Max_M = 80;
    public int Max_N = 60;
    public KeyCode setForward = KeyCode.W;
    public KeyCode setBack = KeyCode.S;
    public KeyCode setLeft = KeyCode.A;
    public KeyCode setRight = KeyCode.D;
    // Input
    private enum PRESSED_KEY { fwd, back, left, right, noKey };
    [SerializeField] private PRESSED_KEY holdingKey;
    [SerializeField] private float xAxisInput = 0.0f;
    [SerializeField] private float zAxisInput = 0.0f;
    // moving status
    public float MoveSpeed = 10.0f;
    // Component
    Rigidbody rigidbodyComponent;


    void Start()
    {
        holdingKey = PRESSED_KEY.noKey;
        rigidbodyComponent = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer) { return; }


        // update
        //xAxisInput = Input.GetAxis("Horizontal");
        //zAxisInput = Input.GetAxis("Vertical");
        zAxisInput = 0;
        xAxisInput = 0;
        if (Input.GetKey(setForward)) zAxisInput = 1;
        else if (Input.GetKey(setBack)) zAxisInput = -1;
        else if (Input.GetKey(setLeft)) xAxisInput = -1;
        else if (Input.GetKey(setRight)) xAxisInput = 1;


        // update holding key
        if (Input.GetKey(setForward)) holdingKey = PRESSED_KEY.fwd;
        else if (Input.GetKey(setBack)) holdingKey = PRESSED_KEY.back;
        else if (Input.GetKey(setLeft)) holdingKey = PRESSED_KEY.left;
        else if (Input.GetKey(setRight)) holdingKey = PRESSED_KEY.right;
        else holdingKey = PRESSED_KEY.noKey;
        // Correction
        if (holdingKey == PRESSED_KEY.back || holdingKey == PRESSED_KEY.fwd)
        {
            if (transform.position.x - Convert.ToInt32(transform.position.x) != 0.0)
                transform.position = new Vector3(Convert.ToInt32(transform.position.x), 0, transform.position.z);
        }
        else if (holdingKey == PRESSED_KEY.left || holdingKey == PRESSED_KEY.right)
        {
            if (transform.position.z - Convert.ToInt32(transform.position.z) != 0.0)
                transform.position = new Vector3(transform.position.x, 0, Convert.ToInt32(transform.position.z));
        }
        else transform.position = new Vector3(Convert.ToInt32(transform.position.x), 0, Convert.ToInt32(transform.position.z));
    }

    void FixedUpdate()
    {
        if (xAxisInput != 0)
        {
            if (xAxisInput < 0 && transform.position.x <= 0) xAxisInput = 0;
            if (xAxisInput > 0 && transform.position.x >= Max_M - 1) xAxisInput = 0;
            rigidbodyComponent.velocity = new Vector3(xAxisInput * MoveSpeed, 0, 0);
        }
        else
        {
            if (zAxisInput < 0 && transform.position.z <= 0) zAxisInput = 0;
            if (zAxisInput > 0 && transform.position.z >= Max_N - 1) zAxisInput = 0;
            rigidbodyComponent.velocity = new Vector3(0, 0, zAxisInput * MoveSpeed);
        }
    }

}

