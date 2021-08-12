using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconController : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;

    public Image IconP1Accelerate;
    public Image IconP2Accelerate;

    public Image IconP1Miner;
    public Image IconP2Miner;



    void Update()
    {
        // accelerate
        if (player1.GetComponent<OntriggerAcceleration>().is_getMushroom == true)
        {
            IconP1Accelerate.enabled = true;
        } else { IconP1Accelerate.enabled = false; }

        if (player2.GetComponent<OntriggerAcceleration>().is_getMushroom == true)
        {
            IconP2Accelerate.enabled = true;
        } else { IconP2Accelerate.enabled = false; }

        // miner
        if (player1.GetComponent<OntriggerMiner>().is_getMiner == true)
        {
            IconP1Miner.enabled = true;
        } else { IconP1Miner.enabled = false; }

        if (player2.GetComponent<OntriggerMiner>().is_getMiner == true)
        {
            IconP2Miner.enabled = true;
        }
        else { IconP2Miner.enabled = false; }

    }
}
