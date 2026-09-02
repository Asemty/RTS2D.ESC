using Scellecs.Morpeh;
using UnityEngine;

public class InfoInitializer : MonoBehaviour, IGroupInitializer
{
    public Entity BuildEntity(World world, Entity infoEntity)
    {
        world.GetStash<EnvironmentInfoComponent>().Add(infoEntity);
        world.GetStash<MouseInfoComponent>().Add(infoEntity, new MouseInfoComponent()
        {
            isDown = new bool[3],
            isOldDown = new bool[3],
            isClick = new bool[3]
        });
        world.GetStash<KeyboardInfoComponent>().Add(infoEntity);
        world.GetStash<CellInfoComponent>().Add(infoEntity, new CellInfoComponent()
        {
            cellSize = new Vector2(1, 1)
        });
        return infoEntity;
    }

    public SystemsGroup GetSystemGroup(SystemsGroup group)
    {
        group.AddSystem(new EnvironmentInfoSystem());
        group.AddSystem(new MouseInfoSystem());
        group.AddSystem(new KeyboardInfoSystem());
        group.AddSystem(new MouseWorldSyncSystem());
        return group;
    }
}
