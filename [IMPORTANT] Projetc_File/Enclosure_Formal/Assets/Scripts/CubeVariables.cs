using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CubeVariables : MonoBehaviour
{
    // Start is called before the first frame update
    public Stack<Material> levelMaterial=new Stack<Material>();
    public Material FloorMaterial;

    // for LAN multiplayer
    public Material previousTraceMaterial;
    public Material floorMaterial;
    void Start()
    {
        levelMaterial.Push(FloorMaterial);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
