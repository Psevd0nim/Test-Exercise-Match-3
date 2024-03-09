using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointPosition : MonoBehaviour
{
    public static PointPosition Instance;
    
    public Vector3 position { get; private set; }
    public bool freeSpace;
    public GameObject cube;
    public PointPosition(Vector3 position)
    {  
        this.position = position;
    }

    private void Awake()
    {
        Instance = this;
    }
}