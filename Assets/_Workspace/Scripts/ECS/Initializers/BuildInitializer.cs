using Scellecs.Morpeh;
using UnityEngine;

public class BuildInitializer : MonoBehaviour, IGroupInitializer
{
    [SerializeField] BuildDataSO buildDatas;
    [SerializeField] SpriteRenderer buildPrefab;

    public Entity BuildEntity(World world, Entity entity)
    {
        world.GetStash<BuildDataArchiveComponent>().Add(entity, new BuildDataArchiveComponent()
        {
            buildsArchive = buildDatas.builds
        });
        world.GetStash<BuildPrefabComponent>().Add(entity, new BuildPrefabComponent()
        {
            prefab = buildPrefab
        });
        return entity;
    }

    public SystemsGroup GetSystemGroup(SystemsGroup group)
    {
        group.AddSystem(new BuildingConstructionSystem());
        group.AddSystem(new StartBuildingByMouseSystem());
        group.AddSystem(new BuildingViewSyncSystem());
        group.AddSystem(new BuildingViewClearSystem());
        return group;
    }
}
/*
    add BuildDataArchiveComponent
    write BuildingServices.CanBuild
    write buildRequestSystem
    */