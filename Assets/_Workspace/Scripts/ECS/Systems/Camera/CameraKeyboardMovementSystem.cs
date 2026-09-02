using Scellecs.Morpeh;
using UnityEngine;

public class CameraKeyboardMovementSystem : ISystem
{
    Filter filterCamera;
    Filter filterInput;
    Stash<CameraMoveDeltaComponent> cameraMoveDeltaStash;
    Stash<CameraConfigComponent> cameraConfigStash;
    Stash<KeyboardInfoComponent> keyboardInputStash;

    public World World { get; set; }

    public void OnAwake()
    {
        filterCamera = World.Filter
            .With<CameraMoveDeltaComponent>()
            .With<CameraConfigComponent>()
            .Build();
        filterInput = World.Filter
            .With<KeyboardInfoComponent>()
            .Build();
        cameraMoveDeltaStash = World.GetStash<CameraMoveDeltaComponent>();
        cameraConfigStash = World.GetStash<CameraConfigComponent>();
        keyboardInputStash = World.GetStash<KeyboardInfoComponent>();
    }

    public void OnUpdate(float deltaTime)
    {
        ref var keyboard = ref keyboardInputStash.Get(filterInput.FirstOrDefault());
        foreach (var entity in filterCamera)
        {
            ref var moveDelta = ref cameraMoveDeltaStash.Get(entity);
            ref var config = ref cameraConfigStash.Get(entity);

            moveDelta.delta += GetMoveDelta(config, keyboard);
        }
    }

    private Vector3 GetMoveDelta(CameraConfigComponent config, KeyboardInfoComponent keyboard)
    {
        Vector3 result = new Vector3();
        if (keyboard.isArrowRight) result.x += config.moveSpeedByKeyboard;
        if (keyboard.isArrowLeft) result.x -= config.moveSpeedByKeyboard;
        if (keyboard.isArrowUp) result.y += config.moveSpeedByKeyboard;
        if (keyboard.isArrowDown) result.y -= config.moveSpeedByKeyboard;
        return result;
    }

    public void Dispose() { }
}
