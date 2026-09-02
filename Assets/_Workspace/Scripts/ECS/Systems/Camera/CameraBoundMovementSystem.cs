using Scellecs.Morpeh;
using UnityEngine;

public class CameraBoundMovementSystem : ISystem
{
    Filter filterCamera;
    Filter filterInput;
    Filter filterEnviroment;
    Stash<CameraMoveDeltaComponent> cameraMoveDeltaStash;
    Stash<CameraConfigComponent> cameraConfigStash;
    Stash<MouseInfoComponent> mouseInputStash;
    Stash<EnvironmentInfoComponent> environmentInfoStash;

    public World World { get; set; }

    public void OnAwake()
    {
        filterCamera = World.Filter
            .With<CameraMoveDeltaComponent>()
            .With<CameraConfigComponent>()
            .Build();
        filterInput = World.Filter
            .With<MouseInfoComponent>()
            .Build();
        filterEnviroment = World.Filter
            .With<EnvironmentInfoComponent>()
            .Build();
        cameraMoveDeltaStash = World.GetStash<CameraMoveDeltaComponent>();
        cameraConfigStash = World.GetStash<CameraConfigComponent>();
        mouseInputStash = World.GetStash<MouseInfoComponent>();
        environmentInfoStash = World.GetStash<EnvironmentInfoComponent>();
    }

    public void OnUpdate(float deltaTime)
    {
        ref var mouse = ref mouseInputStash.Get(filterInput.FirstOrDefault());
        ref var env = ref environmentInfoStash.Get(filterEnviroment.FirstOrDefault());
        foreach (var entity in filterCamera)
        {
            ref var moveDelta = ref cameraMoveDeltaStash.Get(entity);
            ref var config = ref cameraConfigStash.Get(entity);

            moveDelta.delta += GetMoveDelta(config, mouse, env);
        }
    }

    private Vector3 GetMoveDelta(CameraConfigComponent config, MouseInfoComponent mouse, EnvironmentInfoComponent env)
    {
        Vector3 result = new Vector3();
        if(config.borderSize == 0)
        {
            Debug.LogError("borderSize equal zero");
            return result;
        }
        float rightMove = Mathf.Clamp01((mouse.screenPosition.x - (env.screenSize.x - config.borderSize)) / config.borderSize);
        float leftMove = Mathf.Clamp01((config.borderSize - mouse.screenPosition.x) / config.borderSize);
        float upMove = Mathf.Clamp01((mouse.screenPosition.y - (env.screenSize.y - config.borderSize)) / config.borderSize);
        float downMove = Mathf.Clamp01((config.borderSize - mouse.screenPosition.y) / config.borderSize);

        result.x = (rightMove - leftMove) * config.moveSpeedByMouse;
        result.y = (upMove - downMove) * config.moveSpeedByMouse;
        return result;
    }

    public void Dispose() { }
}
