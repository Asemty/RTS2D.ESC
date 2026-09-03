using Scellecs.Morpeh;
using System;
using UnityEngine;

public class BuildingConstructionSystem : ISystem
{
    Filter requestFilter;
    Filter gridDataFilter;
    Stash<BuildRequestComponent> buildRequestStash;

    public World World { get; set; }

    public void OnAwake()
    {
        requestFilter = World.Filter
            .With<BuildRequestComponent>()
            .Build();
        gridDataFilter = World.Filter
            .With<GridConfigComponent>()
            .With<GridOccupancyComponent>()
            //.With<GridTerrainComponent>()
            .Build();
        buildRequestStash = World.GetStash<BuildRequestComponent>();
    }

    public void OnUpdate(float deltaTime)
    {
        if (requestFilter.IsEmpty()) return;
        var gridData = gridDataFilter.First();
        ref var gridConf = ref World.GetStash<GridConfigComponent>().Get(gridData);
        ref var gridOccMap = ref World.GetStash<GridOccupancyComponent>().Get(gridData);
        foreach (var request in requestFilter)
        {
            ref var reqComp = ref buildRequestStash.Get(request);
            if (BuildingServices.CanBuild(gridConf.gridSize, gridOccMap.occupancyMap, reqComp.buildingPos, reqComp.buildData))
            {
                int index = 0;
                Debug.Log($"size: {reqComp.buildData.size}");
                var entity = BuildingServices.GenerateBuild(World, reqComp.buildingPos, reqComp.buildData);
                for (int i = reqComp.buildingPos.x; i < reqComp.buildingPos.x + reqComp.buildData.size.x; i++)
                    for (int j = reqComp.buildingPos.y; j < reqComp.buildingPos.y + reqComp.buildData.size.y; j++)
                    {
                        index = BuildingServices.GetMapIndex(gridConf.gridSize.x, gridConf.gridSize.y, i, j);
                        Debug.Log($"x:{i} y:{j}, index:{index}");
                        if (index == -1 || gridOccMap.occupancyMap[index]) continue;

                        gridOccMap.buildEntityMap[index] = entity;
                        gridOccMap.occupancyMap[index] = true;
                    }
            }
            buildRequestStash.Remove(request);
        }
    }
    public void Dispose() { }
}

