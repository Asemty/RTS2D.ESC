using Scellecs.Morpeh;
using UnityEngine;

public static class BuildingServices
{
    public static bool CanBuild(Vector2Int gridSize, bool[] occupancyMap, Vector2Int cursorPos, BuildData buildData)
    {
        if (cursorPos.x < 0
            || cursorPos.y < 0
            || cursorPos.x + buildData.size.x > gridSize.x
            || cursorPos.y + buildData.size.y > gridSize.y
            ) return false;
        int index = 0;
        for(int i = cursorPos.x; i < cursorPos.x + buildData.size.x; i++)
            for(int j = cursorPos.y; j < cursorPos.y + buildData.size.y; j++)
            {
                index = GetMapIndex(gridSize.x, gridSize.y, i, j);
                if(index == -1 || occupancyMap[index]) return false;
            }
        return true;
    }
    public static int GetMapIndex(int width, int height, int x, int y)
    {
        if (x < 0 || y < 0 || x >= width || y >= height) return -1;
        return x + y * width; 
    }
    public static Entity GenerateBuild(World world, Vector2Int pos, BuildData buildData)
    {
        var entity = world.CreateEntity();
        world.GetStash<BuildDataComponent>().Add(entity, new BuildDataComponent()
        {
            data = buildData
        });
        world.GetStash<GridPositionComponent>().Add(entity, new GridPositionComponent()
        {
            position = pos
        });
        return entity;
    }
}