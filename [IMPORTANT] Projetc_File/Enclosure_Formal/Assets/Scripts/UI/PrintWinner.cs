using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrintWinner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Text>().text = AllData.winner + "wins!";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
