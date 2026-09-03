using Scellecs.Morpeh;
using UnityEngine;

public class BuildingViewClearSystem : ISystem
{
    Filter filter;
    private Stash<BuildViewComponent> buildViewStash;

    public World World { get; set; }

    public void OnAwake()
    {
        filter = World.Filter
            .Without<BuildDataComponent>()
            .With<BuildViewComponent>()
            .Build();
        buildViewStash = World.GetStash<BuildViewComponent>();
    }

    public void OnUpdate(float deltaTime)
    {
        foreach (var entity in filter)
        {
            ref var view = ref buildViewStash.Get(entity);
            GameObject.Destroy(view.spriteRenderer.gameObject);
            buildViewStash.Remove(entity);
        }
    }

    public void Dispose() { }
}