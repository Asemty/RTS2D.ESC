using Scellecs.Morpeh;
using UnityEngine;

public class GridDrawSystem : ISystem
{
    Filter cellInfoFilter;
    Filter gridFilter;
    Stash<CellInfoComponent> cellInfoStash;
    Stash<GridConfigComponent> gridConfigStash;
    Stash<GridViewComponent> gridViewStash;
    Stash<GridDrawRequestComponent> drawRequireStash;

    public World World { get; set; }

    public void OnAwake()
    {
        cellInfoFilter = World.Filter
            .With<CellInfoComponent>()
            .Build();
        gridFilter = World.Filter
            .With<GridConfigComponent>()
            .With<GridViewComponent>()
            .With<GridDrawRequestComponent>()
            .Build();
        cellInfoStash = World.GetStash<CellInfoComponent>();
        gridConfigStash = World.GetStash<GridConfigComponent>();
        gridViewStash = World.GetStash<GridViewComponent>();
        drawRequireStash = World.GetStash<GridDrawRequestComponent>();
    }
    public void OnUpdate(float deltaTime)
    {
        if (gridFilter.IsEmpty()) return;
        var gridEntity = gridFilter.FirstOrDefault();
        ref var cellInfo = ref cellInfoStash.Get(cellInfoFilter.FirstOrDefault());
        ref var gridConfig = ref gridConfigStash.Get(gridEntity);
        ref var gridView = ref gridViewStash.Get(gridEntity);

        DrawGrid(gridView.lineRenderer, gridConfig, cellInfo);

        drawRequireStash.Remove(gridEntity);
    }
    private void DrawGrid(LineRenderer lr, GridConfigComponent config, CellInfoComponent cell)
    {
        int pointCount = 2 + 2 * config.gridSize.x + 1 - config.gridSize.x % 2 + 1 + 2 * config.gridSize.y;
        lr.positionCount = pointCount;

        var index = 0;
        //vertical
        var fullHeight = cell.cellSize.y * config.gridSize.y;
        var top = new Vector3(0, fullHeight, 0);
        var bottom = new Vector3(0, 0, 0);
        lr.SetPosition(index++, bottom);
        lr.SetPosition(index++, top);
        for (var i = 0; i< config.gridSize.x; i++)
        {
            top.x += cell.cellSize.x;
            bottom.x += cell.cellSize.x;
            if (i % 2 == 0)
            {
                lr.SetPosition(index++, top);
                lr.SetPosition(index++, bottom);
            }
            else
            {
                lr.SetPosition(index++, bottom);
                lr.SetPosition(index++, top);
            }
        }
        //horizontal
        var fullWidth = cell.cellSize.x * config.gridSize.x;
        var left = new Vector3(0, 0, 0);
        var right = new Vector3(fullWidth, 0, 0);
        //return
        if (config.gridSize.x % 2 == 0) lr.SetPosition(index++, right);
        lr.SetPosition(index++, left);
        for (var i = 0; i < config.gridSize.y; i++)
        {
            left.y += cell.cellSize.y;
            right.y += cell.cellSize.y;
            if (i % 2 == 0)
            {
                lr.SetPosition(index++, left);
                lr.SetPosition(index++, right);
            }
            else
            {
                lr.SetPosition(index++, right);
                lr.SetPosition(index++, left);
            }
        }
    }
    public void Dispose(){ }

}
