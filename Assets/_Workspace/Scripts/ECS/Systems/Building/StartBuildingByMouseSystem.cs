using Scellecs.Morpeh;
using UnityEngine;

public class StartBuildingByMouseSystem : ISystem
{
    Filter mouseFilter;
    Filter cursorFilter;
    Filter gridFilter;
    Filter buildDatasFilter;
    Stash<MouseInfoComponent> mouseInfoStash;
    Stash<GridPositionComponent> gridPositionStash;
    Stash<GridConfigComponent> gridConfigStash;
    Stash<GridOccupancyComponent> gridOccupancyStash;
    Stash<BuildDataArchiveComponent> buildDataArchiveStash;
    Stash<BuildRequestComponent> buildRequestStash;
    public World World { get; set; }

    public void OnAwake()
    {
        mouseFilter = World.Filter
            .With<MouseInfoComponent>()
            .Build();
        cursorFilter = World.Filter
            .With<IsCursorComponent>()
            .With<GridPositionComponent>()
            .Build();
        gridFilter = World.Filter
            .With<GridConfigComponent>()
            .With<GridOccupancyComponent>()
            .Build();
        buildDatasFilter = World.Filter
            .With<BuildDataArchiveComponent>()
            .Build();
        mouseInfoStash = World.GetStash<MouseInfoComponent>();
        gridPositionStash = World.GetStash<GridPositionComponent>();
        gridConfigStash = World.GetStash<GridConfigComponent>();
        gridOccupancyStash = World.GetStash<GridOccupancyComponent>();
        buildDataArchiveStash = World.GetStash<BuildDataArchiveComponent>();
        buildRequestStash = World.GetStash<BuildRequestComponent>();
    }

    public void OnUpdate(float deltaTime)
    {
        ref var mouse = ref mouseInfoStash.Get(mouseFilter.First());
        if (!mouse.isClick[0] || mouse.isOverUI) return;
        var grid = gridFilter.First();
        ref var gridConfig = ref gridConfigStash.Get(grid);
        ref var gridMap = ref gridOccupancyStash.Get(grid);
        ref var buildDataArchive = ref buildDataArchiveStash.Get(buildDatasFilter.First());
        ref var cursorPos = ref gridPositionStash.Get(cursorFilter.First());

        var data = buildDataArchive.buildsArchive[1];
        if (!BuildingServices.CanBuild(gridConfig.gridSize, gridMap.occupancyMap, cursorPos.position, data)) return;
        buildRequestStash.Add(grid, new BuildRequestComponent()
        {
            buildData = data,
            buildingPos = cursorPos.position,
        });
    }
    public void Dispose() { }
    
}
