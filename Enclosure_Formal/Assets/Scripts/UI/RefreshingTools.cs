using System.Collections;
using System.Collections.Generic;
// using UnityEditorInternal;
using UnityEngine;

public class RefreshingTools : MonoBehaviour
{
    public GameObject AccelerationPrefab; //the accelerationmushroom
    public GameObject MinerPrefab;
    public int x_max = 80;
    public int x_min = 0;
    public int z_max = 60;
    public int z_min = 0;
    public float y = -1;
    public int intervalTime = 10; //the interval time to create the next tool
    // create the 4 locations for initializing the accelerationTool
    int x1, x2;
    int x, z;
    public int decision = 0;

    Timer timer;
    bool is_toolCreated; // record whether tool is created
                         // to prevent the toolbeing created twice


    // Start is called before the first frame update
    void Start()
    {
        int deltaX = (x_max - x_min) / 4;
        int deltaZ = (z_max - z_min) / 4;
        x1 = x_min + deltaX; // set the four positions
        x2 = x_max - deltaX;
        timer = GetComponent<Timer>();
    }

    // Update is called once per frame
    void Update()
    {
        int seconds = timer.secondsLeft;
        if (seconds % 10 == 0)
        {
            InitializeTool();
        }
        else
        {
            is_toolCreated = false; // set back to false so that tool can
                                    // be created again
                                   
        }
    }

    void InitializeTool() // initialize the tool (only once)
    {
        if (!is_toolCreated)
        {
            decision = UnityEngine.Random.Range(1, 3); // randomly decide a position
            if (decision == 1)
            {
                x = x1;
                z = (z_max + z_min) / 2 + 5;
            }
            else if (decision == 2)
            {
                x = x2;
                z = (z_max + z_min) / 2 - 5;
            }
    
            Instantiate(AccelerationPrefab, new Vector3(x, y, z), Quaternion.identity); // create a accelerationtool at a random space in the range
            Instantiate(MinerPrefab, new Vector3((x_max + x_min) / 2, y, (z_max + z_min) / 2), Quaternion.identity); 
            // create a tool at the middle of the map
            is_toolCreated = true;
            decision = 0;
        }
    }
}
