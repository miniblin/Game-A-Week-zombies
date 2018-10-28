using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class LevelCreator : MonoBehaviour
{


    [Serializable]
    public class Count
    {
        public int min;
        public int max;

        public Count(int min, int max)
        {
            this.min = min;
            this.max = max;
        }
    }

    int columns = 16;
    int rows = 16;

    public GameObject[] enemies;
    public GameObject[] floorTiles;
    public GameObject[] leftWalls;
    public GameObject[] rightWalls;
    public GameObject[] topWalls;
    public GameObject[] bottomWalls;

    public GameObject[] treeTops;

    private Transform boardHolder;
    private List<Vector3> gridPositions = new List<Vector3>();
    // Use this for initialization
    void InitializeList()
    {
        gridPositions.Clear();

        for (int x = 1; x < columns - 1; x++)
        {
            for (int y = 1; y < rows - 1; y++)
            {
                gridPositions.Add(new Vector3(x, y, 0f));
            }
        }
    }

    // Update is called once per frame
    void BoardSetup()
    {
        boardHolder = new GameObject("Board").transform;
        for (int x = -columns; x < 2*columns + 1; x++)
        {
            for (int y = -rows; y < 2*rows + 1; y++)
            {
                GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];
                if (x <= -1 || x >= columns || y >= rows || y <= -1)
                {
                    toInstantiate = treeTops[Random.Range(0, treeTops.Length)];
                }

                GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
                instance.transform.SetParent(boardHolder);

                if (x == 0)
                {
                    toInstantiate = leftWalls[Random.Range(0, leftWalls.Length)];
                }
                else if (x == columns - 1)
                {
                    toInstantiate = rightWalls[Random.Range(0, rightWalls.Length)];
                }
                else if (y == 0)
                {
                    toInstantiate = bottomWalls[Random.Range(0, bottomWalls.Length)];
                }
                else if (y == rows - 1)
                {
                    toInstantiate = topWalls[Random.Range(0, topWalls.Length)];
                }

                instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
                instance.transform.SetParent(boardHolder);

            }
        }
    }

    Vector3 RandomPosition()
    {
        int randomIndex = Random.Range(0, gridPositions.Count);
        Vector3 randomPosition = gridPositions[randomIndex];
        gridPositions.RemoveAt(randomIndex);
        return randomPosition;
    }


    void LayoutObjectAtRandom(GameObject[] tileArray, int minimum, int maximum)
    {
        int ObjectCount = Random.Range(minimum, maximum);
        for (int i = 0; i < ObjectCount; i++)
        {
            Vector3 randomPosition = RandomPosition();
            GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length)];
            Instantiate(tileChoice, randomPosition, Quaternion.identity);
        }
    }

    public void setupScene(int level)
    {
        BoardSetup();
        InitializeList();

        int enemycount = level;
        LayoutObjectAtRandom(enemies, enemycount, enemycount);
    }
}
