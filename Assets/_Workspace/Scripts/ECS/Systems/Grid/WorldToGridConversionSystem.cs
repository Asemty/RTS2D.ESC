using Scellecs.Morpeh;
using UnityEngine;

public class WorldToGridConversionSystem : ISystem
{
    Filter filter;
    Filter gridFilter;
    Filter cellFilter;
    Stash<WorldPositionComponent> worldPositionStash;
    Stash<GridPositionComponent> gridPositionStash;
    Stash<GridConfigComponent> gridConfigStash;
    Stash<CellInfoComponent> cellInfoStash;
    public World World { get; set; }

    public void OnAwake()
    {
        filter = World.Filter
            .With<WorldPositionComponent>()
            .With<GridPositionComponent>()
            .Build();
        gridFilter = World.Filter
            .With<GridConfigComponent>()
            .Build();
        cellFilter = World.Filter
            .With<CellInfoComponent>()
            .Build();
        worldPositionStash = World.GetStash<WorldPositionComponent>();
        gridPositionStash = World.GetStash<GridPositionComponent>();
        gridConfigStash = World.GetStash<GridConfigComponent>();
        cellInfoStash = World.GetStash<CellInfoComponent>();
    }

    public void OnUpdate(float deltaTime)
    {
        ref var cellInfo = ref cellInfoStash.Get(cellFilter.First());
        ref var gridInfo = ref gridConfigStash.Get(gridFilter.First());
        foreach (var entity in filter)
        {
            ref var wPos = ref worldPositionStash.Get(entity);
            ref var gPos = ref gridPositionStash.Get(entity);
            var x = Mathf.Clamp((int)(wPos.position.x / cellInfo.cellSize.x), 0, gridInfo.gridSize.x);
            var y = Mathf.Clamp((int)(wPos.position.y / cellInfo.cellSize.y), 0, gridInfo.gridSize.y);
            gPos.position = new Vector2Int(x, y);
        }
    }
    public void Dispose() { }
}
