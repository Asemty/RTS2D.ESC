using Scellecs.Morpeh;
using UnityEditor.VersionControl;
using UnityEngine;

public class BuildingViewSyncSystem : ISystem
{
    Filter buildFilter;
    Filter buildPrefFilter;
    Filter cellInfoFilter;
    private Stash<BuildDataComponent> buildDataStash;
    private Stash<GridPositionComponent> gridPositionStash;
    private Stash<BuildViewComponent> buildViewStash;
    private Stash<BuildPrefabComponent> buildPrefabStash;
    private Stash<CellInfoComponent> cellInfoStash;

    public World World { get; set; }

    public void OnAwake()
    {
        buildFilter = World.Filter
            .With<BuildDataComponent>()
            .With<GridPositionComponent>()
            .Without<BuildViewComponent>()
            .Build();
        buildPrefFilter = World.Filter
            .With<BuildPrefabComponent>()
            .Build();
        cellInfoFilter = World.Filter
            .With<CellInfoComponent>()
            .Build();
        buildDataStash = World.GetStash<BuildDataComponent>();
        gridPositionStash = World.GetStash<GridPositionComponent>();
        buildViewStash = World.GetStash<BuildViewComponent>();
        buildPrefabStash = World.GetStash<BuildPrefabComponent>();
        cellInfoStash = World.GetStash<CellInfoComponent>();
    }

    public void OnUpdate(float deltaTime)
    {
        if(buildFilter.IsEmpty()) return;
        ref var pref = ref buildPrefabStash.Get(buildPrefFilter.First());
        ref var cellInfo = ref cellInfoStash.Get(cellInfoFilter.First());
        foreach (var entity in buildFilter)
        {
            ref var buildData = ref buildDataStash.Get(entity);
            ref var fPos = ref gridPositionStash.Get(entity);
            var build = GameObject.Instantiate(pref.prefab);
            build.sprite = buildData.data.sprite;
            build.transform.position = new Vector3(cellInfo.cellSize.x * (fPos.position.x + 0.5f), cellInfo.cellSize.y * (fPos.position.y + 0.5f));
            buildViewStash.Add(entity, new BuildViewComponent()
            {
                spriteRenderer = build
            });
        }
    }
    public void Dispose() { }
}
