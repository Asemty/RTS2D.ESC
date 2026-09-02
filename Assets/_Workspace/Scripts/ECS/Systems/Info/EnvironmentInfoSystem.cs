using Scellecs.Morpeh;
using UnityEngine;

public class EnvironmentInfoSystem : ISystem
{
    Filter filter;

    public World World { get; set ; }

    public void OnAwake()
    {
        filter = World.Filter
            .With<EnvironmentInfoComponent>()
            .Build();
    }

    public void OnUpdate(float deltaTime)
    {
        ref EnvironmentInfoComponent env = ref World.GetStash<EnvironmentInfoComponent>().Get(filter.FirstOrDefault());
        env.screenSize.x = Screen.width;
        env.screenSize.y = Screen.height;
    }
    public void Dispose() { }
}
