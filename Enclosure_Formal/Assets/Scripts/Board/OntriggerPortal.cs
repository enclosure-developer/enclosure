using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class OntriggerPortal : MonoBehaviour
{
    public GameObject portalEffect;
    // variables for the portalTool
    public bool is_portable = true; //  check if we can use the portal
    public bool is_portNow = false; // check if we can port now 
    public Vector3 destination;
    public float timer2 = 0f;
    public float portalFreezeTime = 10f;
    Vector3 portalPosition;
    Vector3 position1;
    Vector3 position2;
    Vector3 position3;
    Vector3 position4;

    // the size of the map
    public int x_max = 80;
    public int x_min = 0;
    public int z_max = 60;
    public int z_min = 0;
    public float y = -1;



    // Start is called before the first frame update
    void Start()
    {
        position1 = new Vector3(x_min + 10, y, z_min + 10);
        position2 = new Vector3(x_max - 10, y, z_min + 10);
        position3 = new Vector3(x_min + 10, y, z_max - 10);
        position4 = new Vector3(x_max - 10, y, z_max - 10);

    }

    // Update is called once per frame
    void Update()
    {
        if (is_portNow)
        {
            if (is_portable)
            {
                portPlayer(); //move the layer when both the conditions statisfies

            }
            is_portable = false; // player cannot port immediately after porting
            timer2 += Time.deltaTime;
            if (timer2 > portalFreezeTime)
            {
                timer2 = 0f;
                is_portNow = false;
                is_portable = true;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 12)
        {
            portalPosition = other.gameObject.transform.position;
            is_portNow = true;
        }

    }

    void portPlayer() // port the player to another door, we can only port the player to the diagonal portal
    {
        int middleX = (x_min + x_max) / 2;
        int middleZ = (z_min + z_max) / 2;
        if (portalPosition.x < middleX && portalPosition.z < middleZ)
        {
            transform.position = new Vector3(x_max - 10, y, z_max - 10);
        }
        else if (portalPosition.x < middleX && portalPosition.z > middleZ)
        {
            transform.position = new Vector3(x_max - 10, y, z_min + 10);
        }
        else if (portalPosition.x >middleX && portalPosition.z < middleZ)
        {
            transform.position = new Vector3(x_min + 10, y, z_max - 10);
        }
        else
        {
            transform.position = new Vector3(x_min + 10, y, z_min + 10);
        }
        portalPosition = new Vector3(0, -100, 0); // set the portalposition back so that player won't automatically port
        if (is_portable)
        {
            Instantiate(portalEffect, transform.position, Quaternion.identity);
        }
    }
}
