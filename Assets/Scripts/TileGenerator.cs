using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TileGenerator : MonoBehaviour
{
    [Serializable]
    public class TileSize
    {
        public int min;
        public int max;

        public TileSize(int minn, int maxn)
        {
            min = minn;
            max = maxn;
        }
    }

    public TileSize tileSize;
    public GameObject[] blocks;
    public GameObject[] boundarys;
    public GameObject[] floors;
    public GameObject food;
    public GameObject soda;
    public GameObject exit;

    public GameObject player;
    public GameObject[] enemies;

    private Transform boundaryHolder;
    private Transform blockWallHolder;
    private Transform itemHolder;
    private Transform enemyHolder;
    private GameManager gameManager;

    public void Awake()
    {
        gameManager = GetComponent<GameManager>();
    }

    public void GenerateTiles()
    {
        boundaryHolder = new GameObject("Boundary").transform;
        blockWallHolder = new GameObject("Walls").transform;
        itemHolder = new GameObject("Items").transform;
        enemyHolder = new GameObject("Enemies").transform;

        float diffOffset = Mathf.Log(gameManager.level, 2) / 50;
        Debug.Log("Difficulty in this stage: " + diffOffset);

        for (int i = tileSize.min; i < tileSize.max; i++)
        {
            for (int j = tileSize.min; j < tileSize.max; j++)
            {
                Vector3 nowPos = new Vector3(i, j);
                // Generate Boundaries
                if (i == tileSize.min || i == tileSize.max - 1 || j == tileSize.min || j == tileSize.max - 1)
                {
                    (Instantiate(boundarys[Random.Range(0, boundarys.Length)], nowPos, Quaternion.identity) as GameObject).transform.SetParent(boundaryHolder);
                }
                // Generate Floors
                else
                {
                    (Instantiate(floors[Random.Range(0, floors.Length)], nowPos, Quaternion.identity) as GameObject).transform.SetParent(boundaryHolder);

                    float luckyLevel = Random.Range(0.0f, 1.0f);

                    //Generate Player
                    if (i == 1 && j == 1)
                    {
                        Instantiate(player, nowPos, Quaternion.identity);
                    }
                    //Generate Exit
                    else if (i == tileSize.max - 2 && j == tileSize.max - 2)
                    {
                        Instantiate(exit, nowPos, Quaternion.identity);
                    }
                    // Generate Items
                    // Soda
                    else if (luckyLevel >= 0.94f + diffOffset)
                    {
                        (Instantiate(soda, nowPos, Quaternion.identity) as GameObject).transform.SetParent(itemHolder);
                    }
                    // Food
                    else if (luckyLevel >= 0.82f + diffOffset)
                    {
                        (Instantiate(food, nowPos, Quaternion.identity) as GameObject).transform.SetParent(itemHolder);
                    }
                    // Easy Enemy
                    else if (luckyLevel <= 0.02f + diffOffset)
                    {
                        (Instantiate(enemies[0], nowPos, Quaternion.identity) as GameObject).transform.SetParent(enemyHolder);
                    }
                    // Hard Enemy
                    else if (luckyLevel <= 0.05f + diffOffset)
                    {
                        // Spawn Protection
                        if (i >= 3 || j >= 3)
                        {
                            (Instantiate(enemies[1], nowPos, Quaternion.identity) as GameObject).transform.SetParent(enemyHolder);
                        }
                    }

                    //Generate Wall Blocks
                    else if (Random.Range(0.0f, 1.0f) < 0.24f)
                    {
                        // Spawn Protection
                        if (i >= 3 || j >= 3)
                        {
                            (Instantiate(blocks[Random.Range(0, blocks.Length)], nowPos, Quaternion.identity) as GameObject).transform.SetParent(blockWallHolder);
                        }
                    }
                }
            }
        }
    }
}
