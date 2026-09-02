using Scellecs.Morpeh;
using UnityEngine;

public class KeyboardInfoSystem: ISystem
{
    Filter filter;

    public World World { get; set; }

    public void OnAwake()
    {
        filter = World.Filter
            .With<KeyboardInfoComponent>()
            .Build();
    }

    public void OnUpdate(float deltaTime)
    {
        ref KeyboardInfoComponent keyboard = ref World.GetStash<KeyboardInfoComponent>().Get(filter.FirstOrDefault());
        keyboard.isArrowLeft = Input.GetKey(KeyCode.LeftArrow);
        keyboard.isArrowRight = Input.GetKey(KeyCode.RightArrow);
        keyboard.isArrowUp = Input.GetKey(KeyCode.UpArrow);
        keyboard.isArrowDown = Input.GetKey(KeyCode.DownArrow);
    }
    public void Dispose() { }
}
