using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomWalkLevelCreator : MonoBehaviour
{

    public int dimensions = 5;

    public int maxTunnels = 3;
    public int maxLength = 3;
    int rows;
    int columns;

    private int[][] directions = new int[4][];
    int[] lastDirection = new int[2], randomDirection;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public int[][] CreateArray(int num, int dimensions)
    {
        int[][] array = new int[dimensions][];
        for (var i = 0; i < dimensions; i++)
        {
            for (var j = 0; j < dimensions; j++)
            {
                array[i][j] = num;
            }
        }
        return array;
    }


    public int[][] createMap()
    {
        //UP DOWN LLEFt AND RIGHT
        directions[0] = new int[] { -1, 0 };
        directions[1] = new int[] { 1, 0 };
        directions[2] = new int[] { 0, -1 };
        directions[3] = new int[] { 0, 1 };


        int currentRow = Random.Range(0, dimensions);
        int currentColumn = Random.Range(0, dimensions);

        int[][] map = CreateArray(1, dimensions);
        while (maxTunnels > 0 && dimensions > 0 && maxLength > 0)
        {
            do
            {
                randomDirection = directions[Random.Range(0, directions.GetLength(0))];
            } while ((randomDirection[0] == -lastDirection[0] &&
              randomDirection[1] == -lastDirection[1]) ||
             (randomDirection[0] == lastDirection[0] &&
              randomDirection[1] == lastDirection[1]));


            int randomLength = Random.Range(1, maxLength);
            int tunnelLength = 0;

            while (tunnelLength < randomLength)
            {
                if (((currentRow == 0) && (randomDirection[0] == -1)) ||
                   ((currentColumn == 0) && (randomDirection[1] == -1)) ||
                   ((currentRow == dimensions - 1) && (randomDirection[0] == 1)) ||
                 ((currentColumn == dimensions - 1) && (randomDirection[1] == 1)))
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
                lastDirection = randomDirection;
                maxTunnels--;
            }

        }
        return map;
    }
}


