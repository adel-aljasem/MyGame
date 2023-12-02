using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

public class SpatialGrid
{
    private int cellSize;
    private List<GameObject>[,] grid;
    private int gridWidth, gridHeight;

    public SpatialGrid(int width, int height, int cellSize)
    {
        this.cellSize = cellSize;
        this.gridWidth = Math.Max(1, width );
        this.gridHeight = Math.Max(1, height);
        grid = new List<GameObject>[gridWidth, gridHeight];

        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                grid[x, y] = new List<GameObject>();
            }
        }
    }


    public void AddObject(GameObject gameObject)
    {
        var cellIndices = GetCellIndices(gameObject);
        grid[cellIndices.X, cellIndices.Y].Add(gameObject);
    }
    public void Clear()
    {
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                grid[x, y].Clear();
            }
        }
    }

    public void RemoveObject(GameObject gameObject)
    {
        var cellIndices = GetCellIndices(gameObject);
        grid[cellIndices.X, cellIndices.Y].Remove(gameObject);
    }

    public List<GameObject> GetNearbyObjects(GameObject gameObject)
    {
        var nearbyObjects = new List<GameObject>();
        var cellIndices = GetCellIndices(gameObject);

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                int checkX = cellIndices.X + x;
                int checkY = cellIndices.Y + y;

                if (checkX >= 0 && checkX < gridWidth && checkY >= 0 && checkY < gridHeight)
                {
                    nearbyObjects.AddRange(grid[checkX, checkY]);
                }
            }
        }

        return nearbyObjects;
    }

    private Point GetCellIndices(GameObject gameObject)
    {
        // Assume gameObject has a Position property of type Vector2
        int cellX = (int)(gameObject.Transform.Position.X / cellSize);
        int cellY = (int)(gameObject.Transform.Position.Y / cellSize);

        // Ensure the cell indices are within the bounds of the grid
        cellX = Math.Clamp(cellX, 0, gridWidth - 1);
        cellY = Math.Clamp(cellY, 0, gridHeight - 1);

        return new Point(cellX, cellY);
    }


}
