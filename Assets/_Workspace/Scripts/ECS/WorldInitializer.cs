using Scellecs.Morpeh;
using UnityEngine;

public class WorldInitializer : MonoBehaviour
{

    [SerializeField] InfoInitializer infoInitializer;
    [SerializeField] CameraInitializer cameraInitializer;
    [SerializeField] GridInitializer gridInitializer;
    [SerializeField] BuildInitializer buildInitializer;
    private World world;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        world = World.Default;

        world.AddSystemsGroup(0, infoInitializer.GetSystemGroup(world.CreateSystemsGroup()));
        world.AddSystemsGroup(10, cameraInitializer.GetSystemGroup(world.CreateSystemsGroup()));
        world.AddSystemsGroup(20, gridInitializer.GetSystemGroup(world.CreateSystemsGroup()));
        world.AddSystemsGroup(30, buildInitializer.GetSystemGroup(world.CreateSystemsGroup()));

        infoInitializer.BuildEntity(world, world.CreateEntity());
        cameraInitializer.BuildEntity(world, world.CreateEntity());
        gridInitializer.BuildEntity(world, world.CreateEntity());
        buildInitializer.BuildEntity(world, world.CreateEntity());
        BuildCursorEntity(world.CreateEntity());
        world.Commit();
    }

    // Update is called once per frame
    void Update() => world.Update(Time.deltaTime);
    
    private void OnDestroy() => world?.Dispose();
    private Entity BuildCursorEntity(Entity cursorEntity)
    {
        world.GetStash<WorldPositionComponent>().Add(cursorEntity);
        world.GetStash<GridPositionComponent>().Add(cursorEntity);
        world.GetStash<IsCursorComponent>().Add(cursorEntity);
        return cursorEntity;
    }
}

public interface IGroupInitializer
{
    public SystemsGroup GetSystemGroup(SystemsGroup group);
    public Entity BuildEntity(World world, Entity entity);
}
