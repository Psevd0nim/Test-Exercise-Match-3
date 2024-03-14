using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    private GameManager gameManager;
    int count = 0;
    private int loseCondition;
    public GameObject GameOverObject;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        StartCoroutine(TimeSkip());
    }

    IEnumerator TimeSkip()
    {
        while(true)
        {
            yield return new WaitForSeconds(5);
            CheckForLose();
            if(loseCondition == gameManager.points.Length)
            {
                GameOverObject.SetActive(true);
                gameManager.GameOverOrWin = true;
                break;
            }
        }
    }

    void CheckForLose()
    {
        loseCondition = 0;
        foreach (var point in gameManager.points)
        {
            var gameObjectCube = point.cube;
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
                break;
            }
            else
            {
                loseCondition++;
            }
        }
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
                if (dx >= 0 && dy >= 0 && dy < gameManager.points.GetLength(0) && dx < gameManager.points.GetLength(1) && currentIteration % 2 == 0)
                    gameObjectCubes.Add(gameManager.points[dy, dx].cube);
            }
    }
}