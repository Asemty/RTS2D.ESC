using Scellecs.Morpeh;
using UnityEngine;

public class MouseInfoSystem : ISystem
{
    Filter filter;

    public World World { get; set; }

    public void OnAwake()
    {
        filter = World.Filter
            .With<MouseInfoComponent>()
            .Build();
    }

    public void OnUpdate(float deltaTime)
    {
        ref MouseInfoComponent mouse = ref World.GetStash<MouseInfoComponent>().Get(filter.FirstOrDefault());
        mouse.screenPosition = Input.mousePosition;
        for(int i = 0; i < 3; i++)
        {
            mouse.isOldDown[i] = mouse.isDown[i];
            mouse.isDown[i] = Input.GetMouseButton(i);
            mouse.isClick[i] = mouse.isDown[i] && !mouse.isOldDown[i];
        }
    }
    public void Dispose() { }
}
