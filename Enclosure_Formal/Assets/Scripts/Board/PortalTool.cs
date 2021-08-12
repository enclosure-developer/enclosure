using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTool : MonoBehaviour
{
    public GameObject portal;

    // to set the locations of the 4 portals
    public int x_min;
    public int x_max;
    public int z_min;
    public int z_max;
    public int y;
    public Vector3 position1;
    public Vector3 position2;
    public Vector3 position3;
    public Vector3 position4;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(portal, new Vector3(x_min + 10, y, z_min + 10), Quaternion.identity);
        Instantiate(portal, new Vector3(x_max - 10, y, z_min + 10), Quaternion.identity);
        Instantiate(portal, new Vector3(x_min + 10, y, z_max - 10), Quaternion.identity);
        Instantiate(portal, new Vector3(x_max - 10, y, z_max - 10), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    { 

    }
}
//iji