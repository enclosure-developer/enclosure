using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class startChangeGround : MonoBehaviour
{
    [SerializeField] private static int MAX_N = 60, MAX_M = 80;
    public GameObject level;
    public Material mat;



    int getI(int entry)
    {
        return entry / (MAX_M + 2);
    }
    int getJ(int entry)
    {
        return entry % (MAX_M + 2);
    }

    int toEntry(int i, int j)
    {
        return (i) * (MAX_M + 2) + j;
    }

    GameObject getObject(int entry)
    {
        return level.transform.Find("Row " + "(" + Convert.ToString(getI(entry) - 1) + ")").Find("BoardObject " + "(" + Convert.ToString(getJ(entry) - 1) + ")").gameObject;
    }


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 1; i <= MAX_N; i++)
        {
            for (int j = 1; j <= MAX_M; j++)
            {
                getObject(toEntry(i, j)).transform.Find("Ground").gameObject.GetComponent<MeshRenderer>().material = mat;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
