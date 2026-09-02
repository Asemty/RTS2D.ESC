using Scellecs.Morpeh;
using UnityEngine;

public class CameraInitializer : MonoBehaviour, IGroupInitializer
{
    [SerializeField] CameraConfigSO cameraConfig;
    [SerializeField] Camera gameCamera;

    public Entity BuildEntity(World world, Entity cameraEntity)
    {
        world.GetStash<CameraPositionComponent>().Add(cameraEntity, new CameraPositionComponent()
        {
            position = gameCamera.transform.position,
        });
        world.GetStash<CameraViewComponent>().Add(cameraEntity, new CameraViewComponent()
        {
            transform = gameCamera.transform
        });
        world.GetStash<CameraObjectComponent>().Add(cameraEntity, new CameraObjectComponent()
        {
            camera = gameCamera
        });
        world.GetStash<CameraConfigComponent>().Add(cameraEntity, new CameraConfigComponent()
        {
            moveSpeedByMouse = cameraConfig.moveSpeedByMouse,
            moveSpeedByKeyboard = cameraConfig.moveSpeedByKeyboard,
            borderSize = cameraConfig.borderSize,
            boundsOffset = cameraConfig.boundsOffset
        });
        world.GetStash<CameraMoveDeltaComponent>().Add(cameraEntity);
        world.GetStash<CameraGridSyncRequestComponent>().Add(cameraEntity);

        return cameraEntity;
    }

    public SystemsGroup GetSystemGroup(SystemsGroup group)
    {

        //group.AddSystem(new CameraBoundMovementSystem());
        group.AddSystem(new CameraGridSyncSystem());
        group.AddSystem(new CameraKeyboardMovementSystem());
        group.AddSystem(new CameraApplyMovementSystem());
        group.AddSystem(new CameraViewSyncSystem());
        return group;
    }
}
