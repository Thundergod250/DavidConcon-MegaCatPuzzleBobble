using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    [SerializeField] private GemType gemType;
    [SerializeField] private Sprite[] gemSprites;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private int indexNumber;
    private int gridRow, gridColumn; // Add these to store the gem's position in the grid

    public void RandomizeGemType()
    {
        indexNumber = Random.Range(0, 6);
        SetType(indexNumber);
    }

    public void SetType(int index)
    {
        gemType = (GemType)index;
        spriteRenderer.sprite = gemSprites[index];
    }

    public void Activate(int index)
    {
        SetType(index);
        CheckMatches();
    }

    public void InitializePosition(int row, int column)
    {
        gridRow = row;
        gridColumn = column;
    }

    private void CheckMatches()
    {
        List<Gem> matchedGems = new List<Gem>();
        FindMatches(matchedGems, gridRow, gridColumn);

        if (matchedGems.Count >= 3)
        {
            foreach (Gem gem in matchedGems)
            {
                gem.gameObject.SetActive(false); // Deactivate the matching gems
                // Alternatively, you can destroy them if that is your game logic
                // Destroy(gem.gameObject);
            }
        }
    }

    private void FindMatches(List<Gem> matchedGems, int row, int column)
    {
        if (matchedGems.Contains(this)) return; // Prevent adding the same gem multiple times
        matchedGems.Add(this);

        // Get the 2D array of gems from the GameManager
        GameObject[,] grid = GameManager.Instance.GetGrid();

        // Check all four directions for matches
        CheckDirection(matchedGems, row, column, 1, 0);  // Right
        CheckDirection(matchedGems, row, column, -1, 0); // Left
        CheckDirection(matchedGems, row, column, 0, 1);  // Up
        CheckDirection(matchedGems, row, column, 0, -1); // Down
    }

    private void CheckDirection(List<Gem> matchedGems, int row, int column, int rowOffset, int colOffset)
    {
        GameObject[,] grid = GameManager.Instance.GetGrid();

        int newRow = row + rowOffset;
        int newCol = column + colOffset;

        // Check if the new position is within the grid boundaries
        if (newRow >= 0 && newRow < grid.GetLength(0) && newCol >= 0 && newCol < grid.GetLength(1))
        {
            GameObject adjacentGemObject = grid[newRow, newCol];

            if (adjacentGemObject != null)
            {
                Gem adjacentGem = adjacentGemObject.GetComponent<Gem>();
                if (adjacentGem != null && adjacentGem.gemType == gemType && !matchedGems.Contains(adjacentGem))
                {
                    adjacentGem.FindMatches(matchedGems, newRow, newCol);
                }
            }
        }
    }

    public GemType GetGemType() => gemType;
    public int GetIndexNumber() => indexNumber;
}
