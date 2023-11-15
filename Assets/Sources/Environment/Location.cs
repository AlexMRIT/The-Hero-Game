using UnityEngine;

public sealed class Location
{
    private Cell[] _cells;
    private Vector2 _gridSize;
    private Vector2 _blockSize;

    public Location SetLocation(Cell[] cells, Vector2 gridSize, Vector2 blockSize)
    {
        _cells = cells;
        _gridSize = gridSize;
        _blockSize = blockSize;

        return this;
    }

    public bool CheckPositionWithinLimitOfLocation(Vector3 direction)
    {
        return (direction.x < -(_gridSize.x - 1) / 2.0f * _blockSize.x ||
            direction.x > (_gridSize.x - 1) / 2.0f * _blockSize.x ||
            direction.y < -(_gridSize.y - 1) / 2.0f * _blockSize.y ||
            direction.y > (_gridSize.y - 1) / 2.0f * _blockSize.y);
    }

    public Vector2 GetGridSize() => _gridSize;
    public Vector2 GetBlockSize() => _blockSize;
}