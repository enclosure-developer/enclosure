using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class OntriggerMiner : MonoBehaviour
{
    public bool is_getMiner = false;
    public GameObject atomicBomb; // the bomb to be released
    public GameObject theOpponent; // the gameobject referring to the other player
    public GameObject bigExplosion; // the gameobject invoking the explosion
    public GameObject getToolAudio; // the sound of getting the tool
    public GameObject setMinerAudio; // the sound of setting the miner
    public GameObject sparking;
    public string bombSetKey = "e";

    public bool is_setter = false; // check if the player is the setter of the atomicbomb
    bool is_freezing = false; // determine if the player is freezing due to the explosion

    float timer3 = 0f; // timer to count the time of freezing
    float stunDuration = 5f; // the other player need to freeze for 5 seconds to recover

    // reference the playermove to modify the speed
    PlayerMove playerMove;

    // Start is called before the first frame update
    void Start()
    {
        playerMove = GetComponent<PlayerMove>();

    }

    // Update is called once per frame
    void Update()
    {
        if (is_getMiner)
        {
            if (Input.GetKeyDown(bombSetKey))
            {
                ReleaseMiner();
            }
        }
        if (is_freezing)
        {
            playerMove.MoveSpeed = 0f;
            timer3 += Time.deltaTime;
            if (timer3 > stunDuration)
            {
                playerMove.MoveSpeed = 10f;
                is_freezing = false;
                timer3 = 0f;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 10)
        {
            is_getMiner = true;
            Destroy(other.gameObject);
            Instantiate(getToolAudio, transform.position, Quaternion.identity);
            getToolAudio.GetComponent<AudioSource>().Play();
        }
        if (other.gameObject.layer == 11)
        {
            if (!is_setter)
            {
                Vector3 position = other.GetComponent<Transform>().position;
                Instantiate(bigExplosion, position, Quaternion.identity);
                Destroy(other.gameObject);
                Instantiate(sparking, transform.position, Quaternion.identity);
                is_freezing = true;
                theOpponent.GetComponent<OntriggerMiner>().is_setter = false;
            }
        }
    }

    void ReleaseMiner()
    {
        Vector3 positionNow = transform.position;
        Instantiate(atomicBomb, positionNow, Quaternion.identity);
        Instantiate(setMinerAudio, transform.position, Quaternion.identity);
        setMinerAudio.GetComponent<AudioSource>().Play();
        is_setter = true;
        is_getMiner = false;
    }

    void RunIntoBomb() // the other player run into the bomb and cause a explosion
    {
        // implement the explosion effect
    }
}
