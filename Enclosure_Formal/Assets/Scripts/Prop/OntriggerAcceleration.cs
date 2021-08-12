using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OntriggerAcceleration : MonoBehaviour
{
    public string accelerateKey = "q";
    // variables for the accelerateTool
    public bool is_accelerate = false;
    public bool is_getMushroom = false;
    bool is_pressedOnce = false; // check if the acceleration key is pressed once
    public float timer1 = 0f; // timer to count the time for the acceleration
    public float accelerateTime = 5f; // set the time for acceleration

    public GameObject accelerateAudio; // the sound of zooming

    public GameObject getToolSound; // the sound of getting a tool

    // the size of the map
    public int x_max = 80;
    public int x_min = 0;
    public int z_max = 60;
    public int z_min = 0;
    public float y = -1;

    // reference the player move
    PlayerMove playerMove;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        playerMove = GetComponent<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        speed = playerMove.MoveSpeed;
        if (is_accelerate) // make sure the accelerate time is 5 seconds 
        {
            is_getMushroom = true;
            pressedAcceleraeKey();
            if (is_pressedOnce)
            {
                is_getMushroom = false;
                playerMove.MoveSpeed = 20f;
                timer1 += Time.deltaTime;
                if (timer1 > accelerateTime)
                {
                    is_accelerate = false;
                    playerMove.MoveSpeed = 10f;
                    timer1 = 0;
                    is_pressedOnce = false;
                }

            }
        }
    }


    void pressedAcceleraeKey() // check if the accelerate key is pressed once
    {
        if (Input.GetKeyDown(accelerateKey))
        {
            is_pressedOnce = true;
            Instantiate(accelerateAudio, transform.position, Quaternion.identity);
            accelerateAudio.GetComponent<AudioSource>().Play();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            is_accelerate = true;
            Instantiate(getToolSound, transform.position, Quaternion.identity);
            getToolSound.GetComponent<AudioSource>().Play();
            Destroy(other.gameObject);
        }
    }

}
