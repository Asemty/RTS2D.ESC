using Scellecs.Morpeh;
using System.Security.Principal;
using UnityEngine;
using UnityEngine.LightTransport;

public class GridInitializer : MonoBehaviour, IGroupInitializer
{
    [SerializeField] LineRenderer gridLineRenderer;
    [SerializeField] GridConfigSO gridConfig;
    public Entity BuildEntity(World world, Entity entity)
    {
        var config = new GridConfigComponent()
        {
            gridSize = gridConfig.gridSize
        };
        world.GetStash<GridConfigComponent>().Add(entity, config);
        world.GetStash<GridDrawRequestComponent>().Add(entity);
        world.GetStash<GridOccupancyComponent>().Add(entity, new GridOccupancyComponent()
        {
            buildIdMap = new int[config.gridSize.x * config.gridSize.y]
        });
        world.GetStash<GridTerrainComponent>().Add(entity, new GridTerrainComponent()
        {
            heightMap = new int[config.gridSize.x * config.gridSize.y]
        });
        world.GetStash<GridViewComponent>().Add(entity, new GridViewComponent()
        {
            lineRenderer = gridLineRenderer
        });
        return entity;
    }

    public SystemsGroup GetSystemGroup(SystemsGroup group)
    {
        group.AddSystem(new GridDrawSystem());
        group.AddSystem(new WorldToGridConversionSystem());
        return group;
    }
}
