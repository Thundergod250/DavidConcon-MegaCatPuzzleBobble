using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private GameObject gemPrefab;
    [SerializeField] private int rows = 8;
    [SerializeField] private int columns = 15;
    [SerializeField] private float gemSize = 1.0f;
    [SerializeField] private float staggerOffset = 0.5f;
    [SerializeField] private Transform startingPoint;
    [SerializeField] private Transform ceiling;

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

                GameObject newGem = Instantiate(gemPrefab, gemPosition, Quaternion.identity);
                grid[row, column] = newGem;
                GameManager.Instance.AddGem(newGem);
                newGem.GetComponent<Rigidbody2D>().isKinematic = true;
                newGem.AddComponent<Snap>();
                newGem.transform.parent = ceiling; 

                if (row < rows / 2)
                {
                    if (newGem.TryGetComponent(out Gem gem))
                    {
                        gem.RandomizeGemType();
                    }
                }
                else
                {
                    newGem.SetActive(false);
                }
            }
        }
    }
}
