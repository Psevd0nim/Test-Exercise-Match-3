using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] IsometricCubes;
    [SerializeField] private GameObject[] DiceCubes;
    [SerializeField] private GameObject[] GameCubes;
    public Point[,] points = new Point[8, 5];
    int count = 0;
    public GameObject LeftDown;
    public GameObject RightUp;
    private float xStep;
    private float yStep;
    private GameObject[] tempGameObjects;
    private MainSceneUI mainSceneUI;
    public bool GameOverOrWin;
    private CurrentLevel currentLevel;


    private void SkinSet()
    {
        switch (DataPersistence.Instance.arrayNumber)
        //switch (1)
        {
            case 0:
                tempGameObjects = IsometricCubes;
                break;
            case 1:
                tempGameObjects = GameCubes;
                break;
            case 2:
                tempGameObjects = DiceCubes;
                break;
        }
    }

    void LevelSet()
    {
        switch (DataPersistence.Instance.LevelNumber)
        //switch (1)
        {
            case 1:
                currentLevel.LevelOne();
                break;
            case 2:
                currentLevel.LevelTwo();
                break;
            case 3:
                currentLevel.LevelThree();
                break;
            case 4:
                currentLevel.LevelFour();
                break;
            default:
                currentLevel.EndlessMode();
                break;
        }
    }

    private void Start()
    {
        mainSceneUI = GameObject.Find("Canvas").GetComponent<MainSceneUI>();
        currentLevel = GameObject.Find("CurrentLevel").GetComponent<CurrentLevel>();
        SkinSet();
        LevelSet();
        //tempGameObjects = GameCubes;
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

        foreach(var point in points)
        {
            if(point.Y != points.GetLength(0)-1)
                point.upperPoint = points[point.Y + 1, point.X];
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
                points[y, x].cube = Instantiate(tempGameObjects[rnd], points[y, x].position, tempGameObjects[rnd].transform.rotation);
                var value = points[y, x].cube.GetComponent<CubeScript>();
                value.X = x;
                value.Y = y;
            }
        }
    }

    private void LateUpdate()
    {
        foreach (var point in points)
        {
            if (point.freeSpace && point.Y == 7)
            {
                var rnd = Random.Range(0, 5);
                point.cube = Instantiate(tempGameObjects[rnd], point.position + new Vector3(0, 1, 0), tempGameObjects[rnd].transform.rotation);
                {
                    var value = point.cube.GetComponent<CubeScript>();
                    value.X = point.X;
                    value.Y = point.Y;
                }
                point.cube.transform.DOMove(point.position, 1f).SetLink(point.cube);
                point.freeSpace = false;
            }
            else if (point.freeSpace)
            {
                point.SetNewCube();
            }
        }
    }

    public bool GameCondition(GameObject gameObjectCube)
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
            DestroyCubes(gameObjectToDestroy);
            SoundManager.Instance.SuccessClickMethod();
            currentLevel.MovesText.text = $"{--currentLevel.MovesLeft}";
            return false;
        }
        return true;
    }

    void DestroyCubes(List<GameObject> gameObjectToDestroy)
    {
        int pointPerUnit = 5;
        foreach (var c in gameObjectToDestroy)
        {
            var value = c.GetComponent<CubeScript>();
            //Destroy(c);
            value.DestroyCube();
            points[value.Y, value.X].freeSpace = true;
            pointPerUnit += 5;
        }
        mainSceneUI.UpdateScore(pointPerUnit * gameObjectToDestroy.Count);
    }

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