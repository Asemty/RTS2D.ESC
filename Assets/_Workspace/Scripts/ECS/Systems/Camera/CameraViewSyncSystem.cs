using Scellecs.Morpeh;
using UnityEngine;

public class CameraViewSyncSystem : ISystem
{
    Filter filter;
    Stash<CameraPositionComponent> cameraPositionsStash;
    Stash<CameraViewComponent> cameraViewStash;

    public World World { get; set; }

    public void OnAwake()
    {
        filter = World.Filter
            .With<CameraPositionComponent>()
            .With<CameraViewComponent>()
            .Build();
        cameraPositionsStash = World.GetStash<CameraPositionComponent>();
        cameraViewStash = World.GetStash<CameraViewComponent>();
    }

    public void OnUpdate(float deltaTime)
    {
        foreach (var entity in filter)
        {
            ref var pos = ref cameraPositionsStash.Get(entity);
            ref var view = ref cameraViewStash.Get(entity);
            if (view.transform)
            {
                view.transform.position = new Vector3(pos.position.x, pos.position.y, view.transform.position.z);
            }
        }
    }
    public void Dispose() { }
}