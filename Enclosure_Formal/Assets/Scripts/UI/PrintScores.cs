using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrintScores : MonoBehaviour
{
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        //player1Count = printer1.GetComponent<PlayerChangeColor>().FillCubeCount;
        //player2Count = printer2.GetComponent<PlayerChangeColor>().FillCubeCount;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Text>().text = Convert.ToString(player.GetComponent<PlayerChangeColor>().FillCubeCount);
    }
}
