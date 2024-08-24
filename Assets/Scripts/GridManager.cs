using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private GameObject[] gemPrefabs;  
    [SerializeField] private int rows = 8;             
    [SerializeField] private int columns = 10;         
    [SerializeField] private float gemSize = 1.0f;     
    [SerializeField] private float staggerOffset = 0.5f;
    [SerializeField] private Transform startingPoint;

    private GameObject[,] grid;  

    private void Start()
    {
        grid = new GameObject[rows, columns];  
        InitializeGrid();  
    }

    private void InitializeGrid()
    {
        for (int row = 0; row < rows; row++)
        {
            for (int column = 0; column < columns; column++)
            {
                float xOffset = (row % 2 == 0) ? 0 : staggerOffset * gemSize;
                float xPosition = column * gemSize + xOffset;
                float yPosition = -row * gemSize;
                Vector3 gemPosition = startingPoint.position + new Vector3(xPosition, yPosition, 0);
                int randomIndex = Random.Range(0, gemPrefabs.Length);
                GameObject randomGemPrefab = gemPrefabs[randomIndex];
                GameObject newGem = Instantiate(randomGemPrefab, gemPosition, Quaternion.identity, transform);
                grid[row, column] = newGem;
            }
        }
    }
}
