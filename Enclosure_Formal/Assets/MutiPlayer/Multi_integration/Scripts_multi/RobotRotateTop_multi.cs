using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
// using TreeEditor;
using UnityEngine;

public class RobotRotateTop_multi : NetworkBehaviour
{
    public KeyCode setForward = KeyCode.W;
    public KeyCode setBack = KeyCode.S;
    public KeyCode setLeft = KeyCode.A;
    public KeyCode setRight = KeyCode.D;
    // rotate input
    Boolean upKeyPressed, downKeyPressed, leftKeyPressed, rightKeyPressed;
    public float rotateSpeed;
    Quaternion targetAngle;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!gameObject.transform.parent.gameObject.GetComponent<NetworkIdentity>().isLocalPlayer) { return; }


        // get direction key.
        if (Input.GetKeyDown(setLeft))
            leftKeyPressed = true;
        else if (Input.GetKeyDown(setRight))
            rightKeyPressed = true;
        else if (Input.GetKeyDown(setForward))
            upKeyPressed = true;
        else if (Input.GetKeyDown(setBack))
            downKeyPressed = true;
    }

    private void FixedUpdate()
    {
        // rotate object.
        if (leftKeyPressed) { leftKeyPressed = false; targetAngle = Quaternion.Euler(0, -90f, 0); }
        else if (rightKeyPressed) { rightKeyPressed = false; targetAngle = Quaternion.Euler(0, 90f, 0); }
        else if (upKeyPressed) { upKeyPressed = false; targetAngle = Quaternion.Euler(0, 0f, 0); }
        else if (downKeyPressed) { downKeyPressed = false; targetAngle = Quaternion.Euler(0, 180f, 0); }
        transform.rotation = Quaternion.Lerp(transform.rotation, targetAngle, rotateSpeed/* adjust rotate speed*/ * Time.deltaTime);
    }
    /*
    float rotateSpeed = 10f;
    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;
    Rigidbody m_Rigidbody;
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        m_Movement.Set(horizontal,0f,vertical);
        m_Movement.Normalize();

        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, rotateSpeed*Time.deltaTime,0f);
        m_Rotation = Quaternion.LookRotation(desiredForward);
        m_Rigidbody.MoveRotation(m_Rotation);
    }
    */
}
