using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerChangeColor : MonoBehaviour
{
    [SerializeField] private static int MAX_N = 60, MAX_M = 80;
    private enum VisitState { UNVISITED=0, TRACEVISITED=1, SEARCHVISITED=2,LOOPVISITED=3,INTERRUPTED=-1 };
    // gameobject
    public GameObject level;
    public GameObject Opponent;
    // Trace
    public int[] entryVisited = new int[(MAX_N+2)*(MAX_M+2) + 2];
    Stack<int> loopStack = new Stack<int>();
    private int stackBottom;
    private bool canBeAddToStack;
    // material
    public Material TraceMaterial;
    public Material FillMaterial;
    // Status
    public int FillCubeCount;
    // Component
    private PlayerChangeColor opponentChangeColorScript;


    int getI(int entry)
    {
        return entry / (MAX_M+2);
    }
    int getJ(int entry)
    {
        return entry % (MAX_M+2);
    }
    int toEntry(int i, int j)
    {
        return (i) * (MAX_M+2) + j;
    }
    int getEntry(GameObject cube)
    {
        int i = (int)cube.transform.position.z+1;
        int j = (int)cube.transform.position.x+1;
        return toEntry(i,j);
    }
    int gmax(int a, int b) 
    {
        return (a > b) ? (a ) : (b);
    }
    int gmin(int a, int b)
    {
        return (a > b) ? ( b) : (a);
    }
    GameObject getObject(int entry)
    {
        return level.transform.Find("Row " + "(" + Convert.ToString(getI(entry)-1) + ")").Find("BoardObject " + "(" + Convert.ToString(getJ(entry)-1) + ")").gameObject;
    }
    bool hasUp(int entry,int val)
    {
        int i = getI(entry);
        int j = getJ(entry);
        return entryVisited[toEntry(i + 1, j)] == val;
    }
    bool hasDw(int entry, int val)
    {
        int i = getI(entry);
        int j = getJ(entry);
        return entryVisited[toEntry(i - 1, j)] == val;
    }
    void cubeUpdate(GameObject cube)
    {
        cube.transform.Find("Surface").gameObject.GetComponent<MeshRenderer>().material = cube.GetComponent<CubeVariables>().levelMaterial.Peek();
    }
    void cubeAdjustColorLayer(GameObject cube)
    {
        Stack<Material> fillMaterialStack = new Stack<Material>();
        Stack<Material> traceMaterialStack = new Stack<Material>();
        Material tmpMaterial;
        while (cube.GetComponent<CubeVariables>().levelMaterial.Count > 0)
        {
            tmpMaterial = cube.GetComponent<CubeVariables>().levelMaterial.Pop();
            if (tmpMaterial == TraceMaterial || tmpMaterial==opponentChangeColorScript.TraceMaterial) traceMaterialStack.Push(tmpMaterial);
            else fillMaterialStack.Push(tmpMaterial);
        }
        while (fillMaterialStack.Count > 0)
        {
            tmpMaterial = fillMaterialStack.Pop();
            cube.GetComponent<CubeVariables>().levelMaterial.Push(tmpMaterial);
            if (fillMaterialStack.Count == 0) cube.GetComponent<CubeVariables>().FloorMaterial = tmpMaterial;
        }
        while (traceMaterialStack.Count > 0)
            cube.GetComponent<CubeVariables>().levelMaterial.Push(traceMaterialStack.Pop());
    }


    bool findLoopSuccess(int entry)
    {
        loopStack.Clear();
        canBeAddToStack = false;
        stackBottom = -1;
        return findLoop(entry, entry);
    }

    bool findLoop(int entry, int parentEntry)
    // Don't change entryVisit State
    {
        // Check loop
        if (entryVisited[entry]==2) // find loop
        {
            canBeAddToStack = true;
            loopStack.Push(entry);
            stackBottom = entry;
            entryVisited[entry] = 1;// trace back to 1
            return true;
        }
        else entryVisited[entry] = 2;
        // DFS
        int i = getI(entry);
        int j = getJ(entry);
        Stack<int> destination = new Stack<int>();
        // along 1 & 2, no 3 exist
        if (entryVisited[ toEntry(i + 1, j)] > 0) destination.Push(toEntry(i + 1, j ));
        if (entryVisited[ toEntry(i, j + 1)] > 0) destination.Push(toEntry(i, j + 1));
        if (entryVisited[ toEntry(i - 1, j)] > 0) destination.Push(toEntry(i - 1, j ));
        if (entryVisited[ toEntry(i, j - 1)] > 0) destination.Push(toEntry(i , j - 1));
        while(destination.Count>0)
        {
            int desEntry = destination.Pop();
            if (desEntry!=parentEntry/*cannot search parent*/ && findLoop(desEntry, entry))
            {
                if (entry==stackBottom)
                    canBeAddToStack = false;
                if (canBeAddToStack==true)    
                    loopStack.Push(entry);
                entryVisited[entry] = 1;// trace back to 1
                return true;
            }
        }
        entryVisited[entry] = 1;// trace back to 1
        return false;
    }
    void fillColor()
    // Enter entryVisit: -1, 0, 1. Out entryVisit: 0
    {
        int[] A = loopStack.ToArray();
        int lt=MAX_M, rt=1, up=1, dw=MAX_N;
        for (int p = 0; p < A.Length;++p)
        {
            entryVisited[A[p]] = 3;
            GameObject cube = getObject(A[p]);
            // Update count
            Material previousFloorMaterial = cube.GetComponent<CubeVariables>().FloorMaterial;
            FillCubeCount += 1;
            if (previousFloorMaterial == opponentChangeColorScript.FillMaterial)
                opponentChangeColorScript.FillCubeCount -= 1;
            // Change color
            cube.GetComponent<CubeVariables>().levelMaterial.Push(FillMaterial);
            cubeAdjustColorLayer(cube);
            cubeUpdate(cube);
            int i = getI(A[p]);
            int j = getJ(A[p]);
            lt=gmin(lt, j);
            rt=gmax(rt, j);
            up=gmax(up, i);
            dw=gmin(dw, i);
        }
        for (int i=dw;i<=up;++i)
        {
            bool AllowToFill = false;
            bool upExist = false;
            bool dwExist = false;
            for (int j = lt; j <= rt; ++j)
            {
                int entry = toEntry(i, j);
                if (entryVisited[entry] == 3)
                {
                    upExist |= hasUp(entry, 3);
                    dwExist |= hasDw(entry, 3);
                    if (upExist && dwExist)
                    {
                        AllowToFill = !AllowToFill;
                        upExist = dwExist = false;
                    }
                }
                else if (AllowToFill)
                {
                    GameObject cube = getObject(entry);
                    // Update count
                    Material previousFloorMaterial = cube.GetComponent<CubeVariables>().FloorMaterial;
                    FillCubeCount += 1;
                    if (previousFloorMaterial == opponentChangeColorScript.FillMaterial)
                        opponentChangeColorScript.FillCubeCount -= 1;
                    // Change color
                    cube.GetComponent<CubeVariables>().levelMaterial.Push(FillMaterial);
                    cubeAdjustColorLayer(cube);
                    cubeUpdate(cube);
                }
            }
        }
        for (int p = 0; p < A.Length; ++p)// trace back to 0
            entryVisited[A[p]] = 0;
    }
    void clearTraceColor(int entry, int parentEntry)
    {
        entryVisited[entry] = 0;
        GameObject cube = getObject(entry);
        // Trace: recover previous material.
        Stack<Material> tmpStack=new Stack<Material>();
        Material tmpMaterial;
        while (cube.GetComponent<CubeVariables>().levelMaterial.Count>0)
        {
            tmpMaterial = cube.GetComponent<CubeVariables>().levelMaterial.Pop();
            if (tmpMaterial != TraceMaterial) tmpStack.Push(tmpMaterial);
        }
        while(tmpStack.Count > 0)
            cube.GetComponent<CubeVariables>().levelMaterial.Push(tmpStack.Pop());
        cubeUpdate(cube);
        // DFS
        int i = getI(entry);
        int j = getJ(entry);
        Stack<int> destination = new Stack<int>();
        if (i + 1 <= MAX_N)
            destination.Push(toEntry(i + 1, j));
        if (j + 1 <= MAX_M)
            destination.Push(toEntry(i, j + 1));
        if (i - 1 >= 1)
            destination.Push(toEntry(i - 1, j));
        if (j - 1 >= 1)
            destination.Push(toEntry(i, j - 1));
        while (destination.Count > 0)
        {
            int desEntry = destination.Pop();
            if (desEntry != parentEntry/*cannot search parent*/ && (entryVisited[desEntry] == 1|| entryVisited[desEntry]==-1)/* DFS along 1 & -1*/) 
                clearTraceColor(desEntry, entry);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        // initialize node.
        for (int i = 0; i < (MAX_M + 2) * (MAX_N + 2) + 2; ++i)
            entryVisited[i] = 0;
        FillCubeCount = 0;
        opponentChangeColorScript = Opponent.GetComponent<PlayerChangeColor>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter(Collider other)// other is cube object
    {
        if (other.gameObject.layer == 9) // collide with floor layer(leyer number 9).
        {
            // Update edge state
            GameObject cube = other.gameObject;
            int curEntry = getEntry(cube);
            int i = getI(curEntry);
            int j = getJ(curEntry);

            int flag = Convert.ToInt32(entryVisited[toEntry(i + 1, j)]==1) +
                Convert.ToInt32(entryVisited[toEntry(i, j+1)] == 1) +
                Convert.ToInt32(entryVisited[toEntry(i-1, j)] == 1) +
                Convert.ToInt32(entryVisited[toEntry(i, j-1)] == 1);
            //If the entry is visited
            if (flag >= 2 && findLoopSuccess(curEntry))
            {
                clearTraceColor(curEntry, curEntry);
                fillColor();
            }   
            // If the entry is not visited 
            else
            {
                //  Change opponent's state & Restore current color
                if (opponentChangeColorScript.entryVisited[curEntry] == 1)
                {
                    opponentChangeColorScript.entryVisited[curEntry] = -1;
                    cube.GetComponent<CubeVariables>().levelMaterial.Pop(); // pop opponent's trace
                }
                cube.GetComponent<CubeVariables>().levelMaterial.Push(TraceMaterial);
                // Change color
                //Debug.Log("Change color");
                cubeUpdate(cube);
                entryVisited[curEntry] = 1;
            }
        }
    }
}
