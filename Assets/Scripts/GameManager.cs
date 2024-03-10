using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] IsometricCubes;
    [SerializeField] private GameObject[] DiceCubes;
    public Point[,] points = new Point[8, 5];
    int count = 0;
    public GameObject LeftDown;
    public GameObject RightUp;
    private float xStep;
    private float yStep;


    private void Start()
    {
        xStep = (RightUp.transform.position.x - LeftDown.transform.position.x) / 5;
        yStep = (RightUp.transform.position.y - LeftDown.transform.position.y) / 8;
        float xTemp = LeftDown.transform.position.x - (xStep/2);
        float yTemp = LeftDown.transform.position.y - (yStep/2);



        for (int y = 0; y < points.GetLength(0); y++)
        {
            yTemp += yStep;
            for (int x = 0; x < points.GetLength(1); x++)
            {
                xTemp += xStep;
                points[y, x] = new Point(new Vector3(xTemp, yTemp, 0), x, y);
            }
            xTemp = LeftDown.transform.position.x - (xStep / 2);
        }
        for (int y = 0; y < points.GetLength(0) - 1; y++)
        {
            for (int x = 0; x < points.GetLength(1); x++)
            {
                points[y, x].upperPoint = points[y + 1, x];
            }
        }
        SpawnCubes();
    }

    void SpawnCubes()
    {
        for (int y = 0; y < points.GetLength(0); y++)
        {
            for (int x = 0; x < points.GetLength(1); x++)
            {
                var rnd = Random.Range(0, 5);
                points[y, x].cube = Instantiate(IsometricCubes[rnd], points[y, x].position, IsometricCubes[rnd].transform.rotation);
                var value = points[y, x].cube.GetComponent<CubeScript>();
                value.X = x;
                value.Y = y;
            }
        }
    }

    //private void Start()
    //{
    //    for (int y = 0; y < points.GetLength(0); y++)
    //    {
    //        for (int x = 0; x < points.GetLength(1); x++)
    //        {
    //            points[y, x] = new Point(new Vector3(x, y, 0), x, y);
    //        }
    //    }
    //    for (int y = 0; y < points.GetLength(0) - 1; y++)
    //    {
    //        for (int x = 0; x < points.GetLength(1); x++)
    //        {
    //            points[y, x].upperPoint = points[y + 1, x];
    //        }
    //    }
    //    SpawnCubes();
    //}

    //void SpawnCubes()
    //{
    //    for (int y = 0; y < points.GetLength(0); y++)
    //    {
    //        for (int x = 0; x < points.GetLength(1); x++)
    //        {
    //            var rnd = Random.Range(0, 5);
    //            points[y, x].cube = Instantiate(IsometricCubes[rnd], points[y, x].position, IsometricCubes[rnd].transform.rotation);
    //        }
    //    }
    //}



    private void LateUpdate()
    {
        foreach (var point in points)
        {
            if (point.freeSpace && point.Y == 7)
            {
                var rnd = Random.Range(0, 5);
                point.cube = Instantiate(IsometricCubes[rnd], point.position + new Vector3(0, 1, 0), IsometricCubes[rnd].transform.rotation);
                {
                    var value = point.cube.GetComponent<CubeScript>();
                    value.X = point.X;
                    value.Y = point.Y;
                }
                point.cube.transform.DOMove(point.position, 1f);
                point.freeSpace = false;
            }
            else if (point.freeSpace)
            {
                point.SetNewCube();
            }
        }
    }

    public class Point
    {
        public Vector3 position { get; private set; }
        public bool freeSpace;
        public GameObject cube;
        public Point upperPoint;
        public int X { get; private set; }
        public int Y { get; private set; }

        public Point(Vector3 position, int x, int y)
        {
            this.position = position;
            this.X = x;
            this.Y = y;
        }

        public void SetNewCube()
        {
            Point tempPoint = upperPoint;
            while (true)
            {
                if (tempPoint.cube == null)
                {
                    if (tempPoint.Y == 7) break;
                    tempPoint = tempPoint.upperPoint;
                }
                else
                {
                    cube = tempPoint.cube;
                    {
                        var value = cube.GetComponent<CubeScript>();
                        value.X = X;
                        value.Y = Y;
                    }
                    tempPoint.cube = null;
                    cube.transform.DOMove(position, 1f);
                    tempPoint.freeSpace = true;
                    freeSpace = false;
                    break;
                }
            }
        }
    }

    public void GameCondition(GameObject gameObjectCube)
    {
        DoIt(gameObjectCube);
    }

    void DoIt(GameObject gameObjectCube)
    {
        var colorCube = gameObjectCube.GetComponent<SpriteRenderer>().color;
        var gameObjectToDestroy = new List<GameObject>();
        gameObjectToDestroy.Add(gameObjectCube);
        while (true)
        {
            var value = gameObjectToDestroy[count].GetComponent<CubeScript>();
            int x = value.X;
            int y = value.Y;

            var gameObjectCubes = new List<GameObject>();

            CollectCrossGameObjects(gameObjectCubes, x, y);

            foreach (var c in gameObjectCubes)
            {
                if (c.GetComponent<SpriteRenderer>().color == colorCube)
                {
                    if (!gameObjectToDestroy.Contains(c))
                        gameObjectToDestroy.Add(c);
                }
            }
            count++;
            if (count == gameObjectToDestroy.Count)
            {
                count = 0;
                break;
            }
        }
        if (gameObjectToDestroy.Count > 1)
        {

            foreach (var c in gameObjectToDestroy)
            {
                var value = c.GetComponent<CubeScript>();
                Destroy(c);
                points[value.Y, value.X].freeSpace = true;
            }
        }

    }

    //void DoIt(GameObject gameObjectCube)
    //{
    //    var colorCube = gameObjectCube.GetComponent<SpriteRenderer>().color;
    //    var gameObjectToDestroy = new List<GameObject>();
    //    gameObjectToDestroy.Add(gameObjectCube);
    //    while (true)
    //    {
    //        int x = (int)gameObjectToDestroy[count].transform.position.x;
    //        int y = (int)gameObjectToDestroy[count].transform.position.y;

    //        var gameObjectCubes = new List<GameObject>();

    //        CollectCrossGameObjects(gameObjectCubes, x, y);

    //        foreach (var c in gameObjectCubes)
    //        {
    //            if (c.GetComponent<SpriteRenderer>().color == colorCube)
    //            {
    //                if (!gameObjectToDestroy.Contains(c))
    //                    gameObjectToDestroy.Add(c);
    //            }
    //        }
    //        count++;
    //        if (count == gameObjectToDestroy.Count)
    //        {
    //            count = 0;
    //            break;
    //        }
    //    }
    //    if (gameObjectToDestroy.Count > 1)
    //    {

    //        foreach (var c in gameObjectToDestroy)
    //        {
    //            var pos = c.transform.position;
    //            Destroy(c);
    //            points[(int)pos.y, (int)pos.x].freeSpace = true;
    //        }
    //    }

    //}

    void CollectCrossGameObjects(List<GameObject> gameObjectCubes, int x, int y)
    {
        int currentIteration = 0;
        int[] d = { -1, 0, 1 };
        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
            {
                var dx = x + d[i];
                var dy = y + d[j];
                currentIteration++;
                if (dx >= 0 && dy >= 0 && dy < points.GetLength(0) && dx < points.GetLength(1) && currentIteration % 2 == 0)
                    gameObjectCubes.Add(points[dy, dx].cube);
            }
    }
}