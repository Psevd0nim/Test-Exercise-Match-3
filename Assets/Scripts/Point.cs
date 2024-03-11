using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            float timeStep = 1f;
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
                    cube.transform.DOMove(position, timeStep);
                    tempPoint.freeSpace = true;
                    freeSpace = false;
                    break;
                }
            }
        }
}
