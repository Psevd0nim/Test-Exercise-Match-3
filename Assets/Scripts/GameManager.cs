using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] playableCubes;
    //private Point[] points = new Point[40];
    private Point[,] points = new Point[8,5];

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

    private void Start()
    {
        for(int y = 0; y < points.GetLength(0); y++)
        {
            for(int x = 0;  x < points.GetLength(1); x++)
            { 
                points[y,x] = new Point(new Vector3(x + 0.5f, y + 0.5f, 0));
            }
        }
        SpawnCubes();
    }

    //void SpawnCubes()
    //{
    //    for (int i = 0; i < points.Length; i++)
    //    {
    //        var rnd = Random.Range(0, 5);
    //        Instantiate(playableCubes[rnd], points[i].position, playableCubes[rnd].transform.rotation);
    //    }
    //}

    void SpawnCubes()
    {
        for (int y = 0; y < points.GetLength(0); y++)
        {
            for (int x = 0; x < points.GetLength(1); x++)
            {
                var rnd = Random.Range(0, 5);
                Instantiate(playableCubes[rnd], points[y,x].position, playableCubes[rnd].transform.rotation);
            }
        }
    }

    class Point
    {
        public Vector3 position { get; private set; }
        private bool freeSpace;
        public Point(Vector3 position)
        {
            this.position = position;
        }
    }
}