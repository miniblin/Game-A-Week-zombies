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

    public int maxTunnels = 40;
    public int maxLength = 10;


    public int dimensions = 20;

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
        for (int x = -columns; x < 2 * columns + 1; x++)
        {
            for (int y = -rows; y < 2 * rows + 1; y++)
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
        // BoardSetup();
        // InitializeList();

        // int enemycount = level;
        // LayoutObjectAtRandom(enemies, enemycount, enemycount);
        int[][] map = CreateMap();
       
        instantiateMapGameobjects(map);       
    }

    public void instantiateMapGameobjects( int[][] map)
    {
        for (var i = 0; i < dimensions; i++)
        {
            for (var j = 0; j < dimensions; j++)
            {
                if (map[i][j] == 1)
                {
                    GameObject toInstantiate = treeTops[Random.Range(0, treeTops.Length)];
                    GameObject instance = Instantiate(toInstantiate, new Vector3(i, j, 0f), Quaternion.identity) as GameObject;
                    instance.transform.SetParent(boardHolder);
                }
                else if (map[i][j] == 0)
                {
                    // Debug.Log("Spawning floor tile");
                    GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];
                    GameObject instance = Instantiate(toInstantiate, new Vector3(i, j), Quaternion.identity) as GameObject;
                    instance.transform.SetParent(boardHolder);
                }
            }
        }
    }

    public int[][] CreateMap()
    {
        // Debug.Log("Creating map");
        int[][] directions = new int[4][];
        int[] lastDirection = new int[2], randomDirection;
        //UP DOWN LLEFt AND RIGHT
        directions[0] = new int[] { -1, 0 };
        directions[1] = new int[] { 1, 0 };
        directions[2] = new int[] { 0, -1 };
        directions[3] = new int[] { 0, 1 };


        int currentRow = 11;//Random.Range(0, dimensions);
        int currentColumn =11;// Random.Range(0, dimensions);

        bool reachedExit = false;

        int[][] map = CreateArray(1, dimensions);
        while (maxTunnels > 0 && dimensions > 0 && maxLength > 0 && !reachedExit)
        {
            // Debug.Log("map loop begun");
            do
            {
                // Debug.Log("selecting random direction");
                randomDirection = directions[Random.Range(0, directions.GetLength(0))];
            } while ((randomDirection[0] == -lastDirection[0] &&
              randomDirection[1] == -lastDirection[1]) ||
             (randomDirection[0] == lastDirection[0] &&
              randomDirection[1] == lastDirection[1]));


            int randomLength = Random.Range(1, maxLength);
            int tunnelLength = 0;

            while (tunnelLength < randomLength)
            {
                // Debug.Log("Creating tunnel");
                if (((currentRow <= 10) && (randomDirection[0] == -1)) ||
                   ((currentColumn <= 10) && (randomDirection[1] == -1)) ||
                   ((currentRow >= dimensions - 10) && (randomDirection[0] == 1)) ||
                 ((currentColumn >= dimensions - 10) && (randomDirection[1] == 1)))
                { break; }
                
                else
                {

                    map[currentRow][currentColumn] = 0;
                    currentRow += randomDirection[0];
                    currentColumn += randomDirection[1];
                    tunnelLength++;
                }
            }
            if (tunnelLength > 0)
            {
                // Debug.Log("Reducing max tunnels");
                //does this make sense
                lastDirection = randomDirection;
                maxTunnels--;
            }

        }
        return map;
    }
    public int[][] CreateArray(int num, int dimensions)
    {
        int[][] array = new int[dimensions][];
        for (var i = 0; i < dimensions; i++)
        {
            array[i] = new int[dimensions];
            for (var j = 0; j < dimensions; j++)
            {
                array[i][j] = num;
            }
        }
        return array;
    }
}

