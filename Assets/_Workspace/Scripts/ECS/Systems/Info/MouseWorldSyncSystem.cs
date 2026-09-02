using Scellecs.Morpeh;
using UnityEngine;

public class MouseWorldSyncSystem : ISystem
{
    Filter cursorFilter;
    Filter cameraFilter;
    Filter mouseInfoFilter;
    Stash<WorldPositionComponent> worldPositionStash;
    Stash<CameraObjectComponent> cameraObjectStash;
    Stash<MouseInfoComponent> mouseInfoStash;
    public World World { get; set; }

    public void OnAwake()
    {
        cursorFilter = World.Filter
            .With<WorldPositionComponent>()
            .With<IsCursorComponent>()
            .Build();
        cameraFilter = World.Filter
            .With<CameraObjectComponent>()
            .Build();
        mouseInfoFilter = World.Filter 
            .With<MouseInfoComponent>()
            .Build();
        worldPositionStash = World.GetStash<WorldPositionComponent>();
        cameraObjectStash = World.GetStash<CameraObjectComponent>();
        mouseInfoStash = World.GetStash<MouseInfoComponent>();
    }

    public void OnUpdate(float deltaTime)
    {
        ref var cursor = ref worldPositionStash.Get(cursorFilter.First());
        ref var mouse = ref mouseInfoStash.Get(mouseInfoFilter.First());
        ref var camera = ref cameraObjectStash.Get(cameraFilter.First());

        cursor.position = camera.camera.ScreenToWorldPoint(mouse.screenPosition);
    }
    public void Dispose() { }
}
