using Scellecs.Morpeh;
using UnityEngine;

public class CameraGridSyncSystem : ISystem
{
    Filter cameraConfigFilter;
    Filter gridConfigFilter;
    Filter cellInfoFilter;
    Stash<CameraGridSyncRequestComponent> cameraGridSyncRequestStash;
    Stash<CameraConfigComponent> cameraConfigStash;
    Stash<CameraPositionComponent> cameraPositionStash;
    Stash<GridConfigComponent> gridConfigStash;
    Stash<CellInfoComponent> cellInfoStash;
    public World World { get; set; }


    public void OnAwake()
    {
        cameraConfigFilter = World.Filter
            .With<CameraGridSyncRequestComponent>()
            .With<CameraConfigComponent>()
            .With<CameraPositionComponent>()
            .Build();
        gridConfigFilter = World.Filter
            .With<GridConfigComponent>()
            .Build();
        cellInfoFilter = World.Filter
            .With<CellInfoComponent>()
            .Build();
        cameraGridSyncRequestStash = World.GetStash<CameraGridSyncRequestComponent>();
        cameraConfigStash = World.GetStash<CameraConfigComponent>();
        cameraPositionStash = World.GetStash<CameraPositionComponent>();
        gridConfigStash = World.GetStash<GridConfigComponent>();
        cellInfoStash = World.GetStash<CellInfoComponent>();
    }

    public void OnUpdate(float deltaTime)
    {
        ref var gridConfig = ref gridConfigStash.Get(gridConfigFilter.First());
        ref var cellInfo = ref cellInfoStash.Get(cellInfoFilter.First());
        foreach (var entity in cameraConfigFilter)
        {
            ref var camConfig = ref cameraConfigStash.Get(entity);
            ref var camPos = ref cameraPositionStash.Get(entity);
            camConfig.bounds = new Rect(
                -camConfig.boundsOffset, 
                -camConfig.boundsOffset, 
                camConfig.boundsOffset * 2 + gridConfig.gridSize.x * cellInfo.cellSize.x,
                camConfig.boundsOffset * 2 + gridConfig.gridSize.y * cellInfo.cellSize.y);
            camPos.position = camConfig.bounds.center;

            cameraGridSyncRequestStash.Remove(entity);
        }
    }
    public void Dispose() { }
}
