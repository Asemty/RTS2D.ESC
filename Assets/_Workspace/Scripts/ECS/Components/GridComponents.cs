using Scellecs.Morpeh;
using UnityEngine;

public struct GridConfigComponent : IComponent
{
    public Vector2Int gridSize;
}
public struct GridOccupancyComponent : IComponent
{
    public Entity[] buildEntityMap;
    public bool[] occupancyMap;
}
public struct GridTerrainComponent : IComponent
{
    public int[] heightMap;
}
public struct GridViewComponent : IComponent
{
    public LineRenderer lineRenderer;
}

public struct GridDrawRequestComponent : IComponent { }
public struct IsCursorComponent : IComponent { }
public struct GridCursorPositionComponent : IComponent
{
    public Vector2Int position;
}
public struct GridPositionComponent : IComponent
{
    public Vector2Int position;
}
public struct GridSizeComponent : IComponent
{
    public Vector2Int size;
}