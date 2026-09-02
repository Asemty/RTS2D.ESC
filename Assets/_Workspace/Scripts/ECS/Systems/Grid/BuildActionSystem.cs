using Scellecs.Morpeh;
using System.Security.Cryptography;

public class BuildActionSystem : ISystem
{
    Filter cursorFilter;
    Filter gridDataFilter;
    Filter buildArchiveFilter;
    Stash<GridPositionComponent> gridPositionStash;
    public World World { get; set; }


    public void OnAwake()
    {
        cursorFilter = World.Filter
            .With<IsCursorComponent>()
            .With<GridPositionComponent>()
            .Build();
        gridDataFilter = World.Filter
            .With<GridConfigComponent>()
            .With<GridOccupancyComponent>()
            .Build();
        buildArchiveFilter = World.Filter
            .With<BuildDataArchiveComponent>()
            .Build();
        gridPositionStash = World.GetStash<GridPositionComponent>();
    }

    public void OnUpdate(float deltaTime)
    {
        throw new System.NotImplementedException();
    }
    public void Dispose() { }
}
