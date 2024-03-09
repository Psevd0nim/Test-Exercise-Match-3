using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] playableCubes;
    public Point[,] points = new Point[8, 5];
    private bool areaNotFull;


    //private void Start()
    //{
    //    float y = 0.5f;
    //    int number = 0;
    //    for (int i = 0; i < 8; i++)
    //    {
    //        float x = 0.5f;
    //        for (int n = 0; n < 5; n++)
    //        {
    //            points[number] = new Point(new Vector3(x++, y, 0));
    //            number++;
    //        }
    //        y++;
    //    }
    //    SpawnCubes();
    //}

    //private void Start()
    //{
    //    for(int y = 0; y < points.GetLength(0); y++)
    //    {
    //        for(int x = 0;  x < points.GetLength(1); x++)
    //        { 
    //            points[y,x] = new Point(new Vector3(x + 0.5f, y + 0.5f, 0));
    //        }
    //    }
    //    SpawnCubes();
    //}



    //void SpawnCubes()
    //{
    //    for (int i = 0; i < points.Length; i++)
    //    {
    //        var rnd = Random.Range(0, 5);
    //        Instantiate(playableCubes[rnd], points[i].position, playableCubes[rnd].transform.rotation);
    //    }
    //}
    private void Start()
    {
        for (int y = 0; y < points.GetLength(0); y++)
        {
            for (int x = 0; x < points.GetLength(1); x++)
            {
                    points[y, x] = new Point(new Vector3(x, y, 0));
            }
        }
        for (int y = 0; y < points.GetLength(0)-1; y++)
        {
            for (int x = 0; x < points.GetLength(1); x++)
            {
                points[y, x].upperPoint = points[y+1, x];
            }
        }


        SpawnCubes();
    }

    private void Update()
    {
        foreach(var point in points)
        {
            if (point.freeSpace)
            {
                areaNotFull = true;
            }
        }
        
        
        if (areaNotFull)
        {
            foreach(var point in points)
            {
                if (point.freeSpace && point.position.y == 7f)
                {
                    var rnd = Random.Range(0, 5);
                    point.cube = Instantiate(playableCubes[rnd], point.position + new Vector3(0,1,0), playableCubes[rnd].transform.rotation);
                    point.cube.transform.DOMove(point.position, 1f);
                    point.freeSpace = false;
                }
                if (point.freeSpace)
                {
                    point.SetNewCube();
                }
            }
            areaNotFull = false;
        }
    }

    

    void SpawnCubes()
    {
        for (int y = 0; y < points.GetLength(0); y++)
        {
            for (int x = 0; x < points.GetLength(1); x++)
            {
                var rnd = Random.Range(0, 5);
                points[y, x].cube = Instantiate(playableCubes[rnd], points[y, x].position, playableCubes[rnd].transform.rotation);
            }
        }
    }

    public class Point
    {
        public Vector3 position { get; private set; }
        public bool freeSpace;
        public GameObject cube;
        public Point upperPoint;

        public Point(Vector3 position)
        {
            this.position = position;
        }

        public void SetNewCube()
        {
            cube = upperPoint.cube;
            upperPoint.cube = null;
            cube.transform.DOMove(position, 1f);
            upperPoint.freeSpace = true;
            freeSpace = false;
        }
    }
}