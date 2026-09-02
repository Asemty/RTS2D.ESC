using Scellecs.Morpeh;
using UnityEngine;

public struct CameraPositionComponent : IComponent
{
    public Vector3 position;
}
public struct CameraMoveDeltaComponent : IComponent
{
    public Vector3 delta;
}

public struct CameraConfigComponent : IComponent
{
    public float moveSpeedByMouse;
    public float moveSpeedByKeyboard;
    public float borderSize;
    public float boundsOffset;
    public Rect bounds; // can bound any cameras individual
}
public struct CameraGridSyncRequestComponent : IComponent { }
public struct CameraViewComponent : IComponent
{
    public Transform transform;
}
public struct CameraObjectComponent : IComponent
{
    public Camera camera;
}
