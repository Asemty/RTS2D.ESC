using Scellecs.Morpeh;
using UnityEngine;

public class CameraApplyMovementSystem : ISystem
{
    Filter filter;
    Stash<CameraPositionComponent> cameraPositionStash;
    Stash<CameraConfigComponent> cameraConfigStash;
    Stash<CameraMoveDeltaComponent> cameraMoveDeltaStash;
    public World World { get; set; }

    public void OnAwake()
    {
        filter = World.Filter
            .With<CameraPositionComponent>()
            .With<CameraMoveDeltaComponent>()
            .Build();
        cameraPositionStash = World.GetStash<CameraPositionComponent>();
        cameraConfigStash = World.GetStash<CameraConfigComponent>();
        cameraMoveDeltaStash = World.GetStash<CameraMoveDeltaComponent>();
    }

    public void OnUpdate(float deltaTime)
    {
        foreach (var entity in filter)
        {
            ref var pos = ref cameraPositionStash.Get(entity);
            ref var config = ref cameraConfigStash.Get(entity);
            ref var delta = ref cameraMoveDeltaStash.Get(entity);
            pos.position += delta.delta * deltaTime;
            pos.position.x = Mathf.Clamp(pos.position.x, config.bounds.xMin, config.bounds.xMax);
            pos.position.y = Mathf.Clamp(pos.position.y, config.bounds.yMin, config.bounds.yMax);
            delta.delta = Vector3.zero;
        }
    }
    public void Dispose() { }
}
