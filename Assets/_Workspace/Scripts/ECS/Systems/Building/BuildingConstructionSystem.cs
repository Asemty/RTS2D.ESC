using Scellecs.Morpeh;
using System;
using UnityEngine;

public class BuildingConstructionSystem : ISystem
{
    Filter requestFilter;
    Filter gridDataFilter;
    Filter buildPrefFilter;
    Filter cellInfoFilter;
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
        buildPrefFilter = World.Filter
            .With<BuildPrefabComponent>()
            .Build();
        cellInfoFilter = World.Filter
            .With<CellInfoComponent>()
            .Build();
        buildRequestStash = World.GetStash<BuildRequestComponent>();
    }

    public void OnUpdate(float deltaTime)
    {
        if(requestFilter.IsEmpty()) return;
        var gridData = gridDataFilter.First();
        ref var gridConf = ref World.GetStash<GridConfigComponent>().Get(gridData);
        ref var gridOccMap = ref World.GetStash<GridOccupancyComponent>().Get(gridData);
        ref var pref = ref World.GetStash<BuildPrefabComponent>().Get(buildPrefFilter.First());
        ref var cellInfo = ref World.GetStash<CellInfoComponent>().Get(cellInfoFilter.First());
        foreach (var request in requestFilter)
        {
            ref var reqComp = ref buildRequestStash.Get(request);
            if (BuildingServices.CanBuild(gridConf.gridSize, gridOccMap.buildIdMap, reqComp.buildingPos, reqComp.buildData))
            {
                //create entity
                //add him buildInfoComponent with ref to spriteRender
                var build = GameObject.Instantiate(pref.prefab);
                build.sprite = reqComp.buildData.sprite;
                build.transform.position = new Vector3(cellInfo.cellSize.x * (reqComp.buildingPos.x + 0.5f), cellInfo.cellSize.y * (reqComp.buildingPos.y + 0.5f));
                int index = 0;
                Debug.Log($"size: {reqComp.buildData.size}");
                for (int i = reqComp.buildingPos.x; i < reqComp.buildingPos.x + reqComp.buildData.size.x; i++)
                    for (int j = reqComp.buildingPos.y; j < reqComp.buildingPos.y + reqComp.buildData.size.y; j++)
                    {
                        index = BuildingServices.GetMapIndex(gridConf.gridSize.x, gridConf.gridSize.y, i, j);
                        Debug.Log($"x:{i} y:{j}, index:{index}");
                        if (index == -1 || gridOccMap.buildIdMap[index] != 0) continue;
                        gridOccMap.buildIdMap[index] = 1;//use entity ID
                    }
            }
            buildRequestStash.Remove(request);
        }
    }
    public void Dispose() { }
}

