using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentWinner : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;

    private int player1Count;
    private int player2Count;

    void Update()
    {
        player1Count = player1.GetComponent<PlayerChangeColor>().FillCubeCount;
        player2Count = player2.GetComponent<PlayerChangeColor>().FillCubeCount;

        if(player1Count > player2Count)
        {
            AllData.winner = "Player1";
        }
        else
        {
            AllData.winner = "Player2";
        }
    }
}
