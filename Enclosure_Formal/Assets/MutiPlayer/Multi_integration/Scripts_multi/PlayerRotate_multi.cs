using System;
using System.Collections;
using System.Collections.Generic;
// using TreeEditor;
using UnityEngine;

public class PlayerRotate_multi : MonoBehaviour
{
    public KeyCode setForward = KeyCode.W;
    public KeyCode setBack = KeyCode.S;
    public KeyCode setLeft = KeyCode.A;
    public KeyCode setRight = KeyCode.D;
    // rotate input
    Boolean upKeyPressed, downKeyPressed, leftKeyPressed, rightKeyPressed;
    float rotateSpeed = 4f;
    Quaternion targetAngle;
    // component
    private Rigidbody rigidbodyComponent;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
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
        if (leftKeyPressed) { leftKeyPressed = false; targetAngle = Quaternion.Euler(0, 0f, 0); }
        else if (rightKeyPressed) { rightKeyPressed = false; targetAngle = Quaternion.Euler(0, 180f, 0); }
        else if (upKeyPressed) { upKeyPressed = false; targetAngle = Quaternion.Euler(0, 90f, 0); }
        else if (downKeyPressed) { downKeyPressed = false; targetAngle = Quaternion.Euler(0, -90f, 0); }
        transform.rotation = Quaternion.Slerp(transform.rotation, targetAngle, rotateSpeed/* adjust rotate speed*/ * Time.deltaTime);
    }
}
